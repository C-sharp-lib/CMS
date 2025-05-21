import {Component, ElementRef, OnInit, Renderer2} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {Job, Priority, Status} from "../../../models/job";
import {JobsService, UsersService} from "../../../services";
import {Router} from "@angular/router";
import {User} from "../../../models/user";

@Component({
  selector: 'app-job-create',
  templateUrl: './job-create.component.html',
  styleUrls: ['./job-create.component.css']
})
export class JobCreateComponent implements OnInit {
  jobForm: FormGroup;
  users: User[] = [];

  status: Status[] = ['Pending', 'Approved', 'Rejected', 'Completed', 'Cancelled'];
  priority: Priority[] = ['Low', 'Normal', 'High', 'Urgent'];

  constructor(
    private fb: FormBuilder,
    private jobService: JobsService,
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

  initializeForm(): void {
    this.jobForm = this.fb.group({
      title: ['', Validators.required],
      description: [''],
      status: ['', Validators.required],
      priority: ['', Validators.required],
      scheduledDate: ['', Validators.required],
      completionDate: [''],
      estimatedCost: [0, [Validators.required, Validators.min(0)]],
      actualCost: [''],
      notes: [''],
      assignedUserId: [''],
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
    if (this.jobForm.invalid) {
      this.jobForm.markAllAsTouched();
      return;
    }

    const formValues = this.jobForm.value;

    const newJob: Job = {
      ...formValues,
      dateCreated: new Date(),
      dateUpdated: undefined,
      createdByUserId: this.getCurrentUser(),
    };

    this.jobService.createJob(newJob).subscribe({
      next: () => this.router.navigate(['/jobs']),
      error: err => console.error('Job creation failed', err)
    });
  }
  getCurrentUser(): User {
    console.log(this.userService.getCurrentUser());
    return this.userService.getCurrentUser();
  }
  newSectionUp() {
    const section = this.el.nativeElement.querySelector('#job-create-section');
    this.renderer.addClass(section, 'active');
  }

}
