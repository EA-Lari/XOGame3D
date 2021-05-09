using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XOGame3D.Enum;
using XOGame3D.Interfaces;
using XOGame3D.Models;

namespace XOGame3D.Logic
{
    class AreaChecked
    {
        public IArea Area { get; }

        public AreaChecked(IArea area)
        {
            Area = area;
        }
        
        /// <summary>
        /// Проверка поля на выйгрышь статуса
        /// </summary>
        /// <param name="states">статус по которому нужно проверить выйгрышь</param>
        /// <returns>Статус установленный полю</returns>
        public States CheckByState(States states)
        {
            var newState = Area.State;
            if (states != States.Draw)
                if (CheckWin(states))
                    newState = states;
            if (CheckDraw()) newState = States.Draw;
            return newState;
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
                cellsState.AddRange(Area.Cells
                    .Where(x => x.State == States.Empty));

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
