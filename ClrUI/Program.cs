using ConsoleUI.Models;
using System;
using XOGame3D.Interfaces;
using XOGame3D.Logic;
using XOGame3D.Robots;

namespace ConsoleUI
{
    class Program
    {
        private static IUser _player1;
        private static IUser _player2;
        private static TicTacToeController _controller;

        static void Main(string[] args)
        {
            try
            {
                Console.SetWindowSize(100,40);
                Console.WriteLine("Hello World!");
                var logic = new TicTacToeLogic();                
                ChooseGameMode();
                _controller = new TicTacToeController(logic, _player1, _player2);
                _player1.Fraction = XOGame3D.Enum.States.X;
                _player2.Fraction = XOGame3D.Enum.States.O;
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

        private static bool GameOver { get;set; }

        private static void ChooseGameMode()
        {
            var isChoose = false;
            Console.WriteLine("Choose Game Mode:" +
                "\n1 Player with Player" +
                "\n2 Robot with Robot" +
                "\n3 Player with Robot");
            while (!isChoose)
            {
                var cki = Console.ReadKey();
                switch (cki.Key)
                {
                    case ConsoleKey.D1:
                        _player1 = GeneratePlayers();
                        _player2 = GeneratePlayers();
                        isChoose = true;
                        break;
                    case ConsoleKey.D2:
                        _player1 = GenerateRobot(1);
                        _player2 = GenerateRobot(2);
                        isChoose = true;
                        break;
                    case ConsoleKey.D3:
                        _player1 = GeneratePlayers();
                        _player2 = GenerateRobot(2);
                        isChoose = true;
                        break;
                    default:
                        break;
                }
            }
        }

        private static void _controller_SetWinner(object sender, XOGame3D.Enum.States states)
        {
            GameOver = true;
            if (states == XOGame3D.Enum.States.Empty)
                Console.WriteLine("Draw");
            else
            {
                if(_player1.Fraction == states)
                    Console.WriteLine($"{_player1.Name} winner!");

                if (_player2.Fraction == states)
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

        private static IUser GenerateRobot(int num)
        {
            var player = new RandomRobot(new Random())
                { Name=$"Robot_#{num}"};
            return player;
        }

        private static void Play()
        {
            while (!GameOver)
            {
                var artist = new PlayingAreaArtist(_controller);
                artist.DrawArea();
                Console.WriteLine();
                var currentUser = _controller.GetCurrenUser();
                Console.WriteLine($"Player '{currentUser.Name}' turn");
                MakeMove();
            }
        }

        private static void MakeMove()
        {
            try
            {
                _controller.MakeMove();
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

        private static void PrintError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}
