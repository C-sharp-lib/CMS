import { Injectable } from '@angular/core';
import {Observable} from "rxjs";
import {User} from "../models/user";
import {HttpClient} from "@angular/common/http";
import {Router} from "@angular/router";
import {environment} from "../../environments/environment";
import {Job} from "../models/job";

@Injectable({
  providedIn: 'root'
})
export class JobsService {
  job: any;
  private baseUrl = `${environment.apiUrl}/Main/Job`;
  constructor(private http: HttpClient, private router: Router) { }
  getJobs(): Observable<Job[]> {
    return this.http.get<Job[]>(this.baseUrl);
  }

  getJobById(id: number): Observable<Job> {
    return this.http.get<Job>(`${this.baseUrl}/${id}`);
  }

  updateJobById(id: number, job: Job): Observable<Job> {
    return this.http.put<Job>(`${this.baseUrl}/${id}`, job);
  }
  createJob(job: Partial<Job>): Observable<Job> {
    return this.http.post<Job>(this.baseUrl, job);
  }
  deleteJob(id: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`);
  }
}
