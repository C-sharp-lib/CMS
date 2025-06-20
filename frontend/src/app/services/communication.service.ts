import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {Conversation, Message} from "../models/communication";
import {environment} from "../../environments/environment";

@Injectable({
  providedIn: 'root'
})
export class CommunicationService {
  private apiUrl = `${environment.apiUrl}/Communication/Conversation`;

  constructor(private http: HttpClient) {
  }

  getUserConversations(userId: string): Observable<Conversation[]> {
    return this.http.get<Conversation[]>(`${this.apiUrl}/user/${userId}`);
  }

  getMessages(conversationId: number): Observable<Message[]> {
    return this.http.get<Message[]>(`${this.apiUrl}/${conversationId}/messages`);
  }

  sendMessage(conversationId: number, message: Omit<Message, 'sentAt'>): Observable<Message> {
    return this.http.post<Message>(`${this.apiUrl}/${conversationId}/messages`, message);
  }
}
