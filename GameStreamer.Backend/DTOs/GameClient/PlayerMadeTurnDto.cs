using GameStreamer.Backend.Models.Common;

namespace GameStreamer.Backend.DTOs.GameClient
{
    public class PlayerMadeTurnDto
    {

        public Coordinate SmallAreaCoordinates { get; set; }

        public Coordinate CellCoordinates { get; set; }

    }
}