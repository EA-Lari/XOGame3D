using System.Linq;
using TicTacToeGame.WPF.Models;

namespace TicTacToeGame.WPF
{
    public class GameService
    {
        public BigAreaModel CreateGame()
        {
            BigAreaModel bigAreaModel = new BigAreaModel();
            return bigAreaModel;
        }

        public void CheckWin(MiniAreaModel area, States cellState)
        {
            
            var checkedCells = area.CellsList.Where(x => x.CellState == cellState);

            var diagonalRight = 0;
            var diagonalLeft = 0;            

            for (int x = 0; x < area.Size; x++)
            {
                var horizontLines = 0;
                var verticalLines = 0;

                for (int y = 0; y < area.Size; y++)
                {
                    horizontLines += checkedCells.Any(d => d.Coordinates.CoordX == x && d.Coordinates.CoordY == y) ? 1 : 0;
                    verticalLines += checkedCells.Any(d => d.Coordinates.CoordX == y && d.Coordinates.CoordY == x) ? 1 : 0;
                }

                if ( (verticalLines == 3 || horizontLines == 3) && area.AreaState == State.Empty )
                {
                    SetWinner(area, cellState);                    
                    return;
                }

                diagonalRight += checkedCells.Any(d => d.Coordinates.CoordX == x && d.Coordinates.CoordY == x) ? 1 : 0;
                diagonalLeft += checkedCells.Any(d => d.Coordinates.CoordX == x && d.Coordinates.CoordY == area.Size - x - 1) ? 1 : 0;
            }            

            if ( (diagonalRight == 3 || diagonalLeft == 3) && area.AreaState == State.Empty )
            {
                SetWinner(area, cellState);
                return;
            }

            CheckDraw(area);
        }

        private void SetWinner(Area<Cell> area, States winState)
        {
            // TODO похоже на костыль, исправить, в универсальном классе не должны быть реальные объекты
            if ( area is MiniAreaModel model)
            {
                model.CellState = winState;
            }
            else
            {
                area.AreaState = winState;
            }
        }

        private void CheckDraw(MiniAreaModel area)
        {
            bool flagOfDraw = area.CellsList.All(x => x.CellState != States.Empty);
            if (flagOfDraw)
            {
                area.AreaState = States.Draw;
            }
        }        
    }
}
