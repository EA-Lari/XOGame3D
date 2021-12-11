using System;
using XOGame3D.Enum;
using XOGame3D.Interfaces;
using XOGame3D.Logic;

namespace XOGame3D.Robots
{
    public class RandomRobot : IUser
    {
        private readonly TicTacToeLogic _game;
        private readonly IRobotInteraction _interaction;
        private readonly Random _random;

        public string Name { get; set; }
        public States Fraction { get; set; }

        public RandomRobot(Random random, TicTacToeLogic game, IRobotInteraction interaction = null)
        {
            this._game = game;
            this._random = random;
            _interaction = interaction;
            _game.ChangeCurrentState += _game_ChangeCurrentState;
        }

        private void _game_ChangeCurrentState(object sender, States e)
        {
            if (Fraction == e)
                MakeMove();
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
                var rowArea = GetIntRandom();
                var columnArea = GetIntRandom();
                if (_interaction == null)
                    _game.SetCurrentArea(rowArea, columnArea);
                else
                    _interaction.SetArea(rowArea, columnArea);
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
                var rowArea = GetIntRandom();
                var columnArea = GetIntRandom();
                if(_interaction == null)
                    _game.SetState(rowArea, columnArea);
                else
                    _interaction.SetArea(rowArea, columnArea);
            }
            catch (Exception e)
            {
                if (repid == 0)
                    throw new Exception($"Robot can't choose Cell, because: {e.Message}");
                ChooseArea(repid);
            }
        }

        private int GetIntRandom()
            => _random.Next(0, 2);
    }
}
