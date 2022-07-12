namespace Contracts.ServiceBus.Events
{

    /// <summary>
    /// Событие "Игра завершена"
    /// </summary>
    public record GameOverEvent
    {
        public string RoomId { get; init; }

        public string WhoIsWin { get; init; }

    }

}