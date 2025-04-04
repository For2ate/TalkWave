import { createSlice } from "@reduxjs/toolkit";
import { Chat } from "../Types/Chat";
import { fetchChats } from "../Services/FetchChats";

interface ChatState {
    chats: Chat[];
    loading: boolean;
    error: string | null;
  }
  
  const initialState: ChatState = {
    chats: [],
    loading: false,
    error: null,
  };
  
  const chatSlice = createSlice({
    name: "chats",
    initialState,
    reducers: {},
    extraReducers: (builder) => {
      builder
        .addCase(fetchChats.pending, (state) => {
          state.loading = true;
          state.error = null;
        })
        .addCase(fetchChats.fulfilled, (state, action) => {
          state.loading = false;
          state.chats = action.payload;
        })
        .addCase(fetchChats.rejected, (state, action) => {
          state.loading = false;
          state.error = action.payload || "Failed to load chats";
        });
    },
  });

export const chatReducer = chatSlice.reducer;