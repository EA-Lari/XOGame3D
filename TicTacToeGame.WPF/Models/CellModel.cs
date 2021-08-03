using TicTacToeGame.BLL.NotifyBinding;
using TicTacToeGame.BLL.Structures;
using XOGame3D.Enum;

namespace TicTacToeGame.WPF.Models
{
    /// <summary>
    /// Ячейка, минимальная игровая единица
    /// </summary>
    public class CellModel : NotifyPropertyChanged
    {
        public string ParentAreaGuid { get; }

        public Coordinates Coordinates  { get; set; }

        private States _cellState;
        public States CellState          
        {
            get => _cellState;
            set
            {
                _cellState = value;
                OnPropertyChanged();
            }
        }

        public bool IsActive { get; set; }

        public CellModel(int x, int y, States state, string parentAreaGuid)
        {
            this.ParentAreaGuid = parentAreaGuid;
            this.Coordinates = new Coordinates(x, y);
            this.CellState = state;
        }
    }
}
