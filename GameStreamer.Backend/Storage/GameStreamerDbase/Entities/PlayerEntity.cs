namespace GameStreamer.Backend.Storage.GameStreamerDbase.Entities
{

    /// <summary>
    ///Entity that shows Player who joined to the Game Room
    /// </summary>
    public class PlayerEntity
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        #region One-To-Many relationship (One room -> Many players)

        /// <summary>
        /// Link to Room by Id
        /// </summary>
        public int RoomId { get; set; }

        /// <summary>
        /// Room object
        /// </summary>
        public RoomEntity Room { get; set; }

        #endregion

        /// <summary>
        /// PlayerFromRoomHub nickname
        /// </summary>
        public string Nickname { get; set; }

        public string ChatHubId { get; set; }

        public string GameHubId { get; set; }

        public string RoomHubId { get; set; }

        /// <summary>
        /// PlayerFromRoomHub's Guid for external systems
        /// </summary>
        public Guid PlayerHashGuid { get; set; }

        /// <summary>
        /// Flag of player readiness
        /// </summary>
        public bool IsReadyForGame { get; set; }

        /// <summary>
        /// Flag of game type that player choosed
        /// </summary>
        public bool IsRandomGameMode { get; set; }

        /// <summary>
        /// Date/Time of PlayerFromRoomHub creation
        /// </summary>
        public DateTime CreatedAt { get; set; }
        
    }
}