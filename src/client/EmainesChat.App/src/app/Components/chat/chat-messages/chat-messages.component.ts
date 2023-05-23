import {
    AfterContentInit,
    AfterViewChecked,
    AfterViewInit,
    Component,
    OnInit,
} from '@angular/core';
import { HubConnection, HubConnectionState } from '@microsoft/signalr';
import { Message } from 'src/app/Interfaces/Messages/message';
import { ChatMessagesService } from 'src/app/Services/ChatServices/chat-messages.service';
import { Subject } from 'rxjs';
import { FormsModule } from '@angular/forms';

@Component({
    selector: 'ehm-chat-messages',
    templateUrl: './chat-messages.component.html',
    styleUrls: [
        './chat-messages.component.scss',
        '../../../styles/variables.scss',
    ],
})
export class ChatMessagesComponent implements OnInit {
    Allmessages: Message[] = [];
    signalRConnection!: HubConnection;
    signalRConnectionState: HubConnectionState =
        HubConnectionState.Disconnected;
    messagesSubject!: Subject<Message[]>;
    constructor(private chatMessagesService: ChatMessagesService) {}

    ngOnInit() {
        this.messagesSubject = this.chatMessagesService.getMessagesSubject();
        this.messagesSubject.subscribe((messages: Message[]) => {
            this.Allmessages = messages; // Atualizar a propriedade Allmessages quando novas mensagens forem recebidas
            console.log('All Messages na 34: ', this.Allmessages);
        });

        const connection = this.chatMessagesService.getSignalRConnection();
        this.signalRConnectionState = connection.state;
        if (this.signalRConnectionState === HubConnectionState.Disconnected) {
            connection.start().then(() => {
                console.log('Conexão SignalR estabelecida no componente.');
                this.chatMessagesService.getAllMessages();
            });
        } else {
            console.log(
                'A conexão SignalR já está estabelecida no componente.'
            );
            this.chatMessagesService.getAllMessages();
        }
    }
}
