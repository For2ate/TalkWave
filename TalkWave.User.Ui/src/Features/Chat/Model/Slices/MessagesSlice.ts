import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import { MessageModel } from "Entities/Messages/MessageTypes";
import { loadMessages } from "Features/Chat/Model/Services";

export interface MessagesState {
    messages: Record<string, MessageModel[]>; // { [chatId]: Message[] }
    hasMore: Record<string, boolean>;
    loading: boolean;
    error: string | null;
}
const initialState: MessagesState = {
    messages: {},
    hasMore:{},
    loading: false,
    error: null,
};

const messagesSlice = createSlice({
    name:'messages',
    initialState,
    reducers:{
        addNewMessage: (state, action:PayloadAction<MessageModel>) => {
            const message = action.payload;
            if (!state.messages[message.chatId]) {
                state.messages[message.chatId] = [];
            }
            state.messages[message.chatId].push(message);
        }
    },
    extraReducers: (builder) => {
        builder
          .addCase(loadMessages.pending, (state) => {
            state.loading = true;
            state.error = null;
          })
          .addCase(loadMessages.fulfilled, (state, action) => {
            const { chatId, messages, direction, hasMore } = action.payload;
            state.loading = false;
      
            // Обновляем флаг hasMore только для загрузки вверх (before)
            if (direction === 'before') {
              state.hasMore[chatId] = hasMore;
            }
      
            if (!messages.length) return;
      
            const existingMessages = state.messages[chatId] || [];
            const existingIds = new Set(existingMessages.map(m => m.id));
            const newMessages = messages.filter(msg => !existingIds.has(msg.id));
      
            if (!newMessages.length) return;
      
            state.messages[chatId] = direction === 'before'
              ? [...newMessages, ...existingMessages]
              : [...existingMessages, ...newMessages];
          })
          .addCase(loadMessages.rejected, (state, action) => {
            state.loading = false;
            state.error = action.payload as string;
          });
      }

})

export const {addNewMessage} = messagesSlice.actions;
export const messagesReduser = messagesSlice.reducer;