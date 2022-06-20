namespace GameStreamer.Backend.DTO.RemoveFromRoom
{

    /// <summary>
    /// Модель запроса к MatchMake серверу на удаление игрока из комнаты
    /// </summary>
    public class RemovePlayerFromRoomRequestDTO
    {
        public string ConnectionId { get; set; }

        public string RoomGuid { get; set; }
    }
}