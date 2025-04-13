import { RootState } from "App/Store/MainStore";

export const selectAllChats = (state: RootState) => Object.values(state.chats.chats);

export const selectCurrentChat = (state: RootState) => 
    state.chats.selectedChatId ? state.chats.chats[state.chats.selectedChatId] : null;

export const selectChatById = (chatId: string) => (state: RootState) => 
    state.chats.chats[chatId];
  
  export const selectChatsStatus = (state: RootState) => 
    state.chats.status;