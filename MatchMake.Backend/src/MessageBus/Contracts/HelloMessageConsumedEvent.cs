namespace MatchMake.Backend.MessageBus.Contracts
{
    public record HelloMessageConsumedEvent
    {
        public string Status { get; init; }
    }
}