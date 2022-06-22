using Contracts.GameXO.Models;

namespace Contracts.GameXO
{
    /// <summary>
    /// Сообщение запускает игру
    /// </summary>
    public class StartGameMessage
    {
        /// <summary>
        /// Идентификатор комнаты
        /// </summary>
        public Guid Room { get; set; }

        /// <summary>
        /// Игрок 1 
        /// </summary>
        public PlayerXO Player1 { get; set; }

        /// <summary>
        /// Игрок 2
        /// </summary>
        public PlayerXO Player2 { get; set; }
    }
}
