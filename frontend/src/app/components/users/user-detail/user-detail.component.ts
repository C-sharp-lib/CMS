import {Component, ElementRef, OnInit, Renderer2} from '@angular/core';
import {ActivatedRoute} from "@angular/router";
import {User} from "../../../models/user";
import {BreadcrumbService, CommunicationService, ToasterService, UsersService} from "../../../services";

@Component({
  selector: 'app-user-detail',
  templateUrl: './user-detail.component.html',
  styleUrls: ['./user-detail.component.css']
})
export class UserDetailComponent implements OnInit {
  user: any;
  imagePath: string = '';
  isLoading: boolean = true;
  userImageUrl: string  | null = null;
  conversations: any[] = [];
  error: string = '';
  editorInstance: any;
  imageWidth: number | null = null;
  imageHeight: number | null = null;
  maxDisplayWidth: number | null = null;
  maxDisplayHeight: number | null = null;
  conversationCount: number | null = null;
  messageCount: number | null = null;

  constructor(
    private route: ActivatedRoute,
    private userService: UsersService,
    private breadcrumbService: BreadcrumbService,
    private comminicationService: CommunicationService,
    private toast: ToasterService,
    private el: ElementRef,
    private renderer: Renderer2
  ) {}

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    this.loadUser();
    this.loadBreadcrumb();
    this.getRawHtml();
    this.newSectionUp();
    this.loadConversations(id);
  }

  loadUser(): void {
    const id = this.route.snapshot.paramMap.get('id');
    this.userService.getUserById(id).subscribe({
      next: (data) => {
        this.user = data;
        this.loadConversations(id);
        if (this.user?.imageUrl) {
          const relativePath = `Uploads/User/${this.user.imageUrl}`;
          this.loadUserImage(relativePath);
        } else {
          this.userImageUrl = null;
        }
        this.isLoading = false;
      },
      error: (err) => {
        this.toast.showErrorToast(`${err.message}`, 'Error Loading User');
        this.isLoading = false;
      }
    });
  }
  loadUserImage(relativePath: string): void {
    this.userService.getUserImageUrl(relativePath).subscribe({
      next: (response) => {
        this.userImageUrl = response.imageUrl;
      },
      error: (err) => {
        this.userImageUrl = null;
        this.toast.showErrorToast('Failed to load user image', 'Image Error');
        console.error(err);
      }
    });
  }
  onImageLoad(event: Event) {
    const img = event.target as HTMLImageElement;
    const aspRatio = img.naturalWidth/img.naturalHeight;
    this.maxDisplayWidth = 400;
    this.maxDisplayHeight = 400;
    if(img.naturalWidth > img.naturalHeight) {
      this.imageWidth = this.maxDisplayWidth;
      this.imageHeight = this.maxDisplayWidth / aspRatio;
    } else {
      this.imageHeight = this.maxDisplayHeight;
      this.imageWidth = this.maxDisplayHeight / aspRatio;
    }
  }
  loadConversations(userId: string) {
    this.comminicationService.getUserConversations(userId).subscribe({
      next: (conversations) => {
        this.conversations = conversations;
        this.conversationCount = this.conversations.length;
        this.messageCount = this.conversations.reduce((acc, cur) => acc + cur.messages.length, 0);
        console.log(this.conversations);
      },
      error: (err) => {
        this.toast.showErrorToast(`${err.message}`, 'Error Loading Conversations');
        console.error(err.message);
      }
    })
  }
  loadBreadcrumb(): void {
    const id = this.route.snapshot.paramMap.get('id');
    this.userService.getUserById(id).subscribe(user => {
      this.user = user;
      this.breadcrumbService.setBreadcrumb(user.userName);
    });
  }
  newSectionUp() {
    const section = this.el.nativeElement.querySelector('#user-detail-section');
    this.renderer.addClass(section, 'active');
  }
  getRawHtml(): string {
    return this.editorInstance?.root.innerHTML || '';
  }
}
