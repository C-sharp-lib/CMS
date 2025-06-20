import {Contact} from "./contact";
import {CompanyTasks, Tasks} from "./tasks";
import {CompanyNotes} from "./note";
import {CompanyContacts} from "./company-contacts";

export interface Company {
  id: number;
  name: string;
  industry: string;
  website: string;
  address: string;
  city: string;
  state: string;
  country: string;
  zipCode: string;
  phoneNumber: string;
  fax: string;
  description: string;
  dateCreated: Date;
  companyContacts: CompanyContacts[];
  tasks?: Tasks[];
  companyNotes?: CompanyNotes[];
  companyTasks?: CompanyTasks[];
}


