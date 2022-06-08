import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { GameComponent } from './components/game/game.component';
import { ChatNewComponent } from './chat-new/chat-new.component';

const routes: Routes = [
	{ path: '', component: ChatNewComponent }
];

@NgModule({
	imports: [RouterModule.forRoot(routes)],
	exports: [RouterModule]
})
export class AppRoutingModule { }
