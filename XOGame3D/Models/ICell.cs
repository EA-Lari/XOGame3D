using System;
using XOGame3D.Enum;

namespace XOGame3D.Models
{
    public interface ICell : IState
    {
        IArea<ICell> ParentArea { get; set; }
        int Oy { get; set; }
        int Ox { get; set; }
        
    }

    public interface IState
    {
        States State { get; set; } 
    }

}