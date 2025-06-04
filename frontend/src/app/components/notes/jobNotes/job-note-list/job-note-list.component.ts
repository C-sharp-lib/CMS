import {Component, ElementRef, OnInit, Renderer2} from '@angular/core';
import {JobsService, NotesService, ToasterService, UsersService} from "../../../../services";
import {ActivatedRoute, Router} from "@angular/router";
import {User} from "../../../../models/user";

@Component({
  selector: 'app-job-note-list',
  templateUrl: './job-note-list.component.html',
  styleUrls: ['./job-note-list.component.css']
})
export class JobNoteListComponent implements OnInit {
  jobs: any[] = [];
  paginatedJobs: any[] = [];
  isLoading = false;
  error: string = '';
  currentPage: number = 1;
  itemsPerPage: number = 10;
  totalPages: number = 0;
  jobNotes!: any[];

  constructor(private jobsService: JobsService, private router: Router, private notesService: NotesService,
              private toast: ToasterService, private route: ActivatedRoute, private renderer: Renderer2,
              private el: ElementRef, private userService: UsersService) {
  }
  ngOnInit() {
    this.fetchJobNotes();
    this.totalPages = Math.ceil(this.jobs.length / this.itemsPerPage);
    this.updatePaginatedJobNotes();
    this.newSectionUp();
  }

  fetchJobNotes(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.isLoading = true;
    this.notesService.getJobNotesByJobId(id).subscribe({
      next: (data) => {
        this.jobNotes = data;
        this.totalPages = Math.ceil(this.jobNotes.length / this.itemsPerPage);
        this.updatePaginatedJobNotes();
        this.isLoading = false;
        console.log(this.jobNotes);
      },
      error: err => {
        this.error = err;
        this.isLoading = false;
        console.error(this.error)
      }
    })
  }
  get totalPagesArray(): number[] {
    return Array.from({ length: this.totalPages }, (_, i) => i + 1);
  }

  updatePaginatedJobNotes(): void {
    const startIndex = (this.currentPage - 1) * this.itemsPerPage;
    const endIndex = startIndex + this.itemsPerPage;
    if(this.jobNotes && this.jobNotes.length > 0) {
      this.paginatedJobs = this.jobNotes.slice(startIndex, endIndex);
    } else {
      this.paginatedJobs = [];
    }

  }

  getToPage(page: number): void {
    if (page >= 1 && page <= this.totalPages) {
      this.currentPage = page;
      this.updatePaginatedJobNotes();
    }
  }

  prevPage(): void {
    if (this.currentPage > 1) {
      this.currentPage--;
      this.updatePaginatedJobNotes();
    }
  }

  nextPage(): void {
    if (this.currentPage < this.totalPages) {
      this.currentPage++;
      this.updatePaginatedJobNotes();
    }
  }

  onDeleteJobNote(id: number): void {
    this.notesService.deleteJobNote(id).subscribe({
      next: () => {
        this.router.navigate([`/jobs`]);
        this.fetchJobNotes();
      },
      error: err => {
        this.error = err;
        console.error(this.error);
      }
    })
  }
  newSectionUp() {
    const section = this.el.nativeElement.querySelector('#job-note-list-section');
    this.renderer.addClass(section, 'active');
  }
  resetSectionPosition() {
    const section = this.el.nativeElement.querySelector('#job-note-list-section');
    this.renderer.removeClass(section, 'active');
  }
}
