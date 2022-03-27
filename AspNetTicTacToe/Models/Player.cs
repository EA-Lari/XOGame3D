using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XOGame3D.Enum;
using XOGame3D.Interfaces;

namespace AspNetTicTacToe.Models
{
    public class Player : IUser
    {
        public string Name { get; set; }
        public States Fraction { get; set; }
    }
}
