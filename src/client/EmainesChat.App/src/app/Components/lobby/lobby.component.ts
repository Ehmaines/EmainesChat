import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormControl, Validators } from '@angular/forms';
import { RoomService } from 'src/app/Services/RoomServices/room.service';
import { UserService } from 'src/app/Services/UserServices/user.service';
import { AuthTokenService } from 'src/app/core/authentication/auth-token.service';
import { AuthService } from 'src/app/core/authentication/auth.service';
import { Roles } from 'src/app/core/authentication/shared/roles.enum';
import { Room } from 'src/app/Interfaces/room';

@Component({
  selector: 'ehm-lobby',
  templateUrl: './lobby.component.html',
  styleUrls: ['./lobby.component.scss']
})
export class LobbyComponent implements OnInit {
  rooms: Room[] = [];
  currentUserName = '';
  currentUserAvatar: string | null = null;
  isAdmin = false;
  showCreateForm = false;
  newRoomName = new FormControl('', [Validators.required, Validators.minLength(2)]);
  createError = '';
  creating = false;

  constructor(
    private roomService: RoomService,
    private userService: UserService,
    private authTokenService: AuthTokenService,
    private authService: AuthService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.currentUserName = this.authTokenService.token.name;
    this.isAdmin = this.authTokenService.role === Roles.Admin;
    this.showCreateForm = this.isAdmin;
    this.userService.getProfile().subscribe(profile => {
      this.currentUserAvatar = profile.profilePictureUrl;
    });

    this.roomService.getAllRooms().subscribe(rooms => {
      this.rooms = rooms;
    });
  }

  enterRoom(roomId: string): void {
    this.roomService.changeRoom(roomId);
    this.router.navigate(['/room', roomId]);
  }

  toggleCreateForm(): void {
    this.showCreateForm = !this.showCreateForm;
    this.newRoomName.reset();
    this.createError = '';
  }

  submitCreateRoom(): void {
    if (this.newRoomName.invalid || this.creating) return;
    const name = this.newRoomName.value!.trim();
    this.creating = true;
    this.createError = '';
    this.roomService.createRoom(name).subscribe({
      next: (room) => {
        this.rooms.push(room);
        this.showCreateForm = false;
        this.newRoomName.reset();
        this.creating = false;
      },
      error: () => {
        this.createError = 'Não foi possível criar a sala. Tente novamente.';
        this.creating = false;
      }
    });
  }

  logout(): void {
    this.authService.logout();
    this.router.navigate(['/login']);
  }
}
