import { RootState } from "App/Store/MainStore";

export const selectMessagesByChatId = (chatId:string) => (state:RootState) => state.messages.messages[chatId]

export const selectHasMoreByChatId = (chatId:string) => (state:RootState) => state.messages.messages[chatId]