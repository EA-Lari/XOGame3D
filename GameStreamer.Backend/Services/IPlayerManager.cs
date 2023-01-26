using GameStreamer.Backend.DTOs;

namespace GameStreamer.Backend.Services
{
    public interface IPlayerManager
    {

        public PlayerDataResponseDTO MakePlayerReadyToGame(string connectionId);

        public PlayerDataResponseDTO SetMatchTypeToPlayer(string connectionId, bool matchType);

        public PlayerDataResponseDTO ChangePlayerNickName(string prevNickName, string newNickName);

        public PlayerDataResponseDTO AddNewPlayerToServer(string nickName);

        public PlayerDataResponseDTO RemovePlayer(string connectionId);

        public PlayerDataResponseDTO GetRandomPlayer();

        public List<PlayerDataResponseDTO> GetAllPlayersWithoutRoom();

    }
}
