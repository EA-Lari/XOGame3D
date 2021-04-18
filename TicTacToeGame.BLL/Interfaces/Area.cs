using System.Collections.Generic;
using TicTacToeGame.BLL.Enums;

namespace TicTacToeGame.BLL.Interfaces
{
    public interface Area<T>
    {
        public string MiniAreaGuid { get; }
        /// <summary>
        /// Размерность поля
        /// </summary>
        int Size            { get; set; }

        /// <summary>
        /// Список ячеек поля
        /// </summary>
        List<T> CellsList   { get; set; }

        /// <summary>
        /// Состояние поля
        /// </summary>
        State AreaState     { get; set; }

        /// <summary>
        /// Победитель в поле
        /// </summary>
        State Winner        { get; set; }

        /// <summary>
        /// Активность поля для хода игрока
        /// </summary>
        bool IsActive       { get; set; }
        
    }
}
