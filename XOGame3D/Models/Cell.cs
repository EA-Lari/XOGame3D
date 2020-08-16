using System;
using XOGame3D.Enum;

namespace XOGame3D.Models
{
    internal class Cell : ICell
    {
        public int Oy { get; set; }
        public int Ox { get; set; }
        private States _state;

        public event EventState SetState;

        public States State { get => _state;
            set
            {
                if (value != States.Empty) throw new Exception("Статус уже установлен");
                _state = value;
                SetState?.Invoke(this);

            }
        }
    }

}
