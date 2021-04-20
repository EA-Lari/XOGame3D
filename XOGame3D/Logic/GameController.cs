using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XOGame3D.Enum;
using XOGame3D.Models;

namespace XOGame3D.Logic
{
    public class GameController
    {
        BigArea _bigArea { get; set; }

        public delegate void Winner(States states);
        public event Winner SetWinner;

        public void SetState(Cell cell, States state)
        {
            if (_bigArea.State != States.Empty)
                throw new ApplicationException("Невозможно продолжить игру!" +
                    "\nИгра завершина.");
            if (cell.State != States.Empty)
                throw new ArgumentException("Невозможно изменить заполненное поле");
            cell.State = state;
            var area = cell.ParentArea;
            if (area == null) return;
            if (area.State != States.Empty) return;
            CheckStateInArea(area, cell.State);
            SetNextArea(cell);
            if (_bigArea.State != States.Empty)
                SetWinner?.Invoke(_bigArea.State);
        }

        private void CheckStateInArea(IArea<ICell> area, States state)
        {
            if (area == null) return;
            var checker = new AreaChecked(area);
            var newState = checker.CheckByState(state);
            if (newState != States.Empty)
                if (area is ICell cellArea)
                    CheckStateInArea(cellArea.ParentArea, newState);
        }

        private void SetNextArea(Cell cell)
        {
            _bigArea.CurrentArea = _bigArea.Cells.Single(x => cell.Equals(x));
            if (!_bigArea.CurrentArea.Cells.Any(x => x.State == States.Empty))
                _bigArea.CurrentArea = null;
        }
    }
}
