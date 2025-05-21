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
  userId!: string;
  user?: User;

  constructor(
    private route: ActivatedRoute,
    private userService: UsersService,
    private toast: ToasterService,
    private el: ElementRef,
    private renderer: Renderer2
  ) {}

  ngOnInit(): void {
    this.userId = this.route.snapshot.paramMap.get('id')!;
    this.loadUser();
    this.newSectionUp();
  }
  newSectionUp() {
    const section = this.el.nativeElement.querySelector('#user-detail-section');
    this.renderer.addClass(section, 'active');
  }
  loadUser(): void {
    this.userService.getUserById(this.userId).subscribe({
      next: (user) => this.user = user,
      error: (err) => this.toast.showErrorToast(`${err.message.toString()}`, 'Error Loading User'),
    });
  }
}
