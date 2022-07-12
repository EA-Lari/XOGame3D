namespace MatchMake.Backend.Models
{

    /// <summary>
    /// Класс описывает Игрока
    /// </summary>
    public class PlayerData
    {
        public bool IsReadyForGame { get; private set; }

        public string PlayerName { get; }

        public Guid PlayerGuid { get; }

        public PlayerData(string playerName)
        {
            PlayerName = playerName;
            PlayerGuid = new Guid();            
            IsReadyForGame = false;
        }

        internal void ChangeGameReadyState()
            => IsReadyForGame = !IsReadyForGame;

    }
}