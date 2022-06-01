import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { RealTimeService } from './services/realtime.service';

@Component({
	selector: 'app-root',
	templateUrl: './app.component.html',
	styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
	title = 'TicTacToe-Web-Client';

	constructor(public realTimeService: RealTimeService, private webClient: HttpClient) { }

	ngOnInit(): void {
		this.realTimeService.startConnection();
	}

}