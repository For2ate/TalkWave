import { RootState } from "App/Store/MainStore";

export const selectAllChats = (state: RootState) => state.chats.chats;
export const selectChatsLoading = (state: RootState) => state.chats.loading;
export const selectChatsError = (state: RootState) => state.chats.error;
export const selectChatById = (chatId: string) => (state: RootState) =>
  state.chats.chats.find(chat => chat.id === chatId);