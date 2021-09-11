using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XOGame3D.Enum;
using XOGame3D.Logic;

namespace AspNetTicTacToe.Signal
{
    public class GameHub:Hub
    {
        GameController _controller;

        public GameHub(GameController controller)
        {
            _controller = controller;
            _controller.SetWinner +=  _controller_SetWinner;
        }

        public void SetState(States state, int row, int column)
        {
            _controller.SetState(row, column);
            Clients.All.SendAsync("NextPlayer",_controller.CurrenUser);
            Clients.All.SendAsync("NextArea", _controller.GetCurrentArea());
        }

        private void _controller_SetWinner(object sender, States states)
        {
            Clients.All.SendAsync("Winner", states);
        }
    }
}
