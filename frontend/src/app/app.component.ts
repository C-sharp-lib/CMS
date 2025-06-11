import {Component, OnInit} from '@angular/core';
import {MenuService, ToasterService, UsersService} from "./services";
import {User} from "./models/user";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'frontend';
  fullUser: User | null = null;
  constructor(private userService: UsersService, private toast: ToasterService,
              private menuService: MenuService) {
   /* this.userService.getCurrentUser = JSON.parse(localStorage.getItem("currentUser") || 'null');*/
  }
  ngOnInit() {
    const isLoggedIn = this.userService.isLoggedIn();
    if(isLoggedIn) {
      this.getCurrentUser();
    } else {
      this.userService.logout();
    }
    this.refreshMainMenu();
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
  getCurrentUser() {
    const userId = this.getCurrentUserId();

    this.userService.getUserById(userId).subscribe({
      next: (user) => {
        this.fullUser = user;
      },
      error: (err) => {
        console.error('Failed to fetch user:', err);
      }
    });
  }
  refreshMainMenu(): void {
    this.menuService.refreshMenu();
  }
}
