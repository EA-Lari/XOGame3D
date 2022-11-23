using GameStreamer.Backend.DTOs;

namespace GameStreamer.Backend.Services
{

    /// <summary>
    /// Интерфейс сервиса управления игровыми комнатами
    /// </summary>
    public interface IRoomsManager
    {

        public bool RemovePlayer();

        public bool RemoveRoom();

        public PlayerNickNameResponseDTO GetRandomPlayer();

        public GameRoomResponseDTO GetRandomRoom();

        public List<GameRoomResponseDTO> GetAllGameRooms();

        public List<PlayerNickNameResponseDTO> GetAllPlayers();
    }
}