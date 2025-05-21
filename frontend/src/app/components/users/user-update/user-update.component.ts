import { Component, OnInit } from '@angular/core';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {ActivatedRoute, Router} from "@angular/router";
import {ToasterService, UsersService} from "../../../services";
import {User} from "../../../models/user";


@Component({
  selector: 'app-user-update',
  templateUrl: './user-update.component.html',
  styleUrls: ['./user-update.component.css']
})
export class UserUpdateComponent implements OnInit {
  userForm!: FormGroup;
  userId!: string;
  user!: User;

  constructor(
    private fb: FormBuilder,
    private userService: UsersService,
    private route: ActivatedRoute,
    private router: Router,
    private toast: ToasterService,
  ) {}

  ngOnInit(): void {
    this.userId = this.route.snapshot.paramMap.get('id')!;
    this.buildForm();
    this.loadUser();
  }

  buildForm(): void {
    this.userForm = this.fb.group({
      name: ['', Validators.required],
      address: ['', Validators.required],
      city: ['', Validators.required],
      state: ['', Validators.required],
      zipCode: ['', Validators.required],
      phoneNumber: ['', Validators.required],
      description: [''],
      dateOfBirth: ['', Validators.required]
    });
  }

  loadUser(): void {
    this.userService.getUserById(this.userId).subscribe(user => {
      this.user = user;
      this.userForm.patchValue({
        name: user.name,
        address: user.address,
        city: user.city,
        phoneNumber: user.phoneNumber,
        state: user.state,
        zipCode: user.zipCode,
        description: user.description,
        dateOfBirth: this.formatDate(user.dateOfBirth)
      });
    });
  }

  formatDate(date: string): string {
    const d = new Date(date);
    return d.toISOString().substring(0, 10);
  }

  onSubmit(): void {
    if (this.userForm.invalid) {
      return;
    }

    const updatedUser: User = {
      ...this.user,
      ...this.userForm.value,
    };

    this.userService.updateUserById(this.userId, updatedUser).subscribe(() => {
      this.toast.showSuccessToast('User successfully updated', 'User updated successfully');
      this.router.navigate(['/users']);
    });
  }
}
