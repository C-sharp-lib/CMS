import {Component, ElementRef, OnInit, Renderer2} from '@angular/core';
import {ActivatedRoute, Router} from "@angular/router";
import {ToasterService, UsersService} from "../../../services";
import {Conversation, ConversationParticipant, Message} from "../../../models/communication";
import {CommunicationService} from "../../../services";

@Component({
  selector: 'app-messaging',
  templateUrl: './messaging.component.html',
  styleUrls: ['./messaging.component.css']
})
export class MessagingComponent implements OnInit {
  isLoading: boolean = false;
  conversations: Conversation[] = [];
  conversationParticipants: ConversationParticipant[] = [];
  messages: Message[] = [];
  error: string = '';
  userId: string | null = null;
  userImage: string | null = null;
  selectedConversation: Conversation | null = null;
  theContent = '';
  constructor(private userService: UsersService, private router: Router, private communicationService: CommunicationService,
              private toast: ToasterService, private route: ActivatedRoute, private renderer: Renderer2, private el: ElementRef) { }
  ngOnInit() {
    this.userId = this.getCurrentUserId();
    this.fetchConversations(this.userId);

  }
  getCurrentUserId(): string {
    const token = localStorage.getItem('token');
    if (!token || token.split('.').length !== 3) {
      return '';
    }

    try {
      const payload = JSON.parse(atob(token.split('.')[1]));
      return payload.nameid || '';
    } catch (e) {
      console.error('Invalid token:', e);
      return '';
    }
  }

  fetchConversations(userId: string) {
    this.communicationService.getUserConversations(userId).subscribe({
      next: (participants) => {
       this.conversationParticipants = participants;
       console.log(participants.length);
       this.conversationParticipants.forEach(user => {
         this.userImage = user.user.imageUrl;
         const conversation = user.conversation;
         console.log(conversation);
         const ps = user.conversation.participants;
         console.log(ps);
         if(user?.user?.imageUrl) {
           const relativePath = `Uploads/User/${this.userImage}`;
           this.loadUserPhoto(relativePath);
         }
       })
      },
      error: (err) => {
        console.error('Failed to fetch conversations:', err);
      }
    });
  }

  loadUserPhoto(relativePath: string) {
    this.communicationService.getParticipantImageUrl(relativePath).subscribe({
      next: (res) => {
        this.userImage = res.imageUrl;
      },
      error: (err) => {
        this.error = err.message;
        console.error('Failed to load user photo:', this.error);
      }
    });
  }

  selectConversation(convo: Conversation) {
    this.selectedConversation = convo;
    this.communicationService.getMessages(convo.id).subscribe({
      next: (msgs) => (this.messages = msgs),
      error: (err) => console.error('Failed to load messages', err),
    });
  }


  sendMessage() {
    if (!this.selectedConversation || !this.theContent.trim()) return;

    const message = {
      content: this.theContent,
      senderId: this.userId
    };

    this.communicationService.sendMessage(this.selectedConversation.id, message).subscribe(sent => {
      this.messages.push(sent);
      this.theContent = '';
    });
  }
}
