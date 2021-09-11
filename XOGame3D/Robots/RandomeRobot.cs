using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XOGame3D.Enum;
using XOGame3D.Interfaces;
using XOGame3D.Logic;

namespace XOGame3D.Robots
{
    public class RandomeRobot
    {
        private readonly GameController _game;
        private readonly IUser _myUser;
        private readonly Random _random;
        private bool _isCurrent;

        public RandomeRobot(Random random, GameController game, IUser myUser)
        {
            this._game = game;
            this._random = random;
            _myUser = myUser;
        }

        private void MakeMove()
        {
            if (_game.GetCurrentArea() == null) 
                ChooseArea();
            ChooseCell();
        }

        private void ChooseArea(int repid = 100)
        {
            try
            {
                repid--;
                var rowArea = GetIntRandome();
                var columnArea = GetIntRandome();
                _game.SetCurrentArea(rowArea, columnArea);
            }
            catch(Exception e)
            {
                if (repid == 0)
                    throw new Exception($"Robot can't choose next Area, because: {e.Message}");
                ChooseArea(repid);
            }
        }

        private void ChooseCell(int repid = 100)
        {
            try
            {
                repid--;
                var rowArea = GetIntRandome();
                var columnArea = GetIntRandome();
                _game.SetState(rowArea, columnArea);
            }
            catch (Exception e)
            {
                if (repid == 0)
                    throw new Exception($"Robot can't choose Cell, because: {e.Message}");
                ChooseArea(repid);
            }
        }

        private int GetIntRandome()
            => _random.Next(0, 2);
    }
}
