using GameStreamer.Backend.Storage.GameStreamerDbase.Entities;

namespace GameStreamer.Backend.Storage
{
    public interface IRoomRepository : IDisposable
    {
        IEnumerable<RoomEntity> GetAllRooms();
        
        RoomEntity GetRoomById(int roomId);
        
        void InsertRoom(RoomEntity room);
        
        void DeleteRoom(int roomId);
        
        void UpdateRoom(RoomEntity room);

        void Save();
    }
}