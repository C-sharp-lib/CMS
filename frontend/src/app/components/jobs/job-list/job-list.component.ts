import {Component, ElementRef, OnInit, Renderer2} from '@angular/core';
import {JobsService, UsersService} from "../../../services";

@Component({
  selector: 'app-job-list',
  templateUrl: './job-list.component.html',
  styleUrls: ['./job-list.component.css']
})
export class JobListComponent implements OnInit {
  jobs: any[] = [];
  paginatedJobs: any[] = [];
  isLoading = false;
  error: string = '';
  currentPage: number = 1;
  itemsPerPage: number = 10;
  totalPages: number = 0;
  constructor(private jobService: JobsService, private renderer: Renderer2, private el: ElementRef) { }

  ngOnInit() {
    this.fetchJobs();
    this.totalPages = Math.ceil(this.jobs.length / this.itemsPerPage);
    this.updatePaginatedJobs();
    this.newSectionUp();
  }
  fetchJobs(): void {
    this.isLoading = true;
    this.jobService.getJobs().subscribe({
      next: (data) => {
        this.jobs = data;
        this.totalPages = Math.ceil(this.jobs.length / this.itemsPerPage);
        this.updatePaginatedJobs();
        this.isLoading = false;
        console.log(this.jobs);
      },
      error: (err) => {
        console.error("Error fetching users: ", err);
        this.error = "Failed to fetch users";
        this.isLoading = false;
      }
    })
  }
  get totalPagesArray(): number[] {
    return Array.from({ length: this.totalPages }, (_, i) => i + 1);
  }

  updatePaginatedJobs(): void {
    const startIndex = (this.currentPage - 1) * this.itemsPerPage;
    const endIndex = startIndex + this.itemsPerPage;
    this.paginatedJobs = this.jobs.slice(startIndex, endIndex);
  }

  getToPage(page: number): void {
    if (page >= 1 && page <= this.totalPages) {
      this.currentPage = page;
      this.updatePaginatedJobs();
    }
  }

  prevPage(): void {
    if (this.currentPage > 1) {
      this.currentPage--;
      this.updatePaginatedJobs();
    }
  }

  nextPage(): void {
    if (this.currentPage < this.totalPages) {
      this.currentPage++;
      this.updatePaginatedJobs();
    }
  }
  newSectionUp() {
    const section = this.el.nativeElement.querySelector('#job-list-section');
    this.renderer.addClass(section, 'active');
  }
  resetSectionPosition() {
    const section = this.el.nativeElement.querySelector('#job-list-section');
    this.renderer.removeClass(section, 'active');
  }

}
