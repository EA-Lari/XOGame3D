namespace GameStreamer.Backend.Persistance.GameStreamerDbase.Entities
{
    public class PlayerEntity
    {
        
        public int Id { get; set; }

        public string ConnectionId { get; set; }

        public string NickName { get; set; }        

        public ClientType ClientType { get; set; }

        public bool IsActive { get; set; }

        public Guid RoomGuid { get; set; }


    }

    public enum ClientType
    {
        Unknown,
        AngularWeb,
        WpfDesktop,
        XamarinMobile
    }

}