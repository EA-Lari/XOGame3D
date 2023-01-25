namespace GameStreamer.Backend.DTOs.MessageBus.Consume
{
    public class TurnDeniedDto
    {
        public Guid RoomGuid { get; set; }

        public Guid PlayerGuid { get; set; }
    }
}