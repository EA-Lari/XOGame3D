namespace GameStreamer.Backend.DTOs
{
    public class GameRoomResponseDTO
    {
        public string RoomName { get; set; }
        public List<PlayerDataResponseDTO> PlayersList { get; set; }
    }
}
