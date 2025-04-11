import { configureStore } from "@reduxjs/toolkit";
import { chatReducer } from "Features/Chat/Model/Slice/ChatSlice";
import { messagesReducer } from "Features/Chat/Model/Slice/MessagesSlice";

export const store = configureStore({

    reducer: {

        chats: chatReducer,
        messages: messagesReducer

    }

});

export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;