import { User } from './../../Interfaces/Users/user';
import { Injectable } from '@angular/core';
import {
    HubConnection,
    HubConnectionBuilder,
    Subject,
} from '@microsoft/signalr';
import { Subject as SubjectRxjs } from 'rxjs';
import { Message } from 'src/app/Interfaces/Messages/message';

@Injectable({
    providedIn: 'root',
})
export class ChatMessagesService {
    private hubConnection: HubConnection;
    public messages: Message[] = [];

    constructor() {
        this.hubConnection = new HubConnectionBuilder()
            .withUrl('https://localhost:7080/messageHub')
            .build();

        this.hubConnection.on('ReceberMensagem', (message) => {
            console.log('Nova mensagem recebida:', message);
        });

        this.hubConnection
            .start()
            .then(() =>
                console.log('Conexão estabelecida com o servidor SignalR')
            )
            .catch((error) =>
                console.error('Erro ao estabelecer a conexão:', error)
            );

        this.registerHandlers();
    }

    private registerHandlers(): void {
        this.hubConnection.on('MessageCreated', (message: Message) => {
            this.messages.push(message);
            console.log(message.content);
        });
    }

    sendMessage(message: Message) {
        this.hubConnection
            .invoke('CreateMessage', message)
            .catch((error) =>
                console.error('Erro ao enviar a mensagem:', error)
            );
    }
}
