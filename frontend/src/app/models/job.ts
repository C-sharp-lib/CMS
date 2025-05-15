import {User} from "./user";


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

  assignedUserId?: string;
  assignedUser?: User;

  createdByUserId?: string;
  createdByUser?: User;

}

// Enum representations
export type Status = 'Pending' | 'InProgress' | 'Completed' | 'Cancelled';
export type Priority = 'Low' | 'Medium' | 'High' | 'Urgent';
