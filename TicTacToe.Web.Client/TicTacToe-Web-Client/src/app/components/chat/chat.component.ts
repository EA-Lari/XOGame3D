import { Component, OnInit } from '@angular/core';
import { RealTimeService } from 'src/app/services/realtime.service';
// import { RealTimeService } from './services/realtime.service';

@Component({
	selector: 'app-chat',
	templateUrl: './chat.component.html',
	styleUrls: ['./chat.component.scss']
})
export class ChatComponent implements OnInit {

	constructor(private realTimeService: RealTimeService) { }

	ngOnInit(): void {
		setTimeout(() => {
			this.realTimeService.startConnection();
			this.realTimeService.addSendMessageListener(this.appendMessageToChat);
		}, 2000);
	}

	private appendMessageToChat(msg: IMessage) {
		console.log('Message Received!');
		console.log(`Sender: ${msg.sender}`);
		console.log(`Text: ${msg.text}`);
	}

	private appendMessageToChat_1(sender: any, message: string, color: string) {
		// document.querySelector('#messages-content').insertAdjacentHTML("beforeend", `<div style="color:${color}"><p>Sender: ${sender}</p><p>Text: ${message}</p></div><br>`);
	}

}

export interface IMessage {
	sender: string,
	text: string
}