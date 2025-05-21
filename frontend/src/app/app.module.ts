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
import {ReactiveFormsModule} from "@angular/forms";
import {BrowserAnimationsModule} from "@angular/platform-browser/animations";
import {ToastrModule} from "ngx-toastr";
import { MainNavComponent } from './components/layout/main-nav/main-nav.component';
export const routes: Routes = [
  {path: '', children: [
      {path: '', component: fromPages.HomeComponent, pathMatch: 'full'},
      {path: 'about', component: fromPages.AboutComponent},
      {path: 'contact', component: fromPages.ContactComponent},
      {path:'privacy-policy', component: fromPages.PrivacyPolicyComponent},
      {path:'terms-and-conditions', component: fromPages.TermsAndConditionsComponent},
      {path:'faq', component: fromPages.FaqComponent},
    ]},

  {path: 'users', children: [
      {path: '', component: fromUsers.UserListComponent},
      {path:'register-page', component: fromUsers.RegisterComponent},
      {path:'login-page', component: fromUsers.LoginComponent},
      {path: ':id', component: fromUsers.UserDetailComponent},
      {path: 'update/:id', component: fromUsers.UserUpdateComponent},
    ]},
  {path:'jobs', children: [
      {path: '', component: fromJobs.JobListComponent},
      {path:'create', component: fromJobs.JobCreateComponent},
      {path:':id', component: fromJobs.JobDetailComponent},
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
    MainNavComponent,
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
    })
  ],
  exports: [RouterModule],
  providers: [...fromServices.services],
  bootstrap: [AppComponent]
})
export class AppModule { }
