using Microsoft.AspNetCore.SignalR;

namespace GameStreamer.Backend.Hubs
{

    public class RoomHub : Hub<IGameClient>
    {

        /// <summary>
        /// Метод входа в выделенную комнату, если выбран режим "Игра с другом"
        /// </summary>
        /// <param name="roomId">Id комнаты</param>
        /// <returns></returns>
        public Task JoinDedicatedRoom(int roomId)
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// Метод входа в любую комнату, если выбран режим "Случайная игра"
        /// </summary>
        /// <returns></returns>
        public Task JoinRandomRoom()
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// Метод выхода из комнаты
        /// </summary>
        /// <param name="roomId">Id комнаты</param>
        /// <returns></returns>
        public Task LeaveRoom(int roomId)
        {
            return Task.CompletedTask;
        }

    }
}