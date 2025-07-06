import { NgModule } from '@angular/core';
import {RouterModule, Routes} from "@angular/router";
import * as fromPages from "./components/pages";
import * as fromUsers from "./components/users";
import {authGuard} from "./utils/guards/auth.guard";
import * as fromNotes from "./components/notes";
import * as fromTasks from "./components/tasks";
import * as fromJobs from "./components/jobs";
import * as fromContacts from "./components/contacts";
import * as fromCampaigns from "./components/campaigns";
import * as fromCompanies from "./components/company";
import * as fromMessages from "./components/communication";
import * as fromBlackboard from "./components/blackboard";

export const routes: Routes = [
  {path: '', children: [
      {path: '', component: fromPages.HomeComponent, pathMatch: 'full'},
      {path: 'about', component: fromPages.AboutComponent},
      {path: 'contact', component: fromPages.ContactComponent},
      {path:'privacy-policy', component: fromPages.PrivacyPolicyComponent},
      {path:'terms-and-conditions', component: fromPages.TermsAndConditionsComponent},
      {path:'faq', component: fromPages.FaqComponent},
    ]},
  {path: 'account', children: [
      {path: '', component: fromUsers.LoginComponent},
      {path:'register-page', component: fromUsers.RegisterComponent},
    ]},
  {path: 'users', canActivate: [authGuard], children: [
      {path: '', component: fromUsers.UserListComponent},
      {path: ':id', component: fromUsers.UserDetailComponent},
      {path: 'update/:id', component: fromUsers.UserUpdateComponent},
      {path: 'roles', component: fromUsers.RoleListComponent},
      {path: 'roles/:id', component: fromUsers.RoleDetailComponent},
      {path: 'roles/update/:id', component: fromUsers.RoleUpdateComponent},
      {path: 'roles/create', component: fromUsers.RoleCreateComponent},
      {path: 'user/:id/notes', component: fromNotes.UserNoteListComponent},
      {path:'notes/create/:id', component: fromNotes.UserNoteCreateComponent},
      {path: 'notes/:id', component: fromNotes.UserNoteDetailComponent},
      {path: 'notes/update/:id', component: fromNotes.UserNoteUpdateComponent},
      {path: 'user/:id/tasks', component: fromTasks.UserTaskListComponent},
      {path: 'tasks/create/:id', component: fromTasks.UserTaskCreateComponent},
      {path: 'tasks/:id', component: fromTasks.UserTaskDetailComponent},
      {path: 'tasks/update/:id', component: fromTasks.UserTaskUpdateComponent},
    ]},
  {path:'jobs', canActivate: [authGuard], children: [
      {path: '', component: fromJobs.JobListComponent},
      {path:'create', component: fromJobs.JobCreateComponent},
      {path:':id', component: fromJobs.JobDetailComponent},
      {path:'update/:id', component: fromJobs.JobUpdateComponent},
      {path: 'job/:id/notes', component: fromNotes.JobNoteListComponent},
      {path:'notes/create/:id', component: fromNotes.JobNoteCreateComponent},
      {path: 'notes/:id', component: fromNotes.JobNoteDetailComponent},
      {path: 'notes/update/:id', component: fromNotes.JobNoteUpdateComponent},
      {path: 'job/:id/tasks', component: fromTasks.JobTaskListComponent},
      {path: 'tasks/create/:id', component: fromTasks.JobTaskCreateComponent},
      {path: 'tasks/:id', component: fromTasks.JobTaskDetailComponent},
      {path: 'tasks/update/:id', component: fromTasks.JobTaskUpdateComponent},
    ]},
  {path:'contacts', canActivate: [authGuard], children: [
      {path: '', component: fromContacts.ContactListComponent},
      {path:'create', component: fromContacts.ContactCreateComponent},
      {path:':id', component: fromContacts.ContactDetailComponent},
      {path:'update/:id', component: fromContacts.ContactUpdateComponent},
      {path: 'contact/:id/notes', component: fromNotes.ContactNoteListComponent},
      {path:'notes/create/:id', component: fromNotes.ContactNoteCreateComponent},
      {path: 'notes/:id', component: fromNotes.CampaignNoteDetailComponent},
      {path: 'notes/update/:id', component: fromNotes.ContactNoteUpdateComponent},
      {path: 'contact/:id/tasks', component: fromTasks.ContactTaskListComponent},
      {path: 'tasks/create/:id', component: fromTasks.ContactTaskCreateComponent},
      {path: 'tasks/:id', component: fromTasks.ContactTaskDetailComponent},
      {path: 'tasks/update/:id', component: fromTasks.ContactTaskUpdateComponent},
    ]},
  {path:'notes', canActivate: [authGuard], children: [
      {path: '', component: fromNotes.NoteListComponent},
      {path:'create', component: fromNotes.NoteCreateComponent},
      {path:':id', component: fromNotes.NoteDetailComponent},
      {path:'update/:id', component: fromNotes.NoteUpdateComponent},
    ]},
  {path:'tasks', canActivate: [authGuard], children: [
      {path: '', component: fromTasks.TaskListComponent},
      {path:'create', component: fromTasks.TaskCreateComponent},
      {path:':id', component: fromTasks.TaskDetailComponent},
      {path:'update/:id', component: fromTasks.TaskUpdateComponent},
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
  {path: 'communication', canActivate: [authGuard], children: [
      {path: 'user/:id', component: fromMessages.MessagingComponent},
      {path: 'create', component: fromMessages.MessageCreateComponent},
    ]},
  {path: 'blackboard', canActivate: [authGuard], children: [
      {path: '', component: fromBlackboard.HomeComponent},
      {path: 'about', component: fromBlackboard.AboutComponent},
      {path: 'admin', children: [
          {path: '', component: fromBlackboard.AdminDashboardComponent}
        ]},
      {path: 'schedules', children: [
          {path: '', component: fromBlackboard.ScheduleListComponent},
          {path: ':id', component: fromBlackboard.ScheduleDetailComponent},
          {path: 'create', component: fromBlackboard.ScheduleCreateComponent},
          {path: 'update/:id', component: fromBlackboard.ScheduleUpdateComponent},
        ]},
      {path: 'teachers', children: [
          {path: '', component: fromBlackboard.TeacherListComponent},
          {path: ':id', component: fromBlackboard.TeacherDetailComponent},
          {path: 'create', component: fromBlackboard.TeacherCreateComponent},
          {path: 'update/:id/', component: fromBlackboard.TeacherUpdateComponent},
        ]},
      {path: 'students', children: [
          {path: '', component: fromBlackboard.StudentListComponent},
          {path: ':id', component: fromBlackboard.StudentDetailComponent},
          {path: 'create', component: fromBlackboard.StudentCreateComponent},
          {path: 'update/:id', component: fromBlackboard.StudentDetailComponent},
        ]},
      {path: 'courses', children: [
          {path: '', component: fromBlackboard.CourseListComponent},
          {path: ':id', component: fromBlackboard.CourseDetailComponent},
          {path: 'create', component: fromBlackboard.CourseCreateComponent},
          {path: 'update/:id', component: fromBlackboard.CourseUpdateComponent},
          {path: 'modules', children: [
              {path: '', component: fromBlackboard.ModuleListComponent},
              {path: ':id', component: fromBlackboard.ModuleDetailComponent},
              {path: 'create', component: fromBlackboard.ModuleCreateComponent},
              {path: 'update/:id', component: fromBlackboard.ModuleUpdateComponent},
              {path: 'assignments', children: [
                  {path: '', component: fromBlackboard.AssignmentListComponent},
                  {path: ':id', component: fromBlackboard.AssignmentDetailComponent},
                  {path: 'create', component: fromBlackboard.AssignmentCreateComponent},
                  {path: 'update/:id', component: fromBlackboard.AssignmentUpdateComponent},
                ]}
            ]}
        ]}

    ]}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule { }
