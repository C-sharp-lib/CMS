import {Component, ElementRef, OnInit, Renderer2} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {Job, Priority, Status} from "../../../models/job";
import {ActivatedRoute, Router} from "@angular/router";
import {JobsService} from "../../../services";

@Component({
  selector: 'app-job-update',
  templateUrl: './job-update.component.html',
  styleUrls: ['./job-update.component.css']
})
export class JobUpdateComponent implements OnInit {
  jobUpdateForm!: FormGroup;
  jobId!: number;
  job!: Job;

  status: Status[] = ['Pending', 'Approved', 'Rejected', 'Completed', 'Cancelled'];
  priority: Priority[] = ['Low', 'Normal', 'High', 'Urgent'];

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private jobService: JobsService,
    private renderer: Renderer2,
    private el: ElementRef,
  ) {}

  ngOnInit(): void {
    this.jobId = +this.route.snapshot.paramMap.get('id')!;
    this.loadJob();
    this.newSectionUp();
  }

  loadJob(): void {
    this.jobService.getJobById(this.jobId).subscribe((job: Job) => {
      this.job = job;
      this.initializeForm();
    });
  }

  initializeForm(): void {
    this.jobUpdateForm = this.fb.group({
      title: [this.job.title, Validators.required],
      description: [this.job.description],
      status: [this.job.status, Validators.required],
      priority: [this.job.priority, Validators.required],
      scheduledDate: [this.job.scheduledDate, Validators.required],
      completionDate: [this.job.completionDate],
      estimatedCost: [this.job.estimatedCost, [Validators.required, Validators.min(0)]],
      actualCost: [this.job.actualCost],
      notes: [this.job.notes],
      assignedUserId: [this.job.assignedUserId]
    });
  }

  onSubmit(): void {
    if (this.jobUpdateForm.invalid) {
      return;
    }

    const updatedJob: Job = {
      ...this.job,
      ...this.jobUpdateForm.value,
      dateUpdated: new Date()
    };

    this.jobService.updateJobById(this.jobId, updatedJob).subscribe(() => {
      this.router.navigate(['/jobs']);
    });
  }

  newSectionUp() {
    const section = this.el.nativeElement.querySelector('#job-update-section');
    this.renderer.addClass(section, 'active');
  }
}
