import { Message } from "./Message";

export interface ChatMember {
    id: string;
    role: number;
  }
  
  export interface Chat {
    id: string;
    name: string;
    isGroupChat: boolean;
    createdAt: string;
    createdBy: string;
    role: number;
    lastMessage: Message | null;
    chatMembers: ChatMember[];
  }
  
  export interface ApiChatResponse {
    id: string;
    name: string;
    isGroupChat: boolean;
    createdAt: string;
    createdBy: string;
    role: number;
    lastMessageId: string | null;
    chatMembers: ChatMember[];
  }