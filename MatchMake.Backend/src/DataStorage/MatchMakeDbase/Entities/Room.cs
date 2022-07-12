namespace MatchMake.Backend.Storage.Entities
{
    public class Room : Entity<int>
    {
        
        /// <summary>
        /// GUID комнаты
        /// </summary>
        public Guid RoomGuid { get; set; }

        /// <summary>
        /// Состояние игровой комнаты
        /// </summary>
        public int RoomState { get; set; }

        /// <summary>
        /// Дата/время создания комнаты
        /// </summary>
        public DateTime CreatedDt { get; set; }

    }
}
