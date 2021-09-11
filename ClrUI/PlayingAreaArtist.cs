using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XOGame3D.Interfaces;
using XOGame3D.Logic;

namespace ConsoleUI
{
    internal class PlayingAreaArtist
    {
        private IArea _bigArea;
        private TicTacToeLogic _controller;
        private int _sizeBigArea, _sizeSmallArea;
        private ConsoleColor _colorX, _colorO;

        public PlayingAreaArtist(TicTacToeLogic controller, 
            ConsoleColor colorX = ConsoleColor.Yellow,
            ConsoleColor colorO = ConsoleColor.Green)
        {
            _controller = controller;
            _bigArea = _controller.GetBigArea();
            _colorX = colorX;
            _colorO = colorO;
        }

        public void DrawArea()
        {
            Console.Clear();
            
            var areas = _bigArea.Cells
                .Select(x => _controller.GetAllSmallAreasByCell(x));

            _sizeBigArea = _bigArea.Size;
            _sizeSmallArea = areas.FirstOrDefault().Size;

            for (int j = 0; j < _sizeBigArea; j++)
            {
                DrawHorisontalMain(j);

                for (int i = 0; i < _sizeSmallArea; i++)
                {
                    DrawCellsInRow(j,i);
                }
            }
            PrintInfo();
        }

        private void PrintInfo()
        {
            Console.ForegroundColor = _colorX;
            Console.WriteLine();
            Console.WriteLine($"Small area has {_colorX} when State equels X.");
            Console.ForegroundColor = _colorO;
            Console.WriteLine($"Small area has {_colorO} when State equels O.");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Current cell is red color.");
            Console.ResetColor();
            Console.WriteLine($"Coordinate has value from 1 to 3.");
        }

        private void DrawHorisontalMain(int bigRow)
        {
            Console.WriteLine();
            var breakSymbol = " ";
            var columnSymbol = "______";
            for (int i = 0; i < _sizeBigArea; i++)
            {
                var bigCell = _bigArea.Cells
                   .Single(x => x.Column == i && x.Row == bigRow);
                var smallArea = bigCell as IArea;
                if (smallArea == _controller.GetCurrentArea())
                    Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(breakSymbol);
                for (int j = 0; j < _sizeSmallArea; j++)
                    Console.Write(columnSymbol);
                Console.Write(breakSymbol);
                Console.ResetColor();
            }
        }

        private void DrawCellsInRow(int bigRow, int smallRow)
        {
            DrawEmptyRow(bigRow,smallRow, "     |");
            DrawEmptyRow(bigRow, smallRow);
            DrawEmptyRow(bigRow,smallRow, "_____|");
        }

        private void DrawEmptyRow(int bigRow, int smallRow, string columnSymbol = null)
        {
            Console.WriteLine();
            Console.Write("|");
            for (int i = 0; i < _sizeBigArea; i++)
            {
                var bigCell = _bigArea.Cells
                    .Single(x => x.Column == i && x.Row == bigRow);
                var smallArea = bigCell as IArea;
                if(smallArea == _controller.GetCurrentArea())
                    Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("|");
                Console.ResetColor();
                if (bigCell.State == XOGame3D.Enum.States.O)
                    Console.ForegroundColor = _colorO;
                if (bigCell.State == XOGame3D.Enum.States.X)
                    Console.ForegroundColor = _colorX;

                for (int j = 0; j < _sizeSmallArea; j++)
                {
                    if(string.IsNullOrEmpty(columnSymbol))
                    {
                        var cell = smallArea.Cells
                             .Single(x => x.Row == smallRow && x.Column == j);
                        var state = " ";
                        if (cell.State != XOGame3D.Enum.States.Empty)
                            state = cell.State.ToString();
                        Console.Write($" {state}   |");
                    }    
                    else
                        Console.Write(columnSymbol);
                }

                if (smallArea == _controller.GetCurrentArea())
                    Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("|");
                Console.ResetColor();
            }
            Console.Write("|");
        }
    }
}
