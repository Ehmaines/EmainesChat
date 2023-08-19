import { Injectable } from '@angular/core';
import { HttpClient as AngularHttpClient } from '@angular/common/http';
import { Room } from 'src/app/Interfaces/room';
@Injectable({
  providedIn: 'root'
})
export class RoomService {
  private readonly apiUrl: string = 'https://localhost:7080/';

  constructor(private http: AngularHttpClient) { }

  getAllRooms() {
    return this.http.get<Room[]>(this.apiUrl + 'Room');
  }
}
