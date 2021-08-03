using System.Collections.Generic;

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
        States AreaState     { get; set; }

        /// <summary>
        /// Победитель в поле
        /// </summary>
        States Winner        { get; set; }

        /// <summary>
        /// Активность поля для хода игрока
        /// </summary>
        bool IsActive       { get; set; }
        
    }
}
