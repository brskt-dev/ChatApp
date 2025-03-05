import { Component, OnInit } from '@angular/core';
import { ChatService } from '../../services/chat.service';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-chat',
  imports: [CommonModule, FormsModule],
  templateUrl: './chat.component.html',
  styleUrl: './chat.component.scss'
})
export class ChatComponent implements OnInit {
  messages: string[] = [];
  message = '';
  username = 'Usuário' + Math.floor(Math.random() * 1000);

  constructor(private chatService: ChatService) {}

  ngOnInit(): void {
    this.chatService.startConnection();
    this.chatService.messages$.subscribe(msgs => (this.messages = msgs));
  }

  sendMessage(): void {
    if (this.message.trim()) {
      this.chatService.sendMessage(this.username, this.message);
      this.message = '';
    }
  }
}
