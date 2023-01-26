using GameStreamer.Backend.DTOs;
using GameStreamer.Backend.Models;
using System.Collections.Concurrent;
using GameStreamer.Backend.Storage;

namespace GameStreamer.Backend.Services
{

    public class PlayerManager : IPlayerManager
    {
        private readonly IHashService _hashService;
        private readonly IGameStreamRepository _gameRepo;
        //private readonly Random _random = new Random();
        //private readonly ConcurrentDictionary<string, PlayerFromRoomHub> _playersConcurrDict = new ConcurrentDictionary<string, PlayerFromRoomHub>();

        public PlayerManager(IGameStreamRepository gameRepo, IHashService hashService)
        {
            _gameRepo = gameRepo;
            _hashService = hashService;
        }

        public PlayerDataResponseDTO AddNewPlayerToServer(string nickName)
        {
            var playerForAdd = new PlayerFromRoomHub(nickName);
            var addedPlayerData = _gameRepo.AddNewPlayer(playerForAdd);

            return addedPlayerData;
        }

        public PlayerDataResponseDTO ChangePlayerNickName(string prevNickName, string newNickName)
        {
            //var playerOldGuid = _hashService.CalculateHashCodeFrom(prevNickName);
            //var existedPlayer = _gameRepo.GetPlayerBy(playerOldGuid);
            //Console.WriteLine($"Меняем никнейм игрока, нашли успешно по Guid: {playerOldGuid}");
            //Console.WriteLine($"Найден игрок: ник-{existedPlayer.NickName}, id в хабе-{existedPlayer.RoomConnectionId}");
            //existedPlayer.SetNewNickName(newNickName);
            //existedPlayer.PlayerDataHashGuid = _hashService.CalculateHashCodeFrom(newNickName);
            //_gameRepo.UpdatePlayer(existedPlayer, playerOldGuid);

            return new PlayerDataResponseDTO() { ConnectionId = "", NickName = newNickName };
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