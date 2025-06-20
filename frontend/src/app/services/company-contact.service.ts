import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {environment} from "../../environments/environment";
import {CompanyContacts} from "../models/company-contacts";
import {Observable} from "rxjs";
import {Contact} from "../models/contact";

@Injectable({
  providedIn: 'root'
})
export class CompanyContactService {
  private apiUrl = `${environment.apiUrl}/Main/CompanyContact`;
  constructor(private http: HttpClient) { }


  getCompanyContacts() {
    return this.http.get<CompanyContacts[]>(this.apiUrl)
  }
  getCompanyContactByCompanyId(companyId: number): Observable<CompanyContacts[]> {
    return this.http.get<CompanyContacts[]>(`${this.apiUrl}/company/${companyId}`);
  }
  getCompanyContactByContactId(contactId: number): Observable<CompanyContacts[]> {
    return this.http.get<CompanyContacts[]>(`${this.apiUrl}/contact/${contactId}`);
  }
  getCompanyContactById(id: number): Observable<CompanyContacts> {
    return this.http.get<CompanyContacts>(`${this.apiUrl}/${id}`);
  }
  createCompanyContact(companyContacts: CompanyContacts): Observable<CompanyContacts> {
    return this.http.post<CompanyContacts>(`${this.apiUrl}`, companyContacts);
  }
  updateCompanyContact(id: number, companyContacts: CompanyContacts): Observable<CompanyContacts> {
    return this.http.put<CompanyContacts>(`${this.apiUrl}/${id}`, companyContacts);
  }
  deleteCompanyContact(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
  countCompanyContacts(): Observable<number> {
    return this.http.get<number>(`${this.apiUrl}/companyContact/count`);
  }
}
