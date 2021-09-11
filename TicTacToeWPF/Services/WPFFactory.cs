using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XOGame3D.Enum;
using XOGame3D.Interfaces;
using XOGame3D.Logic;

namespace TicTacToeWPF.Services
{
    public static class WPFFactory
    {
        public static GameController GetGameController()
        {
            var user1 = new User { 
                State = States.O,
                Name = "Player1"
            };
            var user2 = new User{
                State = States.X,
                Name = "Player2"
            };
            return new GameController(user1, user2);
        }

        private class User : IUser
        {
            public string Name { get; set; }
            public States State { get; set; }
            public bool IsWinner { get; set; }
        }

        public static IMapper GetMapper()
        {
            var configuration = new MapperConfiguration(cfg => {
                cfg.AddProfile<ConvertProfile>();
            });
            return configuration.CreateMapper();
        }
    }
}
