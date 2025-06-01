import {Component, ElementRef, forwardRef, OnInit, Renderer2, ViewChild} from '@angular/core';
import {FormBuilder, FormGroup, NG_VALUE_ACCESSOR, Validators} from "@angular/forms";
import {User} from "../../../models/user";
import {ToasterService, UsersService, ContactService, CompanyService} from "../../../services";
import {Router} from "@angular/router";
import {Contact} from "../../../models/contact";
import Quill from "quill";
import {Company} from "../../../models/company";

@Component({
  selector: 'app-contact-create',
  templateUrl: './contact-create.component.html',
  styleUrls: ['./contact-create.component.css'],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => ContactCreateComponent),
      multi: true,
    }
  ]
})
export class ContactCreateComponent implements OnInit {
  @ViewChild('contactNotes', {static: true}) notes: ElementRef;
  contactForm!: FormGroup;
  contacts: Contact[] = [];
  companies: Company[] = [];
  users: User[] = [];
  selectedFile: File | null = null;
  selectedImagePath: string = '';
  imagePreview: string | ArrayBuffer | null = null;
  fullUser: User | null = null;
  editorContent: string = '';
  contact: any = {notes: ''};
  quill!: Quill;
  value: string = '';
  private onChange = (_: any) => {};
  private onTouched = () => {};
  constructor(
    private fb: FormBuilder,
    private contactService: ContactService,
    private router: Router,
    private userService: UsersService,
    private renderer: Renderer2,
    private el: ElementRef,
    private toast: ToasterService,
    private companyService: CompanyService,
  ) {}
  ngOnInit(): void {
    this.initializeForm();
    this.fetchUsers();
    this.fetchCompanies();
    this.newSectionUp();
    this.getCurrentUserId();
    this.getCurrentUser();
  }
/*  onFileChange(event: any): void {
    if (event.target.files && event.target.files.length) {
      this.selectedFile = event.target.files[0];
      this.contactForm.patchValue({
        imageUrl: this.selectedFile
      });
    }
  }*/

/*  onFileSelected(event: any): void {
    const file = event.target.files[0];
    if (file) {
      this.selectedFile = file;
      this.contactForm.patchValue({ imageUrl: file });
      this.contactForm.get('imageUrl')?.updateValueAndValidity();
    }
  }*/
/*  onFileSelected(event: Event): void {
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
  }*/
  onFileSelected(event: any) {
    if (event.target.files && event.target.files[0]) {
      this.selectedFile = event.target.files[0];

      const reader = new FileReader();
      reader.onload = () => {
        this.imagePreview = reader.result as string;
      };
      reader.readAsDataURL(this.selectedFile);
    }
  }
  initializeForm(): void {
    this.contactForm = this.fb.group({
      firstName: ['', [Validators.required]],
      lastName: ['', [Validators.required]],
      jobTitle: ['',[Validators.required]],
      email: ['',[Validators.required]],
      phoneNumber: ['', [Validators.required]],
      addressLine1: ['', [Validators.required]],
      addressLine2: [''],
      city: ['', [Validators.required]],
      state: ['', [Validators.required]],
      zipCode: ['', [Validators.required]],
      country: ['', [Validators.required]],
      notes: [''],
      imageUrl: [null],
      ownerUserId: [''],
      companyId: [1],
      dateCreated: [new Date().toISOString()],
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
  fetchCompanies(): void {
    this.companyService.getAllCompanies().subscribe({
      next: (data) => {
        this.companies = data;
      },
      error: (err) => {
        console.error("Error fetching companies: ", err);
      }
    })
  }
  onSubmit(): void {
    if (this.contactForm.invalid) return;
      const formData = new FormData();
      const formValues = this.contactForm.value;

      formData.append('firstName', formValues.firstName);
      formData.append('lastName', formValues.lastName);
      formData.append('jobTitle', formValues.jobTitle);
      formData.append('email', formValues.email);
      formData.append('phoneNumber', formValues.phoneNumber);
      formData.append('addressLine1', formValues.addressLine1);
      formData.append('addressLine2', formValues.addressLine2);
      formData.append('city', formValues.city);
      formData.append('state', formValues.state);
      formData.append('zipCode', formValues.zipCode);
      formData.append('country', formValues.country);
      formData.append('notes', formValues.notes);
      formData.append('ownerUserId', formValues.ownerUserId);
      formData.append('companyId', formValues.companyId);
      formData.append('dateCreated', formValues.dateCreated);
      if(this.selectedFile) {
        formData.append('imageUrl', this.selectedFile); // 🔥 Must match backend param name
      }
        this.contactService.createContact(formData).subscribe({
        next: () => {
          this.contactForm.reset({
            notes: ''
          });
          this.toast.showSuccessToast('Contact created successfully', 'Contact Created');
          this.router.navigate(['/contacts']);
        },
        error: err => {
          console.error('Contact creation failed', err);
          this.toast.showErrorToast(`${err.message}`, 'Error creating contact');
        }
      });
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
        console.log('Full user info:', user);
        this.fullUser = user;
      },
      error: (err) => {
        console.error('Failed to fetch user:', err);
      }
    });
  }
  onEditorCreated(quill: any) {
    this.editorContent = quill;
  }
  onEditorContentChanged(event: any, field: string) {
    this.contactForm.get(field)?.setValue(event.html);
  }
  newSectionUp() {
    const section = this.el.nativeElement.querySelector('#contact-create-section');
    this.renderer.addClass(section, 'active');
  }
}
