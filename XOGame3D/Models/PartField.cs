
using XOGame3D.Business;
using XOGame3D.Enum;
using XOGame3D.Interface;

namespace XOGame3D.Models
{
    class PartField : Area<Cell>, ICell
    {
        public int Oy { get ; set ; }
        public int Ox { get ; set ; }
        public State State { get ; set ; }
    }
}
