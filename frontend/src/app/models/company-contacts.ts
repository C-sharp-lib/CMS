import {Contact} from "./contact";
import {Company} from "./company";

export interface CompanyContacts {
  id: number;
  contactId: number;
  companyId: number;
  contact: Contact;
  company: Company;
}
