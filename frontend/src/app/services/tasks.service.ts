import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {environment} from "../../environments/environment";
import {Observable} from "rxjs";
import {JobNotes, Note} from "../models/note";
import {JobTasks} from "../models/tasks";

@Injectable({
  providedIn: 'root'
})
export class TasksService {
  private tasksBaseUrl = `${environment.apiUrl}/Main/Tasks`;
  private userTasksUrl = `${environment.apiUrl}/Identity/User`;
  private jobTasksUrl = `${environment.apiUrl}/Main/Job`;
  private campaignTasksUrl = `${environment.apiUrl}/Main/Campaign`;
  constructor(private http: HttpClient) { }

  /*Job Tasks*/
  getJobTasksByJobId(jobId: number): Observable<JobTasks[]> {
    const url = `${this.jobTasksUrl}/tasks/${jobId}`;
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
}
