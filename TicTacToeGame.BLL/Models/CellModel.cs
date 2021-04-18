using TicTacToeGame.BLL.Enums;
using TicTacToeGame.BLL.Interfaces;
using TicTacToeGame.BLL.NotifyBinding;
using TicTacToeGame.BLL.Structures;

namespace TicTacToeGame.BLL.Models
{
    /// <summary>
    /// Ячейка, минимальная игровая единица
    /// </summary>
    public class CellModel : NotifyPropertyChanged, Cell
    {
        public string ParentAreaGuid { get; }

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

        public bool IsActive { get; set; }

        public CellModel(int x, int y, State state, string parentAreaGuid)
        {
            this.ParentAreaGuid = parentAreaGuid;
            this.Coordinates = new Coordinates(x, y);
            this.CellState = state;
        }
    }
}
