import { ActivatedRoute, Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { Room } from 'src/app/Interfaces/room';
import { RoomService } from 'src/app/Services/RoomServices/room.service';
import { AuthService } from 'src/app/core/authentication/auth.service';
import { AuthTokenService } from 'src/app/core/authentication/auth-token.service';
import { UserService } from 'src/app/Services/UserServices/user.service';

@Component({
  selector: 'ehm-rooms-side-bar',
  templateUrl: './rooms-side-bar.component.html',
  styleUrls: ['./rooms-side-bar.component.scss']
})

export class RoomsSideBarComponent implements OnInit {
  allRooms: Room[] = [];
  activeRoomId: string = '';
  currentUserName: string = '';
  currentUserAvatar: string | null = null;

  constructor(
    private roomService: RoomService,
    private router: Router,
    private route: ActivatedRoute,
    private authService: AuthService,
    private authTokenService: AuthTokenService,
    private userService: UserService
  ) {}

  ngOnInit(): void {
    this.currentUserName = this.authTokenService.token.name;

    this.userService.getProfile().subscribe((profile) => {
      this.currentUserAvatar = profile.profilePictureUrl;
    });

    this.roomService.getAllRooms().subscribe((response) => {
      this.allRooms = response;
      this.route.params.subscribe(params => {
        this.activeRoomId = params['id'];
      });
    });
  }

  changeRoom(newRoomId: string) {
    this.roomService.changeRoom(newRoomId);
    this.router.navigate(['/room', newRoomId])
  }

  logout(): void {
    this.authService.logout();
    this.router.navigate(['/login']);
  }
}
