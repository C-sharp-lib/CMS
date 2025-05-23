import {Component, ElementRef, OnInit, Renderer2} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {User} from "../../../models/user";
import {Job, Priority, Status} from "../../../models/job";
import {JobsService, UsersService} from "../../../services";
import {Router} from "@angular/router";
import {Contact} from "../../../models/contact";
import {ContactService} from "../../../services/contact.service";

@Component({
  selector: 'app-contact-create',
  templateUrl: './contact-create.component.html',
  styleUrls: ['./contact-create.component.css']
})
export class ContactCreateComponent implements OnInit {
  contactForm: FormGroup;
  contact: Contact[] = [];
  users: User[] = [];
  selectedFile: File | null = null;
  imagePreview: string | ArrayBuffer | null = null;
  constructor(
    private fb: FormBuilder,
    private contactService: ContactService,
    private router: Router,
    private userService: UsersService,
    private renderer: Renderer2,
    private el: ElementRef
  ) {}

  ngOnInit(): void {
    this.initializeForm();
    this.fetchUsers();
    this.newSectionUp();
    this.getCurrentUser();
  }
  onFileSelected(event: Event): void {
    const input = event.target as HTMLInputElement;
    if (input.files && input.files.length > 0) {
      this.selectedFile = input.files[0];

      // Preview the image using FileReader
      const reader = new FileReader();
      reader.onload = () => {
        this.imagePreview = reader.result;
      };
      reader.readAsDataURL(this.selectedFile);
    }
  }
  initializeForm(): void {
    this.contactForm = this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      companyName: ['', Validators.required],
      jobTitle: ['', Validators.required],
      email: ['', Validators.required],
      phoneNumber: ['', Validators.required],
      addressLine1: ['', [Validators.required]],
      addressLine2: ['', [Validators.required]],
      city: ['', [Validators.required]],
      state: ['', [Validators.required]],
      zipCode: ['', [Validators.required]],
      notes: [''],
      ownerUserId: ['', Validators.required],
    });
  }
  fetchUsers(): void {
    this.userService.getUsers().subscribe({
      next: (data) => {
        this.users = data;
      },
      error: (err) => {
        console.error("Error fetching users: ", err);
      }
    })
  }
  onSubmit(): void {
    if (this.contactForm.invalid) {
      this.contactForm.markAllAsTouched();
      return;
    }

    const formValues = this.contactForm.value;
    formValues.append('imageUrl', this.selectedFile, this.selectedFile.name);
    const newContact: Contact = {
      ...formValues,
      dateCreated: new Date(),
      createdByUserId: this.getCurrentUser(),
    };

    this.contactService.createContact(newContact).subscribe({
      next: () => this.router.navigate(['/contacts']),
      error: err => console.error('Contact creation failed', err)
    });
  }
  getCurrentUser(): User {
    console.log(this.userService.getCurrentUser());
    return this.userService.getCurrentUser();
  }
  newSectionUp() {
    const section = this.el.nativeElement.querySelector('#contact-create-section');
    this.renderer.addClass(section, 'active');
  }
}
