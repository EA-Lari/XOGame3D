import { Component, OnInit } from '@angular/core';
import { GameLogic } from 'src/app/game-logic/gamelogic';

@Component({
	selector: 'app-game',
	templateUrl: './game.component.html',
	styleUrls: ['./game.component.scss'],
	providers: [GameLogic]
})
export class GameComponent implements OnInit {

	constructor(public game: GameLogic) { }

	ngOnInit(): void {
	}

	startGame(): void {
		this.game.gameStart();
		console.log(`GameStatus: ${this.game.gameStatus}`);

		const currentPlayer = 'Current turn: Player: ' + this.game.currentTurn;
		const information: any = document.querySelector('.current-status');
		information.innerHTML = currentPlayer;
	}

	async clickCell(subfield: any): Promise<void> {

		if (this.game.gameStatus === 1) {
			const position = subfield.currentTarget.getAttribute('position');
			const information: any = document.querySelector('.current-status');

			this.game.setField(position, this.game.currentTurn);
			// console.log(`position of click: ${position}`);

			const color = this.game.getPlayerColorClass();
			subfield.currentTarget.classList.add(color);

			// End Game If Exist Winner Logic
			await this.game.checkGameEndWinner().then((end: boolean) => {
				console.log('checkWinner Method work!');
				console.log(`gameStatus: ${this.game.gameStatus}`)
				console.log(`isGameEnd: ${end}`);

				if (this.game.gameStatus === 0 && end) {
					information.innerHTML = 'The winner is Player: ' + this.game.currentTurn;
					console.log('Winner in Game! Player: ' + this.game.currentTurn);
				}
			});

			// End Game If All Cells Full Logic (Draw Case)
			await this.game.checkGameEndFull().then((end: boolean) => {
				if (this.game.gameStatus === 0 && end) {
					information.innerHTML = 'No winner, draw!';
					console.log('Draw in Game!');
				}
			});

			this.game.changePlayerOrder();

			if (this.game.gameStatus === 1) {
				const currentPlayer = 'Current turn: Player: ' + this.game.currentTurn;
				information.innerHTML = currentPlayer;
			}
		}
	}
}