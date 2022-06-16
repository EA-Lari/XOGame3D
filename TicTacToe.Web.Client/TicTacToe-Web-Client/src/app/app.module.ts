// Core And Common
import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { BrowserModule } from '@angular/platform-browser';
// Pretty Look
import { MatButtonModule } from '@angular/material/button';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
// Http
import { HttpClient, HttpClientModule } from '@angular/common/http';
// Components
import { AppComponent } from './app.component';
import { GameComponent } from './components/game/game.component';
// Elements, shared between Components
import { SharedModule } from './shared/shared.module';
// import { ChatNewModule } from './chat-new/chat-new.module';

import { TranslateModule, TranslateLoader } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';

import { MatMenuModule } from '@angular/material/menu';
import { ChatComponentComponent } from './components/chat/chat-component/chat-component.component';

export function HttpLoaderFactory(http: HttpClient) {
	return new TranslateHttpLoader(http);
}

@NgModule({
	declarations: [
		AppComponent,
		GameComponent,
  ChatComponentComponent
	],
	imports: [
		BrowserModule,
		BrowserAnimationsModule,
		AppRoutingModule,
		MatButtonModule,
		MatMenuModule,
		HttpClientModule,
		SharedModule,
		// ChatNewModule,
		TranslateModule.forRoot({
			loader: {
				provide: TranslateLoader,
				useFactory: HttpLoaderFactory,
				deps: [HttpClient]
			}
		})
	],
	providers: [],
	bootstrap: [AppComponent]
})
export class AppModule { }