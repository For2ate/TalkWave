import { MessageModel } from "Entities/Messages/MessageTypes";

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
    lastMessage: MessageModel | null;
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