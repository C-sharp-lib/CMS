import {Contact} from "./contact";
import {CampaignTasks, Tasks} from "./tasks";
import {CampaignNotes} from "./note";
import {Lead} from "./lead";

export interface Campaign {
  id: number;
  name: string;
  description: string;
  type: string;
  status: string;
  startDate: Date,
  endDate?: Date,
  budget?: number;
  actualCost?: number;
  expectedResponses?: number;
  actualResponses?: number;
  expectedSales?: number;
  actualSales?: number;
  notes?: string;
  dateCreated: Date;
  dateUpdated?: Date;
  createdByUserId: string;
  leads: Lead[];
  contacts: Contact[];
  tasks: Tasks[];
  campaignNotes: CampaignNotes[];
  campaignTask: CampaignTasks[];
}


