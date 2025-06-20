import {Job, Priority, Status} from "./job";
import {User} from "./user";
import {Contact} from "./contact";
import {Company} from "./company";
import {Campaign} from "./campaign";
import {TaskNotes} from "./note";
import {LeadTasks} from "./lead";

export interface Tasks {
  id: number;
  taskTitle: string;
  taskDescription: string;
  dueDate: Date;
  status: Status;
  priority: Priority;
  assignedToUserId: string;
  assignedToUser: User;
  campaignId: number;
  campaign: Campaign;
  jobId: number;
  job: Job;
  contactId: number;
  contact: Contact;
  companyId: number;
  company: Company;
  dateCreated: Date;
  dateUpdated?: Date;
  dateCompleted?: Date;
  taskNotes?: TaskNotes[];
  jobTasks?: JobTasks[];
  leadTasks?: LeadTasks[];
  campaignTasks?: CampaignTasks[];
  companyTasks?: CompanyTasks[];
}

export interface JobTasks {
  id: number;
  taskId: number;
  jobId: number;
  job: Job;
  tasks: Tasks;
}

export interface CampaignTasks {
  id: number;
  taskId: number;
  campaignId: number;
  campaign: Campaign;
  tasks: Tasks;
}

export interface UserTasks {
  id: number;
  taskId: number;
  userId: string;
  tasks: Tasks;
  user: User;
}

export interface ContactTasks {
  id: number;
  taskId: number;
  contactId: number;
  contact: Contact;
  tasks: Tasks;
}
export interface CompanyTasks {
  id: number;
  companyId: number;
  taskId: number;
  tasks: Tasks;
  company: Company;
}
