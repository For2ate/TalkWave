import { MessageModel } from "Entities/Messages/MessageTypes";
import { ChatApi } from "Shared/Api";

export const MessageApiEndpoints = {

    getMessageById: async (id : string): Promise<MessageModel | null> => {

        try {
                
            return await ChatApi.get<MessageModel>(`Api/Message/Message/${id}`);

        } catch(error) {

            console.error(error);

            return null;

        }
    
    },

    getMessagesFromMessage: async (
        data: {
            chatId:string,
            messageId:string, 
            take:number
        }
    ): Promise<MessageModel[]|null> => {
        
        try {

            const params = new URLSearchParams();
            params.append('chatId', data.chatId);
            params.append('messageId', data.messageId);
            params.append('take', data.take.toString());

            const response = await ChatApi.get<MessageModel[]>(
                `Api/Message/Messages?${params.toString()}`
            );
    
            return response;

        } catch(error) {
            console.error(error);
            return null;
        }

    }

}