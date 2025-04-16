import { ApiChatResponse } from "Entities/Chats/ChatTypes";
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

    createPersonalChat:async(data:{
        senderUserId: string,
        recipientUserId: string,
        message: string}
    ) : Promise<ApiChatResponse|null> =>{

        try {

            return await ChatApi.post<ApiChatResponse>(`Api/Chats/Chat/Personal`, data);

        } catch(error) {
            return null;
        }

    } 


}
