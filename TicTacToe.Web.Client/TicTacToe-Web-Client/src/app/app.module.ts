// Core And Common
import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { BrowserModule } from '@angular/platform-browser';
// Pretty Look
import { MatButtonModule } from '@angular/material/button';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
// Http
import { HttpClientModule } from '@angular/common/http';
// Components
import { AppComponent } from './app.component';
import { GameComponent } from './components/game/game.component';
import { ChatComponent } from './components/chat/chat.component';

@NgModule({
	declarations: [
		AppComponent,
		GameComponent,
		ChatComponent
	],
	imports: [
		BrowserModule,
		AppRoutingModule,
		BrowserAnimationsModule,
		MatButtonModule,
		HttpClientModule
	],
	providers: [],
	bootstrap: [AppComponent]
})
export class AppModule { }