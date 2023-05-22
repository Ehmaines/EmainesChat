import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Message } from 'src/app/Interfaces/Messages/message';
import { ChatMessagesService } from 'src/app/Services/ChatServices/chat-messages.service';

@Component({
    selector: 'ehm-chat-bar',
    templateUrl: './chat-bar.component.html',
    styleUrls: ['./chat-bar.component.scss'],
})
export class ChatBarComponent implements OnInit {
    message = new FormControl("message");//provavel aqui
    content: string = '';
    constructor(private chatMessageService: ChatMessagesService) {}

    ngOnInit(): void {}

    sendMessage(event: Event): void {
        event.preventDefault();
        this.message.value == null
            ? (this.content = '')
            : (this.content = this.message.value);
            //Aqui esta sempre retornando "message"

        console.log("content -> ",this.content)

        var messageToSend: Message = {
            content: this.content,
        };

        //this.chatMessageService.sendMessage(messageToSend);
    }
}
