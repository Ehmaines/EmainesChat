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
import { HttpClient as AngularHttpClient } from '@angular/common/http';

@Injectable({
    providedIn: 'root',
})
export class ChatMessagesService {
    private hubConnection!: HubConnection;
    public messages: Message[] = [];
    private isConnectionEstablished = false;
    private readonly apiUrl: string = 'https://localhost:7080/';

    private messagesSubject: SubjectRxjs<Message[]> = new SubjectRxjs<
        Message[]
    >();

    constructor(private http: AngularHttpClient) {
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
            this.getAllMessagesBySignalR();
            console.log(message.content);
        });

        this.hubConnection.on(
            'ReciveAllMessages',
            (Recivedmessage: Message[]) => {
                this.messages = Recivedmessage;
                this.messagesSubject.next(this.messages); //notifica o componente sobre novas mensagens
            }
        );

        this.hubConnection.on(
            'ReciveMessageByRoomId',
            (Recivedmessage: Message[]) => {
                this.messages = Recivedmessage;
                this.messagesSubject.next(this.messages); //notifica o componente sobre novas mensagens
            }
        );
    }

    sendMessage(message: Message) {
        debugger;
        if (this.isConnectionEstablished) {
            this.hubConnection
                .invoke('CreateMessage', message)
                .catch((error) =>
                    console.error('Erro ao enviar a mensagem:', error)
                );
        }
    }

    getAllMessagesBySignalR() {
        if (this.isConnectionEstablished) {
            this.hubConnection
                .invoke('GetAllMessages')
                .catch((error) =>
                    console.error('Erro ao iniciar a conexão:', error)
                );
        }
    }

    getMessagesByRoomIdInSignalR(roomId: number) {
        if (this.isConnectionEstablished) {
            this.hubConnection
                .invoke('GetMessagesByRoomId', roomId)
                .catch((error) =>
                    console.error('Erro ao iniciar a conexão:', error)
                );
        }
    }

    getAllMessagesByDataBase() {
        return this.http.get<Message[]>(this.apiUrl + 'message');
    }

    getMessagesByRoom(roomId: Number) {
        console.log(`procurando mensagens pela sala ${roomId}`)
        return this.http.get<Message[]>(this.apiUrl + `message/${roomId}`);
    }

    getMessagesSubject(): SubjectRxjs<Message[]> {
        return this.messagesSubject;
    }

    getSignalRConnection(): HubConnection {
        return this.hubConnection;
    }
}
