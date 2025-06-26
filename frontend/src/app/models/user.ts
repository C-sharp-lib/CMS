import {UserRole} from "./userRoles";
import {Job} from "./job";
import {Contact} from "./contact";
import {Campaign} from "./campaign";
import {UserNotes} from "./note";
import {Tasks, UserTasks} from "./tasks";
import {ConversationParticipant, Message} from "./communication";

export interface User {
  id?: string;
  userName?: string;
  email?: string;
  phoneNumber?: string;
  name?: string;
  address?: string;
  city?: string;
  state?: string;
  zipCode?: string;
  description?: string;
  dateOfBirth?: string; // ISO format string (e.g., "1990-01-01")
  dateCreated?: string;
  imageUrl?: string;
  tasks?: Tasks[];
  userNotes?: UserNotes[];
  userTasks?: UserTasks[];
  userRoles?: UserRole[];
  createdJobs?: Job[];
  comments?: Comment[];
  assignedJobs?: Job[];
  contacts?: Contact[];
  campaigns?: Campaign[];
  participants?: ConversationParticipant[];
  messages?: Message[];
}
