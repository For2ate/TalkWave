import { HubConnection, HubConnectionBuilder } from "@microsoft/signalr";
import { ChatApiUrl } from "Shared/Api/Constants";

export class ChatHub{

    private static instance: HubConnection;

    public static connect() {

        if (!this.instance) {

            this.instance = new HubConnectionBuilder()
                .withUrl(`${ChatApiUrl}/ChatHub`)
                .withAutomaticReconnect()
                .build();

        }

        return this.instance;

    }

}