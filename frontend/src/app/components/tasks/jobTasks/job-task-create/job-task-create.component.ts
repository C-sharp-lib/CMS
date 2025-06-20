import {Component, ElementRef, OnInit, Renderer2, ViewChild} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import Quill from "quill";
import {JobsService, NotesService, TasksService, ToasterService, UsersService} from "../../../../services";
import {ActivatedRoute, Router} from "@angular/router";
import {Priority, Status} from "../../../../models/job";
import {User} from "../../../../models/user";

@Component({
  selector: 'app-job-task-create',
  templateUrl: './job-task-create.component.html',
  styleUrls: ['./job-task-create.component.css']
})
export class JobTaskCreateComponent implements OnInit {
  @ViewChild('taskDescription', {static: true}) taskDescription: ElementRef;
  jobTasksForm: FormGroup;
  job: any;
  jobs: any[] = [];
  isLoading: boolean = true;
  error: string = '';
  quill: Quill;
  editorContent: string = '';
  users: User[] = [];
  status: Status[] = ['Pending', 'Approved', 'Rejected', 'Completed', 'Cancelled'];
  priority: Priority[] = ['Low', 'Normal', 'High', 'Urgent'];
  constructor(private renderer: Renderer2, private el: ElementRef, private tasksService: TasksService,
              private jobsService: JobsService, private router: Router, private toast: ToasterService,
              private fb: FormBuilder, private route: ActivatedRoute, private usersService: UsersService) {
  }

  ngOnInit() {
    this.initializeForm();
    this.newSectionUp();
    this.loadJob();
    this.loadUsers();
  }

  loadJob() {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.jobsService.getJobById(id).subscribe({
      next: (data) => {
        this.job = data;
        this.isLoading = false;
      },
      error: err => {
        this.error = err;
        this.isLoading = false;
        console.error(this.error)
      }
    })
  }
  loadUsers(): void {
    this.usersService.getUsers().subscribe({
      next: (data) => {
        this.users = data;
        this.isLoading = false;
      },
      error: err => {
        this.error = err.message;
        console.error(this.error);
        this.isLoading = false;
      }
    })
  }
  initializeForm(): void {
    this.jobTasksForm = this.fb.group({
      taskTitle: ['', [Validators.required]],
      taskDescription: ['', [Validators.required]],
      dueDate: ['', [Validators.required]],
      status: [1, [Validators.required]],
      priority: [1, [Validators.required]],
      assignedToUserId: ['', [Validators.required]],
    });
  }
  onSubmit(): void {
    if (this.jobTasksForm.invalid) return;
    const formValues = this.jobTasksForm.value;
    const id = Number(this.route.snapshot.paramMap.get('id'));
    const jobTask = {
      ...formValues,
      created: new Date().toISOString(),
      dateCreated: new Date().toISOString(),
      jobId: id
    }
    this.tasksService.createJobTask(jobTask).subscribe({
      next: (data) => {
        this.toast.showSuccessToast(`Job note created for the job`, 'Job note created successfully!');
        this.jobTasksForm.reset({
          taskDescription: ''
        });
        this.router.navigate([`/jobs`]);
      },
      error: err => {
        this.error = err;
        this.isLoading = false;
        console.error(this.error)
      }
    })
  }

  onEditorCreated(quill: any) {
    this.editorContent = quill;
  }
  newSectionUp() {
    const section = this.el.nativeElement.querySelector('#job-task-create-section');
    this.renderer.addClass(section, 'active');
  }
  resetSectionPosition() {
    const section = this.el.nativeElement.querySelector('#job-task-create-section');
    this.renderer.removeClass(section, 'active');
  }
}
