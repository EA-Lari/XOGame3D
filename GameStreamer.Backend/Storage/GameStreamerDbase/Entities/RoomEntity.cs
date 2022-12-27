namespace GameStreamer.Backend.Storage.GameStreamerDbase.Entities
{
    public class RoomEntity
    {
        /// <summary>
        /// Id row in DB
        /// </summary>
        public int Id { get; set; }

        #region One-To-Many relationship (One room -> Many players)

        /// <summary>
        /// Collection of Players who join room
        /// </summary>
        public ICollection<PlayerEntity> JoinedPlayers { get; } = new List<PlayerEntity>();

        #endregion

        /// <summary>
        /// Id room in SignalR Rooms hub (GroupId)
        /// </summary>
        public string HubGroupId { get; set; }

        /// <summary>
        /// Room Guid for external systems
        /// </summary>
        public Guid RoomGuid { get; set; }

        /// <summary>
        /// Date/Time of Room creation
        /// </summary>
        public DateTime CreatedAt { get; set; }

    }
}