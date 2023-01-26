using GameStreamer.Backend.DTOs;
using GameStreamer.Backend.Models;
using GameStreamer.Backend.Storage.GameStreamerDbase.Entities;

namespace GameStreamer.Backend.Storage
{
    public interface IGameStreamRepository : IDisposable
    {

        #region Rooms

        IQueryable<RoomEntity> GetAllRooms();

        RoomEntity GetRoomById(int roomId);

        void InsertRoom(RoomEntity room);

        void DeleteRoom(int roomId);

        void UpdateRoom(RoomEntity room);

        #endregion

        #region Players

        public PlayerDataResponseDTO AddPlayer(PlayerFromRoomHub forAdd);

        public PlayerFromRoomHub GetPlayerBy(string roomHubConnectionId);

        public PlayerDataResponseDTO UpdatePlayer(PlayerFromRoomHub playerFromRoom);

        #endregion

    }
}