import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import {HttpClientModule} from "@angular/common/http";
import { AppComponent } from './app.component';
import {RouterModule, Routes} from "@angular/router";
import * as fromLayout from './components/layout/index';
import * as fromUsers from './components/users/index';
import * as fromJobs from './components/jobs/index';
import * as fromBlog from './components/blog/index';
import * as fromPages from './components/pages/index';
import * as fromServices from './services/index';



export const routes: Routes = [
  {path: '', component: fromPages.HomeComponent, children: [
      {path: '', component: fromPages.HomeComponent, pathMatch: 'full'},
      {path: 'about', component: fromPages.AboutComponent},
      {path: 'contact', component: fromPages.ContactComponent},
      {path:'privacy-policy', component: fromPages.PrivacyPolicyComponent},
      {path:'terms-and-conditions', component: fromPages.TermsAndConditionsComponent},
      {path:'faq', component: fromPages.FaqComponent},
    ]},

  {path: 'users', component: fromUsers.UserListComponent, children: [
      {path: '', component: fromUsers.UserListComponent, pathMatch: 'full'},
      {path:'register', component: fromUsers.RegisterComponent},
      {path:'login', component: fromUsers.LoginComponent},
      {path: ':id', component: fromUsers.UserDetailComponent},
      {path: 'update/:id', component: fromUsers.UserUpdateComponent},
    ]},
  {path:'jobs', component: fromJobs.JobListComponent, children: [
      {path: '', component: fromJobs.JobListComponent, pathMatch: 'full'},
      {path:':id', component: fromJobs.JobDetailComponent},
      {path:'create', component: fromJobs.JobCreateComponent},
      {path:'update/:id', component: fromJobs.JobUpdateComponent},
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
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    RouterModule.forRoot(routes)
  ],
  exports: [RouterModule],
  providers: [...fromServices.services],
  bootstrap: [AppComponent]
})
export class AppModule { }
