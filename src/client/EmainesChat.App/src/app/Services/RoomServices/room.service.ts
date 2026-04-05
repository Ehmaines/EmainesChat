import { Injectable } from '@angular/core';
import { HttpClient as AngularHttpClient } from '@angular/common/http';
import { Room } from 'src/app/Interfaces/room';
import { BehaviorSubject } from 'rxjs';
import { environment } from 'src/environments/environment';
@Injectable({
  providedIn: 'root'
})
export class RoomService {
  private readonly apiUrl = `${environment.apiUrl}/`;
  private roomIdSource = new BehaviorSubject<string>('');
  roomId$ = this.roomIdSource.asObservable();

  constructor(private http: AngularHttpClient) { }

  getAllRooms() {
    return this.http.get<Room[]>(this.apiUrl + 'Room');
  }

  getRoomById(roomId: string){
    return this.http.get<Room>(this.apiUrl + `Room/${roomId}`);
  }

  createRoom(name: string) {
    return this.http.post<Room>(this.apiUrl + 'Room', { name });
  }

  changeRoom(roomId: string) {
    this.roomIdSource.next(roomId);
  }
}
