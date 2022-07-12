using Microsoft.AspNetCore.SignalR;

namespace GameStreamer.Backend.Hubs
{
    public class GameHub : Hub
    {
        /// <summary>
        /// Метод добавляет клиентское соединение в группу
        /// </summary>
        /// <returns></returns>
        public Task JoinRoom()
        {
            return Groups.AddToGroupAsync(Context.ConnectionId, $"GameRoom-{Context.ConnectionId}");
        }

        /// <summary>
        /// Метод удаляет клиентское соединение из группы
        /// </summary>
        /// <returns></returns>
        public Task LeaveRoom()
        {
            return Groups.RemoveFromGroupAsync(Context.ConnectionId, $"GameRoom-{Context.ConnectionId}");
        }

    }
}
