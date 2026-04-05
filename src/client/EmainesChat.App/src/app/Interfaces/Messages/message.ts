export interface Message {
  id?: string;
  content: string;
  sentAt: Date;
  userId: string;
  userName: string;
  roomId: string;
  profilePictureUrl?: string | null;
}
