
namespace Contracts.TicTacToeGame
{
    /// <summary>
    /// Игрок для игры крестики-нолики
    /// </summary>
    public class PlayerTicTacToe
    {
        /// <summary>
        /// Идентификатор игрока
        /// </summary>
        public Guid Guid { get; set; }

        /// <summary>
        /// Null - не выбрано, 1 - крестик, 0 - нолик
        /// </summary>
        public bool? Fraction { get; set; }
    }
}
