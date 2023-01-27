using GameStreamer.Backend.DTOs;
using GameStreamer.Backend.DTOs.DataAccess;
using GameStreamer.Backend.Storage;

namespace GameStreamer.Backend.Services
{

    public class PlayerManager : IPlayerManager
    {
        private readonly IHashService _hashService;
        private readonly IGameStreamRepository _gameRepo;

        public PlayerManager(IGameStreamRepository gameRepo, IHashService hashService)
        {
            _gameRepo = gameRepo;
            _hashService = hashService;
        }

        public PlayerDataResponseDTO AddNewPlayerToServer(string nickName)
        {

            var playerForAdd = new PlayerWithHashDto(nickName, _hashService.CalculateHashCodeFrom(nickName));
            var addedPlayerData = _gameRepo.AddNewPlayer(playerForAdd);

            return addedPlayerData;
        }

        public PlayerDataResponseDTO ChangePlayerNickName(string prevNickName, string newNickName)
        {
            if (string.IsNullOrEmpty(prevNickName) || string.IsNullOrEmpty(newNickName))
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"Попытка задать пустое значение ника. Старый - {prevNickName}, Новый - {newNickName} Ничего не меняем!");
                Console.ForegroundColor = ConsoleColor.DarkGray;

                return null;
            }

            var playerPreviousGuid = _hashService.CalculateHashCodeFrom(prevNickName);
            var playerNewGuid = _hashService.CalculateHashCodeFrom(prevNickName);

            var existedNewPlayer = _gameRepo.GetNewPlayerBy(playerPreviousGuid);

            if (existedNewPlayer != null)
            {
                existedNewPlayer.SetNewNickName(newNickName);
                existedNewPlayer.SetNewHashGuid(playerNewGuid);

                _gameRepo.UpdateNewPlayer(existedNewPlayer);

                Console.WriteLine($"Поменяли никнейм новому игроку, старый: {prevNickName}, новый: {newNickName}, успешно нашли его под старым uuid: {playerPreviousGuid}, новый uuid: {playerNewGuid}");
            }
            else
            {
                var existedInRoomPlayer = _gameRepo.GetPlayerWithRoomBy(playerPreviousGuid);

                if (existedInRoomPlayer != null)
                {
                    existedInRoomPlayer.SetNewNickName(newNickName);
                    existedInRoomPlayer.SetNewHashGuid(playerNewGuid);

                    _gameRepo.UpdatePlayerWithRoom(existedInRoomPlayer);
                    Console.WriteLine($"Поменяли никнейм игроку в группе, старый: {prevNickName}, новый: {newNickName}, успешно нашли его под старым uuid: {playerPreviousGuid}, новый uuid: {playerNewGuid}");
                }
            }

            return new PlayerDataResponseDTO() { NickName = newNickName };
        }

        public PlayerDataResponseDTO GetPlayerDataBy(string connectionId)
        {
            return GetRandomPlayer();
        }

        public PlayerDataResponseDTO GetRandomPlayer()
        {
            return new PlayerDataResponseDTO();
            //return new PlayerDataResponseDTO { NickName = $"Player_{_random.Next(1, 999)}", RoomConnectionId = $"id_{_random.Next(3000, 9000)}" };
        }

        public List<PlayerDataResponseDTO> GetAllPlayersWithoutRoom()
        {
            return new List<PlayerDataResponseDTO>();
            //return _playersConcurrDict.Select(x => new PlayerDataResponseDTO { RoomConnectionId = x.Key, NickName = x.Value.NickName }).ToList();
        }

        public PlayerDataResponseDTO RemovePlayer(string connectionId)
        {
            return new PlayerDataResponseDTO();
        }

        public PlayerDataResponseDTO MakePlayerReadyToGame(string connectionId)
        {
            Console.WriteLine("Метод MakePlayerReadyToGame еще не реализован!");
            return new PlayerDataResponseDTO();
        }

        public PlayerDataResponseDTO SetMatchTypeToPlayer(string connectionId, bool matchType)
        {
            Console.WriteLine("Метод SetMatchTypeToPlayer еще не реализован!");
            return new PlayerDataResponseDTO();
        }

    }
}