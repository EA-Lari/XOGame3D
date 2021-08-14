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

        public GameController(IUser user1, IUser user2)
        {
            _bigArea = BigArea.GetBigArea();
            User2 = user2;
            User1 = user1;
            User1.Fraction = States.X;
            User2.Fraction = States.O;
            CurrenUser = User1;
        }

        /// <summary>
        /// Set the current state in cell by number row and column
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        public void SetState(int row, int column)
        {
            var area = _bigArea.CurrentCell as IArea;
            var cell = area.Cells.Single(x => x.Row == row && x.Column == column);
            SetState(cell);
        }

        private void SetState(ICell cell)
        {
            var state = CurrenUser.Fraction;
            if (_bigArea.State != States.Empty)
                throw new ApplicationException("Невозможно продолжить игру!" +
                    "\nИгра завершина.");
            if (cell.State != States.Empty)
                throw new ApplicationException("Невозможно изменить заполненное поле");
            cell.State = state;
            var area = cell.ParentArea;
            if (area == null) return;
            if (area.State == States.Empty) 
                CheckStateInArea(area, cell.State);
            SetNextArea(cell);
            if (_bigArea.State != States.Empty)
                SetWinner?.Invoke(_bigArea.State);
            CurrenUser = CurrenUser == User1 ? User2 : User1;
        }

        private void CheckStateInArea(IArea area, States state)
        {
            if (area == null) return;
            var checker = new AreaChecked(area);
            var newState = checker.CheckByState(state);
            if (newState != States.Empty)
            {
                area.State = newState;
                if (area is ICell cellArea)
                    CheckStateInArea(cellArea.ParentArea, newState);
                FindedWinner(area);
            }
        }

        private void FindedWinner(IArea area)
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
            var smallArea = _bigArea.Cells.Single(x => cell.Equals(x)) as SmallArea;
            _bigArea.CurrentCell = smallArea;
            if (!smallArea.Cells.Any(x => x.State == States.Empty))
                _bigArea.CurrentCell = null;
        }

        /// <summary>
        /// Return current area for next turn player
        /// </summary>
        /// <returns></returns>
        public IArea GetCurrentArea() => _bigArea.CurrentCell as IArea;

        /// <summary>
        /// Set area for turn, allow if there is no current area
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        
        public void SetCurrentArea(int row, int column)
        {
            var cell = _bigArea.Cells
                .Single(x => x.Row == row && x.Column == column);
            SetCurrentArea(cell);
        }

        private void SetCurrentArea(ICell cell)
        {
            if (_bigArea.CurrentCell != null)
                throw new Exception("Current Cell not empty");
            _bigArea.CurrentCell = cell;
        }

        /// <summary>
        /// Get Main plaing area
        /// </summary>
        /// <returns></returns>
        public IArea GetBigArea() => _bigArea as IArea;

        /// <summary>
        /// Convert cell to area(working for only small area)
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        public IArea GetAllSmallAreasByCell(ICell cell)
        {
            return _bigArea.Cells.Single(x => x == cell) as IArea;
        }
    }
}
