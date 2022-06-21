namespace GameStreamer.Backend.DTO.RemoveFromRoom
{

    /// <summary>
    /// Модель запроса к MatchMake серверу на удаление игрока из комнаты
    /// </summary>
    public class LeaveRoomRequestDTO
    {
        public string ConnectionId { get; set; }

        public string RoomGuid { get; set; }
    }
}