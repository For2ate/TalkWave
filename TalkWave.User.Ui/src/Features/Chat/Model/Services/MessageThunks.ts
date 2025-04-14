import { MessagesState } from "Features/Chat/Model/Slices/MessagesSlice";
import { MessageApiEndpoints } from "Features/Chat/Api/MessageApiEndpoints";
import { createAsyncThunk } from "@reduxjs/toolkit";

export const loadMessages = createAsyncThunk(
    'messages/load',
    async (params: { 
      chatId: string; 
      messageId?: string; 
      take?: number;
      direction: 'before' | 'after';
    }, { getState, rejectWithValue }) => {
      try {
        const { chatId, messageId, take = 20, direction } = params;
        const state = getState() as { messages: MessagesState };
  
        // Проверка, есть ли еще сообщения для загрузки
        if (direction === 'before' && state.messages.hasMore[chatId] === false || !messageId) {
          return { chatId, messages: [], direction, hasMore: false };
        }

  
        const response = await MessageApiEndpoints.getMessagesFromMessage({
          chatId,
          messageId,
          take
        });
  
        const loadedMessages = response || [];
        const hasMoreMessages = loadedMessages.length >= take;
  
        return { 
          chatId, 
          messages: loadedMessages,
          direction,
          hasMore: hasMoreMessages
        };
  
      } catch (error) {
        return rejectWithValue(error instanceof Error ? error.message : 'Unknown error');
      }
    }
  );