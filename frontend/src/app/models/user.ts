import {UserRole} from "./userRoles";
import {Job} from "./job";

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

  userRoles?: UserRole[];
  createdJobs?: Job[];
  assignedJobs?: Job[];
  comments?: Comment[];
}
