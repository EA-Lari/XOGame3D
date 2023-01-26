namespace GameStreamer.Backend.Models
{

    public class PlayerFromRoomHub : PlayerBase
    {
        public PlayerFromRoomHub(string roomConnectionId, string nickName) : base(nickName)
        {
            this.RoomConnectionId = roomConnectionId;
        }

        public string RoomConnectionId { get; }
    }
}