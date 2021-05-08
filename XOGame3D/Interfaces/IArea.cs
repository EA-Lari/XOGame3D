using System;
using System.Collections.Generic;
using System.Text;

namespace XOGame3D.Interfaces
{
    public interface IArea<C> : IState where C : ICell
    {
        List<C> Cells { get; }

        int Size { get; }
    }
}
