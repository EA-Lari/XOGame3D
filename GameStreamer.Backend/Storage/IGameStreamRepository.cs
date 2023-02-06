using GameStreamer.Backend.DTOs;
using GameStreamer.Backend.DTOs.DataAccess;

namespace GameStreamer.Backend.Storage
{
    public interface IGameStreamRepository : IDisposable
    {

        #region Rooms

        RoomDto? GetFirstIncompleteRoom();

        RoomDto? GetRoomBy(Guid roomGuid);

        void AddRoom(RoomDto roomForAdd);

        void UpdateRoom(RoomDto roomForUpdate);

        void DeleteRoom(RoomDto roomForDelete);

        #endregion

        #region Players

        PlayerDto GetPlayerBy(Guid playerDataHashGuid);

        PlayerDataResponseDTO UpdatePlayer(PlayerDto playerForUpdate);

        PlayerDataResponseDTO DeletePlayer(PlayerDto playerForDelete);

        #endregion

    }
}