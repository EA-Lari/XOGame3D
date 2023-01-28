using GameStreamer.Backend.DTOs;
using GameStreamer.Backend.DTOs.DataAccess;
using GameStreamer.Backend.Models;
using GameStreamer.Backend.Storage.GameStreamerDbase.Entities;

namespace GameStreamer.Backend.Storage
{
    public interface IGameStreamRepository : IDisposable
    {

        #region Rooms

        RoomDto GetRoomBy(Guid roomGuid);

        void AddRoom(RoomDto roomForAdd);

        void UpdateRoom(RoomDto roomForUpdate);

        void DeleteRoom(RoomDto roomForDelete);

        #endregion

        #region Players

        PlayerDataResponseDTO AddPlayer(PlayerDto playerForAdd);

        PlayerDto GetPlayerBy(Guid playerDataHashGuid);

        PlayerDataResponseDTO UpdatePlayer(PlayerDto playerForUpdate);

        PlayerDataResponseDTO DeletePlayer(PlayerDto playerForDelete);

        #endregion

    }
}