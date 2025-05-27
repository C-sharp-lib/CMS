import {User} from "./user";
import {Campaign} from "./campaign";
import {Contact} from "./contact";
import {Company} from "./company";
import {Job} from "./job";
import {LeadNotes} from "./lead";

export interface Note {
  id: number;
  title: string;
  content: string;
  created: Date;
  updated: Date;
  userNotes: UserNotes[];
  leadNotes: LeadNotes[];
  taskNotes: TaskNotes[];
  companyNotes: CompanyNotes[];
  contactNotes: ContactNotes[];
  campaignNotes: CampaignNotes[];
  jobNotes: JobNotes[];
}

export interface UserNotes {
  id: number;
  userId: string;
  user: User;
  noteId: number;
  note: Note;
}

export interface CampaignNotes {
  id: number;
  campaignId: number;
  campaign: Campaign;
  note: Note;
  noteId: number;
}
export interface ContactNotes {
  id: number;
  contactId: number;
  noteId: number;
  note: Note;
  contact: Contact;
}
export interface CompanyNotes {
  id: number;
  companyId: number;
  noteId: number;
  note: Note;
  company: Company;
}
export interface TaskNotes {
  id: number;
  taskId: number;
  task: Task;
  noteId: number;
  note: Note;
}
export interface JobNotes {
  id: number;
  jobId: number;
  noteId: number;
  note: Note;
  job: Job;
}
