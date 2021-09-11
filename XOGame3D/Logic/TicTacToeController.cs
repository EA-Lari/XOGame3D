using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XOGame3D.Enum;
using XOGame3D.Interfaces;

namespace XOGame3D.Logic
{
    public class TicTacToeController
    {
        private readonly TicTacToeLogic _logic;
        private readonly IUser _user1;
        private readonly IUser _user2;

        public TicTacToeController(TicTacToeLogic logic, IUser user1, IUser user2)
        {
            _logic = logic;
            _user1 = user1;
            _user2 = user2;
        }

        public void ChooseUserFraction(IUser user, States state)
        {
            if (_user1.State != States.Empty || _user2.State != States.Empty)
                throw new Exception("User state was choose");
            if (_user1 == user)
            {
                _user1.State = state;
                _user2.State = state == States.X ? States.O : States.X;
            }
            else
            {
                _user2.State = state;
                _user1.State = state == States.X ? States.O : States.X;
            }
        }
    }
}
