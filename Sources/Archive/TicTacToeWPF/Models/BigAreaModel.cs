using System.Collections.Generic;

using TicTacToeGame.BLL.Interfaces;
using TicTacToeGame.BLL.NotifyBinding;
using XOGame3D.Enum;

namespace TicTacToeWPF.Models
{
    /// <summary>
    /// Класс большого игрового поля
    /// </summary>
    public class BigAreaModel : NotifyPropertyChanged, Area<Cell>
    {
        public int Size { get; set; } = 3;
        public List<Cell> CellsList { get; set; }

        private States _areaState;
        public States AreaState
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
        public States Winner         { get; set; }

        public BigAreaModel()
        {
            this.AreaState  = States.Empty;            
            this.CellsList  = new List<Cell>();

            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                   // this.CellsList.Add(new MiniAreaModel(i, j, States.Empty, Size));
                }
            }
        }
    }
}
