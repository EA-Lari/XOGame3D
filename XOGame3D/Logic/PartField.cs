
using System;
using System.Collections.Generic;
using System.Linq;
using XOGame3D.Enum;
using XOGame3D.Models;

namespace XOGame3D.Logic
{
    internal class PartField : Area<Cell>, ICell
    {
        public int Oy { get ; set ; }
        public int Ox { get ; set ; }
        public List<Cell> Cells => _cells.ToList();
        public bool Crowded => !_cells.Any(x => x.State == States.Empty);
        private States _state;

        public event EventState SetState;

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
    }
}
