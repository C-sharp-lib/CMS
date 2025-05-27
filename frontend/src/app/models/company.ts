import {Contact} from "./contact";
import {CampaignTasks, Tasks} from "./tasks";
import {Note} from "./note";

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
  contacts: Contact[];
  tasks: Tasks[];
  companyNotes?: CompanyNotes[];
  companyTasks: CompanyTasks[];
}
export interface CompanyTasks {
  id: number;
  companyId: number;
  taskId: number;
  tasks: Tasks;
  company: Company;
}
export interface CompanyNotes {
  id: number;
  noteId: number;
  companyId: number;
  company: Company;
  note: Note;
}
