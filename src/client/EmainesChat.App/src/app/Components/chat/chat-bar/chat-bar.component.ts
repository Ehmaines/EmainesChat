import { ActivatedRoute } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';

import { Message } from 'src/app/Interfaces/Messages/message';
import { ChatMessagesService } from 'src/app/Services/ChatServices/chat-messages.service';
import { AuthTokenService } from 'src/app/core/authentication/auth-token.service';

@Component({
    selector: 'ehm-chat-bar',
    templateUrl: './chat-bar.component.html',
    styleUrls: ['./chat-bar.component.scss'],
})
export class ChatBarComponent implements OnInit {
    content: string = '';
    message = new FormControl('');
    roomId: number = 0;

    constructor(
        private chatMessageService: ChatMessagesService,
        private route: ActivatedRoute,
        private authTokenService: AuthTokenService
    ) { }

    ngOnInit(): void {
        this.getActualRoom()
    }

    sendMessage(event: Event): void {
        event.preventDefault();
        this.message.value == null
            ? (this.content = '')
            : (this.content = this.message.value);

        const messageToSend: Message = {
            content: this.content,
            sentAt: new Date(),
            userId: 0,
            userName: this.authTokenService.token.name,
            roomId: this.roomId,
        };
        this.chatMessageService.sendMessage(messageToSend);
        this.message.setValue('');
    }

    getActualRoom() {
        this.route.params.subscribe((params) => {
            this.roomId = parseInt(params['id']);
        });
    }
}
