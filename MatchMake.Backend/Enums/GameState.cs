namespace MatchMake.Backend.Enums
{

    /// <summary>
    /// Перечисление представляет Статус игры
    /// </summary>
    public enum GameState : int
    {

        /// <summary>
        /// Ожидаются игроки
        /// </summary>
        WaitingPlayers = 0,

        /// <summary>
        /// В процессе игры
        /// </summary>
        InProcess = 1,

        /// <summary>
        /// Игра отменена
        /// </summary>
        Canceled = 2

    }
}