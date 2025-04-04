import { ApiChatResponse } from "../Model/Types/Chat";
import { ChatApi } from "Shared/Api";


export const ChatApiEndpoints = {

    getChats: async (id : string): Promise<ApiChatResponse[] | null> => {

        try {
                
            return await ChatApi.get<ApiChatResponse[]>(`Api/Chats/Chats/${id}`);

        } catch(error) {

            console.error(error);

            return null;

        }
    
    },

}