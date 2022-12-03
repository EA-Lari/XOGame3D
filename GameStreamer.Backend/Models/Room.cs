namespace GameStreamer.Backend.Models
{
    public class Room
    {
        public string Name { get; set; }

        public List<Player> PlayersList { get; } = new List<Player>();

    }
}