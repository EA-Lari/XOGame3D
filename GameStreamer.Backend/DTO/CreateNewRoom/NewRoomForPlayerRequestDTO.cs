namespace GameStreamer.Backend.DTO
{

    /// <summary>
    /// Модель запроса к MatchMake серверу на создание новой комнаты для игрока
    /// </summary>
    public class NewRoomForPlayerRequestDTO
    {
        public string ConnectionId { get; set; }
    }
}