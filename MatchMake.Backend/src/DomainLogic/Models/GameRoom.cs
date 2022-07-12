using MatchMake.Backend.Contracts;

namespace MatchMake.Backend.Models
{
    public sealed class GameRoom : IRoom
    {

        /// <summary>
        /// Поле описывает Список игроков, попавших в Комнату Ожидания
        /// </summary>
        private readonly List<PlayerData> _playersList;

        /// <summary>
        /// Св-во описывает лимит игроков в комнате
        /// </summary>
        internal int PlayersRoomLimit { get; }

        internal bool IsRoomReady
        {
            get => _playersList.All(x => x.IsReadyForGame);
        }

        internal bool Empty { get => _playersList.Count == 0; }

        /// <summary>
        /// Св-во описывает идентификатор комнаты
        /// </summary>
        internal Guid RoomGuid { get; }

        /// <summary>
        /// Конструктор
        /// </summary>
        public GameRoom(int playersRoomLimit)
        {
            _playersList = new List<PlayerData>();
            PlayersRoomLimit = playersRoomLimit;
            RoomGuid = new Guid();
        }

        /// <summary>
        /// Метод добавляет Игрока в комнату
        /// </summary>
        /// <param name="userName">Добавляемый игрок</param>
        public string Add(string userName)
        {
            if (_playersList.Count >= PlayersRoomLimit)
            {
                throw new ArgumentOutOfRangeException($"Лимит игроков в комнате {RoomGuid} превысил {PlayersRoomLimit} ед.");
            }

            var addedPlayer = new PlayerData(userName);
            _playersList.Add(addedPlayer);

            return addedPlayer.PlayerName;
        }

        /// <summary>
        /// Метод удаляет Игрока из комнаты
        /// </summary>
        /// <param name="userName">Удаляемый игрок</param>
        public string Remove(string userName)
        {
            var findedPlayer = _playersList.Find(x => x.PlayerName == userName);
            _playersList.Remove(findedPlayer);
            return findedPlayer.PlayerName;
        }

        /// <summary>
        /// Метод очищает комнату
        /// </summary>
        /// <returns>Статус очистки комнаты</returns>
        public bool Clear()
        {
            _playersList.Clear();

            if (_playersList.Count > 0)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Метод возвращает список игроков в комнате
        /// </summary>
        /// <returns>List of Player Logins</returns>
        public IEnumerable<string> GetAllPlayers()
        {
            var resultPlayersDataList = new List<string>();

            foreach (var player in _playersList)
            {
                resultPlayersDataList.Add($"{player.PlayerName}");
            }

            return resultPlayersDataList;
        }

    }

    /// <summary>
    /// Перечисление представляет Статус игры
    /// </summary>
    public enum GameRoomState : int
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