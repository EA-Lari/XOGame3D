namespace GameStreamer.Backend.DTOs.MessageBus.Consume
{
    public class TurnNotAcceptedDto
    {
        public Guid RoomGuid { get; set; }

        public Guid PlayerGuid { get; set; }
    }
}