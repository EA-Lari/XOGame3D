namespace GameStreamer.Backend.DTOs.MessageBus.Publish
{
    public class MakeTurnDto
    {
        public Guid RoomGuid { get; set; }

        public Guid PlayerGuid { get; set; }

        public Coordinate BigAreaCoordinates { get; set; }

        public Coordinate SmallAreaCoordinates { get; set; }

        public GameFraction GameFraction { get; set; }
    }

    public record struct Coordinate
    {
        public int X { get; set; }

        public int Y { get; set; }
    }

    public enum GameFraction
    {
        Zero = 0,
        Cross = 1
    }
}