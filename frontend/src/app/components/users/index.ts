import {LoginComponent} from "./login/login.component";
import {RegisterComponent} from "./register/register.component";
import {UserListComponent} from "./user-list/user-list.component";
import {UserDetailComponent} from "./user-detail/user-detail.component";
import {UserUpdateComponent} from "./user-update/user-update.component";
import {RoleListComponent} from "./roles/role-list/role-list.component";
import {RoleDetailComponent} from "./roles/role-detail/role-detail.component";
import {RoleUpdateComponent} from "./roles/role-update/role-update.component";
import {RoleCreateComponent} from "./roles/role-create/role-create.component";


export const components: any[]  = [
  LoginComponent,
  RegisterComponent,
  UserListComponent,
  UserDetailComponent,
  UserUpdateComponent,
  RoleListComponent,
  RoleDetailComponent,
  RoleUpdateComponent,
  RoleCreateComponent,
];

export * from './login/login.component';
export * from './register/register.component';
export * from './user-list/user-list.component';
export * from './user-detail/user-detail.component';
export * from './user-update/user-update.component';
export * from './roles/role-list/role-list.component';
export * from './roles/role-detail/role-detail.component';
export * from './roles/role-update/role-update.component';
export * from './roles/role-create/role-create.component';
