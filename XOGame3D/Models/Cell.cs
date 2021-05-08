using System;
using XOGame3D.Enum;
using XOGame3D.Interfaces;

namespace XOGame3D.Models
{
    internal class Cell : ICell
    {
        public int Oy { get; set; }

        public int Ox { get; set; }

        private States _state = States.Empty;

        public States State { get => _state;
            set
            {
                if (value != States.Empty) throw new Exception("Статус уже установлен");
                _state = value;
            }
        }

        public IArea<ICell> ParentArea { get; set; }

        public override bool Equals(object obj)
        {
            return obj is ICell cell &&
                   Oy == cell.Oy &&
                   Ox == cell.Ox;
        }
    }

}
