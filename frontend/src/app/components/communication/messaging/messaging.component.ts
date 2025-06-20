import {Component, OnInit} from '@angular/core';
import {MessageService} from "../../../services/message.service";
import {Router} from "@angular/router";
import {ToastrService} from "ngx-toastr";
import {ToasterService} from "../../../services";
import {Message} from "../../../models/message";

@Component({
  selector: 'app-messaging',
  templateUrl: './messaging.component.html',
  styleUrls: ['./messaging.component.css']
})
export class MessagingComponent implements OnInit {
  messages: Message[] = [];
  newMessageContent: '';
  currentUserId: string;
  selectedUserId: string;
  constructor(private messageService: MessageService, private router: Router, private toast: ToasterService) {
  }
  ngOnInit() {
    this.loadMessages();
  }

  loadMessages() {
    this.messageService.getMessages(this.currentUserId, this.selectedUserId).subscribe(
      messages => {
        this.messages = messages;
      }
    );
  }
  sendMessage() {
    const message : Message = {
      senderId: this.currentUserId,
      receiverId: this.selectedUserId,
      content: this.newMessageContent
    };
    this.messageService.sendMessage(message).subscribe(sent => {
      this.messages.push(sent);
      this.newMessageContent = '';
    });
  }
}
