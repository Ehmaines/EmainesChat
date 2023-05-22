import { Component, OnInit } from '@angular/core';
import { Message } from 'src/app/Interfaces/Messages/message';
import { ChatMessagesService } from 'src/app/Services/ChatServices/chat-messages.service';

@Component({
    selector: 'ehm-chat-messages',
    templateUrl: './chat-messages.component.html',
    styleUrls: [
        './chat-messages.component.scss',
        '../../../styles/variables.scss',
    ],
})

export class ChatMessagesComponent implements OnInit {
    messages: Message[] = [];
    constructor(private chatMessagesService: ChatMessagesService) {}

    ngOnInit() {
        this.messages = this.chatMessagesService.messages;
    }
}
