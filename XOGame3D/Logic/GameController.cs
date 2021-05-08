using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XOGame3D.Enum;
using XOGame3D.Interfaces;
using XOGame3D.Models;

namespace XOGame3D.Logic
{
    public class GameController
    {
        BigArea _bigArea { get; set; }

        public delegate void Winner(States states);
        public event Winner SetWinner;

        public IUser User1 { get; set; }

        public IUser User2 { get; set; }

        public IUser CurrenUser { get; private set; }

        public IUser WinnerUser { get; set; }

        public GameController (IUser user1, IUser user2)
        {
            User2 = user2;
            User1 = user1;
            User1.Fraction = States.X;
            User2.Fraction = States.O;
            CurrenUser = User1;
        }

        public void SetState(ICell cell)
        {
            var state = CurrenUser.Fraction;
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
            CurrenUser = CurrenUser == User1 ? User2 : User1;
        }

        private void CheckStateInArea(IArea<ICell> area, States state)
        {
            if (area == null) return;
            var checker = new AreaChecked(area);
            var newState = checker.CheckByState(state);
            if (newState != States.Empty)
            {
                if (area is ICell cellArea)
                    CheckStateInArea(cellArea.ParentArea, newState);
                FindedWinner(area);
            }
        }

        private void FindedWinner(IArea<ICell> area)
        {
            if (area == _bigArea)
            {
                SetWinnerUser(_bigArea.State);
                SetWinner?.Invoke(_bigArea.State);
            }
        }
        private void SetWinnerUser(States winnerState)
        {
            if (User1.Fraction == winnerState) WinnerUser = User1;
            if (User2.Fraction == winnerState) WinnerUser = User2;
        }

        private void SetNextArea(ICell cell)
        {
            _bigArea.CurrentCell = _bigArea.Cells.Single(x => cell.Equals(x));
            if (!_bigArea.CurrentCell.Cells.Any(x => x.State == States.Empty))
                _bigArea.CurrentCell = null;
        }

        public IArea<ICell> GetCurrentArea() => _bigArea.CurrentCell as IArea<ICell>;

        public IArea<ICell> GetBigArea() => _bigArea as IArea<ICell>;
    }
}
