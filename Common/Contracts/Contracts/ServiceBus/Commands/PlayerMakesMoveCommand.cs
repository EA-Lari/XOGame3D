namespace Contracts.ServiceBus.Commands
{

    /// <summary>
    /// Команда "Игрок сделал ход"
    /// </summary>
    public record PlayerMakesMoveCommand
    {
        public string PlayerId { get; init; }

        public string RoomId { get; init; }

        public string AreaCoordinates { get; init; }

    }

}