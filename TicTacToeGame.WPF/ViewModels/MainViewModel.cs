using System.Windows.Input;
using System.Linq;

using TicTacToeGame.BLL.NotifyBinding;
using TicTacToeGame.BLL.Interfaces;




using TicTacToeGame.WPF.Commands;
using System.Diagnostics;
using System.Collections.Generic;

namespace TicTacToeGame.WPF.ViewModels
{
    public class MainViewModel : NotifyPropertyChanged
    {
        #region Поля        

        private GameService _algorithm;

        private Area<Cell> _nextActiveArea;
        private Area<Cell> _currentActiveArea;

        private Area<Cell> _bigGameArea;
        public Area<Cell> BigGameArea
        {
            get => _bigGameArea;
            set
            {
                _bigGameArea = value;
                OnPropertyChanged();
            }
        }
        // TODO Реальный MVVM костыль, переделать!
        private int _turnCounter;

        private States _turn;
        public States Turn
        {
            get => _turn;
            set
            {
                _turn = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Конструктор
        public MainViewModel()
        {
            NewGame();
        }
        #endregion

        #region Команды
        /// <summary>
        /// Команда начала новой игры
        /// </summary>
        private ICommand _newGameCommand;
        public ICommand NewGameCommand => _newGameCommand ??= new RelayCommand(parameter =>
        {
            NewGame();
        });

        /// <summary>
        /// Команда нажатия на ячейку игрового поля
        /// </summary>
        private ICommand _doTurn;
        public ICommand DoTurnCommand => _doTurn ??= new RelayCommand(parameter =>
        {
            PutFigureOnArea(parameter);
            CheckDraw();
            CheckWin(parameter);
        });
        #endregion

        #region Методы
        /// <summary>
        /// Метод начала новой игры, заполняет поле 3х3
        /// </summary>
        private void NewGame()
        {            
            this.Turn = State.Cross;
            this._algorithm = new GameService();            
            // Создаем игровое поле размерностью 3 х 3
            this.BigGameArea = this._algorithm.CreateGame();
            
            // Разблокируем все игровые области
            this.BigGameArea.CellsList.ForEach( x => x.IsActive = true );
            this._turnCounter = 0;
            // TODO Перенести в тесты
            //this.BigGameArea.AreaState = State.Draw;
            //this.BigGameArea.CellsList.ForEach(x => x.CellState = State.Zero);            
        }
        /// <summary>
        /// Установка фигуры на игровое поле
        /// </summary>
        /// <param name="figure">Объект фигуры из View</param>
        private void PutFigureOnArea(object figure)
        {            
            if (figure is Cell cell)
            {
                this._turnCounter++;

                if (cell.CellState == State.Empty)
                {
                    cell.CellState = _turn;                    

                    Turn = Turn == State.Cross ? State.Zero : State.Cross;
                    
                    // Все игровые области отключаются для исключения нарушения правил
                    BigGameArea.CellsList.ForEach(x => x.IsActive = false);

                    GetNextActiveMiniArea(cell);
                    
                    this._nextActiveArea.IsActive = true;                    

                    // Если мы отправляем соперника в мини-поле, где закончились свободные ячейки
                    bool isMiniAreaFill = this._nextActiveArea.CellsList.All(x => x.CellState != State.Empty);

                    if (isMiniAreaFill)
                    {
                        // Все игровые области разблокируются для совершения хода
                        BigGameArea.CellsList.ForEach(x => x.IsActive = true);
                    }
                }
            }
        }

        private void GetNextActiveMiniArea(Cell cell)
        {
            // Активным полем становится поле с координатами == координатам ячейки                
            this._nextActiveArea =
                (Area<Cell>)this.BigGameArea
                    .CellsList
                    .First(
                        x => x.Coordinates.CoordX == cell.Coordinates.CoordX && x.Coordinates.CoordY == cell.Coordinates.CoordY
                        );
        }

        private void GetCurrentActiveMiniArea(Cell cell)
        {
            this._currentActiveArea = 
                (Area<Cell>)this.BigGameArea
                    .CellsList
                    .First(
                        x=> ((MiniAreaModel)x).MiniAreaGuid == cell.ParentAreaGuid
                        );

            // Test
            Debug.WriteLine($"Sel curr Area, AreaState: {((MiniAreaModel)this._currentActiveArea).CellState}");
        }

        /// <summary>
        /// Метод проверки победы в поле
        /// </summary>
        /// <param name="figure"></param>
        private void CheckWin(object figure)
        {
            if (figure is Cell cell)
            {
                GetCurrentActiveMiniArea(cell);
                // Только если поле еще не было выиграно
                if ( ((MiniAreaModel)this._currentActiveArea).CellState == State.Empty ) 
                {
                    // Проверяем, есть ли победитель в мини-поле
                    this._algorithm.CheckWin(this._currentActiveArea, cell.CellState);                   
                    
                    // Проверяем победителя в большом поле, завершаем игру                    
                    this._algorithm.CheckWin(this.BigGameArea, cell.CellState);

                    // Проверка и установка ничьей в игре
                    var listAreas = (List<Cell>)this.BigGameArea.CellsList;
                    
                }

                // Test
                //Debug.WriteLine($"Check Draw in Small Area, MiniAreaState: {((MiniAreaModel)this._currentActiveArea).AreaState}");
                // Test
                //Debug.WriteLine($"Check Draw in Big Area, BigAreaState: {this.BigGameArea.AreaState}");
            }
        }

        private void CheckDraw()
        {
            if (this._turnCounter == 81)
            {
                this.BigGameArea.AreaState = State.Draw;
            }
        }

        #endregion
    }
}
