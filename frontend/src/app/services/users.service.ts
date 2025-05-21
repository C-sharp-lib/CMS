import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {Observable} from "rxjs";
import {User} from "../models/user";
import {environment} from "../../environments/environment";
import {Router} from '@angular/router';
import {jwtDecode} from 'jwt-decode';

@Injectable({
  providedIn: 'root'
})
export class UsersService {
  private currentUser: User | null = null;
  user: any;
  private baseUrl = `${environment.apiUrl}/Identity/User`;
  constructor(private http: HttpClient, private router: Router) {
    this.currentUser = JSON.parse(localStorage.getItem("currentUser") || 'null');
  }

  register(user: {name: string, email: string, userName: string,
    address: string, city: string, state: string, zipCode: string,
    dateOfBirth: Date, password: string, confirmPassword: string}): Observable<any> {
    return this.http.post(`${this.baseUrl}/register`, user);
  }

  login(credentials: { email: string; password: string, rememberMe: boolean }): Observable<any> {
    return this.http.post<any>(`${this.baseUrl}/login`, credentials);
  }

  logout() {
    this.clearToken();
    this.router.navigate(['/login-page']);
  }

  saveToken(token: string): void {
    localStorage.setItem('token', token);
  }

  // Remove the token (e.g., after logout)
  clearToken(): void {
    localStorage.removeItem('token');
  }

  // Retrieve the token
  getToken(): string | null {
    return localStorage.getItem('token');
  }
  getDecodedToken(): any {
    const token = this.getToken();
    if(!token) {
      return null;
    }
    return jwtDecode(token);
  }
  getUsers(): Observable<User[]> {
    return this.http.get<User[]>(this.baseUrl);
  }

  getUserById(id: string): Observable<User> {
    return this.http.get<User>(`${this.baseUrl}/${id}`);
  }

  updateUserById(id: string, user: User): Observable<User> {
    return this.http.put<User>(`${this.baseUrl}/${id}`, user);
  }

  deleteUser(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`);
  }


  getCurrentUser(): User | null {
    return this.currentUser;
  }

  setCurrentUser(user: User): void {
    this.currentUser = user;
    localStorage.setItem('currentUser', JSON.stringify(user));
  }
}
