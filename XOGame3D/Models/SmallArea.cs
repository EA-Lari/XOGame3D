using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XOGame3D.Enum;
using XOGame3D.Interfaces;

namespace XOGame3D.Models
{
    internal class SmallArea : BaseArea<Cell>, ICell
    {
        
        public SmallArea() : base()
        {

        }

        public bool IsCrowded => !Cells.Any(x => x.State == States.Empty);
        public int Oy { get; set; }
        public int Ox { get; set; }
     
        public IArea<ICell> ParentArea { get; set; }
    }
}
