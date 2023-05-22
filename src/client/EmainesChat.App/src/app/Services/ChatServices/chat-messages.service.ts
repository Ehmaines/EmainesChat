import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';

@Injectable({
    providedIn: 'root',
})
export class ChatMessagesService {
    private hubConnection: HubConnection;

    constructor() {
        this.hubConnection = new HubConnectionBuilder()
            .withUrl('https://localhost:7080/messageHub') // Substitua pela URL do seu servidor SignalR e hub específico
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
    }

    sendMessage() {
        this.hubConnection
            .invoke(
                'SendMessage',
                'Olá, servidor! Esta é uma mensagem do cliente.'
            )
            .catch((error) =>
                console.error('Erro ao enviar a mensagem:', error)
            );
    }
}
