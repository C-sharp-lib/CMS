import { Injectable } from '@angular/core';
import {environment} from "../../environments/environment";
import {HttpClient} from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class StudentService {
  private apiUrl = `${environment.apiUrl}/Blackboard/Student`;
  private teacherUrl = `${environment.apiUrl}/Blackboard/Teacher`;
  constructor(private http: HttpClient) { }
}
