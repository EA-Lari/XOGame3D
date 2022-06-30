namespace Contracts.ServiceBus.Commands
{

    /// <summary>
    /// Команда "Игрок подключается к игровому матчу"
    /// </summary>
    public record PlayerJoinMatchCommand
    {
        public string PlayerId { get; init; }

        public string? RoomId { get; init; }

        public bool IsMatchRandom { get; init; }
    }
}