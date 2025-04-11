import { HubConnection } from '@microsoft/signalr';

export type SignalRStatus = 
  | 'disconnected'
  | 'connecting'
  | 'connected'
  | 'reconnecting'
  | 'error';

export interface SignalRState {
  hub: HubConnection | null;
  status: SignalRStatus;
  error: string | null;
}