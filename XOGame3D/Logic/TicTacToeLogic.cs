using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XOGame3D.Enum;
using XOGame3D.Interfaces;
using XOGame3D.Models;

namespace XOGame3D.Logic
{
    public class TicTacToeLogic
    {
        private States _currentState;
        BigArea BigArea { get; set; }
        
        public event EventHandler<States> SetWinner;
        public event EventHandler<IUser> ChangeCurrentUser;

        public States CurrenState => _currentState;

        public States WinnerState { get; set; }

        public TicTacToeLogic()
        {
            Reset();
        }

        /// <summary>
        /// Set the current state in cell by number row and column
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        public void SetState(int row, int column)
        {
            var area = BigArea.CurrentCell as IArea;
            var cell = area.Cells.Single(x => x.Row == row && x.Column == column);
            SetState(cell);
        }

        private void SetState(ICell cell)
        {
            if (BigArea.State != States.Empty)
                throw new ApplicationException("Невозможно продолжить игру!" +
                    "\nИгра завершина.");
            if (cell.State != States.Empty)
                throw new ApplicationException("Невозможно изменить заполненное поле");
            cell.State = _currentState;
            var area = cell.ParentArea;
            if (area == null) return;
            if (area.State == States.Empty) 
                CheckStateInArea(area, cell.State);
            SetNextArea(cell);
            if (BigArea.State != States.Empty)
                SetWinner?.Invoke(this, BigArea.State);
            ChangeCurrent();            
        }

        private void ChangeCurrent()
        {
            if (_currentState == States.X)
                _currentState = States.O;
            else
                _currentState = States.X;
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
            if (area == BigArea)
            {
                SetWinnerUser(BigArea.State);
                SetWinner?.Invoke(this, BigArea.State);
            }
        }

        private void SetWinnerUser(States winnerState)
        {
            WinnerState = winnerState;
        }

        private void SetNextArea(ICell cell)
        {
            var smallArea = BigArea.Cells.Single(x => cell.Equals(x)) as SmallArea;
            BigArea.CurrentCell = smallArea;
            if (!smallArea.Cells.Any(x => x.State == States.Empty))
                BigArea.CurrentCell = null;
        }

        /// <summary>
        /// Return current area for next turn player
        /// </summary>
        /// <returns></returns>
        public IArea GetCurrentArea() => BigArea.CurrentCell as IArea;

        /// <summary>
        /// Set area for turn, allow if there is no current area
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        
        public void SetCurrentArea(int row, int column)
        {
            var cell = BigArea.Cells
                .Single(x => x.Row == row && x.Column == column);
            SetCurrentArea(cell);
        }

        private void SetCurrentArea(ICell cell)
        {
            if (BigArea.CurrentCell != null)
                throw new Exception("Current Cell not empty");
            var area = cell as IArea;
            if (!area.Cells.Any(x => x.State == States.Empty))
                throw new Exception("Area isn't contains empty cell");
            BigArea.CurrentCell = cell;
        }

        /// <summary>
        /// Get Main plaing area
        /// </summary>
        /// <returns></returns>
        public IArea GetBigArea() => BigArea as IArea;

        /// <summary>
        /// Convert cell to area(working for only small area)
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        public IArea GetAllSmallAreasByCell(ICell cell)
        {
            return BigArea.Cells.Single(x => x == cell) as IArea;
        }

        public void Reset()
        {
            BigArea = BigArea.GetBigArea();
            _currentState = States.X;
            WinnerState = States.Empty;
        }
    }
}
