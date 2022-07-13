namespace MatchMake.Backend.MessageBus.Contracts
{
    public record HelloMessage
    {
        public HelloMessage()
        {   }

        public string Name { get; init; }
    }
}