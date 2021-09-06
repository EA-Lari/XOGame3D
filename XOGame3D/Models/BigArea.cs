using System;
using System.Collections.Generic;
using System.Text;
using XOGame3D.Enum;
using XOGame3D.Interfaces;

namespace XOGame3D.Models
{
    internal sealed class BigArea : IArea
    {
        States _state = States.Empty;
        protected static int _sizeArea = 3;

        private BigArea() 
        {
           Cells = new List<ICell>();
        }

        public static BigArea GetBigArea()
        {
            var bigArea = new BigArea();
            CreateCell<SmallArea>(bigArea);
            foreach (IArea area in bigArea.Cells)
                CreateCell<Cell>(area);
            return bigArea;
        }

        private static void CreateCell<C>(IArea area) where C : ICell, new()
        {
            for (int x = 0; x < _sizeArea; x++)
            {
                for (int y = 0; y < _sizeArea; y++)
                {
                    area.Cells.Add(new C
                    {
                        Column = x,
                        Row = y,
                        ParentArea = area,
                        State = States.Empty
                    });
                }
            }
        }

        public List<ICell> Cells { get; }

        public int Size => _sizeArea;

        public ICell CurrentCell { get; set; }

        public States State
        {
            get => _state;
            set
            {
                if (_state != States.Empty) throw new Exception("Статус уже установлен");
                _state = value;
            }
        }
    }
}
