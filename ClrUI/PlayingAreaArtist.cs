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
        private GameController _controller;
        private int _sizeBigArea, _sizeSmallArea;
        private int _higthCell = 3;

        public PlayingAreaArtist(GameController controller)
        {
            _controller = controller;
            _bigArea = _controller.GetBigArea();
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
                DrawHorisontalMain();

                for (int i = 0; i < _sizeSmallArea; i++)
                {
                    DrawRow(_higthCell);
                }
            }
        }

        private void DrawHorisontalMain()
        {
            Console.WriteLine();
            var breakSymbol = " ";
            var columnSymbol = "______";
            for (int i = 0; i < _sizeBigArea; i++)
            {
                Console.Write(breakSymbol);
                for (int j = 0; j < _sizeSmallArea; j++)
                    Console.Write(columnSymbol);
                Console.Write(breakSymbol);
            }
        }

        private void DrawRow(int hight)
        {
            for (int h = 1; h < hight; h++)
            {
                DrawRowAreas("     |");
            }
            DrawRowAreas("_____|");
        }

        private void DrawRowAreas(string columnSymbol)
        {
            Console.WriteLine();
            Console.Write("||");
            for (int i = 0; i < _sizeBigArea; i++)
            {
                Console.ForegroundColor= ConsoleColor.Blue;
                for (int j = 0; j < _sizeSmallArea; j++)
                    Console.Write(columnSymbol);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("||");
            }
        }
    }
}
