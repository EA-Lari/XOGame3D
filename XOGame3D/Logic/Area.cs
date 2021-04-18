using System;
using System.Collections.Generic;
using System.Linq;
using XOGame3D.Enum;
using XOGame3D.Models;

namespace XOGame3D.Logic
{
    internal abstract class Area<T> where T: ICell,new()
    {
        private int _sizeArea = 3;
        private States _winner = States.Empty;

        public event EventHandler SetWinner;
        public List<T> _cells;
        public States Winner
        {
            get => _winner;
            private set
            {
                if (value == States.O || value == States.X)
                {
                    SetWinner?.Invoke(this, new EventArgs());
                }
                _winner = value;
            }
        }

        public ICell LastCell { get; private set; }

        public Area()
        {
            for (int x = 0; x < _sizeArea; x++)
            {
                for (int y = 0; y < _sizeArea; y++)
                {
                    var cell = new T() { Ox = x, Oy =y };
                    cell.SetState += FindWinner;
                    _cells.Add(cell);
                }
            }
        }

        
        //public virtual void SetState(T cell)
        //{
        //    _cells.FirstOrDefault(x => x.Ox == cell.Ox 
        //                            && x.Oy == cell.Oy)
        //        .State = cell.State;
        //}

        

    }
}
