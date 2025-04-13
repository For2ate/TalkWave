import { createAsyncThunk } from "@reduxjs/toolkit";
import { ApiChatResponse, Chat } from "Entities/Chats/ChatTypes";
import { MessageApiEndpoints } from "Features/Chat/Api/MessageApiEndpoints";
import { ChatApiEndpoints } from "Features/Chat/Api/ChatApiEndpoints";
import { UserApiEndpoints } from "Shared/Api";


export const fetchChats = createAsyncThunk<
  Chat[], // Return type
  void, // Argument type
  { rejectValue: string }
>("chats/fetchChats", 
  async (_, { rejectWithValue }) => {
  try {

    const userId = localStorage.getItem("userId");
    if (!userId) throw new Error("User ID not found");

    const chatsResponse = await ChatApiEndpoints.getChats(userId);
    if (!chatsResponse) throw new Error("No chats received");

    // For all chats get last message
    const chatsWithMessages = await Promise.all(
      chatsResponse.map(async (chat: ApiChatResponse) => {

        let lastMessage = null;
        
        if (chat.lastMessageId) {
          lastMessage = await MessageApiEndpoints.getMessageById(chat.lastMessageId);
        }
        let chatResponse = {
          id: chat.id,
          name: chat.name,
          isGroupChat: chat.isGroupChat,
          createdAt: chat.createdAt,
          createdBy: chat.createdBy,
          role: chat.role,
          lastMessage,
          chatMembers: chat.chatMembers,
        } as Chat;
        
        if (chat.isGroupChat === false) {

          let secondUserId = '';

          if (chat.chatMembers[0].id === userId) {
              secondUserId = chat.chatMembers[1].id;
          } else {
            secondUserId = chat.chatMembers[0].id;
          }

          const secondUser = await UserApiEndpoints.getUserById(secondUserId);

          chatResponse.name = `${secondUser?.firstName} ${secondUser?.lastName}`;

        }
        
        return chatResponse;

      })
    );

    return chatsWithMessages;

  } catch (error) {

    const message = error instanceof Error ? error.message : "Unknown error";
    return rejectWithValue(message);
    
  }
});