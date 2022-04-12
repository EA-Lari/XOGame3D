
using TicTacToeGame.BLL.Structures;
using XOGame3D.Enum;

namespace TicTacToeGame.BLL.Interfaces
{
    public interface Cell
    {

        public string ParentAreaGuid { get; }
        /// <summary>
        /// Координаты ячейки
        /// </summary>
        Coordinates Coordinates { get; set; }

        /// <summary>
        /// Статус ячейки - O X Ничья
        /// </summary>
        States CellState  { get; set; }
        
        /// <summary>
        /// Флаг активности ячейки
        /// </summary>
        bool IsActive            { get; set; }
    }
}
