import {AfterViewInit, Component, ElementRef, forwardRef, OnInit, Renderer2, ViewChild} from '@angular/core';
import {ControlValueAccessor, FormBuilder, FormControl, FormGroup, NG_VALUE_ACCESSOR, Validators} from "@angular/forms";
import {Job, Priority, Status} from "../../../models/job";
import {ContactService, JobsService, ToasterService, UsersService} from "../../../services";
import {Router} from "@angular/router";
import {User} from "../../../models/user";
import Quill from "quill";
import {Contact} from "../../../models/contact";


@Component({
  selector: 'app-job-create',
  templateUrl: './job-create.component.html',
  styleUrls: ['./job-create.component.css'],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => JobCreateComponent),
      multi: true,
    }
  ]
})
export class JobCreateComponent implements OnInit, ControlValueAccessor, AfterViewInit {
  @ViewChild('description', {static: true}) description: ElementRef;
  @ViewChild('notes', {static: true}) notes: ElementRef;
  quill!: Quill;
  jobForm: FormGroup;
  users: User[] = [];
  contacts: Contact[] = [];
  editorContent: string = '';
  job: any = {description: '', notes: ''};
  fullUser: User = null;
  private onChange = (_: any) => {};
  private onTouched = () => {};

  value: string = '';

  status: Status[] = ['Pending', 'Approved', 'Rejected', 'Completed', 'Cancelled'];
  priority: Priority[] = ['Low', 'Normal', 'High', 'Urgent'];

  constructor(
    private fb: FormBuilder,
    private jobService: JobsService,
    private router: Router,
    private userService: UsersService,
    private contactService: ContactService,
    private renderer: Renderer2,
    private el: ElementRef,
    private toast: ToasterService
  ) {}

  writeValue(value: any): void {
       this.value = value || '';
       if(this.quill) {
         this.quill.root.innerHTML = this.value;
       }
    }
    registerOnChange(fn: any): void {
       this.onChange = fn;
    }
    registerOnTouched(fn: any): void {
        this.onTouched = fn;
    }


  ngOnInit(): void {
    this.initializeForm();
    this.fetchUsers();
    this.fetchContacts();
    this.newSectionUp();
    this.getCurrentUserId();
    this.getCurrentUser();
  }

  initializeForm(): void {
    this.jobForm = this.fb.group({
      title: ['', Validators.required],
      description: [''],
      status: ['', Validators.required],
      priority: ['', Validators.required],
      scheduledDate: ['', Validators.required],
      contactId: ['', Validators.required],
      estimatedCost: [0, [Validators.required, Validators.min(0)]],
      actualCost: [0, [Validators.min(0)]],
      notes: [''],
      assignedUserId: ['', Validators.required],
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
    });
  }
  fetchContacts(): void {
    this.contactService.getContacts().subscribe({
      next: (data) => {
        this.contacts = data;
      },
      error: (err) => {
        console.error("Error fetching contacts: ", err);
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
  onSubmit(): void {
    if (this.jobForm.invalid) return;
    const formValues = this.jobForm.value;
    formValues.scheduledDate = new Date(formValues.scheduledDate).toISOString();
    const newJob = {
      ...formValues,
      dateCreated: new Date().toISOString(),
      createdByUserId: this.getCurrentUserId().toString(),
    };

    this.jobService.createJob(newJob).subscribe({
      next: () => {
        this.toast.showSuccessToast('Job created successfully', 'Job Created');
        this.jobForm.reset({
          description: '',
          notes: ''
        });
        this.router.navigate(['/jobs']);
      },
      error: (err) => {
        this.toast.showErrorToast(`${err.message}`, 'Error creating job');
      }
    });
  }
  onEditorCreated(quill: any) {
    this.editorContent = quill;
  }
  onEditorContentChanged(event: any, field: string) {
    this.jobForm.get(field)?.setValue(event.html);
  }
  newSectionUp() {
    const section = this.el.nativeElement.querySelector('#job-create-section');
    this.renderer.addClass(section, 'active');
  }
  ngAfterViewInit(): void {

  }
}
