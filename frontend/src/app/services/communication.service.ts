import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {map, Observable} from "rxjs";
import {Conversation, ConversationParticipant, Message} from "../models/communication";
import {environment} from "../../environments/environment";

@Injectable({
  providedIn: 'root'
})
export class CommunicationService {
  private apiUrl = `${environment.apiUrl}/Communication/Conversation`;
  private readonly BLOB_RESPONSE = { responseType: 'blob' as const };
  constructor(private http: HttpClient) {
  }

  getUserConversations(userId: string): Observable<Conversation[]> {
    return this.http.get<Conversation[]>(`${this.apiUrl}/conversations/${userId}`);
  }
  getConversationById(conversationId: number): Observable<Conversation> {
    return this.http.get<Conversation>(`${this.apiUrl}/conversation/${conversationId}`);
  }
  getConversationParticipants(conversationId: number): Observable<ConversationParticipant[]> {
    return this.http.get<ConversationParticipant[]>(`${this.apiUrl}/conversation-participants/${conversationId}`);
  }

  getMessages(conversationId: number): Observable<Message[]> {
    return this.http.get<Message[]>(`${this.apiUrl}/conversation/${conversationId}/messages`);
  }

  sendMessage(conversationId: number, message: { content: string, senderId: string }): Observable<Message> {
    return this.http.post<Message>(`${this.apiUrl}/conversation/${conversationId}/messages`, message);
  }
  createConversation(data: { title?: string, userIds: string[] }): Observable<Conversation> {
    return this.http.post<Conversation>(`${this.apiUrl}`, data);
  }
  getParticipantImageUrl(relativePath: string): Observable<{ imageUrl: string }> {
    return this.http.get<{ imageUrl: string }>(
      `${this.apiUrl}/get-user-image-path`,
      { params: { relativePath } }
    );
  }
  getConversationCount(userId: string){
    return this.http.get<number>(`${this.apiUrl}/conversations/${userId}/count`);
  }
  getMessageCount(conversationId: number){
    return this.http.get<number>(`${this.apiUrl}/conversation/${conversationId}/messages/count`);
  }
  getConversationParticipantCount(conversationId: number){
    return this.http.get<number>(`${this.apiUrl}/conversation/${conversationId}/conversation-participants/count`);
  }
  deleteConversation(conversationId: number){
    return this.http.delete<void>(`${this.apiUrl}/conversation/${conversationId}/delete-conversation`);
  }
  deleteMessage(conversationId: number, userId: string, messageId: number){
    return this.http.delete<void>(`${this.apiUrl}/${conversationId}/conversations/${userId}/conversation/${conversationId}/delete-message/${messageId}`);
  }
  deleteConversationParticipant(conversationId: number, conversationParticipantId: number){
    return this.http.delete<void>(`${this.apiUrl}/conversation/${conversationId}/participant/${conversationParticipantId}/delete`);
  }
}
