import {LoginComponent} from "./login/login.component";
import {RegisterComponent} from "./register/register.component";
import {UserListComponent} from "./user-list/user-list.component";
import {UserDetailComponent} from "./user-detail/user-detail.component";
import {UserUpdateComponent} from "./user-update/user-update.component";


export const components: any[]  = [
  LoginComponent,
  RegisterComponent,
  UserListComponent,
  UserDetailComponent,
  UserUpdateComponent,
];

export * from './login/login.component';
export * from './register/register.component';
export * from './user-list/user-list.component';
export * from './user-detail/user-detail.component';
export * from './user-update/user-update.component';
