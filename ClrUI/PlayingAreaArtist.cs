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

        public PlayingAreaArtist(GameController controller)
        {
            _controller = controller;
            _bigArea = _controller.GetBigArea();
        }

        public void DrawArea()
        {
            var maxSize = 3;
            Console.Clear();

            //for (int rowArea = 1; rowArea <= maxSize; rowArea++)
            //{
            //    var cellsArea = _bigArea.Cells
            //        .Where(x => x.Ox == rowArea);

            //    var areas = _bigArea.Cells
            //        .Where(x => x.Ox == rowArea)
            //        .Select(x => _controller.GetAllSmallAreasByCell(x));

                var size = _bigArea.Size;
                for (int j = 0; j < size; j++)
                {
                    DrawHorisontalMain(size);

                    for (int i = 0; i < size; i++)
                    {
                        DrawRow(3, size);
                    }
                }
                DrawHorisontalMain(size);
            //}
        }

        private void DrawHorisontalMain(int size)
        {
            Console.WriteLine();
            var breakSymbol = " ";
            var columnSymbol = "______";
            for (int i = 0; i < size; i++)
            {
                Console.Write(breakSymbol);
                for (int j = 0; j < size; j++)
                    Console.Write(columnSymbol);
                Console.Write(breakSymbol);
            }
        }

        private void DrawRow(int hight, int count)
        {
            for (int h = 0; h < hight; h++)
            {
                DrawRowAreas("     |", count);
            }
            DrawRowAreas("_____|", count);
        }

        private void DrawRowAreas(string columnSymbol, int count)
        {
            Console.WriteLine();
            Console.Write("||");
            for (int i = 0; i < count; i++)
            {
                for (int j = 0; j < count; j++)
                    Console.Write(columnSymbol);
                Console.Write("||");
            }
        }
    }
}
