import {Component, ElementRef, OnInit, Renderer2, ViewChild} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {ActivatedRoute, Router} from "@angular/router";
import {BreadcrumbService, ToasterService, UsersService} from "../../../services";
import {User} from "../../../models/user";
import Quill from "quill";


@Component({
  selector: 'app-user-update',
  templateUrl: './user-update.component.html',
  styleUrls: ['./user-update.component.css']
})
export class UserUpdateComponent implements OnInit {
  @ViewChild('userDescription', {static: true}) userDescription: ElementRef;
  userForm!: FormGroup;
  userId!: string;
  user: any = {description: ''};
  selectedFile: File | null = null;
  imagePreview: string | ArrayBuffer | null = null;
  fullUser: User | null = null;
  editorContent: string = '';
  quill!: Quill;
  value: string = '';
  userImageUrl: string  | null = null;
  error: string = '';
  private onChange = (_: any) => {};
  private onTouched = () => {};

  constructor(
    private fb: FormBuilder,
    private userService: UsersService,
    private route: ActivatedRoute,
    private router: Router,
    private toast: ToasterService,
    private el: ElementRef,
    private renderer: Renderer2,
    private breadcrumbService: BreadcrumbService,
  ) {}

  ngOnInit(): void {
    this.userId = this.route.snapshot.paramMap.get('id')!.toString();
    this.buildForm();

    this.loadUserData();
    this.loadUser();
    this.loadBreadcrumb();
    this.getCurrentUserId();
    this.getCurrentUser();
    this.newSectionUp();
  }

  buildForm(): void {
    this.userForm = this.fb.group({
      name: ['', [Validators.required]],
      address: ['', [Validators.required]],
      city: ['', [Validators.required]],
      state: ['', [Validators.required]],
      zipCode: ['', [Validators.required]],
      phoneNumber: ['', [Validators.required]],
      description: [''],
      dateOfBirth: ['', [Validators.required]],
      imageUrl: [null],
    });
  }

  loadUserData(): void {
    const id = this.route.snapshot.paramMap.get('id')!.toString();
    this.userService.getUserById(id).subscribe(user => {
      this.user = user;
      const relativePath = `Uploads/User/${this.user.imageUrl}`;
      this.loadUserImage(relativePath);
      this.userForm.patchValue({
        name: user.name,
        address: user.address,
        city: user.city,
        phoneNumber: user.phoneNumber,
        state: user.state,
        zipCode: user.zipCode,
        description: this.safeContent(user.description),
        dateOfBirth: this.formatDate(user.dateOfBirth),
        imageUrl: user?.imageUrl,
      });
    });
  }
  loadUser(): void {
    const id = this.route.snapshot.paramMap.get('id')!.toString();
    this.userService.getUserById(id).subscribe({
      next: (response) => {
        this.user = response;
        if(this.user?.imageUrl) {
          const relativePath = `Uploads/User/${this.user.imageUrl}`;
          this.loadUserImage(relativePath);
          console.log(this.user.imageUrl);
        } else {
          this.userImageUrl = null;
          console.log('Could not load user image.');
        }

      },
      error: (err) => {
        this.error = err;
        console.log(this.error);
      }
    })
  }
  safeContent(value: any): string {
    return value == null ? '' : value;
  }
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
  formatDate(date: string): string {
    const d = new Date(date);
    return d.toISOString().substring(0, 10);
  }
  loadUserImage(relativePath: string): void {
    this.userService.getUserImageUrl(relativePath).subscribe({
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
  onSubmit(): void {
    if (this.userForm.invalid) {
      return;
    }

    const formData = new FormData();
    const formValues = this.userForm.value;
    formData.append('name', formValues.name);
    formData.append('address', formValues.address);
    formData.append('city', formValues.city);
    formData.append('state', formValues.state);
    formData.append('zipCode', formValues.zipCode);
    formData.append('phoneNumber', formValues.phoneNumber);
    formData.append('description', formValues.description);
    formData.append('dateOfBirth', formValues.dateOfBirth);
    if(this.selectedFile) {
      formData.append('imageUrl', this.selectedFile);
    }

    this.userService.updateUserById(this.userId, formData).subscribe({
      next: () => {
        this.toast.showSuccessToast('User successfully updated', 'User updated successfully');
        this.userForm.reset({
          description: ''
        });
        this.router.navigate(['/users']);
      },
      error: (err) => {
        this.toast.showErrorToast(`${err.message}`, 'Error creating user');
      }

    });
  }

  loadBreadcrumb(){
    const id = this.route.snapshot.paramMap.get('id');
    this.userService.getUserById(id).subscribe(user => {
      this.user = user;
      this.breadcrumbService.setBreadcrumb(user.email);
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
    this.userForm.get(field)?.setValue(event.html);
  }
  newSectionUp() {
    const section = this.el.nativeElement.querySelector('#user-update-section');
    this.renderer.addClass(section, 'active');
  }
}
