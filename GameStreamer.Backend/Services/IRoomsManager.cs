using GameStreamer.Backend.DTOs;

namespace GameStreamer.Backend.Services
{

    /// <summary>
    /// Интерфейс сервиса управления игровыми комнатами
    /// </summary>
    public interface IRoomsManager
    {

        #region Rooms

        public GameRoomResponseDTO RemoveRoom(string roomName);

        public GameRoomResponseDTO AddRoom(string roomName);

        public GameRoomResponseDTO GetRandomRoom();

        public List<GameRoomResponseDTO> GetAllGameRooms();

        #endregion

        #region Players

        public PlayerDataResponseDTO ChangePlayerNickName(string connectionId, string nickName);

        public PlayerDataResponseDTO AddPlayerToServer(string connectionId, string nickName);

        public PlayerDataResponseDTO RemovePlayer(string connectionId);

        public PlayerDataResponseDTO GetRandomPlayer();

        public List<PlayerDataResponseDTO> GetAllPlayersWithoutRoom();

        #endregion
        
    }
}