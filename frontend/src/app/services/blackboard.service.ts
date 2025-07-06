import { Injectable } from '@angular/core';
import {environment} from "../../environments/environment";
import {HttpClient} from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class BlackboardService {
  private apiUrl = `${environment.apiUrl}/Blackboard/Dashboard`;
  private syllabusUrl = `${environment.apiUrl}/Blackboard/Syllabus`;
  private courseUrl = `${environment.apiUrl}/Blackboard/Course`;
  private teacherUrl = `${environment.apiUrl}/Blackboard/Teacher`;
  private studentUrl = `${environment.apiUrl}/Blackboard/Student`;
  private moduleUrl = `${environment.apiUrl}/Blackboard/Module`;
  private assignmentUrl = `${environment.apiUrl}/Blackboard/Assignment`;
  constructor(private http: HttpClient) { }
}
