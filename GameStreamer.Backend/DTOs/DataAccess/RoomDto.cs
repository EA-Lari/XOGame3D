namespace GameStreamer.Backend.DTOs.DataAccess
{
    public class RoomDto
    {

        public string HubGroupId { get; set; }

        public Guid RoomGuid { get; set; }

        public IEnumerable<PlayerDto> RoomPlayers { get; set; }

    }
}