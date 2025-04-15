import { configureStore } from "@reduxjs/toolkit";
import { messagesReduser } from "Features/Chat/Model";
import { chatReducer } from "Features/Chat/Model";

export const store = configureStore({

    reducer: {

        chats: chatReducer,
        messages: messagesReduser
    }

});

export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;