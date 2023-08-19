import { Room } from "../Rooms/room";
import { User } from "../Users/user";

export interface Message {
  content: string;
  sentAt: Date;
  user: User;
  room: Room;
}
