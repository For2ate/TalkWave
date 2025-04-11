import { ApiChatResponse } from "../Model/Types/Chat";
import { ChatApi } from "Shared/Api";
import { MessageModel as Message } from "../Model/Types/Message";


export const ChatApiEndpoints = {

    getChats: async (id : string): Promise<ApiChatResponse[] | null> => {

        try {
                
            return await ChatApi.get<ApiChatResponse[]>(`Api/Chats/Chats/${id}`);

        } catch(error) {

            console.error(error);

            return null;

        }
    
    },

    getMessagesFromMessage: async (data: {chatId:string,messageId:string, take:number}):
        Promise<Message[]|null> => {
            
            try {

                const params = new URLSearchParams();
                params.append('chatId', data.chatId);
                params.append('messageId', data.messageId);
                params.append('take', data.take.toString());

                const response = await ChatApi.get<Message[]>(
                    `Api/Message/Messages?${params.toString()}`
                );
        
                return response;

            } catch(error) {
                console.error(error);
                return null;
            }

    }

}
