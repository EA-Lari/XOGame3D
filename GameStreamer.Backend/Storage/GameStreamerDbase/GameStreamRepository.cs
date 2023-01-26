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

        public PlayerDataResponseDTO AddNewPlayer(PlayerFromRoomHub forAdd)
        {
            var newPlayerEntity = new NewPlayerEntity()
            {
                Nickname = forAdd.NickName,
                CreatedAt = DateTime.Now,
            };

            _gameStreamerContext.Set<NewPlayerEntity>().Add(newPlayerEntity);
            Save();

            return new PlayerDataResponseDTO { NickName = forAdd.NickName };
        }

        public PlayerFromRoomHub GetPlayerBy(Guid playerDataHashGuid)
        {
            //var playerEntity = GetOneEntityBy(playerDataHashGuid);

            return new PlayerFromRoomHub("");
        }

        public PlayerDataResponseDTO UpdatePlayer(PlayerFromRoomHub playerFromRoom, Guid oldHashGuid)
        {
            var playerFromDb = GetOneEntityBy(oldHashGuid);
            playerFromDb.Nickname = playerFromRoom.NickName;
            //playerFromDb.PlayerGuid = playerFromRoom.PlayerDataHashGuid;

            Save();

            return new PlayerDataResponseDTO { ConnectionId = "", NickName = playerFromRoom.NickName };
        }

        #endregion

        private JoinedPlayerEntity GetOneEntityBy(Guid playerDataHashGuid) => _gameStreamerContext.Set<JoinedPlayerEntity>()
            .First(p => p.PlayerGuid == playerDataHashGuid);

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