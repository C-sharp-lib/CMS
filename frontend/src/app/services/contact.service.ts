import { Injectable } from '@angular/core';
import {environment} from "../../environments/environment";
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {Contact} from "../models/contact";

@Injectable({
  providedIn: 'root'
})
export class ContactService {
  private apiUrl = `${environment.apiUrl}/Main/Contact`;
  contact: any;
  constructor(private http: HttpClient) {}

  getContacts(): Observable<Contact[]> {
    return this.http.get<Contact[]>(this.apiUrl);
  }

  getContactById(id: number): Observable<Contact> {
    return this.http.get<Contact>(`${this.apiUrl}/${id}`);
  }

  createContact(contact: FormData): Observable<any> {
    return this.http.post(this.apiUrl, contact);
  }

  updateContactById(id: number, contact: Contact): Observable<Contact> {
    return this.http.put<Contact>(`${this.apiUrl}/${id}`, contact);
  }

  deleteContactById(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }

  countContacts(): Observable<number> {
    return this.http.get<number>(`${this.apiUrl}/count`);
  }
  getContactImageUrl(relativePath: string): Observable<{ imageUrl: string }> {
    return this.http.get<{ imageUrl: string }>(
     `${this.apiUrl}/get-contact-image-path`,
      { params: { relativePath } }
    );
  }
}
