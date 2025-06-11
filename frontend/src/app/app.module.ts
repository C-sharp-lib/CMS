import {NgModule} from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import {HTTP_INTERCEPTORS, HttpClientModule} from "@angular/common/http";
import { AppComponent } from './app.component';
import {RouterModule, Routes} from "@angular/router";
import * as fromLayout from './components/layout/index';
import * as fromUsers from './components/users/index';
import * as fromJobs from './components/jobs/index';
import * as fromBlog from './components/blog/index';
import * as fromPages from './components/pages/index';
import * as fromContacts from './components/contacts/index';
import * as fromCampaigns from './components/campaigns/index';
import * as fromCompanies from './components/company/index';
import * as fromNotes from './components/notes/index';
import * as fromTasks from './components/tasks/index';
import * as fromServices from './services/index';
import * as fromPipes from './utils/pipes/index';
import * as fromInterceptors from './utils/interceptors/index';
import {ReactiveFormsModule} from "@angular/forms";
import {BrowserAnimationsModule} from "@angular/platform-browser/animations";
import {ToastrModule} from "ngx-toastr";
import {QuillModule} from "ngx-quill";
import {authGuard} from "./utils/guards/auth.guard";


export const routes: Routes = [
  {path: '', children: [
      {path: '', component: fromPages.HomeComponent, pathMatch: 'full', data: {breadcrumb: 'Home'}},
      {path: 'about', component: fromPages.AboutComponent, data: {breadcrumb: 'About'}},
      {path: 'contact', component: fromPages.ContactComponent, data: {breadcrumb: 'Contact'}},
      {path:'privacy-policy', component: fromPages.PrivacyPolicyComponent, data: {breadcrumb: 'Privacy Policy'}},
      {path:'terms-and-conditions', component: fromPages.TermsAndConditionsComponent, data: {breadcrumb: 'Terms And Conditions'}},
      {path:'faq', component: fromPages.FaqComponent, data: {breadcrumb: 'FAQ'}},
    ]},
  {path: 'account', children: [
      {path: '', component: fromUsers.LoginComponent},
      {path:'register-page', component: fromUsers.RegisterComponent},
    ]},
  {path: 'users', canActivate: [authGuard], children: [
      {path: '', component: fromUsers.UserListComponent, data: {breadcrumb: 'Users'}},
      {path: ':id', component: fromUsers.UserDetailComponent, data: {breadcrumb: 'User Details'}},
      {path: 'update/:id', component: fromUsers.UserUpdateComponent, data: {breadcrumb: 'User Update'}},
      {path: 'user/:id/notes', component: fromNotes.UserNoteListComponent, data: {breadcrumb: 'Users Notes'}},
      {path:'notes/create/:id', component: fromNotes.UserNoteCreateComponent, data: {breadcrumb: 'Create User Note'}},
      {path: 'notes/:id', component: fromNotes.UserNoteDetailComponent, data: {breadcrumb: 'User Note Details'}},
      {path: 'notes/update/:id', component: fromNotes.UserNoteUpdateComponent, data: {breadcrumb: 'User Note Update'}},
      {path: 'user/:id/tasks', component: fromTasks.UserTaskListComponent},
      {path: 'tasks/create/:id', component: fromTasks.UserTaskCreateComponent},
      {path: 'tasks/:id', component: fromTasks.UserTaskDetailComponent},
      {path: 'tasks/update/:id', component: fromTasks.UserTaskUpdateComponent},
    ]},
  {path:'jobs', canActivate: [authGuard], children: [
      {path: '', component: fromJobs.JobListComponent, data: {breadcrumb: 'Jobs'}},
      {path:'create', component: fromJobs.JobCreateComponent, data: {breadcrumb: 'Create Job'}},
      {path:':id', component: fromJobs.JobDetailComponent, data: {breadcrumb: 'Job Details'}},
      {path:'update/:id', component: fromJobs.JobUpdateComponent, data: {breadcrumb: 'Update Job'}},
      {path: 'job/:id', component: fromNotes.JobNoteListComponent, data: {breadcrumb: 'Job Notes'}},
      {path:'notes/create/:id', component: fromNotes.JobNoteCreateComponent, data: {breadcrumb: 'Create Job Note'}},
      {path: 'notes/:id', component: fromNotes.JobNoteDetailComponent, data: {breadcrumb: 'Job Note Details'}},
      {path: 'notes/update/:id', component: fromNotes.JobNoteUpdateComponent, data: {breadcrumb: 'Job Note Update'}},
      {path: 'job/:id/tasks', component: fromTasks.JobTaskListComponent},
      {path: 'tasks/create/:id', component: fromTasks.JobTaskCreateComponent},
      {path: 'tasks/:id', component: fromTasks.JobTaskDetailComponent},
      {path: 'tasks/update/:id', component: fromTasks.JobTaskUpdateComponent},
    ]},
  {path:'contacts', canActivate: [authGuard], children: [
      {path: '', component: fromContacts.ContactListComponent, data: {breadcrumb: 'Contacts'}},
      {path:'create', component: fromContacts.ContactCreateComponent, data: {breadcrumb: 'Create Contact'}},
      {path:':id', component: fromContacts.ContactDetailComponent, data: {breadcrumb: 'Contact Details'}},
      {path:'update/:id', component: fromContacts.ContactUpdateComponent, data: {breadcrumb: 'Update Contact'}},
      {path: 'contact/:id', component: fromNotes.ContactNoteListComponent, data: {breadcrumb: 'Contact Notes'}},
      {path:'notes/create/:id', component: fromNotes.ContactNoteCreateComponent, data: {breadcrumb: 'Create Contact Note'}},
      {path: 'notes/:id', component: fromNotes.CampaignNoteDetailComponent, data: {breadcrumb: 'Contact Note Details'}},
      {path: 'notes/update/:id', component: fromNotes.ContactNoteUpdateComponent, data: {breadcrumb: 'Contact Note Update'}},
      {path: 'contact/:id/tasks', component: fromTasks.ContactTaskListComponent},
      {path: 'tasks/create/:id', component: fromTasks.ContactTaskCreateComponent},
      {path: 'tasks/:id', component: fromTasks.ContactTaskDetailComponent},
      {path: 'tasks/update/:id', component: fromTasks.ContactTaskUpdateComponent},
    ]},
  {path:'notes', canActivate: [authGuard], children: [
      {path: '', component: fromNotes.NoteListComponent, data: {breadcrumb: 'Notes'}},
      {path:'create', component: fromNotes.NoteCreateComponent, data: {breadcrumb: 'Create Note'}},
      {path:':id', component: fromNotes.NoteDetailComponent, data: {breadcrumb: 'Note Details'}},
      {path:'update/:id', component: fromNotes.NoteUpdateComponent, data: {breadcrumb: 'Update Note'}},
    ]},
  {path:'tasks', canActivate: [authGuard], children: [
      {path: '', component: fromTasks.TaskListComponent, data: {breadcrumb: 'Tasks'}},
      {path:'create', component: fromTasks.TaskCreateComponent, data: {breadcrumb: 'Create Task'}},
      {path:':id', component: fromTasks.TaskDetailComponent, data: {breadcrumb: 'Task Details'}},
      {path:'update/:id', component: fromTasks.TaskUpdateComponent, data: {breadcrumb: 'Update Task'}},
    ]},
  {path:'campaigns', canActivate: [authGuard], children: [
      {path: '', component: fromCampaigns.CampaignListComponent},
      {path:'create', component: fromCampaigns.CampaignCreateComponent},
      {path:':id', component: fromCampaigns.CampaignDetailComponent},
      {path:'update/:id', component: fromCampaigns.CampaignUpdateComponent},
      {path: 'campaign/:id/notes', component: fromNotes.CampaignNoteListComponent},
      {path:'notes/create/:id', component: fromNotes.CampaignNoteCreateComponent},
      {path: 'notes/:id', component: fromNotes.CampaignNoteDetailComponent},
      {path: 'notes/update/:id', component: fromNotes.CampaignNoteUpdateComponent},
      {path: 'campaign/:id/tasks', component: fromTasks.CampaignTaskListComponent},
      {path: 'tasks/create/:id', component: fromTasks.CampaignTaskCreateComponent},
      {path: 'tasks/:id', component: fromTasks.CampaignTaskDetailComponent},
      {path: 'tasks/update/:id', component: fromTasks.CampaignTaskUpdateComponent},
    ]},
  {path:'companies', canActivate: [authGuard], children: [
      {path: '', component: fromCompanies.CompanyListComponent},
      {path:'create', component: fromCompanies.CompanyCreateComponent},
      {path:':id', component: fromCompanies.CompanyDetailComponent},
      {path:'update/:id', component: fromCompanies.CompanyUpdateComponent},
      {path: 'company/:id/notes', component: fromNotes.CompanyNoteListComponent},
      {path:'notes/create/:id', component: fromNotes.CompanyNoteCreateComponent},
      {path: 'notes/:id', component: fromNotes.CompanyNoteDetailComponent},
      {path: 'notes/update/:id', component: fromNotes.CompanyNoteUpdateComponent},
      {path: 'company/:id/tasks', component: fromTasks.CompanyTaskListComponent},
      {path: 'tasks/create/:id', component: fromTasks.CompanyTaskCreateComponent},
      {path: 'tasks/:id', component: fromTasks.CompanyTaskDetailComponent},
      {path: 'tasks/update/:id', component: fromTasks.CompanyTaskUpdateComponent},
    ]},
];
@NgModule({
  declarations: [
    AppComponent,
    ...fromUsers.components,
    ...fromJobs.components,
    ...fromBlog.components,
    ...fromLayout.components,
    ...fromPages.components,
    ...fromContacts.components,
    ...fromCampaigns.components,
    ...fromCompanies.components,
    ...fromNotes.components,
    ...fromTasks.components,
    ...fromPipes.pipes,
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    RouterModule.forRoot(routes),
    ReactiveFormsModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot({
      timeOut: 5000,
      positionClass: 'toast-top-center',
      preventDuplicates: false,
      progressBar: true,
      progressAnimation: 'increasing',
      closeButton: true,
      iconClasses: {
        error: 'toast-error',
        success: 'toast-success',
        info: 'toast-info',
        warning: 'toast-warning',
      }
    }),
    QuillModule.forRoot()
  ],
  exports: [RouterModule],
  providers: [
    ...fromServices.services,
    ...fromInterceptors.interceptors,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: fromInterceptors.AuthInterceptor,
      multi: true,
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
