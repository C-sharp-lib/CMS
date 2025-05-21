import {Component, ElementRef, OnInit, Renderer2} from '@angular/core';
import {ToasterService, UsersService} from "../../../services";
import {User} from "../../../models/user";
import {ActivatedRoute} from "@angular/router";

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.css']
})
export class UserListComponent implements OnInit {
  users: any[] = [];
  paginatedUsers: any[] = [];
  isLoading = false;
  error: string = '';
  currentPage: number = 1;
  itemsPerPage: number = 10;
  totalPages: number = 0;
  constructor(private userService: UsersService, private renderer: Renderer2, private el: ElementRef, private route: ActivatedRoute, private toast: ToasterService) { }

  ngOnInit() {
    this.fetchUsers();
    this.totalPages = Math.ceil(this.users.length / this.itemsPerPage);
    this.updatePaginatedUsers();
    this.newSectionUp();
  }
  fetchUsers(): void {
    this.isLoading = true;
    this.userService.getUsers().subscribe({
      next: (data) => {
        this.users = data;
        this.totalPages = Math.ceil(this.users.length / this.itemsPerPage);
        this.updatePaginatedUsers();
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
  get totalPagesArray(): number[] {
    return Array.from({ length: this.totalPages }, (_, i) => i + 1);
  }

  updatePaginatedUsers(): void {
    const startIndex = (this.currentPage - 1) * this.itemsPerPage;
    const endIndex = startIndex + this.itemsPerPage;
    this.paginatedUsers = this.users.slice(startIndex, endIndex);
  }

  getToPage(page: number): void {
    if (page >= 1 && page <= this.totalPages) {
      this.currentPage = page;
      this.updatePaginatedUsers();
    }
  }

  prevPage(): void {
    if (this.currentPage > 1) {
      this.currentPage--;
      this.updatePaginatedUsers();
    }
  }

  nextPage(): void {
    if (this.currentPage < this.totalPages) {
      this.currentPage++;
      this.updatePaginatedUsers();
    }
  }
  deleteUser(userId: string, event: Event): void {
    event.preventDefault();

    if (confirm('Are you sure you want to delete this user?')) {
      this.userService.deleteUser(userId).subscribe({
        next: () => {
          this.toast.showSuccessToast(`Deleted user with the id: ${userId}`, 'Deleted User');
          this.fetchUsers();
          this.users = this.users.filter(user => user.id !== userId);
        },
        error: err => {
          this.toast.showErrorToast(`${err.message.toString()}`, 'Error deleting user');
        }
      });
    }
  }
  newSectionUp() {
    const section = this.el.nativeElement.querySelector('#user-list-section');
    this.renderer.addClass(section, 'active');
  }
  resetSectionPosition() {
    const section = this.el.nativeElement.querySelector('#user-list-section');
    this.renderer.removeClass(section, 'active');
  }
}
