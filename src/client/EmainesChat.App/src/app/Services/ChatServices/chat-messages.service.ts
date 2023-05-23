import { User } from './../../Interfaces/Users/user';
import { Injectable } from '@angular/core';
import {
    HubConnection,
    HubConnectionBuilder,
    Subject,
    HubConnectionState,
} from '@microsoft/signalr';
import { Subject as SubjectRxjs } from 'rxjs';
import { Message } from 'src/app/Interfaces/Messages/message';

@Injectable({
    providedIn: 'root',
})
export class ChatMessagesService {
    private hubConnection!: HubConnection;
    public messages: Message[] = [];
    private isConnectionEstablished = false;

    private messagesSubject: SubjectRxjs<Message[]> = new SubjectRxjs<
        Message[]
    >();

    constructor() {
        this.initializeSignalRConnection();
        this.registerHandlers();
    }

    private initializeSignalRConnection() {
        this.hubConnection = new HubConnectionBuilder()
            .withUrl('https://localhost:7080/messageHub')
            .build();

        this.hubConnection
            .start()
            .then(() => {
                this.isConnectionEstablished = true;
                console.log('Conexão estabelecida com o servidor SignalR');
                this.registerHandlers();
            })
            .catch((error) =>
                console.error('Erro ao estabelecer a conexão:', error)
            );

        // Verificar o estado atual da conexão
        if (this.hubConnection.state !== HubConnectionState.Disconnected) {
            // Aguardar até que a conexão esteja no estado 'Disconnected'
            this.hubConnection.onclose(() => {
                // Iniciar a conexão após a desconexão
                this.hubConnection
                    .start()
                    .catch((error) =>
                        console.error('Erro ao estabelecer a conexão:', error)
                    );
            });
        }
    }

    private registerHandlers(): void {
        this.hubConnection.on('MessageCreated', (message: Message) => {
            this.messages.push(message);
            this.messagesSubject.next(this.messages); //notifica o componente sobre novas mensagens
            this.getAllMessages();
            console.log(message.content);
        });

        this.hubConnection.on(
            'ReciveAllMessages',
            (Recivedmessage: Message[]) => {
                this.messages = Recivedmessage;
                this.messagesSubject.next(this.messages); //notifica o componente sobre novas mensagens
            }
        );
    }

    sendMessage(message: Message) {
        if (this.isConnectionEstablished) {
            this.hubConnection
                .invoke('CreateMessage', message)
                .catch((error) =>
                    console.error('Erro ao enviar a mensagem:', error)
                );
        }
    }

    getAllMessages() {
        if (this.isConnectionEstablished) {
            // if (this.hubConnection.state === HubConnectionState.Disconnected) {
            //     this.hubConnection
            //         .start()
            //         .then(() => {
            //             console.log('Conexão SignalR estabelecida.');
            //             this.hubConnection.invoke('GetAllMessages', '');
            //         })
            //         .catch((error) =>
            //             console.error('Erro ao iniciar a conexão:', error)
            //         );
            // } else if (this.hubConnection.state === HubConnectionState.Connected) {
            this.hubConnection
                .invoke('GetAllMessages', [])
                .catch((error) =>
                    console.error('Erro ao iniciar a conexão:', error)
                );
        }
    }

    getMessagesSubject(): SubjectRxjs<Message[]> {
        return this.messagesSubject;
    }

    getSignalRConnection(): HubConnection {
        return this.hubConnection;
    }
}
