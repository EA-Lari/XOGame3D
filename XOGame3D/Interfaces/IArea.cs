using System;
using System.Collections.Generic;
using System.Text;

namespace XOGame3D.Interfaces
{
    public interface IArea : IState
    {
        List<ICell> Cells { get; }

        int Size { get; }
    }
}
