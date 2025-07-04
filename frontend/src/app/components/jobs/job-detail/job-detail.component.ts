import { Component, OnInit } from '@angular/core';
import {Job} from "../../../models/job";
import {BreadcrumbService, JobsService, ToasterService} from "../../../services";
import {ActivatedRoute, Router} from "@angular/router";
import {ToastrService} from "ngx-toastr";

@Component({
  selector: 'app-job-detail',
  templateUrl: './job-detail.component.html',
  styleUrls: ['./job-detail.component.css']
})
export class JobDetailComponent implements OnInit {
  jobId!: number;
  job!: Job;
  error!: string;
  isLoading: boolean = true;
  editorInstance: any;
  constructor(private jobService: JobsService, private router: Router,
              private route: ActivatedRoute, private toast: ToasterService, private breadcrumbService: BreadcrumbService) { }

  ngOnInit(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    if(isNaN(id)) {
      this.error = 'Invalid Job ID';
      this.isLoading = false;
      return;
    }
    this.loadJob();
    this.loadBreadcrumb();
    this.getRawHtml();
  }
  loadJob(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.jobService.getJobById(id).subscribe({
      next: (data) => {
        this.job = data;
        this.isLoading = false;
      },
      error: (err) => {
        this.error = err.message;
        this.isLoading = false;
        this.toast.showErrorToast(`${this.error.toString()}`, 'Could not load job');
      }
    });
  }

  statusMap: { [key: number]: string } = {
    1: 'Pending',
    2: 'Approved',
    3: 'Rejected',
    4: 'Completed',
    5: 'Cancelled',
  };

  priorityMap: { [key: number]: string } = {
    1: 'Low',
    2: 'Normal',
    3: 'High',
    4: 'Urgent'
  };

loadBreadcrumb(){
  const id = Number(this.route.snapshot.paramMap.get('id'));
  this.jobService.getJobById(id).subscribe(job => {
    this.job = job;
    this.breadcrumbService.setBreadcrumb(job.title);
  });
}

  onEditorCreated(quill: any) {
    this.editorInstance = quill;
  }

  getRawHtml(): string {
    return this.editorInstance?.root.innerHTML || '';
  }
}
