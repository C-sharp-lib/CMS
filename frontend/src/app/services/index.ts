import {UsersService} from "./users.service";
import {BlogService} from "./blog.service";
import {AuthService} from "./auth.service";
import {JobsService} from "./jobs.service";
import {ToasterService} from "./toaster.service";


export const services: any[] = [
  UsersService,
  AuthService,
  BlogService,
  JobsService,
  ToasterService,
];

export * from './users.service';
export * from './auth.service';
export * from './blog.service';
export * from './jobs.service';
export * from './toaster.service';
