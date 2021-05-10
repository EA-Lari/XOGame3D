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
                var artist = new PlayingAreaArtist(_controller);
                artist.DrawArea();
                Console.ReadKey();
                return;
            }
        }
    }
}
