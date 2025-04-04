import { ChatApi } from "Shared/Api";
import { Message } from "../Model/Types/Message";


export const MessageApiEndpoints = {

    getMessageById: async (id : string): Promise<Message | null> => {

        try {
                
            return await ChatApi.get<Message>(`Api/Message/Message/${id}`);

        } catch(error) {

            console.error(error);

            return null;

        }
    
    },

}