namespace GameStreamer.Backend.DTO
{

    /// <summary>
    /// Модель ответа от MatchMake сервера на создание новой комнаты для игрока
    /// </summary>
    public class NewRoomForPlayerResponseDTO
    {
        public string ConnectionId { get; set; }
        
        public string RoomGuid { get; set; }

        public string Status { get; set; }
    }
}