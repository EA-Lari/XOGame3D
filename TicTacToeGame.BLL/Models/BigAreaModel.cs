using System.Collections.Generic;
using TicTacToeGame.BLL.Enums;
using TicTacToeGame.BLL.Interfaces;
using TicTacToeGame.BLL.NotifyBinding;

namespace TicTacToeGame.BLL.Models
{
    /// <summary>
    /// Класс большого игрового поля
    /// </summary>
    public class BigAreaModel : NotifyPropertyChanged, Area<Cell>
    {
        public int Size { get; set; } = 3;
        public List<Cell> CellsList { get; set; }

        private State _areaState;
        public State AreaState
        {
            get => _areaState;
            set
            {
                _areaState = value;
                OnPropertyChanged();
            }
        }
       
        public bool IsActive        { get; set; }

        public string MiniAreaGuid  { get; }
        public State Winner         { get; set; }

        public BigAreaModel()
        {
            this.AreaState  = State.Empty;            
            this.CellsList  = new List<Cell>();

            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    this.CellsList.Add(new MiniAreaModel(i, j, State.Empty, Size));
                }
            }
        }
    }
}
