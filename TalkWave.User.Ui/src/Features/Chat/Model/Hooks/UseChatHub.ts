import { HubConnection } from "@microsoft/signalr"
import { ChatHub } from "Features/Lib/SignalR";
import { useEffect, useState } from "react"


export const useChatHub = () => {

    const [connection, setConnection] = useState<HubConnection | null>(null);

    useEffect(() => {

        const createConnection = async () => {

            const hub = ChatHub.connect();

            try {
                // Проверяем состояние перед запуском
                if (hub.state === 'Disconnected') {
                  await hub.start()
                  setConnection(hub)
                }
              } catch (err) {
                console.error('SignalR Connection Error:', err)
            }

        }

        createConnection();

        return () => {
            if (connection) {
              connection.stop().catch(err => {
                console.error('Stop connection error:', err)
              })
            }
        }

    }, []); 

    return connection;

}