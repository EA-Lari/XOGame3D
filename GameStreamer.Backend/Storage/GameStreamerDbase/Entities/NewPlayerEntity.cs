namespace GameStreamer.Backend.Storage.GameStreamerDbase.Entities
{

    /// <summary>
    /// Entity that shows which Players who just connected to the RoomHub
    /// </summary>
    public class NewPlayerEntity
    {
        /// <summary>
        /// Id row in DB
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// PlayerFromRoomHub nickname
        /// </summary>
        public string Nickname { get; set; }

        /// <summary>
        /// Date/Time of PlayerFromRoomHub creation
        /// </summary>
        public DateTime CreatedAt { get; set; }
    }
}