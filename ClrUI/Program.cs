using ConsoleUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using XOGame3D.Interfaces;
using XOGame3D.Logic;

namespace ConsoleUI
{
    class Program
    {
        private static IUser _player1;
        private static IUser _player2;

        static void Main(string[] args)
        {
            try
            {
                Console.SetWindowSize(100,40);
                Console.WriteLine("Hello World!");
                _player1 = GeneratePlayers();
                _player2 = GeneratePlayers();
                _player1.Fraction = XOGame3D.Enum.States.X;
                _player2.Fraction = XOGame3D.Enum.States.O;
                _controller = new TicTacToeLogic();
                _controller.SetWinner += _controller_SetWinner;
                Play();
                Console.WriteLine("Game Over!" +
                "\nPress any key for Exit.");
                Console.ReadKey();
            }
            catch(Exception e)
            {
                Console.WriteLine($"Unhandled error: {e.Message}." +
                    $"Press any key for Exit.");
                Console.ReadKey();
            }
        }

        private static TicTacToeLogic _controller;

        private static bool GameOver { get;set; }

        private static void _controller_SetWinner(object sender, XOGame3D.Enum.States states)
        {
            GameOver = true;
            if (_controller.WinnerState == XOGame3D.Enum.States.Empty)
                Console.WriteLine("Draw");
            else
            {
                if(_player1.Fraction == _controller.WinnerState)
                    Console.WriteLine($"{_player1.Name} winner!");

                if (_player2.Fraction == _controller.WinnerState)
                    Console.WriteLine($"{_player2.Name} winner!");
            }
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
                var artist = new PlayingAreaArtist(_controller);
                artist.DrawArea();
                Console.WriteLine();
                var currentUser = GetCurrentPlayer();
                Console.WriteLine($"Player '{currentUser.Name}' turn");
                MakeMove();
            }
        }

        private static IUser GetCurrentPlayer()
        {
            if (_controller.CurrenState == _player1.Fraction)
                return _player1;
            return _player2;
        }

        private static void MakeMove()
        {
            try
            {
                var currentArea = _controller.GetCurrentArea();
                if (currentArea == null)
                {
                    Console.WriteLine("Enter current area(column, row):");
                    int rowCurrent, columnCurrent;
                    EnterCoordinates(out columnCurrent, out rowCurrent);
                    _controller.SetCurrentArea(rowCurrent, columnCurrent);
                }
                else
                {
                    Console.WriteLine("Enter cell (column, row):");
                    int row, column;
                    EnterCoordinates(out column, out row);
                    _controller.SetState(row, column);
                }
            }
            catch(ApplicationException e)
            {
                PrintError($"Warnning! {e.Message}." +
                    $"\nPress key Enter to continue." +
                    $"\nPress key Esc to exit.");
                var key = Console.ReadKey();
                if (key.Key == ConsoleKey.Escape)
                    GameOver = true;
            }
        }

        private static void EnterCoordinates(out int column, out int row)
        {
            var current = Console.ReadLine().Split(",");
            if(current.Length != 2)
                throw new ApplicationException("Coordinats must be Enter with ','." +
                    "\nFirst value is column, second is row.");
            column = Convert.ToInt32(current[0]) - 1;
            row = Convert.ToInt32(current[1]) - 1;
            if (!((column >= 0 && column <= 2)
                || (row >= 0 && row <= 2)))
                throw new ApplicationException("Coordinats must be from 1 to 3");
        }

        private static void PrintError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}
