import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import { Chat } from "Entities/Chats/ChatTypes";
import { MessageModel } from "Entities/Messages/MessageTypes";
import { fetchChats } from "Features/Chat/Model/Services";

interface ChatState{
    chats: Record<string, Chat>;
    selectedChatId: string|null;
    status: string;
    error: string|null;
}

const initialState: ChatState = {
    chats:{},
    selectedChatId: null,
    status: 'idle',
    error: null,
}

const chatSlice = createSlice({
    name:"chats",
    initialState,
    reducers: {
        setSelectedChat: (state, action:PayloadAction<string>) => {
            state.selectedChatId = action.payload;
        },
        updateLastMessage: (state, action:PayloadAction<{chatId:string, message:MessageModel}>) => {
            const {chatId, message} = action.payload;
            if (state.chats[chatId]) {
                state.chats[chatId].lastMessage = message;
            }
        },
    },
    extraReducers: (builder) => {
        builder.addCase(fetchChats.pending, (state) => {
            state.status = 'loading';
        }),
        builder.addCase(fetchChats.fulfilled, (state, action) => {
            state.status = 'succeeded';
            action.payload.forEach(chat => {
                state.chats[chat.id] = chat;
            });
        }),
        builder.addCase(fetchChats.rejected, (state, action) => {
            state.status = 'failed';
            state.error = action.error.message || 'Failed to load chats';
        })
    }
});

export const {setSelectedChat, updateLastMessage} = chatSlice.actions; 
export const chatReducer = chatSlice.reducer;