using System;
using XOGame3D.Enum;

namespace XOGame3D.Models
{
    public interface ICell
    {
        event EventState SetState;
        
        int Oy { get; set; }
        int Ox { get; set; }
        States State { get; set; }
    }

    public delegate void EventState(ICell sender);

}