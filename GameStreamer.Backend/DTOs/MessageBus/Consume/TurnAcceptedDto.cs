namespace GameStreamer.Backend.DTOs.MessageBus.Consume
{
    public class TurnAcceptedDto
    {
        public Guid RoomGuid { get; set; }

        public Guid PlayerGuid { get; set; }
    }
}