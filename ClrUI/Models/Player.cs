using System;
using XOGame3D.Enum;
using XOGame3D.Interfaces;
using XOGame3D.Models;

namespace ConsoleUI.Models
{
    class Player : IUser
    {
        public string Name { get; set; }

        public States Fraction { get; set; }

        public bool IsCurrent { get; set; }

        public Player(string name)
        {
            Name = name;
        }

        public Coordinate ChooseCell()
        {
            Console.WriteLine("Enter cell (column, row):");
            int row, column;
            EnterCoordinates(out column, out row);
            return new Coordinate(row, column);
        }

        public Coordinate ChooseArea()
        {
            Console.WriteLine("Enter current area(column, row):");
            int rowCurrent, columnCurrent;
            EnterCoordinates(out columnCurrent, out rowCurrent);
            return new Coordinate(rowCurrent, columnCurrent);
        }

        private void EnterCoordinates(out int column, out int row)
        {
            var current = Console.ReadLine().Split(",");
            if (current.Length != 2)
                throw new ApplicationException("Coordinats must be Enter with ','." +
                    "\nFirst value is column, second is row.");
            column = Convert.ToInt32(current[0]) - 1;
            row = Convert.ToInt32(current[1]) - 1;
            if (!((column >= 0 && column <= 2)
                || (row >= 0 && row <= 2)))
                throw new ApplicationException("Coordinats must be from 1 to 3");
        }
    }
}
