using ClrUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using XOGame3D.Interfaces;
using XOGame3D.Logic;

namespace ClrUI
{
    
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var player1 = GeneratePlayers();
            var player2 = GeneratePlayers();
            _controller = new GameController(player1, player2);
            _controller.SetWinner += _controller_SetWinner;
            Play();
        }

        private static GameController _controller;
        private static bool GameOver { get;set; }


        private static void _controller_SetWinner(XOGame3D.Enum.States states)
        {
            GameOver = true;
            Console.WriteLine("Game Over!");
            if (_controller.WinnerUser == null)
                Console.WriteLine("Draw");
            else
                Console.WriteLine($"{_controller.WinnerUser.Name} winner!");
        }

        private static IUser GeneratePlayers()
        {
            Console.WriteLine("Enter name Player: ");
            var name = Console.ReadLine();
            var player = new Player(name);
            return player;
        }

        private static void Play()
        {
            while (!GameOver)
            {
                PaintArea();
                Console.ReadKey();
                return;
            }
        }

        private static void PaintArea()
        {
            var maxSize = 3;
            Console.Clear();
            var bigArea = _controller.GetBigArea();

            for (int rowArea = 1; rowArea <= maxSize; rowArea++)
            {
                var cellsArea = bigArea.Cells
                    .Where(x => x.Ox == rowArea);

                var areas = bigArea.Cells
                    .Where(x => x.Ox == rowArea)
                    .Select(x => _controller.GetAllSmallAreasByCell(x));

                DrawHorisontalMain(areas.Count());

                DrawRow(5, areas.ToList());
                DrawHorisontalMain(areas.Count()); 
            }
        }

        private static void DrawHorisontalMain(int count)
        {
            Console.WriteLine();
            var breakSymbol = " ";
            var columnSymbol = "______";
            for (int i = 0; i < count; i++)
            {
                Console.Write(breakSymbol);
                for (int j = 0; j < count; j++)
                    Console.Write(columnSymbol);
                Console.Write(breakSymbol);
            }
        }

        private static void DrawRow(int hight, List<IArea<ICell>> areas)
        {
            for (int h = 0; h < hight; h++)
            {
                DrawRowAreas("     |", areas.Count());
            }
            DrawRowAreas("_____|", areas.Count());
        }

        private static void DrawRowAreas(string columnSymbol, int count)
        {
            Console.WriteLine();
            Console.Write("||");
            for (int i = 1; i < count; i++)
            {
                for (int j = 1; j < count; j++)
                    Console.Write(columnSymbol);
                Console.Write("||");
            }
        }
    }
}
