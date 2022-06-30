namespace Contracts.ServiceBus.Events
{
    public record HereIsActualGameRoomsEvent
    {
        public List<string> ActualGameRooms { get; init; }
    }
}