namespace GameStreamer.Backend.Interfaces
{
    public interface IGameHub
    {
        Task TestBroadcastPublish(string message);

        Task JoinRoom();
        
        Task LeaveRoom();
    }
}
