import { createAsyncThunk } from "@reduxjs/toolkit";
import { ChatApiEndpoints } from "../../Api/ChatApiEndpoints";
import { MessageApiEndpoints } from "../../Api/MessageApiEndpoints";
import { ApiChatResponse, Chat } from "../Types/Chat";

export const fetchChats = createAsyncThunk<
  Chat[], // Return type
  void, // Argument type
  { rejectValue: string }
>("chats/fetchChats", async (_, { rejectWithValue }) => {
  try {

    const userId = localStorage.getItem("userId");
    if (!userId) throw new Error("User ID not found");

    const chatsResponse = await ChatApiEndpoints.getChats(userId);
    if (!chatsResponse) throw new Error("No chats received");

    // 2. Для каждого чата получаем последнее сообщение
    const chatsWithMessages = await Promise.all(
      chatsResponse.map(async (chat: ApiChatResponse) => {

        let lastMessage = null;
        
        if (chat.lastMessageId) {
          lastMessage = await MessageApiEndpoints.getMessageById(chat.lastMessageId);
        }

        return {
          id: chat.id,
          name: chat.name,
          isGroupChat: chat.isGroupChat,
          createdAt: chat.createdAt,
          createdBy: chat.createdBy,
          role: chat.role,
          lastMessage,
          chatMembers: chat.chatMembers,
        } as Chat;

      })
    );

    return chatsWithMessages;

  } catch (error) {

    const message = error instanceof Error ? error.message : "Unknown error";
    return rejectWithValue(message);
    
  }
});