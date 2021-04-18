using System;
using XOGame3D.Enum;

namespace XOGame3D.Models
{
    public interface ICell : IState
    {
        int Oy { get; set; }
        int Ox { get; set; }
        
    }

    public delegate void EventState(ICell sender);

    public interface IState
    {
        States State { get; set; }
        event EventState SetState;
    }

}