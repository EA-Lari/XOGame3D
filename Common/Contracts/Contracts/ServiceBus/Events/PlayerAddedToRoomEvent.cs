namespace Contracts.ServiceBus.Events
{

    /// <summary>
    /// Событие "игрок добавлен в комнату"
    /// </summary>
    public record PlayerAddedToRoomEvent
    {

        public string PlayerId { get; init; }

        public string RoomId { get; init; }
    }
}
