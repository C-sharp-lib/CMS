import {Component, OnInit} from '@angular/core';
import {User} from "../../../models/user";
import {HttpClient} from "@angular/common/http";
import {CommunicationService, UsersService} from "../../../services";
import {FormBuilder, FormControl, FormGroup, Validators} from "@angular/forms";
import {Router} from "@angular/router";

@Component({
  selector: 'app-message-create',
  templateUrl: './message-create.component.html',
  styleUrls: ['./message-create.component.css']
})
export class MessageCreateComponent implements OnInit {
  allUsers: User[] = [];
  selectedUserIds: string[] = [];
  title: string = '';
  convoForm: FormGroup;

  constructor(private http: HttpClient, private commService: CommunicationService, private fb: FormBuilder, private router: Router,
              private userService: UsersService) {
    this.convoForm = this.fb.group({
      title: ['', Validators.required],
      userIds: this.fb.group({})
    });
  }

  ngOnInit(): void {
    this.loadUsers();
  }

  loadUsers(): void {
    this.userService.getUsers().subscribe({
      next: (data) => {
        this.allUsers = data;
        const userIdsGroup = this.convoForm.get('userIds') as FormGroup;
        data.forEach(user => {
          userIdsGroup.addControl(user.id, new FormControl(false));
        });
      },
      error: (err) => {
        console.error("Error fetching users: ", err);
      }
    })
  }

  toggleUser(userId: string, checked: boolean): void {
    if (checked) {
      this.selectedUserIds.push(userId);
    } else {
      this.selectedUserIds = this.selectedUserIds.filter(id => id !== userId);
    }
  }

  onSubmit(): void {
    if (this.convoForm.invalid) return;

    const selectedUserIds = Object.entries(this.convoForm.value.userIds)
      .filter(([id, checked]) => checked)
      .map(([id]) => id);

    const payload = {
      title: this.convoForm.value.title,
      userIds: selectedUserIds
    };

    this.commService.createConversation(payload).subscribe({
      next: () => {
        alert('Conversation created!');
        this.convoForm.reset();
      },
      error: err => console.error('Failed to create conversation', err)
    });
  }
}
