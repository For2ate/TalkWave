import { RootState } from "App/Store/MainStore";

export const selectMessagesByChatId = (chatId:string) => (state : RootState) => state.messages.entities[chatId];