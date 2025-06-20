import {Component, ElementRef, OnInit, Renderer2} from '@angular/core';
import {
  CompanyContactService,
  CompanyService,
  ContactService,
  JobsService,
  NotesService,
  TasksService,
  ToasterService,
  UsersService
} from "../../../../services";
import {ActivatedRoute, Router} from "@angular/router";

@Component({
  selector: 'app-job-task-list',
  templateUrl: './job-task-list.component.html',
  styleUrls: ['./job-task-list.component.css']
})
export class JobTaskListComponent implements OnInit{
  job: any;
  jobs: any[] = [];
  paginatedJobs: any[] = [];
  isLoading = false;
  error: string = '';
  currentPage: number = 1;
  itemsPerPage: number = 10;
  totalPages: number = 0;
  jobTasks!: any[];
  contactImage: string | null = null;
  contact: any;
  companies: any[] = [];
  company: any;
  userId: string;
  companyContacts: any[] = [];

  constructor(private jobsService: JobsService, private router: Router, private taskService: TasksService,
              private toast: ToasterService, private route: ActivatedRoute, private renderer: Renderer2,
              private el: ElementRef, private userService: UsersService, private contactsService: ContactService,
              private companyService: CompanyService, private companyContactService: CompanyContactService) {
  }
  ngOnInit() {
    this.fetchJobTasks();
    this.totalPages = Math.ceil(this.jobs.length / this.itemsPerPage);
    this.updatePaginatedJobTasks();
    this.newSectionUp();
    this.loadContact();
    this.loadJob();
  }

  fetchJobTasks(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.isLoading = true;
    this.taskService.getJobTasksByJobId(id).subscribe({
      next: (data) => {
        this.jobTasks = data;
        this.totalPages = Math.ceil(this.jobTasks.length / this.itemsPerPage);
        this.updatePaginatedJobTasks();
        this.isLoading = false;
        console.log(this.jobTasks);
      },
      error: err => {
        this.error = err;
        this.isLoading = false;
        console.error(this.error)
      }
    })
  }
  loadJob() {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.jobsService.getJobById(id).subscribe({
      next: (data) => {
        this.job = data;
        this.isLoading = false;
      },
      error: err => {
        this.error = err.message;
        console.error(this.error)
        this.isLoading = false;
      }
    })
  }
  loadContact(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.contactsService.getContactById(id).subscribe({
      next: (data) => {
        this.contact = data;
        this.loadCompanyContacts(this.contact.id);
        if (this.contact?.imageUrl) {
          const relativePath = `Uploads/Contact/${this.contact?.imageUrl}`;
          this.loadContactImage(relativePath);
        } else {
          this.contactImage = null;
        }
        this.isLoading = false;
      },
      error: (err) => {
        this.error = err.message;
        this.isLoading = false;
        this.toast.showErrorToast(`${this.error.toString()}`, 'Could not load contact');
      }
    });
  }
  loadContactImage(relativePath: string): void {
    this.contactsService.getContactImageUrl(relativePath).subscribe({
      next: (data) => {
        this.contactImage = data.imageUrl;
        console.log(this.contactImage);
      },
      error: (err) => {
        this.error = err.message;
        console.log(this.error.toString());
      }
    })
  }
  loadCompanyContacts(contactId: number): void {
    this.companyContactService.getCompanyContactByContactId(contactId).subscribe({
      next: (data) => {
        this.companyContacts = data;
        this.isLoading = false;
      },
      error: (err) => {
        this.error = err.message;
        console.error(this.error.toString());
        this.isLoading = false;
      }
    })
  }
  get totalPagesArray(): number[] {
    return Array.from({ length: this.totalPages }, (_, i) => i + 1);
  }

  updatePaginatedJobTasks(): void {
    const startIndex = (this.currentPage - 1) * this.itemsPerPage;
    const endIndex = startIndex + this.itemsPerPage;
    if(this.jobTasks && this.jobTasks.length > 0) {
      this.paginatedJobs = this.jobTasks.slice(startIndex, endIndex);
    } else {
      this.paginatedJobs = [];
    }

  }

  getToPage(page: number): void {
    if (page >= 1 && page <= this.totalPages) {
      this.currentPage = page;
      this.updatePaginatedJobTasks();
    }
  }

  prevPage(): void {
    if (this.currentPage > 1) {
      this.currentPage--;
      this.updatePaginatedJobTasks();
    }
  }

  nextPage(): void {
    if (this.currentPage < this.totalPages) {
      this.currentPage++;
      this.updatePaginatedJobTasks();
    }
  }

  onDeleteJobTask(id: number): void {
    this.taskService.deleteJobTask(id).subscribe({
      next: () => {
        this.router.navigate([`/jobs`]);
        this.fetchJobTasks();
      },
      error: err => {
        this.error = err;
        console.error(this.error);
      }
    })
  }
  newSectionUp() {
    const section = this.el.nativeElement.querySelector('#job-task-list-section');
    this.renderer.addClass(section, 'active');
  }
  resetSectionPosition() {
    const section = this.el.nativeElement.querySelector('#job-task-list-section');
    this.renderer.removeClass(section, 'active');
  }
}
