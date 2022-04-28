namespace Contracts.SharedResources
{
    /// <summary>
    /// Message about changing state of game in room
    /// </summary>
    public class StateRoomGame
    {
        /// <summary>
        /// Room guid
        /// </summary>
        public Guid Room { get; set; }

        /// <summary>
        /// New state game
        /// </summary>
        public StateGame State { get; set; }
    }
}
