import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {environment} from "../../environments/environment";
import {Observable} from "rxjs";
import {CampaignTasks, ContactTasks, JobTasks, UserTasks, CompanyTasks} from "../models/tasks";

@Injectable({
  providedIn: 'root'
})
export class TasksService {
  private userTasksUrl = `${environment.apiUrl}/Identity/User`;
  private jobTasksUrl = `${environment.apiUrl}/Main/Job`;
  private campaignTasksUrl = `${environment.apiUrl}/Main/Campaign`;
  private contactTasksUrl = `${environment.apiUrl}/Main/Contact`;
  private companyTasksUrl = `${environment.apiUrl}/Main/Company`;
  constructor(private http: HttpClient) { }

  /*Job Tasks*/
  getJobTasksByJobId(jobId: number): Observable<JobTasks[]> {
    const url = `${this.jobTasksUrl}/job/${jobId}/tasks`;
    return this.http.get<JobTasks[]>(url);
  }
  createJobTask(jobTasks: JobTasks): Observable<any> {
    const url = `${this.jobTasksUrl}/task/${jobTasks.jobId}`;
    return this.http.post<any>(url, jobTasks);
  }
  updateJobTask(taskId: number, jobTasks: JobTasks): Observable<any> {
    const url = `${this.jobTasksUrl}/task/${taskId}`;
    return this.http.put<JobTasks>(url, jobTasks);
  }
  /*End Job Tasks*/

  /*Campaign Tasks*/
  getCampaignTasksByCampaignId(campaignId: number): Observable<CampaignTasks[]> {
    const url = `${this.campaignTasksUrl}/campaign/${campaignId}/tasks`;
    return this.http.get<CampaignTasks[]>(url);
  }
  createCampaignTask(campaignTasks: CampaignTasks): Observable<any> {
    const url = `${this.campaignTasksUrl}/tasks/${campaignTasks.campaignId}`;
    return this.http.post<any>(url, campaignTasks);
  }
  updateCampaignTask(taskId: number, campaignTasks: CampaignTasks): Observable<any> {
    const url = `${this.campaignTasksUrl}/tasks/${taskId}`;
    return this.http.put<CampaignTasks>(url, campaignTasks);
  }
  /*End Campaign Tasks*/

  /*User Tasks*/
  getUserTasksByUserId(userId: number): Observable<UserTasks[]> {
    const url = `${this.userTasksUrl}/user/${userId}/tasks`;
    return this.http.get<UserTasks[]>(url);
  }
  createUserTask(userTasks: UserTasks): Observable<any> {
    const url = `${this.userTasksUrl}/tasks/${userTasks.userId}`;
    return this.http.post<any>(url, userTasks);
  }
  updateUserTask(taskId: number, userTasks: UserTasks): Observable<any> {
    const url = `${this.userTasksUrl}/tasks/${taskId}`;
    return this.http.put<UserTasks>(url, userTasks);
  }
  /*End User Tasks*/

  /*Company Tasks*/
  getCompanyTasksByCompanyId(companyId: number): Observable<CompanyTasks[]> {
    const url = `${this.companyTasksUrl}/company/${companyId}/tasks`;
    return this.http.get<CompanyTasks[]>(url);
  }
  createCompanyTask(companyTasks: CompanyTasks): Observable<any> {
    const url = `${this.companyTasksUrl}/tasks/${companyTasks.companyId}`;
    return this.http.post<any>(url, companyTasks);
  }
  updateCompanyTask(taskId: number, companyTasks: CompanyTasks): Observable<any> {
    const url = `${this.companyTasksUrl}/tasks/${taskId}`;
    return this.http.put<CompanyTasks>(url, companyTasks);
  }
  /*End Company Tasks*/

  /*Contact Tasks*/
  getContactTasksByContactId(contactId: number): Observable<ContactTasks[]> {
    const url = `${this.contactTasksUrl}/contact/${contactId}/tasks`;
    return this.http.get<ContactTasks[]>(url);
  }
  createContactTask(contactTasks: ContactTasks): Observable<any> {
    const url = `${this.contactTasksUrl}/tasks/${contactTasks.contactId}`;
    return this.http.post<any>(url, contactTasks);
  }
  updateContactTask(taskId: number, contactTasks: ContactTasks): Observable<any> {
    const url = `${this.contactTasksUrl}/tasks/${taskId}`;
    return this.http.put<ContactTasks>(url, contactTasks);
  }
  /*End Contact Tasks*/
}
