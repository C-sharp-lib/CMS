import {Job, Priority, Status} from "./job";
import {User} from "./user";
import {Contact} from "./contact";
import {Company, CompanyTasks} from "./company";
import {Campaign} from "./campaign";
import {TaskNotes} from "./note";
import {LeadTasks} from "./lead";

export interface Tasks {
  id: number;
  taskDescription: string;
  dueDate: Date;
  status: Status;
  priority: Priority;
  assignedUserId: string;
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
