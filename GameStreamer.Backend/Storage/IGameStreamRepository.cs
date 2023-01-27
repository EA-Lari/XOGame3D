using GameStreamer.Backend.DTOs;
using GameStreamer.Backend.DTOs.DataAccess;
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

        public PlayerDataResponseDTO AddNewPlayer(PlayerWithHashDto addedPlayer);

        public PlayerWithHashDto GetNewPlayerBy(Guid playerDataHashGuid);

        public PlayerWithHashDto GetPlayerWithRoomBy(Guid playerDataHashGuid);

        public PlayerDataResponseDTO UpdateNewPlayer(PlayerWithHashDto updatedPlayer);

        public PlayerDataResponseDTO UpdatePlayerWithRoom(PlayerWithHashDto updatedPlayer);

        #endregion

    }
}