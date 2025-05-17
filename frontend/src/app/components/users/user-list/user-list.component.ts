import { Component, OnInit } from '@angular/core';
import {UsersService} from "../../../services";
import {User} from "../../../models/user";

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.css']
})
export class UserListComponent implements OnInit {
  users: User[] = [];
  isLoading = false;
  error: string = '';
  constructor(private userService: UsersService) { }

  ngOnInit() {
    this.fetchUsers();
  }
  fetchUsers(): void {
    this.isLoading = true;
    this.userService.getUsers().subscribe({
      next: (data) => {
        this.users = data;
        this.isLoading = false;
        console.log(this.users);
      },
      error: (err) => {
        console.error("Error fetching users: ", err);
        this.error = "Failed to fetch users";
        this.isLoading = false;
      }
    })
  }
}
