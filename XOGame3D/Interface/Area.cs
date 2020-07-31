using System;
using System.Collections.Generic;
using System.Text;
using XOGame3D.Enum;

namespace XOGame3D.Interface
{
    internal abstract class Area
    {
        public State CheckWin()
        {
            return State.Empty;
        }
    }
}
