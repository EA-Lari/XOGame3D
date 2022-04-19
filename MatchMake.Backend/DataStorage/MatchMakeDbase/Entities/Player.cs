namespace MatchMake.Backend.Storage.Entities
{
    public class Player : Entity<int>
    {
        
        /// <summary>
        /// GUID игрока
        /// </summary>
        public Guid PlayerGuid { get; set; }
        
        /// <summary>
        /// Ник игрока
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// Состояние готовности к игре
        /// </summary>
        public bool IsReadyForGame { get; set; }

        /// <summary>
        /// GUID комнаты, в которую зашел игрок (может отсутствовать)
        /// </summary>
        public Guid? RoomGuid { get; set; }

	}
}