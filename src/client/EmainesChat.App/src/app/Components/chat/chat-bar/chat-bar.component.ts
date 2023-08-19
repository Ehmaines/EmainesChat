import { ActivatedRoute } from '@angular/router';
import { RoomService } from 'src/app/Services/RoomServices/room.service';
import { User } from './../../../Interfaces/Users/user';
import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';

import { Message } from 'src/app/Interfaces/Messages/message';
import { ChatMessagesService } from 'src/app/Services/ChatServices/chat-messages.service';
import { Room } from 'src/app/Interfaces/room';

@Component({
    selector: 'ehm-chat-bar',
    templateUrl: './chat-bar.component.html',
    styleUrls: ['./chat-bar.component.scss'],
})
export class ChatBarComponent implements OnInit {
    content: string = '';
    message = new FormControl('');
    roomId: number = 0;
    actualRoom: Room = {name:'', createdAt: new Date(), id: 0};

    constructor(private chatMessageService: ChatMessagesService, private roomService: RoomService, private route: ActivatedRoute) {}

    ngOnInit(): void {
        this.route.params.subscribe((params) => {
            this.roomId = params['id'];
        });
        this.roomService.getRoomById(this.roomId).subscribe((result) => {
            this.actualRoom = result;
        });
    }

    sendMessage(event: Event): void {
        event.preventDefault();
        this.message.value == null
            ? (this.content = '')
            : (this.content = this.message.value);

        var messageToSend: Message = {
            content: this.content,
            sentAt: new Date(),
            user: {
                userName: 'Felipe',
                email: 'felipemaines123@gmail.com',
                password: '',
            },
            room: { name: this.actualRoom?.name, createdAt: new Date() },
        };
        this.chatMessageService.sendMessage(messageToSend);
        this.message.setValue('');
    }
}
