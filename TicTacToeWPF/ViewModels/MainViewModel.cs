using System.Windows.Input;
using System.Linq;
using TicTacToeGame.BLL.NotifyBinding;
using TicTacToeGame.BLL.Interfaces;
using TicTacToeWPF.Commands;
using System.Diagnostics;
using System.Collections.Generic;
using XOGame3D.Enum;
using TicTacToeWPF.Models;
using XOGame3D.Logic;
using AutoMapper;
using XOGame3D.Interfaces;
using TicTacToeWPF.Services;

namespace TicTacToeWPF.ViewModels
{
    public class MainViewModel : NotifyPropertyChanged
    {

        #region  Private Fields

        private readonly TicTacToeLoginc _gameController;
        private readonly IMapper _mapper;
        private bool _hasCurrentArea = false;
        private Area<Cell> _bigGameArea;
        private States _turn;

        #endregion

        #region Public Properties

        public Area<Cell> BigGameArea
        {
            get => _bigGameArea;
            set
            {
                _bigGameArea = value;
                OnPropertyChanged(nameof(BigGameArea));
            }
        }
        
        public States Turn
        {
            get => _turn;
            set
            {
                _turn = value;
                OnPropertyChanged(nameof(Turn));
            }
        }

        #endregion

        #region Constructor

        public MainViewModel()
        {
            _mapper = WPFFactory.GetMapper();
            _gameController = WPFFactory.GetGameController();
            LoadGame();
        }

        #endregion

        #region Commands

        /// <summary>
        /// Команда начала новой игры
        /// </summary>
        private ICommand _newGameCommand;
        public ICommand NewGameCommand => _newGameCommand ??= new RelayCommand(parameter =>
        {
            _gameController.Reset();
            LoadGame();
        });

        /// <summary>
        /// Команда нажатия на ячейку игрового поля
        /// </summary>
        private ICommand _doTurn;
        public ICommand DoTurnCommand => _doTurn ??= new RelayCommand(parameter =>
        {
            PutFigureOnArea(parameter);
        });

        #endregion

        #region Private Methods

        /// <summary>
        /// Метод начала новой игры, заполняет поле 3х3
        /// </summary>
        private void LoadGame()
        {
            _gameController.SetWinner += _gameController_SetWinner;
            this.Turn = States.Empty;
            //TODO: Needs siplification
            var controllerArea = _gameController.GetBigArea();
            this.BigGameArea = (Area<Cell>)_mapper.Map<BigAreaModel>(controllerArea);
            foreach (IArea areaC in controllerArea.Cells)
            {
                var area = _mapper.Map<MiniAreaModel>(areaC);
                foreach (var cellC in areaC.Cells)
                {
                    var cell = _mapper.Map<CellModel>(cellC);
                    area.CellsList.Add((Cell)cell);
                }
                BigGameArea.CellsList.Add((Cell)area);
            }

            // Разблокируем все игровые области
            //this.BigGameArea.CellsList.ForEach(x => x.IsActive = true);
            //this._turnCounter = 0;
            // TODO Перенести в тесты
            //this.BigGameArea.AreaState = States.Draw;
            //this.BigGameArea.CellsList.ForEach(x => x.CellState = States.Zero);            
        }

        private void _gameController_SetWinner(object sender, States states)
        {
            BigGameArea.Winner = states;
        }

        /// <summary>
        /// Установка фигуры на игровое поле
        /// </summary>
        /// <param name="figure">Объект фигуры из View</param>
        private void PutFigureOnArea(object figure)
        {
            if (figure is Cell cell)
            {
                if (cell.CellState == States.Empty)
                {
                    // Все игровые области отключаются для исключения нарушения правил
                    BigGameArea.CellsList.ForEach(x => x.IsActive = false);
                    cell.CellState = _turn;
                    _gameController.SetState(cell.Coordinates.CoordX, cell.Coordinates.CoordY);
                    UpdateActiveArea();
                }
            }
        }

        private void UpdateActiveArea()
        {
            var currentArea = _gameController.GetCurrentArea() as ICell;
            if (currentArea == null)
                BigGameArea.CellsList.ForEach(x => x.IsActive = true);
            else
            {
                var area = BigGameArea.CellsList
                    .FirstOrDefault(x => x.Coordinates.CoordX == currentArea.Row
                        && x.Coordinates.CoordY == currentArea.Column);
                area.IsActive = true;
            }

        }
        
        #endregion
    }
}
