import { createAsyncThunk } from "@reduxjs/toolkit";
import { ChatApiEndpoints } from "Features/Chat/Api/ChatApiEndpoints";


export const fetchMessages = createAsyncThunk(
    'messages/fetch',
    async (params: { chatId: string; messageId: string; take?: number }, { rejectWithValue }) => {
      try {
        const response = await ChatApiEndpoints.getMessagesFromMessage({
          chatId: params.chatId,
          messageId: params.messageId,
          take: params.take || 50
        });
        
        // Явно возвращаем объект с chatId и messages
        return {
          chatId: params.chatId,
          messages: response || [] // Если response null, используем пустой массив
        };
      } catch (error) {
        return rejectWithValue('Failed to load messages');
      }
    }
  );