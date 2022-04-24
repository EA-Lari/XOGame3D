
using Contracts.GameXO.Models;

namespace Contracts.GameXO
{
    /// <summary>
    /// Сообщение инициирует ход
    /// </summary>
    public class MakeMoveMessage : MoveMessageIdentifiers
    {
        /// <summary>
        /// Координата, внутри маленького поля для хода
        /// </summary>
        public Coordinate CoordinateCell{ get; set; }
    }
}
