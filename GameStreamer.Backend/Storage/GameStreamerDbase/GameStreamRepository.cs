using GameStreamer.Backend.DTOs;
using GameStreamer.Backend.Models;
using GameStreamer.Backend.Storage.GameStreamerDbase.Entities;

namespace GameStreamer.Backend.Storage.GameStreamerDbase
{
    public class GameStreamRepository : IGameStreamRepository
    {

        private GameStreamerContext _gameStreamerContext;

        public GameStreamRepository(GameStreamerContext gameStreamerContext)
        {
            _gameStreamerContext = gameStreamerContext;
        }

        #region Rooms

        public IQueryable<RoomEntity> GetAllRooms() => _gameStreamerContext.Set<RoomEntity>().AsQueryable();

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
            Save();
        }

        public void UpdateRoom(RoomEntity room)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Players

        public PlayerDataResponseDTO AddPlayer(PlayerFromRoomHub forAdd)
        {
            var playerEntity = new PlayerEntity()
            {
                Nickname = forAdd.NickName,
                RoomHubId = forAdd.RoomConnectionId,
                CreatedAt = DateTime.Now,
                IsReadyForGame = false
            };

            _gameStreamerContext.Set<PlayerEntity>().Add(playerEntity);
            Save();

            return new PlayerDataResponseDTO { ConnectionId = forAdd.RoomConnectionId, NickName = forAdd.NickName };
        }

        public PlayerFromRoomHub GetPlayerBy(string roomHubConnectionId) => _gameStreamerContext.Set<PlayerEntity>().First(p => p.RoomHubId);

        public PlayerDataResponseDTO UpdatePlayer(PlayerFromRoomHub playerFromRoom)
        {
            throw new NotImplementedException();
        }

        #endregion

        private void Save()
        {
            _gameStreamerContext.SaveChanges();
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