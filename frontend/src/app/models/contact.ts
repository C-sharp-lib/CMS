import {Job} from "./job";
import {Campaign} from "./campaign";
import {ContactTasks, Tasks} from "./tasks";
import {ContactNotes} from "./note";
import {User} from "./user";
import {Company} from "./company";

export interface Contact {
  id: number;
  firstName: string;
  lastName: string;
  jobTitle: string;
  email: string;
  phoneNumber: string;
  addressLine1: string;
  addressLine2: string;
  city: string;
  state: string;
  zipCode: string;
  country: string;
  dateCreated: Date;
  dateUpdated?: Date;
  imageUrl?: string;
  ownerUserId?: string;
  ownerUser?: User;
  companyId?: number;
  company?: Company;
  jobs?: Job[];
  campaigns?: Campaign[];
  tasks?: Tasks[];
  contactNotes?: ContactNotes[];
  contactTasks?: ContactTasks[];
}
