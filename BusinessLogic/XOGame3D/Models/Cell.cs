using System;
using XOGame3D.Enum;
using XOGame3D.Interfaces;

namespace XOGame3D.Models
{
    internal class Cell : ICell
    {
        private States _state = States.Empty;

        public States State { get => _state;
            set
            {
                if (_state != States.Empty) throw new Exception("Статус уже установлен");
                _state = value;
            }
        }

        public IArea ParentArea { get; set; }
        public Coordinate Coordinate { get; set; }

        public override bool Equals(object obj)
        {
            return obj is ICell cell &&
                   cell.Coordinate.Equals(Coordinate);
        }
    }

}
