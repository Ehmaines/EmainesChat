export interface Message {
  id?: number;
  content: string;
  sentAt: Date;
  userId: number;
  userName: string;
  roomId: number;
}
