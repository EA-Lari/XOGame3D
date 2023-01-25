using System.Collections.Concurrent;
using GameStreamer.Backend.DTOs;
using GameStreamer.Backend.Models;

namespace GameStreamer.Backend.Services
{

    public class PlayerManager : IPlayerManager
    {
        private readonly Random _random = new Random();
        private readonly ConcurrentDictionary<string, Player> _playersConcurrDict = new ConcurrentDictionary<string, Player>();

        public PlayerDataResponseDTO AddPlayerToServer(string connectionId, string nickName)
        {
            var playerForAdd = new Player(connectionId, nickName);
            _playersConcurrDict.TryAdd(connectionId, playerForAdd);

            return new PlayerDataResponseDTO { ConnectionId = playerForAdd.ConnectionId, NickName = playerForAdd.NickName };
        }

        public PlayerDataResponseDTO GetPlayerDataBy(string connectionId)
        {
            return GetRandomPlayer();
        }

        public PlayerDataResponseDTO GetRandomPlayer()
        {
            return new PlayerDataResponseDTO { NickName = $"Player_{_random.Next(1, 999)}", ConnectionId = $"id_{_random.Next(3000, 9000)}" };
        }

        public List<PlayerDataResponseDTO> GetAllPlayersWithoutRoom()
        {
            return _playersConcurrDict.Select(x => new PlayerDataResponseDTO { ConnectionId = x.Key, NickName = x.Value.NickName }).ToList();
        }

        public PlayerDataResponseDTO RemovePlayer(string connectionId)
        {
            Player removedPlayer;
            _playersConcurrDict.TryRemove(connectionId, out removedPlayer);

            return new PlayerDataResponseDTO() { ConnectionId = removedPlayer.ConnectionId, NickName = removedPlayer.NickName };
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

        public PlayerDataResponseDTO ChangePlayerNickName(string connectionId, string nickName)
        {
            Player changedPlayer;
            _playersConcurrDict.TryGetValue(connectionId, out changedPlayer);
            changedPlayer?.SetNewNickName(nickName);
            _playersConcurrDict.TryUpdate(connectionId, changedPlayer, changedPlayer);

            return new PlayerDataResponseDTO { ConnectionId = changedPlayer.ConnectionId, NickName = changedPlayer.NickName };
        }

    }
}