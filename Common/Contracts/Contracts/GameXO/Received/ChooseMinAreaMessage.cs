using Contracts.GameXO.Models;

namespace Contracts.GameXO.Received
{
    /// <summary>
    /// Выбор мини поля для продолжения игры
    /// </summary>
    public class ChooseMinAreaMessage : MoveMessageIdentifiers
    {
        /// <summary>
        /// Координата маленького поля
        /// </summary>
        public Coordinate CoordinateArea { get; set; }
    }
}
