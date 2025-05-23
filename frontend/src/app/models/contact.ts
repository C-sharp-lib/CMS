import {Job} from "./job";

export interface Contact {
  id?: number;

  firstName: string;
  lastName: string;
  companyName: string;
  jobTitle: string;

  email: string;
  phoneNumber: string;

  addressLine1: string;
  addressLine2?: string;
  city: string;
  state: string;
  zipCode: string;
  country: string;

  notes: string;

  dateCreated?: Date | null;
  dateUpdated?: Date | null;
  imageUrl?: string | null;
  ownerUserId?: string | null;
  companyId?: number | null;
  jobs?: Job[];
}
