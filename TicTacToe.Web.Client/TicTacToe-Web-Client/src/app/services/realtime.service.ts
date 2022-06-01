import { Injectable } from '@angular/core';
import * as signalR from "@microsoft/signalr";
import { IMessage } from '../components/chat/chat.component';

@Injectable({
	providedIn: 'root'
})
export class RealTimeService {

	// public messages: IMessage[] = new [];
	private hubConnection: signalR.HubConnection;
	private realtimeServerUrl: string = 'https://localhost:5001/messages';

	constructor() {
		this.hubConnection = this.hubConnection = new signalR.HubConnectionBuilder()
			.withUrl(this.realtimeServerUrl)
			.withAutomaticReconnect()
			.build();
	}

	public startConnection = () => {

		this.hubConnection
			.start()
			.then(() => console.log('Connection started'))
			.catch(err => console.log('Error while starting connection: ' + err))
	}

	//#region Chat Server Listeners

	public addSendMessageListener = (newMessageHandler: (msg: IMessage) => void) => {
		this.hubConnection.on('Send', (message) => newMessageHandler(message));
	}

	//#endregion

}