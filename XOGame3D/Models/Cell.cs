using System;
using XOGame3D.Enum;
using XOGame3D.Interfaces;

namespace XOGame3D.Models
{
    internal class Cell : ICell
    {
        public int Row { get; set; }

        public int Column { get; set; }

        private States _state = States.Empty;

        public States State { get => _state;
            set
            {
                if (_state != States.Empty) throw new Exception("Статус уже установлен");
                _state = value;
            }
        }

        public IArea ParentArea { get; set; }

        public override bool Equals(object obj)
        {
            return obj is ICell cell &&
                   Row == cell.Row &&
                   Column == cell.Column;
        }
    }

}
