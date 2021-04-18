using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XOGame3D.Enum;

namespace XOGame3D.Models
{
    public class SmallArea : BaseArea<Cell>, ICell
    {
        States _state;

        public SmallArea() : base()
        {

        }

        public bool IsCrowded => !Cells.Any(x => x.State == States.Empty);
        public int Oy { get; set; }
        public int Ox { get; set; }
        public States State
        {
            get => _state;
            set
            {
                if (value != States.Empty) throw new Exception("Статус уже установлен");
                _state = value;
                SetState?.Invoke(this);
            }
        }

        public event EventState SetState;
    }
}
