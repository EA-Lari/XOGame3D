namespace GameStreamer.Backend.DTO.RemoveFromRoom
{

    /// <summary>
    /// Модель ответа от MatchMake сервера на удаление игрока из комнаты
    /// </summary>
    public class LeaveRoomResponseDTO
    {
        public string ConnectionId { get; set; }

        public string RoomGuid { get; set; }

        public string Status { get; set; }
    }
}