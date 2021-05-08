using System;
using XOGame3D.Enum;
using XOGame3D.Models;

namespace XOGame3D.Interfaces
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