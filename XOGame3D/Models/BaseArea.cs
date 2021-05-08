using System;
using System.Collections.Generic;
using System.Text;
using XOGame3D.Enum;
using XOGame3D.Interfaces;

namespace XOGame3D.Models
{
    public abstract class BaseArea<C> : IArea<C> where C : ICell,new()
    {
        States _state = States.Empty;
        protected int _sizeArea = 3;

        public BaseArea()
        {
            var area = this as IArea<ICell>;
            Cells = new List<C>();
            for (int x = 0; x < _sizeArea; x++)
            {
                for (int y = 0; y < _sizeArea; y++)
                {
                    Cells.Add(new C 
                    {
                        Ox = x, 
                        Oy = y, 
                        ParentArea = area
                    });
                }
            }
        }

        public List<C> Cells { get; }

        public int Size => _sizeArea;

        public C CurrentCell { get; set; }

        public States State
        {
            get => _state;
            set
            {
                if (value != States.Empty) throw new Exception("Статус уже установлен");
                _state = value;
            }
        }
    }
}
