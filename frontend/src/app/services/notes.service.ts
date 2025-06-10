import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {ContactNotes, JobNotes, Note, UserNotes} from "../models/note";
import {forkJoin, Observable} from "rxjs";
import {environment} from "../../environments/environment";
import {Job} from "../models/job";

@Injectable({
  providedIn: 'root'
})
export class NotesService {
  private notesApiUrl = `${environment.apiUrl}/Main/Notes`;
  private userNotesUrl = `${environment.apiUrl}/Identity/User`;
  private jobNotesUrl = `${environment.apiUrl}/Main/Job`;
  private contactNotesUrl = `${environment.apiUrl}/Main/Contact`;
  private campaignNotesUrl = `${environment.apiUrl}/Main/Campaign`;

  constructor(private http: HttpClient) { }

  getNotes(): Observable<Note[]> {
    return this.http.get<Note[]>(this.notesApiUrl);
  }
  getNoteById(id: number): Observable<Note> {
    return this.http.get<Note>(`${this.notesApiUrl}/${id}`);
  }
  createNote(note: Note): Observable<Note> {
    return this.http.post<Note>(this.notesApiUrl, note);
  }
  updateNote(id: number, note: Note): Observable<Note> {
    return this.http.put<Note>(`${this.notesApiUrl}/${note}`, note);
  }
  /*User Notes*/
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
  /*Job Notes*/
  getJobNotes(noteId: number): Observable<JobNotes[]> {
    const url = `${this.jobNotesUrl}/notes/${noteId}`;
    return this.http.get<JobNotes[]>(url);
  }
  getJobNotesByJobId(jobId: number): Observable<JobNotes[]> {
    const url = `${this.jobNotesUrl}/job/${jobId}`;
    return this.http.get<JobNotes[]>(url);
  }
  getJobNotesWithProfile(jobId: number, noteId: number): Observable<any> {
    return forkJoin({
      jobNotes: this.getJobNotes(noteId),
      profile: this.http.get(`${this.jobNotesUrl}/${jobId}`)
    });
  }
  createJobNote(jobNote: JobNotes): Observable<any> {
    const url = `${this.jobNotesUrl}/notes/${jobNote.jobId}`;
    return this.http.post<any>(url, jobNote);
  }

  updateJobNote(noteId: number, note: Note): Observable<JobNotes> {
    const url = `${this.jobNotesUrl}/notes/${noteId}`;
    const payload = {note};
    return this.http.put<JobNotes>(url, payload);
  }
  deleteJobNote(noteId: number): Observable<void> {
    const url = `${this.jobNotesUrl}/notes/${noteId}`;
    return this.http.delete<void>(url);
  }
  /*END JOBS*/
  /*Start Contacts*/
  getContactNotesByContactId(contactId: number): Observable<ContactNotes[]> {
    const url = `${this.contactNotesUrl}/contact/${contactId}/notes`;
    return this.http.get<ContactNotes[]>(url);
  }
  getContactNoteById(noteId: number): Observable<ContactNotes> {
    const url = `${this.contactNotesUrl}/notes/${noteId}`;
    return this.http.get<ContactNotes>(url);
  }
  createContactNote(contactNote: ContactNotes): Observable<any> {
    const url = `${this.contactNotesUrl}/notes/${contactNote.id}`;
    return this.http.post<ContactNotes>(url, contactNote);
  }
  /*END CONTACTS*/
}
