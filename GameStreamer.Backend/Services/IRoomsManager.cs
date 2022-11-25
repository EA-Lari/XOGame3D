using GameStreamer.Backend.DTOs;

namespace GameStreamer.Backend.Services
{

    /// <summary>
    /// Интерфейс сервиса управления игровыми комнатами
    /// </summary>
    public interface IRoomsManager
    {

        public PlayerDataResponseDTO GetPlayerDataBy(string connectionId);

        public bool AddPlayerToServer(string connectionId, string nickName);
        
        public bool RemovePlayer();

        public bool RemoveRoom();

        public PlayerDataResponseDTO GetRandomPlayer();

        public GameRoomResponseDTO GetRandomRoom();

        public List<GameRoomResponseDTO> GetAllGameRooms();

        public List<PlayerDataResponseDTO> GetAllPlayers();
    }
}