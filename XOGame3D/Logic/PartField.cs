
using XOGame3D.Enum;
using XOGame3D.Models;

namespace XOGame3D.Logic
{
    class PartField : Area<Cell>, ICell
    {
        public int Oy { get ; set ; }
        public int Ox { get ; set ; }
        public State State { get ; set ; }
    }
}
