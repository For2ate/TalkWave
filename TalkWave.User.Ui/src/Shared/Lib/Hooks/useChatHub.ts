import { useEffect, useState } from 'react';
import { HubConnection } from '@microsoft/signalr';
import { ChatApiUrl } from 'Shared/Api/Constants';
import { chatHubService } from 'Shared/Lib/Services/SignalR';

export const useChatHub = () => {
    const [connection, setConnection] = useState<HubConnection | null>(null);

    useEffect(() => {
        const setupConnection = async () => {
            try {
                const hub = await chatHubService.connect(`${ChatApiUrl}/ChatHub`);
                setConnection(hub);
            } catch (error) {
                console.error('Failed to connect:', error);
            }
        };

        setupConnection();

        return () => {
            // Очистка подписок, но соединение остается активным
        };
    }, []);

    return connection;
};