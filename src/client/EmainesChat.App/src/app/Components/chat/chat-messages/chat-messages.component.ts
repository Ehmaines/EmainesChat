import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { HubConnection, HubConnectionState } from '@microsoft/signalr';
import { Message } from 'src/app/Interfaces/Messages/message';
import { ChatMessagesService } from 'src/app/Services/ChatServices/chat-messages.service';
import { Subject, takeUntil } from 'rxjs';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { RoomService } from 'src/app/Services/RoomServices/room.service';

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
    signalRConnection!: HubConnection;
    signalRConnectionState: HubConnectionState =
        HubConnectionState.Disconnected;
    messagesSubject!: Subject<Message[]>;
    constructor(
        private chatMessagesService: ChatMessagesService,
        private route: ActivatedRoute,
        private roomService: RoomService
    ) {}

    ngOnInit() {
        this.route.params.subscribe((params) => {
            this.roomId = params['id'];
        });

        this.chatMessagesService
            .getMessagesByRoom(this.roomId)
            .subscribe((response) => {
                this.Allmessages = response;
            });

        this.messagesSubject = this.chatMessagesService.getMessagesSubject();
        this.messagesSubject.subscribe((messages: Message[]) => {
            this.Allmessages = messages; // Atualiza a propriedade Allmessages quando novas mensagens forem recebidas
        });

        const connection = this.chatMessagesService.getSignalRConnection();
        this.signalRConnectionState = connection.state;
        if (this.signalRConnectionState === HubConnectionState.Disconnected) {
            connection.start().then(() => {
                console.log('Conexão SignalR estabelecida no componente.');
                this.chatMessagesService.getMessagesByRoomIdInSignalR(
                    this.roomId
                );
            });
        } else {
            console.log(
                'A conexão SignalR já está estabelecida no componente.'
            );
            this.chatMessagesService.getMessagesByRoomIdInSignalR(this.roomId);
        }

        this.roomService.roomId$.subscribe((newRoomId) => {
            console.log('caindo no observable de changeRoom');
            this.roomId = newRoomId;
            this.chatMessagesService
                .getMessagesByRoom(this.roomId)
                .subscribe((result) => {
                    this.Allmessages = result;
                });
        });
    }
}
