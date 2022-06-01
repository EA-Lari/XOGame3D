import { Injectable } from '@angular/core';
import * as signalR from "@microsoft/signalr"

@Injectable({
	providedIn: 'root'
})
export class RealTimeService {

	// public messages: IMessage[] = new [];
	private hubConnection: signalR.HubConnection;
	private realtimeServerUrl: string = 'https://localhost:5001/messages';

	public startConnection = () => {

		this.hubConnection
			.start()
			.then(() => console.log('Connection started'))
			.catch(err => console.log('Error while starting connection: ' + err))
	}

	constructor() {
		this.hubConnection = this.hubConnection = new signalR.HubConnectionBuilder()
			.withUrl(this.realtimeServerUrl)
			.build();
	}
}

export interface IMessage {
	userName: string,
	message: string
}