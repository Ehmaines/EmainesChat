import { Injectable } from '@angular/core';
import { HttpClient as AngularHttpClient } from '@angular/common/http';
import { Room } from 'src/app/Interfaces/room';
import { BehaviorSubject } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class RoomService {
  private readonly apiUrl: string = 'https://localhost:7080/';
  private roomIdSource = new BehaviorSubject<number>(4); // Valor padrão
  roomId$ = this.roomIdSource.asObservable();

  constructor(private http: AngularHttpClient) { }

  getAllRooms() {
    return this.http.get<Room[]>(this.apiUrl + 'Room');
  }

  getRoomById(roomId: number){
    return this.http.get<Room>(this.apiUrl + `Room/${roomId}`);
  }
  
  changeRoom(roomId: number) {
    this.roomIdSource.next(roomId);
  }
}
