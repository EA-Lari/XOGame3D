using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XOGame3D.Enum;
using XOGame3D.Interfaces;

namespace XOGame3D.Models
{
    internal class SmallArea : IArea, ICell
    {
        private int _sizeArea = 3;
        public SmallArea()
        {
            Cells = new List<ICell>();
        }
        public bool IsCrowded => !Cells.Any(x => x.State == States.Empty);
        public int Row { get; set; }
        public int Column { get; set; }
     
        public IArea ParentArea { get; set; }

        public List<ICell> Cells {get;}

        public int Size => _sizeArea;

        public States State { get; set; }
    }
}
