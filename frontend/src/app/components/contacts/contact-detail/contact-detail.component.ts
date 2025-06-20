import {Component, ElementRef, OnInit, Renderer2} from '@angular/core';
import {BreadcrumbService, CompanyContactService, ContactService, JobsService, ToasterService} from "../../../services";
import {ActivatedRoute, Router} from "@angular/router";
import {environment} from "../../../../environments/environment";

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
  imagePath: string = '';
  contactImageUrl: string  | null = null;
  companyContacts: any[] = [];
  constructor(private contactService: ContactService, private router: Router,
              private route: ActivatedRoute, private toast: ToasterService, private renderer: Renderer2, private el: ElementRef,
              private breadcrumbService: BreadcrumbService, private companyContactService: CompanyContactService, private toasterService: ToasterService,) { }

  ngOnInit(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    if(isNaN(id)) {
      this.error = 'Invalid Contact ID';
      this.isLoading = false;
      return;
    }
    this.loadContact();
    this.loadBreadcrumb();
    this.getRawHtml();
    this.newSectionUp();
  }
  loadContact(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.contactService.getContactById(id).subscribe({
      next: (data) => {
        this.contact = data;
        this.loadContactImage(this.contact);
        this.loadCompanyContacts(this.contact.id);
        this.isLoading = false;
      },
      error: (err) => {
        this.error = err.message;
        this.isLoading = false;
        this.toast.showErrorToast(`${this.error.toString()}`, 'Could not load contact');
      }
    });
  }
  loadContactImage(contact: any){
    this.contactService.getContactImageUrl(contact.imageUrl).subscribe({
      next: (data) => {
        this.contactImageUrl = data.imageUrl;
        console.log(this.contactImageUrl);
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
  loadBreadcrumb(){
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.contactService.getContactById(id).subscribe(contact => {
      this.contact = contact;
      this.breadcrumbService.setBreadcrumb(contact.email);
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
