import { HubConnection, HubConnectionBuilder, LogLevel } from '@microsoft/signalr';

class SignalRService {
    private static instance: SignalRService;
    private connection: HubConnection | null = null;

    private constructor() {}

    public static getInstance(): SignalRService {
        if (!SignalRService.instance) {
            SignalRService.instance = new SignalRService();
        }
        return SignalRService.instance;
    }

    public async connect(url: string): Promise<HubConnection> {
        if (this.connection && this.connection.state === 'Connected') {
            return this.connection;
        }

        this.connection = new HubConnectionBuilder()
            .withUrl(url)
            .withAutomaticReconnect()
            .configureLogging(LogLevel.Information)
            .build();

        try {
            await this.connection.start();
            console.log('SignalR connected');
            return this.connection;
        } catch (error) {
            console.error('Connection failed:', error);
            this.connection = null;
            throw error;
        }
    }

    public async stopConnection(): Promise<void> {
        if (this.connection) {
            await this.connection.stop();
            this.connection = null;
        }
    }

    public getConnection(): HubConnection | null {
        return this.connection;
    }
}

export const chatHubService = SignalRService.getInstance();