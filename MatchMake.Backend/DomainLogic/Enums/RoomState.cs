namespace MatchMake.Backend.Enums
{

    /// <summary>
    /// Перечисление представляет Статус игры
    /// </summary>
    public enum RoomState : int
    {

        /// <summary>
        /// Ожидаются игроки
        /// </summary>
        WaitingPlayers = 0,

        /// <summary>
        /// В процессе игры
        /// </summary>
        GameInProcess = 1,

        /// <summary>
        /// Дисконнект
        /// </summary>
        Disconnected = 2

    }
}