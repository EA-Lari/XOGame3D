using GameStreamer.Backend.Storage.GameStreamerDbase.Entities;

namespace GameStreamer.Backend.Storage.GameStreamerDbase
{
    public class RoomRepository : IRoomRepository
    {

        private GameStreamerContext _gameStreamerContext;

        public RoomRepository(GameStreamerContext gameStreamerContext)
        {
            _gameStreamerContext = gameStreamerContext;
        }

        public IEnumerable<RoomEntity> GetAllRooms()
        {
            throw new NotImplementedException();
        }

        public void DeleteRoom(int roomId)
        {
            throw new NotImplementedException();
        }

        public RoomEntity GetRoomById(int roomId)
        {
            throw new NotImplementedException();
        }

        public void InsertRoom(RoomEntity room)
        {
            _gameStreamerContext.Set<RoomEntity>().Add(room);
        }

        public void Save()
        {
            _gameStreamerContext.SaveChanges();
        }

        public void UpdateRoom(RoomEntity room)
        {
            throw new NotImplementedException();
        }

        #region Dispose

        private bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _gameStreamerContext.Dispose();
                }
            }
            this.disposed = true;
        }

        #endregion

    }
}