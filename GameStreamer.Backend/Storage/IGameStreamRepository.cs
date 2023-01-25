using GameStreamer.Backend.Storage.GameStreamerDbase.Entities;

namespace GameStreamer.Backend.Storage
{
    public interface IGameStreamRepository : IDisposable
    {
        IQueryable<RoomEntity> GetAllRooms();
        
        RoomEntity GetRoomById(int roomId);
        
        void InsertRoom(RoomEntity room);
        
        void DeleteRoom(int roomId);
        
        void UpdateRoom(RoomEntity room);

        void Save();
    }
}