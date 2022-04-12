using System;
using XOGame3D.Enum;
using XOGame3D.Interfaces;
using XOGame3D.Models;

namespace XOGame3D.Robots
{
    public class RandomRobot : IUser
    {
        private readonly Random _random;
        private readonly int _repid = 100;

        public string Name { get; set; }
        public States Fraction { get; set; }

        public RandomRobot(Random random)
        {
            this._random = random;
        }

        public Coordinate ChooseCell()
            => ChooseCoordinates(_repid);

        public Coordinate ChooseArea()
            => ChooseCoordinates(_repid);

        private Coordinate ChooseCoordinates(int repid)
        {
            try
            {
                repid--;
                var rowArea = GetIntRandom();
                var columnArea = GetIntRandom();
                return new Coordinate(rowArea, columnArea);
            }
            catch(Exception e)
            {
                if (repid == 0)
                    throw new Exception($"Robot can't choose next Coordinates, because: {e.Message}");
                return ChooseCoordinates(repid);
            }
        }

        private int GetIntRandom()
            => _random.Next(0, 3);
    }
}
