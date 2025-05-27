import { Component } from '@angular/core';
import {UsersService} from "./services";
import {User} from "./models/user";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'frontend';
  fullUser: User | null = null;
  constructor(private userService: UsersService) {
    const token = localStorage.getItem('token');
    if (token) {
      this.userService.saveToken(token);
      this.getCurrentUser();
    }
  }
  getCurrentUserId(): string {
    const token = localStorage.getItem('token');
    if (!token) return '';
    const decoded = JSON.parse(atob(token.split('.')[1]));
    return decoded.nameid;
  }
  getCurrentUser() {
    const userId = this.getCurrentUserId();

    this.userService.getUserById(userId).subscribe({
      next: (user) => {
        console.log('Full user info:', user);
        this.fullUser = user;
      },
      error: (err) => {
        console.error('Failed to fetch user:', err);
      }
    });
  }
}
