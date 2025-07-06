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
import {CompanyContactService} from "./company-contact.service";
import {SearchService} from "./search.service";
import {CommunicationService} from "./communication.service";
import {RoleService} from "./role.service";
import {BlackboardService} from "./blackboard.service";
import {TeacherService} from "./teacher.service";
import {StudentService} from "./student.service";
import {CourseService} from "./course.service";


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
  CompanyContactService,
  SearchService,
  CommunicationService,
  RoleService,
  BlackboardService,
  TeacherService,
  StudentService,
  CourseService,
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
export * from './company-contact.service';
export * from './search.service';
export * from './communication.service';
export * from './role.service';
export * from './blackboard.service';
export * from './teacher.service';
export * from './student.service';
export * from './course.service';
