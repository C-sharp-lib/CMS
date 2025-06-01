import {Component, ElementRef, OnInit, Renderer2} from '@angular/core';
import {ContactService, JobsService, ToasterService} from "../../../services";
import {ActivatedRoute, Router} from "@angular/router";

@Component({
  selector: 'app-contact-detail',
  templateUrl: './contact-detail.component.html',
  styleUrls: ['./contact-detail.component.css']
})
export class ContactDetailComponent implements OnInit {
  contact: any;
  company: any;
  error!: string;
  isLoading: boolean = true;
  editorInstance: any;
  constructor(private contactService: ContactService, private router: Router,
              private route: ActivatedRoute, private toast: ToasterService, private renderer: Renderer2, private el: ElementRef) { }

  ngOnInit(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    if(isNaN(id)) {
      this.error = 'Invalid Contact ID';
      this.isLoading = false;
      return;
    }
    this.loadContact();
    this.getRawHtml();
    this.newSectionUp();
  }
  loadContact(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.contactService.getContactById(id).subscribe({
      next: (data) => {
        this.contact = data;
        this.isLoading = false;
      },
      error: (err) => {
        this.error = err.message;
        this.isLoading = false;
        this.toast.showErrorToast(`${this.error.toString()}`, 'Could not load contact');
      }
    });
  }
  onEditorCreated(quill: any) {
    this.editorInstance = quill;
  }
  newSectionUp() {
    const section = this.el.nativeElement.querySelector('#contact-detail-section');
    this.renderer.addClass(section, 'active');
  }

  getRawHtml(): string {
    return this.editorInstance?.root.innerHTML || '';
  }
}
