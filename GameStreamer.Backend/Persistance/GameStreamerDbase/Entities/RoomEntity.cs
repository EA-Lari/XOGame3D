namespace GameStreamer.Backend.Persistance.GameStreamerDbase.Entities
{
    public class RoomEntity
    {
        public int Id { get; set; }

        public Guid RoomGuid { get; set; }

        public List<PlayerEntity> Players { get; set; }

    }
}
