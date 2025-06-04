import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Note, UserNotes} from "../models/note";
import {forkJoin, Observable} from "rxjs";
import {environment} from "../../environments/environment";

@Injectable({
  providedIn: 'root'
})
export class NotesService {
  private notesApiUrl = `${environment.apiUrl}/Main/Notes`;
  private userNotesUrl = `${environment.apiUrl}/Identity/User`;
  private jobNotesUrl = `${environment.apiUrl}/Main/Job`;

  constructor(private http: HttpClient) { }

  getNotes(): Observable<Note[]> {
    return this.http.get<Note[]>(this.notesApiUrl);
  }
  getUserNotes(userId: string): Observable<UserNotes[]> {
    const url = `${this.notesApiUrl}/userNotes/${userId}`;
    return this.http.get<UserNotes[]>(url);
  }
  getUserNotesWithProfile(userId: string): Observable<any> {
    return forkJoin({
      userNotes: this.getUserNotes(userId),
      profile: this.http.get(`${this.userNotesUrl}/${userId}`)
    });
  }
  createUserNote(userId: string, note: Note): Observable<UserNotes> {
    const url = `${this.notesApiUrl}/userNotes/${userId}`;
    const payload = {note};
    return this.http.post<UserNotes>(url, payload);
  }
  updateUserNote(userId: string, note: Note): Observable<UserNotes> {
    const url = `${this.notesApiUrl}/userNotes/${userId}`;
    const payload = {note};
    return this.http.put<UserNotes>(url, payload);
  }
}
