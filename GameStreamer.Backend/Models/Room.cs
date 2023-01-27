using GameStreamer.Backend.DTOs.DataAccess;

namespace GameStreamer.Backend.Models
{
    public class Room
    {
        public string Name { get; set; }

        public List<PlayerWithHashDto> PlayersList { get; } = new List<PlayerWithHashDto>();

    }
}