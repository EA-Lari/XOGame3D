using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XOGame3D.Enum;
using XOGame3D.Models;

namespace XOGame3D.Logic
{
    class AreaController
    {
        public IArea<ICell> Area { get; }

        public AreaController(IArea<ICell> area)
        {
            Area = area;
        }

        public void SetState(States states, ICell cell)
        {
            cell.State = states;

            Area.CurrentCell = cell;
            if (Area.State != States.Empty) return;

            if (CheckWin(States.X)) Area.State = States.X;
            if (CheckWin(States.O)) Area.State = States.O;
            if (CheckDraw()) Area.State = States.Draw;
        }

        /// <summary>
        /// Проверка на ничью(момент когда на поле сложилась такая ситуация, когда никто не может выйграть)
        /// </summary>
        /// <returns></returns>
        private bool CheckDraw()
            => !(CheckWin(States.X, true) || CheckWin(States.O, true));

        /// <summary>
        /// Проверка на наличие победителя
        /// </summary>
        /// <param name="state">По какому показателю проверка</param>
        /// <param name="forDraw">все пустые ячейки считаются как установленные в state</param>
        /// <returns>true - state победил, иначе false</returns>
        private bool CheckWin(States state, bool forDraw = false)
        {
            var cellsState = Area.Cells
                .Where(x => x.State == state)
                .ToList();

            if (forDraw)
                cellsState.AddRange(Area.Cells.Where(x => x.State == States.Empty));

            var diagonalR = 0;
            var diagonalL = 0;

            for (int x = 0; x < Area.Size; x++)
            {
                var vertical = 0;
                var horisontal = 0;

                for (int y = 0; y < Area.Size; y++)
                {
                    vertical += cellsState.Any(d => d.Ox == x && d.Oy == y) ? 1 : 0;
                    horisontal += cellsState.Any(d => d.Ox == y && d.Oy == x) ? 1 : 0;
                }

                if (vertical == 3 || horisontal == 3) return true;

                diagonalR += cellsState.Any(d => d.Ox == x && d.Oy == x) ? 1 : 0;
                diagonalL += cellsState.Any(d => d.Ox == x && d.Oy == Area.Size - x - 1) ? 1 : 0;
            }

            if (diagonalR == 3 || diagonalL == 3) return true;

            return false;
        }
    }
}
