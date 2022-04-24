using System;
using XOGame3D.Enum;
using XOGame3D.Interfaces;
using XOGame3D.Logic;

namespace XOGame3D.Controllers
{
    /// <summary>
    /// This game conroller simple integrate in microservice
    /// </summary>
    public class TicTacToeMicroserviceController
    {
        private readonly TicTacToeLogic _logic;
        private readonly IUser _user1;
        private readonly IUser _user2;

        public TicTacToeLogic Play => _logic;

        public TicTacToeMicroserviceController(TicTacToeLogic logic, IUser user1, IUser user2)
        {
            if (_user1.Fraction == States.Empty || _user2.Fraction == States.Empty)
                throw new Exception("Users haven't fraction");
            _logic = logic;
            _user1 = user1;
            _user2 = user2;
            logic.SetWinner += Logic_SetWinner;
        }

        /// <summary>
        /// Choose cell in mini area
        /// </summary>
        /// <param name="user">who make move, must be current user</param>
        /// <param name="coordinate"></param>
        /// <exception cref="Exception">if you are not current user, you have winner in last move, cell have not empty state</exception>
        public void ChooseCell(IUser user, Models.Coordinate coordinate)
        {
            CheckUser(user);
            if (_logic.GetCurrentArea() == null)
                throw new Exception("You must choose area");
            _logic.SetState(coordinate);
        }

        /// <summary>
        /// Choose mini area if there haven't current area
        /// </summary>
        /// <param name="user">who make move, must be current user</param>
        /// <param name="coordinate"></param>
        /// <exception cref="Exception">if you are not current user, you choose crowded mini area,  you have winner in last move, cell have not empty state</exception>
        public void ChooseArea(IUser user, Models.Coordinate coordinate)
        {
            CheckUser(user);
            _logic.SetCurrentArea(coordinate);
        }

        private void CheckUser(IUser user)
        {
            var currentUser = GetCurrenUser();
            if (currentUser == user)
                throw new Exception("This user is not current");
        }

        private void Logic_SetWinner(object sender, States e)
        {
            SetWinner?.Invoke(sender, e);
        }

        /// <summary>
        /// Get user who must make move
        /// </summary>
        /// <returns>Current user</returns>
        /// <exception cref="Exception">If user is broken</exception>
        public IUser GetCurrenUser()
        {
            if (_user1 == null || _user2 == null)
                throw new Exception("Not set user");
            if (_user1.Fraction == States.Empty || _user2.Fraction == States.Empty)
                throw new Exception("Not choose state");
            if (_user1.Fraction == _logic.CurrenState)
                return _user1;
            if (_user2.Fraction == _logic.CurrenState)
                return _user2;
            throw new Exception("Unpossible  return user");
        }

        /// <summary>
        /// Get current mini area for move
        /// </summary>
        /// <returns></returns>
        public IArea GetCurrentArea() => _logic.GetCurrentArea();

        /// <summary>
        /// Get full big area, with states, mini area and cells inside
        /// </summary>
        /// <returns>Full structure of big area</returns>
        public IArea GetBigArea() => _logic.GetBigArea();

        /// <summary>
        /// Convert cell in mini area
        /// </summary>
        /// <param name="cell"></param>
        /// <returns>Mini area that equals cell by coordinates</returns>
        public IArea GetAllSmallAreasByCell(ICell cell) => _logic.GetAllSmallAreasByCell(cell);

        /// <summary>
        /// Invoke when have a winner in big area
        /// </summary>
        public event EventHandler<States> SetWinner;
    }
}
