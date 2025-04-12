import { createAsyncThunk } from "@reduxjs/toolkit";
import { ChatApiEndpoints } from "Features/Chat/Api/ChatApiEndpoints";


export const fetchMessages = createAsyncThunk(
    'messages/fetch',
    async (params: { chatId: string; messageId: string; take?: number, excludeLast?: boolean }, { rejectWithValue }) => {
      try {
        const response = await ChatApiEndpoints.getMessagesFromMessage({
          chatId: params.chatId,
          messageId: params.messageId,
          take: params.take || 20
        });

        let messages = response || [];

        if (params.excludeLast && messages.length > 0 && params.messageId) {
          messages.pop();
        }

        return {
          chatId: params.chatId,
          messages
        };
      } catch (error) {
        return rejectWithValue('Failed to load messages');
      }
    }
  );