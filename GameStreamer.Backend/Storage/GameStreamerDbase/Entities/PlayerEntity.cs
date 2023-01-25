namespace GameStreamer.Backend.Storage.GameStreamerDbase.Entities
{

    /// <summary>
    /// Сущность подключенного игрока в БД 
    /// </summary>
    public class PlayerEntity
    {
        /// <summary>
        /// Id записи в БД
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
        /// Player nickname
        /// </summary>
        public string Nickname { get; set; }

        public string ChatHubId { get; set; }

        public string GameHubId { get; set; }

        public string RoomHubId { get; set; }

        /// <summary>
        /// Player's Guid for external systems
        /// </summary>
        public Guid PlayerGuid { get; set; }

        /// <summary>
        /// Flag of player readiness
        /// </summary>
        public bool IsReadyForGame { get; set; }

        /// <summary>
        /// Flag of game type that player choosed
        /// </summary>
        public bool IsRandomGameMode { get; set; }

        /// <summary>
        /// Date/Time of Player creation
        /// </summary>
        public DateTime CreatedAt { get; set; }
        
    }
}