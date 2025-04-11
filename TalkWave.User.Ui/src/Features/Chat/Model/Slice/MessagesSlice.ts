import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import { MessageModel as Message } from "../Types/Message";
import { fetchMessages } from "../Services/MessageThunks";

interface MessagesState {
  entities: Record<string, Message[]>; // { [chatId]: Message[] }
  loading: boolean;
  error: string | null;
}

const initialState: MessagesState = {
    entities: {},
    loading: false,
    error: null,
};

export const messagesSlice = createSlice({
    name: 'messages',
    initialState,
    reducers: {
      // Добавление новых сообщений
      prependMessages: (state, action: PayloadAction<{ chatId: string; messages: Message[] }>) => {
        const { chatId, messages } = action.payload;
        if (!state.entities[chatId]) {
          state.entities[chatId] = [];
        }
        state.entities[chatId] = [...messages, ...state.entities[chatId]];
      },
      // Добавление одного сообщения (для real-time)
      addNewMessage: (state, action: PayloadAction<Message>) => {
        const { chatId } = action.payload;
        if (!state.entities[chatId]) {
          state.entities[chatId] = [];
        }
        state.entities[chatId].push(action.payload);
      },
      messagesReset: (state, action: PayloadAction<string>) => {
        delete state.entities[action.payload];
      },
    },
    extraReducers: (builder) => {
      builder
        .addCase(fetchMessages.pending, (state) => {
          state.loading = true;
          state.error = null;
        })
        .addCase(fetchMessages.fulfilled, (state, action) => {
          const { chatId, messages } = action.payload;
          state.loading = false;
          if (!state.entities[chatId]) {
            state.entities[chatId] = [];
          }
          state.entities[chatId] = [...messages, ...state.entities[chatId]];
        })
        .addCase(fetchMessages.rejected, (state, action) => {
          state.loading = false;
          state.error = action.payload as string;
        });
    }
  });
  
  export const { prependMessages, addNewMessage, messagesReset } = messagesSlice.actions;
  export const messagesReducer = messagesSlice.reducer;