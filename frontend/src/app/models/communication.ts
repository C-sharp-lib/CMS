import {User} from "./user";

export interface ConversationParticipant {
  id: number;
  userId: string;
  user: User;
  conversationId: number;
  conversation: Conversation;
}

export interface Conversation {
  id: number;
  title: string;
  participants: ConversationParticipant[];
  messages: Message[];
}

export interface Message {
  id?: number;
  conversationId: number;
  senderId: string;
  content: string;
  sentAt?: string;
  conversation: Conversation;
  sender: User;
}
