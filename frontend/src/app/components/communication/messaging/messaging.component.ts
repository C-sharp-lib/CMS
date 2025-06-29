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
  user: any;
  userPhotos: string[] = [];
  conversations: Conversation[] = [];
  messages: Message[] = [];
  error: string = '';
  userId: string | null = null;
  userImage: string | null = null;
  theContent = '';
  selectedConversation: Conversation | null = null;
  constructor(private userService: UsersService, private router: Router, private communicationService: CommunicationService,
              private toast: ToasterService, private route: ActivatedRoute, private renderer: Renderer2, private el: ElementRef) { }
  ngOnInit() {
    this.userId = this.getCurrentUserId();
    const id = Number(this.route.snapshot.paramMap.get('id'));
    if (!this.userId) {
      this.router.navigate(['/account']);
      return;
    }
    this.fetchConversations(this.userId);
    this.loadConversation(id);

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
      next: (convos) => {
        this.conversations = convos.map(convo => {
          const loadedImages = new Set<string>();
          const userPhotos: string[] = [];

          convo.participants.forEach(participant => {
            const imageUrl = participant.user?.imageUrl;

            if (imageUrl && !loadedImages.has(imageUrl)) {
              loadedImages.add(imageUrl);
              const relativePath = `http://localhost:5224/Uploads/User/${imageUrl}`;
              userPhotos.push(relativePath);
              this.userPhotos = userPhotos;
            }
          });

          // Attach photos to the conversation object
          return {
            ...convo,
            userPhotos
          };
        });
      },
      error: (err) => {
        this.error = err.message;
        console.error('Failed to fetch conversations:', this.error);
      }
    });
  }
  loadConversation(convoId: number): void {
    this.communicationService.getConversationById(convoId).subscribe({
      next: (convo) => {
        this.selectedConversation = convo;
      },
      error: (err) => {
        this.error = err.message;
        console.error('Failed to load conversation:', this.error);
      }
    });
  }
/*  loadUserPhoto(relativePath: string) {
   this.userService.getUserImageUrl(relativePath).subscribe({
     next: (res) => {
       this.userImage = res.imageUrl;
     },
     error: (err) => {
       console.error('Failed to load user photo:', err.message);
     }
   })
  }*/

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
