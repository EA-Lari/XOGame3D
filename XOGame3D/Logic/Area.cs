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

        private void FindWinner(ICell sender)
        {
            LastCell = sender;
            if (Winner != States.Empty) return;
            
            if (CheckWin(States.X)) Winner = States.X; 
            if (CheckWin(States.O)) Winner = States.O;
        }

        //public virtual void SetState(T cell)
        //{
        //    _cells.FirstOrDefault(x => x.Ox == cell.Ox 
        //                            && x.Oy == cell.Oy)
        //        .State = cell.State;
        //}

        private bool CheckWin(States state)
        {
            var cellsState = _cells.Where(x => x.State == state);

            var diagonalR = 0;
            var diagonalL = 0;

            for (int x = 0; x < _sizeArea; x++)
            {
                var vertical = 0;
                var horisontal = 0;

                for (int y = 0; y < _sizeArea; y++)
                {
                    vertical += cellsState.Any(d => d.Ox == x && d.Oy == y) ?  1 : 0;
                    horisontal += cellsState.Any(d => d.Ox == y && d.Oy == x) ? 1 : 0;
                }

                if (vertical == 3 || horisontal == 3) return true;

                diagonalR += cellsState.Any(d => d.Ox == x && d.Oy == x) ? 1 : 0;
                diagonalL += cellsState.Any(d => d.Ox == x && d.Oy == _sizeArea - x - 1) ? 1 : 0;
            }

            if (diagonalR == 3 || diagonalL == 3) return true;

            return false;
        }

    }
}
