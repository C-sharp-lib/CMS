import {Component, ElementRef, OnInit, Renderer2, ViewChild} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {BreadcrumbService, ContactService, ToasterService} from "../../../services";
import {ActivatedRoute, Router} from "@angular/router";
import {Contact} from "../../../models/contact";
import {Company} from "../../../models/company";
import {User} from "../../../models/user";
import Quill from "quill";

@Component({
  selector: 'app-contact-update',
  templateUrl: './contact-update.component.html',
  styleUrls: ['./contact-update.component.css']
})
export class ContactUpdateComponent implements OnInit {
  @ViewChild('updateContactNotes', {static: true}) updateContactNotes: ElementRef;
  contactForm!: FormGroup;
  contacts: Contact[] = [];
  companies: Company[] = [];
  users: User[] = [];
  selectedFile: File | null = null;
  imagePreview: string | ArrayBuffer | null = null;
  editorContent: string = '';
  contact: any;
  quill!: Quill;
  value: string = '';
  isLoading: boolean = true;
  error: string = '';
  editorInstance: any;
  imagePath: string = '';
  contactImageUrl: string  | null = null;
  constructor(private contactService: ContactService, private route: ActivatedRoute, private breadcrumbService: BreadcrumbService,
              private toast: ToasterService, private renderer: Renderer2, private el: ElementRef, private fb: FormBuilder,
              private router: Router) { }
  ngOnInit() {
    this.loadBreadcrumb();
    this.initializeForm();
    this.loadContactData();
    this.loadContact();
    this.newSectionUp();
  }
  onFileSelected(event: any) {
    if (event.target.files && event.target.files[0]) {
      this.selectedFile = event.target.files[0];

      const reader = new FileReader();
      reader.onload = () => {
        this.imagePreview = reader.result as string;
      };
      reader.readAsDataURL(this.selectedFile);
    }
  }
  loadContact(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.contactService.getContactById(id).subscribe({
      next: (data) => {
        this.contact = data;
        this.loadContactImage(this.contact);
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

  loadBreadcrumb(){
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.contactService.getContactById(id).subscribe(contact => {
      this.contact = contact;
      this.breadcrumbService.setBreadcrumb(contact.email);
    });
  }

  newSectionUp() {
    const section = this.el.nativeElement.querySelector('#contact-update-section');
    this.renderer.addClass(section, 'active');
  }
  initializeForm(): void {
    this.contactForm = this.fb.group({
      firstName: ['', [Validators.required]],
      lastName: ['', [Validators.required]],
      addressLine1: ['', [Validators.required]],
      addressLine2: ['', [Validators.required]],
      city: ['', [Validators.required]],
      state: ['', [Validators.required]],
      zipCode: ['', [Validators.required]],
      country: ['', [Validators.required]],
      phoneNumber: ['', [Validators.required]],
      jobTitle: ['', [Validators.required]],
      email: ['', [Validators.required]],
      ownerUserId: ['', [Validators.required]],
      companyId: ['', [Validators.required]],
      dateUpdated: [new Date().toISOString()],
      imageUrl: [null],
    });
  }

  loadContactData(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.contactService.getContactById(id).subscribe(contact => {
      this.contact = contact;
      this.loadContactImage(this.contact);
      this.contactForm.patchValue({
        firstName: contact.firstName,
        lastName: contact.lastName,
        addressLine1: contact.addressLine1,
        addressLine2: contact.addressLine2,
        city: contact.city,
        country: contact.country,
        phoneNumber: contact.phoneNumber,
        email: contact.email,
        jobTitle: contact.jobTitle,
        dateUpdated: contact.dateUpdated,
        ownerUserId: contact.ownerUserId,
        imageUrl: contact.imageUrl,
      });
    });
  }

  onSubmit(): void {
    if (this.contactForm.invalid) return;
    const formData = new FormData();
    const formValues = this.contactForm.value;
    const id = Number(this.route.snapshot.paramMap.get('id'));
    formData.append('firstName', formValues.firstName);
    formData.append('lastName', formValues.lastName);
    formData.append('jobTitle', formValues.jobTitle);
    formData.append('email', formValues.email);
    formData.append('phoneNumber', formValues.phoneNumber);
    formData.append('addressLine1', formValues.addressLine1);
    formData.append('addressLine2', formValues.addressLine2);
    formData.append('city', formValues.city);
    formData.append('state', formValues.state);
    formData.append('zipCode', formValues.zipCode);
    formData.append('country', formValues.country);
    formData.append('notes', formValues.notes);
    formData.append('ownerUserId', formValues.ownerUserId);
    formData.append('dateUpdated', formValues.dateUpdated);
    if(this.selectedFile) {
      formData.append('imageUrl', this.selectedFile); // 🔥 Must match backend param name
    }
    this.contactService.updateContactById(id, formData).subscribe({
      next: () => {
        this.contactForm.reset({
          notes: ''
        });
        this.toast.showSuccessToast('Contact updated successfully', 'Contact Updated');
        this.router.navigate(['/contacts']);
      },
      error: err => {
        console.error('Contact update failed', err);
        this.toast.showErrorToast(`${err.message}`, 'Error updating contact');
      }
    });
  }
  onEditorCreated(quill: any) {
    this.editorContent = quill;
  }
  onEditorContentChanged(event: any, field: string) {
    this.contactForm.get(field)?.setValue(event.html);
  }
  getRawHtml(): string {
    return this.editorInstance?.root.innerHTML || '';
  }
}
