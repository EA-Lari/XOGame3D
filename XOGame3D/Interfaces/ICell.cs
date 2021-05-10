using System;
using XOGame3D.Enum;
using XOGame3D.Models;

namespace XOGame3D.Interfaces
{
    public interface ICell : IState
    {
        IArea ParentArea { get; set; }
        int Row { get; set; }
        int Column { get; set; }
        
    }

    public interface IState
    {
        States State { get; set; } 
    }

}