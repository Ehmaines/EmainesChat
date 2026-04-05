import { Injectable, NgZone } from '@angular/core';
import {
    HubConnection,
    HubConnectionState,
} from '@microsoft/signalr';
import { Subject as SubjectRxjs } from 'rxjs';
import { Message } from 'src/app/Interfaces/Messages/message';
import { HttpClient as AngularHttpClient } from '@angular/common/http';
import * as signalR from '@microsoft/signalr';
import { AuthTokenService } from 'src/app/core/authentication/auth-token.service';
import { environment } from 'src/environments/environment';

@Injectable({
    providedIn: 'root',
})
export class ChatMessagesService {
    private hubConnection!: HubConnection;
    private connectionPromise!: Promise<void>;
    public messages: Message[] = [];
    private isConnectionEstablished = false;
    private currentRoomId: string | null = null;
    private readonly apiUrl = `${environment.apiUrl}/`;

    private messagesSubject: SubjectRxjs<Message[]> = new SubjectRxjs<
        Message[]
    >();

    constructor(private http: AngularHttpClient, private authTokenService: AuthTokenService, private ngZone: NgZone) {
        this.initializeSignalRConnection();
    }

    private initializeSignalRConnection() {
        this.hubConnection = new signalR.HubConnectionBuilder()
            .withUrl(`${environment.apiUrl}/messageHub`, { accessTokenFactory: () => this.authTokenService.encodedToken })
            .withAutomaticReconnect([0, 2000, 5000, 10000, 30000])
            .build();

        this.registerHandlers();

        this.hubConnection.onreconnecting(() => {
            this.isConnectionEstablished = false;
            console.warn('Reconectando ao SignalR...');
        });

        this.hubConnection.onreconnected(() => {
            this.isConnectionEstablished = true;
            console.log('Reconexão bem-sucedida.');
            if (this.currentRoomId !== null) {
                this.hubConnection.invoke('GetMessagesByRoomId', this.currentRoomId)
                    .catch(err => console.error('Erro ao reentrar na sala:', err));
            }
        });

        this.connectionPromise = this.hubConnection
            .start()
            .then(() => {
                this.isConnectionEstablished = true;
                console.log('Conexão estabelecida com o servidor SignalR');
            })
            .catch((error) => {
                console.error('Erro ao estabelecer a conexão:', error);
                throw error;
            });

        this.hubConnection.onclose(() => {
            this.isConnectionEstablished = false;
        });
    }

    whenReady(): Promise<void> {
        return this.connectionPromise;
    }

    private registerHandlers(): void {
        this.hubConnection.on('MessageCreated', (message: Message) => {
            this.ngZone.run(() => {
                this.messages.push(message);
                this.messagesSubject.next(this.messages);
            });
        });

        this.hubConnection.on('ReceiveAllMessages', (received: Message[]) => {
            this.ngZone.run(() => {
                this.messages = received;
                this.messagesSubject.next(this.messages);
            });
        });

        this.hubConnection.on('ReceiveMessageByRoomId', (received: Message[]) => {
            this.ngZone.run(() => {
                this.messages = received;
                this.messagesSubject.next(this.messages);
            });
        });
    }

    sendMessage(message: Message) {
        if (this.isConnectionEstablished) {
            this.hubConnection
                .invoke('CreateMessage', message.content, message.roomId)
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

    async getMessagesByRoomIdInSignalR(roomId: string): Promise<void> {
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

    getMessagesByRoom(roomId: string) {
        console.log(`procurando mensagens pela sala ${roomId}`)
        return this.http.get<Message[]>(this.apiUrl + `message/${roomId}`);
    }

    getMessagesSubject(): SubjectRxjs<Message[]> {
        return this.messagesSubject;
    }

    getSignalRConnection(): HubConnection {
        return this.hubConnection;
    }

    async stopConnection(): Promise<void> {
        if (this.hubConnection && this.hubConnection.state !== HubConnectionState.Disconnected) {
            await this.hubConnection.stop();
        }
        this.isConnectionEstablished = false;
        this.currentRoomId = null;
    }

    startConnection(): void {
        this.initializeSignalRConnection();
    }
}
