import {NgModule} from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import {HTTP_INTERCEPTORS, HttpClientModule} from "@angular/common/http";
import { AppComponent } from './app.component';
import {RouterModule} from "@angular/router";
import * as fromServices from './services/index';
import * as fromInterceptors from './utils/interceptors/index';
import {ReactiveFormsModule} from "@angular/forms";
import {BrowserAnimationsModule} from "@angular/platform-browser/animations";
import {ToastrModule} from "ngx-toastr";
import {QuillModule} from "ngx-quill";
import {AppRoutingModule} from "./app-routing.module";
import * as fromUsers from "./components/users";
import * as fromJobs from "./components/jobs";
import * as fromBlog from "./components/blog";
import * as fromLayout from "./components/layout";
import * as fromPages from "./components/pages";
import * as fromContacts from "./components/contacts";
import * as fromCampaigns from "./components/campaigns";
import * as fromCompanies from "./components/company";
import * as fromBlackboard from "./components/blackboard";
import * as fromNotes from "./components/notes";
import * as fromTasks from "./components/tasks";
import * as fromMessages from "./components/communication";
import * as fromPipes from "./utils/pipes";


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
    ...fromBlackboard.components,
    ...fromNotes.components,
    ...fromTasks.components,
    ...fromMessages.components,
    ...fromPipes.pipes,
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
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
