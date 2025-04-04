export interface Chat {
    id: string;
    name: string;
    lastMessage?: string;
    isGroup?: boolean;
    createdAt?: string;
    members?: string[];
}