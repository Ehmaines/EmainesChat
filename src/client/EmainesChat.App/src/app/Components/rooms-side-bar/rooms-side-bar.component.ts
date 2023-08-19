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

  constructor(private roomService: RoomService){}

  ngOnInit(): void {
    this.roomService.getAllRooms().subscribe((response) => {
      console.log(response);
      this.allRooms = response;
    });
  }
}
