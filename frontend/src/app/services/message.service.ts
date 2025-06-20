import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {environment} from "../../environments/environment";
import {Message} from "../models/message";
import {Observable} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class MessageService {
  private baseUrl = `${environment.apiUrl}/Communication/Message`;
  constructor(private http: HttpClient) { }

  sendMessage(message: Message): Observable<Message> {
    return this.http.post<Message>(this.baseUrl, message);
  }

  getMessages(senderId: string, receiverId: string): Observable<Message[]> {
    return this.http.get<Message[]>(`${this.baseUrl}/${senderId}/${receiverId}`);
  }
}
