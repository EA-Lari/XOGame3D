using System;
using System.Collections.Generic;
using System.Text;
using XOGame3D.Enum;
using XOGame3D.Interfaces;

namespace ConsoleUI.Models
{
    class Player : IUser
    {
        public string Name { get; set; }

        public States Fraction { get; set; }

        public bool IsWinner { get; set; }

        public bool IsCurrent { get; set; }

        public Player(string name)
        {
            Name = name;
        }
    }
}
