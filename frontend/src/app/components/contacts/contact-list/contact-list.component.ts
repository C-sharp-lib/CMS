import {Component, ElementRef, OnInit, Renderer2} from '@angular/core';
import {CompanyContactService, ToasterService, UsersService} from "../../../services";
import {ActivatedRoute, Router} from "@angular/router";
import {ContactService} from "../../../services/contact.service";

@Component({
  selector: 'app-contact-list',
  templateUrl: './contact-list.component.html',
  styleUrls: ['./contact-list.component.css']
})
export class ContactListComponent implements OnInit {
  contacts: any[] = [];
  paginatedContacts: any[] = [];
  isLoading = false;
  error: string = '';
  currentPage: number = 1;
  itemsPerPage: number = 10;
  totalPages: number = 0;
  companyContacts: any[] = [];
  constructor(private contactService: ContactService, private renderer: Renderer2, private el: ElementRef,
              private route: ActivatedRoute, private toast: ToasterService, private companyContactService: CompanyContactService, private router: Router) { }

  ngOnInit() {
    this.fetchContacts();
    this.totalPages = Math.ceil(this.contacts.length / this.itemsPerPage);
    this.updatePaginatedContacts();
    this.newSectionUp();
  }
  fetchContacts(): void {
    this.isLoading = true;
    this.contactService.getContacts().subscribe({
      next: (data) => {
        this.contacts = data;
        this.loadCompanyContacts();
        this.totalPages = Math.ceil(this.contacts.length / this.itemsPerPage);
        this.updatePaginatedContacts();
        this.isLoading = false;
      },
      error: (err) => {
        console.error("Error fetching contacts: ", err);
        this.error = "Failed to fetch contacts";
        this.toast.showErrorToast(`${this.error}`, 'Error fetching contacts');
        this.isLoading = false;
      }
    })
  }
  get totalPagesArray(): number[] {
    return Array.from({ length: this.totalPages }, (_, i) => i + 1);
  }

  updatePaginatedContacts(): void {
    const startIndex = (this.currentPage - 1) * this.itemsPerPage;
    const endIndex = startIndex + this.itemsPerPage;
    this.paginatedContacts = this.contacts.slice(startIndex, endIndex);
  }

  getToPage(page: number): void {
    if (page >= 1 && page <= this.totalPages) {
      this.currentPage = page;
      this.updatePaginatedContacts();
    }
  }

  prevPage(): void {
    if (this.currentPage > 1) {
      this.currentPage--;
      this.updatePaginatedContacts();
    }
  }

  nextPage(): void {
    if (this.currentPage < this.totalPages) {
      this.currentPage++;
      this.updatePaginatedContacts();
    }
  }
  loadCompanyContacts(): void {
    this.companyContactService.getCompanyContacts().subscribe({
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
  deleteContact(contactId: number, event: Event): void {
    event.preventDefault();

    if (confirm('Are you sure you want to delete this user?')) {
      this.contactService.deleteContactById(contactId).subscribe({
        next: () => {
          this.toast.showSuccessToast(`Deleted contact with the id: ${contactId}`, 'Deleted Contact');
          this.fetchContacts();
          this.contacts = this.contacts.filter(contact => contact.id !== contactId);
        },
        error: err => {
          this.toast.showErrorToast(`${err.message.toString()}`, 'Error deleting contact');
        }
      });
    }
  }
  newSectionUp() {
    const section = this.el.nativeElement.querySelector('#contact-list-section');
    this.renderer.addClass(section, 'active');
  }
  resetSectionPosition() {
    const section = this.el.nativeElement.querySelector('#contact-list-section');
    this.renderer.removeClass(section, 'active');
  }
}
