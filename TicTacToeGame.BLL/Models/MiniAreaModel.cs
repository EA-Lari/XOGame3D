using System;
using System.Collections.Generic;
using TicTacToeGame.BLL.Enums;
using TicTacToeGame.BLL.Interfaces;
using TicTacToeGame.BLL.NotifyBinding;
using TicTacToeGame.BLL.Structures;

namespace TicTacToeGame.BLL.Models
{
    /// <summary>
    /// Мини-поле, состоит из ячеек
    /// </summary>
    public class MiniAreaModel : NotifyPropertyChanged, Area<Cell>, Cell
    {
        public string MiniAreaGuid { get; }
        public int Size                 { get; set; }
        public List<Cell> CellsList     { get; set; }
        
        public State AreaState { get; set; }
        

        private State _winner;
        public State Winner
        {
            get => _winner;
            set
            {
                _winner = value;
                OnPropertyChanged();
            }
        }

        private bool _isActive;
        public bool IsActive            
        {
            get => _isActive;
            set
            {
                _isActive = value;
                OnPropertyChanged();
            }
        }
        public Coordinates Coordinates  { get; set; }
        
        private State _cellState;
        public State CellState
        {
            get => _cellState;
            set
            {
                _cellState = value;
                OnPropertyChanged();
            }
        }

        public string ParentAreaGuid => throw new NotImplementedException();

        public MiniAreaModel ( int x, int y, State areaState, int smallAreaSize )
        {
            // Костыль, привести в порядок дубликаты полей
            this.MiniAreaGuid = Guid.NewGuid().ToString();
            this.AreaState = areaState;
            this.Coordinates    = new Coordinates(x, y);
            this.CellState      = areaState;
            this.Size           = smallAreaSize;
            this.CellsList      = new List<Cell>();

            for (int i = 0; i < smallAreaSize; i++)
            {
                for (int j = 0; j < smallAreaSize; j++)
                {
                    this.CellsList.Add(new CellModel(i, j, State.Empty, this.MiniAreaGuid));
                }
            }
        }

    }
}
