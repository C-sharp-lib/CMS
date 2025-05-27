import {User} from "./user";
import {Contact} from "./contact";
import {JobNotes} from "./note";
import {JobTasks, Tasks} from "./tasks";


export interface Job {
  id: number;
  title: string;
  description?: string;
  status: Status;
  priority: Priority;
  scheduledDate: Date;
  completionDate?: Date;
  estimatedCost: number;
  actualCost?: number;
  notes?: string;
  dateCreated: Date;
  dateUpdated?: Date;
  contactId: number;
  contact: Contact;
  assignedUserId?: string;
  assignedUser: User;
  createdByUserId?: string;
  createdByUser: User;
  jobNotes?: JobNotes[];
  jobTasks?: JobTasks[];
  tasks?: Tasks[];
}

// Enum representations
export type Status =  'Pending' | 'Approved' | 'Rejected' | 'Completed' | 'Cancelled';
export type Priority = 'Low' | 'Normal' | 'High' | 'Urgent';
