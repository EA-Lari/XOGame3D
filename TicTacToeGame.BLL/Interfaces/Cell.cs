using TicTacToeGame.BLL.Enums;
using TicTacToeGame.BLL.Structures;

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
        State CellState         { get; set; }
        
        /// <summary>
        /// Флаг активности ячейки
        /// </summary>
        bool IsActive            { get; set; }
    }
}
