namespace Contracts.ServiceBus.Commands
{

    /// <summary>
    /// Команда "Игрок готов к игре"
    /// </summary>
    public record PlayerReadyToPlayCommand
    {
        public string PlayerId { get; init; }
    }

}
