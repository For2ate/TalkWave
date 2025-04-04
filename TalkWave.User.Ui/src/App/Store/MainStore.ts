import { configureStore } from "@reduxjs/toolkit";
import { chatReducer } from "Features/Chat/Model/Slice/ChatSlice";


export const store = configureStore({

    reducer: {

        chats: chatReducer,

    }

});


export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;