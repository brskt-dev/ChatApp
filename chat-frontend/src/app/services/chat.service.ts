import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ChatService {
  private hubConnection!: signalR.HubConnection;
  private messages = new BehaviorSubject<string[]>([]);
  messages$ = this.messages.asObservable();

  constructor() {}

  // Iniciar conexão com o SignalR Hub
  startConnection(): void {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl('http://localhost:5000/chathub')
      .withAutomaticReconnect()
      .build();

    this.hubConnection
      .start()
      .then(() => console.log('Conectado ao SignalR'))
      .catch(err => console.error('Erro ao conectar ao SignalR:', err));

    this.hubConnection.on('ReceiveMessage', (user: string, message: string) => {
      const currentMessages = this.messages.value;
      this.messages.next([...currentMessages, `${user}: ${message}`]);
    });
  }

  // Enviar mensagem para o Hub
  sendMessage(user: string, message: string): void {
    this.hubConnection.invoke('SendMessage', user, message).catch(err => console.error(err));
  }
}
