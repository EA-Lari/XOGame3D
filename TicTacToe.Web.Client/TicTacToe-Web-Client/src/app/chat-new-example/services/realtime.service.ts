import { Injectable } from '@angular/core';

import * as signalR from '@microsoft/signalr';

import { Subject, Observable, Observer } from 'rxjs';

import { Message } from '../model/message';
import { Event } from '../model/event';

const SERVER_URL = 'https://localhost:5001/messages';

@Injectable()
export class RealTimeService {

	// chats: Observable<ChatModel> | undefined;

	// public messages: IMessage[] = new [];
	private hubConnection: signalR.HubConnection;

	connectionEstablished = new Subject<Boolean>();
	// messageExample = new Subject<IMessage>();

	constructor() {
		this.hubConnection = this.hubConnection = new signalR.HubConnectionBuilder()
			.withUrl(SERVER_URL)
			.withAutomaticReconnect()
			.build();
	}

	public startConnection = () => {

		this.hubConnection
			.start()
			.then(() => { console.log('Connection started'); this.connectionEstablished.next(true); })
			.catch(err => console.log(err));

		this.hubConnection.onclose(error => console.log('Connection closed. ', error));
		this.hubConnection.onreconnecting(error => console.log('Connection reconnecting. ', error));
		this.hubConnection.onreconnected(connectionId => console.log('Connecting reconnected with id: ', connectionId));
	}

	public stopConnection = () => {
		this.hubConnection.stop();
	}

	//#region Chat Server Listeners

	public onEvent(event: Event): Observable<any> {
		return new Observable<Event>(observer => {
			this.hubConnection.on(event, () => observer.next());
		});
	}

	public onMessage(): Observable<Message> {
		return new Observable<Message>(observer => {
			this.hubConnection.on('Send', (data: Message) => observer.next(data));
		});
	}

	// 	public send(message: Message): void {
	// 		this.socket.emit('message', message);
	//   }

	// public addSendMessageListener = (messageHandler: (msg: IMessage) => void) => {
	// 	this.hubConnection.on('Send', (message) => {
	// 		messageHandler(message);
	// 	});
	// }

	//#endregion


	public disconnect() {
		if (this.hubConnection) {
			this.hubConnection.stop();
		}
	}

}