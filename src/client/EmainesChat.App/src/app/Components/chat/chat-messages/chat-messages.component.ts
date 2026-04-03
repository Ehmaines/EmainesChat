import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { Message } from 'src/app/Interfaces/Messages/message';
import { ChatMessagesService } from 'src/app/Services/ChatServices/chat-messages.service';
import { ActivatedRoute } from '@angular/router';
import { skip } from 'rxjs/operators';
import { RoomService } from 'src/app/Services/RoomServices/room.service';
import { AuthTokenService } from 'src/app/core/authentication/auth-token.service';

@Component({
    selector: 'ehm-chat-messages',
    templateUrl: './chat-messages.component.html',
    styleUrls: [
        './chat-messages.component.scss',
        '../../../styles/variables.scss',
    ],
})
export class ChatMessagesComponent implements OnInit {
    @ViewChild('messagesList', { static: false }) messagesListRef!: ElementRef;
    Allmessages: Message[] = [];
    roomId: number = 0;
    currentUserName: string = '';

    constructor(
        private chatMessagesService: ChatMessagesService,
        private route: ActivatedRoute,
        private roomService: RoomService,
        private authTokenService: AuthTokenService
    ) { }

    ngOnInit() {
        this.currentUserName = this.authTokenService.token.name;
        console.log('this.authTokenService', this.authTokenService)
        this.route.params.subscribe((params) => {
            this.roomId = +params['id'];
        });

        this.chatMessagesService
            .getMessagesByRoom(this.roomId)
            .subscribe((response) => {
                this.Allmessages = response;
            });

        this.chatMessagesService.getMessagesSubject().subscribe((messages: Message[]) => {
            this.Allmessages = [...messages];
        });

        this.chatMessagesService.whenReady().then(() => {
            this.chatMessagesService.getMessagesByRoomIdInSignalR(this.roomId);
        });

        this.roomService.roomId$.pipe(skip(1)).subscribe((newRoomId) => {
            this.roomId = newRoomId;
            this.chatMessagesService
                .getMessagesByRoom(this.roomId)
                .subscribe((result) => {
                    this.Allmessages = result;
                });
            this.chatMessagesService.whenReady().then(() => {
                this.chatMessagesService.getMessagesByRoomIdInSignalR(this.roomId);
            });
        });
    }
}
