namespace GameStreamer.Backend.Models
{
    public class Room
    {
        public string Name { get; set; }

        public List<PlayerFromRoomHub> PlayersList { get; } = new List<PlayerFromRoomHub>();

    }
}