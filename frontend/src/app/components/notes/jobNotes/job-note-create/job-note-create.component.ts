import {Component, ElementRef, OnInit, Renderer2, ViewChild} from '@angular/core';
import {JobsService, NotesService, ToasterService} from "../../../../services";
import {ActivatedRoute, Router} from "@angular/router";
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {JobNotes} from "../../../../models/note";
import Quill from "quill";

@Component({
  selector: 'app-job-note-create',
  templateUrl: './job-note-create.component.html',
  styleUrls: ['./job-note-create.component.css']
})
export class JobNoteCreateComponent implements OnInit {
  @ViewChild('content', {static: true}) content: ElementRef;
  jobNotesForm: FormGroup;
  job: any;
  jobs: any[] = [];
  isLoading: boolean = true;
  error: string = '';
  quill: Quill;
  editorContent: string = '';
  constructor(private renderer: Renderer2, private el: ElementRef, private notesService: NotesService,
              private jobsService: JobsService, private router: Router, private toast: ToasterService,
              private fb: FormBuilder, private route: ActivatedRoute) {
  }
  ngOnInit() {
    this.initializeForm();
    this.newSectionUp();
    this.loadJob();
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
  initializeForm(): void {
    this.jobNotesForm = this.fb.group({
      title: ['', [Validators.required]],
      content: ['', [Validators.required]]
    });
  }
  onSubmit(): void {
    if (this.jobNotesForm.invalid) return;
    const formValues = this.jobNotesForm.value;
    const id = Number(this.route.snapshot.paramMap.get('id'));
    const jobNote = {
      ...formValues,
      created: new Date().toISOString(),
      jobId: id
    }
    this.notesService.createJobNote(jobNote).subscribe({
      next: (data) => {
        this.toast.showSuccessToast(`Job note created for the job`, 'Job note created successfully!');
        this.jobNotesForm.reset({
          content: ''
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
    const section = this.el.nativeElement.querySelector('#job-note-create-section');
    this.renderer.addClass(section, 'active');
  }
  resetSectionPosition() {
    const section = this.el.nativeElement.querySelector('#job-note-create-section');
    this.renderer.removeClass(section, 'active');
  }
}
