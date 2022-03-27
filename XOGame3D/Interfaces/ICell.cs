using XOGame3D.Enum;
using XOGame3D.Models;

namespace XOGame3D.Interfaces
{
    /// <summary>
    /// Cell in small and big array
    /// </summary>
    public interface ICell : IState
    {
        IArea ParentArea { get; set; }
        Coordinate Coordinate { get; set; }
    }

    public interface IState
    {
        States State { get; set; } 
    }

}