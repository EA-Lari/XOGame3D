namespace GameStreamer.Backend.DTO.GetRoomsList
{

    /// <summary>
    /// Модель запроса к MatchMake серверу на получение списка комнат
    /// </summary>
    public class GetRoomsListRequestDTO
    {
        public string RequestId { get; set; }

        public string ConnectionId { get; set; }
    }
}