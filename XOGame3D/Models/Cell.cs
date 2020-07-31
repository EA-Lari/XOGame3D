using XOGame3D.Enum;

namespace XOGame3D.Models
{
    internal class Cell : ICell
    {
        public int Oy { get; set; }
        public int Ox { get; set; }
        public State State { get; set; }
    }
}
