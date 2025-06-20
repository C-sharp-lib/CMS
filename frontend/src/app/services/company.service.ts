import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {environment} from "../../environments/environment";
import {Observable} from "rxjs";
import {Company} from "../models/company";

@Injectable({
  providedIn: 'root'
})
export class CompanyService {
  private apiUrl = `${environment.apiUrl}/Main/Company`;
  company: any;
  constructor(private http: HttpClient) { }
  getAllCompanies(): Observable<Company[]> {
    return this.http.get<Company[]>(this.apiUrl, {
      withCredentials: true,
      headers: {'Content-Type': 'application/json', 'Accept': 'application/json'}
    })
  }
  getCompanyById(id: number) {
    return this.http.get<Company>(`${this.apiUrl}/${id}`);
  }
}
