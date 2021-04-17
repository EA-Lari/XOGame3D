using System;
using System.Collections.Generic;
using System.Text;
using XOGame3D.Enum;

namespace XOGame3D.Models
{
    public abstract class BaseArea<C> where C : ICell,new()
    {
        protected int _sizeArea = 3;
        public BaseArea()
        {
            Cells = new List<C>();
            for (int x = 0; x < _sizeArea; x++)
            {
                for (int y = 0; y < _sizeArea; y++)
                {
                    Cells.Add(new C {Ox=x, Oy=y});
                }
            }
        }

        public List<C> Cells { get; }

        public States Winner { get; set; }

        public C LastCell { get; set; }
    }
}
