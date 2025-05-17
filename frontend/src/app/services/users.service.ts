import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {Observable} from "rxjs";
import {User} from "../models/user";
import {environment} from "../../environments/environment";

@Injectable({
  providedIn: 'root'
})
export class UsersService {
  get headers(): HttpHeaders {
    return this._headers;
  }
  private baseUrl = `${environment.apiUrl}/Identity/User`;
  private _headers = new HttpHeaders({ 'Content-Type': 'application/json' });
  constructor(private http: HttpClient) {}

  register(user: {name: string, email: string, userName: string,
    address: string, city: string, state: string, zipCode: string,
    dateOfBirth: Date, password: string, confirmPassword: string}): Observable<any> {
    return this.http.post(`${this.baseUrl}/register`, user);
  }

  login(credentials: { email: string; password: string, rememberMe: boolean }): Observable<any> {
    return this.http.post<any>(`${this.baseUrl}/login`, credentials);
  }

  getUsers(): Observable<User[]> {
    return this.http.get<User[]>(this.baseUrl);
  }

  getUserById(id: string): Observable<User> {
    return this.http.get<User>(`${this.baseUrl}/${id}`);
  }

  updateUserById(id: string, user: Partial<User>): Observable<User> {
    return this.http.put<User>(`${this.baseUrl}/${id}`, user);
  }

  deleteUser(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`);
  }
}
