import {Campaign} from "./campaign";
import {Tasks} from "./tasks";
import {Note} from "./note";

export interface Lead {
  id: number;
  leadName: string;
  leadAddress?: string;
  leadCity?: string;
  leadState?: string;
  leadZip?: string;
  leadCountry?: string;
  leadPhone?: string;
  leadEmail?: string;
  leadFax?: string;
  leadWebsite?: string;
  created: Date;
  updated?: Date;
  createdBy?: string;
  leadNotes?: LeadNotes[];
  campaigns?: Campaign[];
  leadTasks?: LeadTasks[];
}

export interface LeadTasks {
  id: number;
  taskId: number;
  leadId: number;
  lead: Lead;
  tasks: Tasks;
}

export interface LeadNotes {
  id: number;
  leadId: number;
  noteId: number;
  lead: Lead;
  note: Note;
}
