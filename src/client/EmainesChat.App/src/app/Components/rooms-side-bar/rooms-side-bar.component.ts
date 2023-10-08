import { ActivatedRoute, Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { Room } from 'src/app/Interfaces/room';
import { RoomService } from 'src/app/Services/RoomServices/room.service';

@Component({
  selector: 'ehm-rooms-side-bar',
  templateUrl: './rooms-side-bar.component.html',
  styleUrls: ['./rooms-side-bar.component.scss']
})

export class RoomsSideBarComponent implements OnInit {
  allRooms: Room[] = [];
  activeRoomId: Number = 0;
  constructor(private roomService: RoomService, private router: Router, private route: ActivatedRoute){}

  ngOnInit(): void {
    this.roomService.getAllRooms().subscribe((response) => {
      this.allRooms = response;
      this.route.params.subscribe(params => {
        this.activeRoomId = params['id'];
      });
    });
  }

  changeRoom(newRoomId: number) {
    this.roomService.changeRoom(newRoomId);
    this.router.navigate(['/room', newRoomId])
  }
}
