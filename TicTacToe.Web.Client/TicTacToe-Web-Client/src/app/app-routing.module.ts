import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { GameComponent } from './components/game/game.component';

const routes: Routes = [
	{ path: 'heroes', component: GameComponent }
 ];

@NgModule({
	
  declarations: [],
  imports: [
    CommonModule,
	 RouterModule.forRoot(routes)
  ],
  exports: [ RouterModule ]
})
export class AppRoutingModule { }
