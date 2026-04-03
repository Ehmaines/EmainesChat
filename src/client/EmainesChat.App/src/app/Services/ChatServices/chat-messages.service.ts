import { Room } from 'src/app/Interfaces/room';
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
import * as signalR from '@microsoft/signalr';
import { AuthTokenService } from 'src/app/core/authentication/auth-token.service';

@Injectable({
    providedIn: 'root',
})
export class ChatMessagesService {
    private hubConnection!: HubConnection;
    public messages: Message[] = [];
    private isConnectionEstablished = false;
    private currentRoomId: number | null = null;
    private readonly apiUrl: string = 'https://localhost:7080/';

    private messagesSubject: SubjectRxjs<Message[]> = new SubjectRxjs<
        Message[]
    >();

    constructor(private http: AngularHttpClient, private authTokenService: AuthTokenService) {
        this.initializeSignalRConnection();
        this.registerHandlers();
    }

    private initializeSignalRConnection() {
        this.hubConnection = new signalR.HubConnectionBuilder()
            .withUrl('https://localhost:7080/messageHub', { accessTokenFactory: () => this.authTokenService.encodedToken })
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
            this.messagesSubject.next(this.messages);
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

    async getMessagesByRoomIdInSignalR(roomId: number): Promise<void> {
        if (!this.isConnectionEstablished) return;
        if (this.currentRoomId !== null && this.currentRoomId !== roomId) {
            await this.hubConnection.invoke('LeaveRoom', this.currentRoomId)
                .catch((error) => console.error('Erro ao sair da sala:', error));
        }
        this.currentRoomId = roomId;
        await this.hubConnection.invoke('GetMessagesByRoomId', roomId)
            .catch((error) => console.error('Erro ao buscar mensagens:', error));
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
