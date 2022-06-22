
namespace Contracts.GameXO.Models
{
    public class MoveMessageIdentifiers
    {
        /// <summary>
        /// Идентификатор комнаты
        /// </summary>
        public Guid Room { get; set; }

        /// <summary>
        /// Идентификатор игрока
        /// </summary>
        public Guid Player { get; set; }
    }
}
