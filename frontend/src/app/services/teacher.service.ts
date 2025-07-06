import { Injectable } from '@angular/core';
import {environment} from "../../environments/environment";
import {HttpClient} from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class TeacherService {
  private apiUrl = `${environment.apiUrl}/Blackboard/Teacher`;
  private studentUrl = `${environment.apiUrl}/Blackboard/Student`;
  private coursesUrl = `${environment.apiUrl}/Blackboard/Course`;
  constructor(private http: HttpClient) { }
}
