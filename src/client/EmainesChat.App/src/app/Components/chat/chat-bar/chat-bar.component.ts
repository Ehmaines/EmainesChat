import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';



import { Message } from 'src/app/Interfaces/Messages/message';
import { ChatMessagesService } from 'src/app/Services/ChatServices/chat-messages.service';

@Component({
    selector: 'ehm-chat-bar',
    templateUrl: './chat-bar.component.html',
    styleUrls: ['./chat-bar.component.scss'],
})
export class ChatBarComponent implements OnInit {
    content: string = '';
    constructor(private chatMessageService: ChatMessagesService) {}
    message = new FormControl('');
    ngOnInit(): void {
      console.log(this.message.value)

    }

    sendMessage(event: Event): void {
        event.preventDefault();
        this.message.value == null
            ? (this.content = '')
            : (this.content = this.message.value);

            var messageToSend: Message = {
            content: this.content,
        };
        this.chatMessageService.sendMessage(messageToSend);

        this.message.setValue('');
    }
}
