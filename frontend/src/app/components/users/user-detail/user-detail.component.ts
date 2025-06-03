import {Component, ElementRef, OnInit, Renderer2} from '@angular/core';
import {ActivatedRoute} from "@angular/router";
import {User} from "../../../models/user";
import {ToasterService, UsersService} from "../../../services";

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
  error: string = '';
  editorInstance: any;

  constructor(
    private route: ActivatedRoute,
    private userService: UsersService,
    private toast: ToasterService,
    private el: ElementRef,
    private renderer: Renderer2
  ) {}

  ngOnInit(): void {
    this.loadUser();
    this.getRawHtml();
    this.newSectionUp();
  }

  loadUser(): void {
    const id = this.route.snapshot.paramMap.get('id');
    this.userService.getUserById(id).subscribe({
      next: (data) => {
        this.user = data;
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
    this.userService.getUserImage(relativePath).subscribe({
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
  newSectionUp() {
    const section = this.el.nativeElement.querySelector('#user-detail-section');
    this.renderer.addClass(section, 'active');
  }

  getRawHtml(): string {
    return this.editorInstance?.root.innerHTML || '';
  }
}
