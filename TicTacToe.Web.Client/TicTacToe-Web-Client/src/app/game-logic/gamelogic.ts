import { GameStatus } from "./gamestatus";

export class GameLogic {

	gameField: Array<number> = [];

	currentTurn: number;

	gameStatus: GameStatus;

	winSituationsPlayerOne: Array<Array<number>> = [
		[1, 1, 1, 0, 0, 0, 0, 0, 0],
		[0, 0, 0, 1, 1, 1, 0, 0, 0],
		[0, 0, 0, 0, 0, 0, 1, 1, 1],
		[1, 0, 0, 1, 0, 0, 1, 0, 0],
		[0, 1, 0, 0, 1, 0, 0, 1, 0],
		[0, 0, 1, 0, 0, 1, 0, 0, 1],
		[0, 0, 1, 0, 1, 0, 1, 0, 0],
		[1, 0, 0, 0, 1, 0, 0, 0, 1]
	];

	winSituationsPlayerTwo: Array<Array<number>> = [
		[2, 2, 2, 0, 0, 0, 0, 0, 0],
		[0, 0, 0, 2, 2, 2, 0, 0, 0],
		[0, 0, 0, 0, 0, 0, 2, 2, 2],
		[2, 0, 0, 2, 0, 0, 2, 0, 0],
		[0, 2, 0, 0, 2, 0, 0, 2, 0],
		[0, 0, 2, 0, 0, 2, 0, 0, 2],
		[0, 0, 2, 0, 2, 0, 2, 0, 0],
		[2, 0, 0, 0, 2, 0, 0, 0, 2]
	];


	public constructor() {
		this.currentTurn = 0;
		this.gameStatus = GameStatus.STOP;
		this.gameField = [0, 0, 0, 0, 0, 0, 0, 0, 0];
	}

	gameStart(): void {
		console.log("Game is started!");

		this.gameField = [0, 0, 0, 0, 0, 0, 0, 0, 0];
		this.currentTurn = this.randomPlayerStart();
		console.log(`currentTurn: ${this.currentTurn}`);
		this.gameStatus = GameStatus.START;
	}

	randomPlayerStart(): number {
		// Get random number between 1 and 2
		return Math.floor(Math.random() * 2) + 1;
	}

	setField(position: number, value: number): void {
		this.gameField[position] = value;
		// console.log(this.gameField);
	}

	getPlayerColorClass(): string {
		return (this.currentTurn === 2) ? 'player-two' : 'player-one';
	}

	changePlayerOrder(): void {
		this.currentTurn = (this.currentTurn === 2) ? 1 : 2;
	}

	async checkGameEndFull(): Promise<boolean> {

		let isFull = true;

		// If Game Field contains at least one empty Cell
		if (this.gameField.includes(0)) {
			isFull = false;
		}

		if (isFull) {
			console.log('PlayerArea is full. End of game...');
			this.endingGame();
			return true;
		}
		else {
			return false;
		}

	}

	isArraysEqual(a: Array<any>, b: Array<any>): boolean {
		var resultOfEqual = Array.isArray(a)
			&& Array.isArray(b)
			&& a.length === b.length
			&& a.every((value, index) => { value === b[index] });

		console.log(`IsArraysEqualCheck: ${resultOfEqual}`);

		return resultOfEqual;
	}

	async checkGameEndWinner(): Promise<boolean> {

		let isWinner: boolean = false;

		const checkArray = ((this.currentTurn === 1) ? this.winSituationsPlayerOne : this.winSituationsPlayerTwo);

		const currentArray: number[] = [];

		this.gameField.forEach((subfield, index) => {

			if (subfield !== this.currentTurn) {
				currentArray[index] = 0;
			}
			else {
				currentArray[index] = subfield;
			}
		});

		checkArray.forEach((checkField, checkIndex) => {
			if (this.isArraysEqual(checkField, currentArray)) {
				isWinner = true;
			}
		});

		console.log(currentArray);

		if (isWinner) {
			this.endingGame();
			return true;
		}
		else {
			return false;
		}
	}

	endingGame(): void {
		this.gameStatus = GameStatus.STOP;
		console.log('Game is stopped!');
	}

}