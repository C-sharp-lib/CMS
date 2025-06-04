import {UsersService} from "./users.service";
import {BlogService} from "./blog.service";
import {AuthService} from "./auth.service";
import {JobsService} from "./jobs.service";
import {ToasterService} from "./toaster.service";
import {ContactService} from "./contact.service";
import {MenuService} from "./menu.service";
import {CompanyService} from "./company.service";
import {BreadcrumbService} from "./breadcrumb.service";
import {NotesService} from "./notes.service";
import {TasksService} from "./tasks.service";
import {CampaignsService} from "./campaigns.service";

export const services: any[] = [
  UsersService,
  AuthService,
  BlogService,
  JobsService,
  ToasterService,
  ContactService,
  MenuService,
  CompanyService,
  BreadcrumbService,
  NotesService,
  TasksService,
  CampaignsService,
];

export * from './users.service';
export * from './auth.service';
export * from './blog.service';
export * from './jobs.service';
export * from './toaster.service';
export * from './contact.service';
export * from './menu.service';
export * from './company.service';
export * from './breadcrumb.service';
export * from './notes.service';
export * from './tasks.service';
export * from './campaigns.service';
