using GameStreamer.Backend.DTOs;
using GameStreamer.Backend.DTOs.DataAccess;
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

        public PlayerDataResponseDTO AddNewPlayer(PlayerWithHashDto addedPlayer)
        {
            var newPlayerEntity = new NewPlayerEntity()
            {
                Nickname = addedPlayer.NickName,
                PlayerHashGuid = addedPlayer.PlayerDataHashGuid,
                CreatedAt = DateTime.Now
            };

            _gameStreamerContext.Set<NewPlayerEntity>().Add(newPlayerEntity);
            Save();

            return new PlayerDataResponseDTO { NickName = addedPlayer.NickName };
        }

        public PlayerWithHashDto GetNewPlayerBy(Guid playerDataHashGuid)
        {
            PlayerWithHashDto resultPlayer;
            var foundedPlayer = _gameStreamerContext.Set<NewPlayerEntity>()
                .First(p => p.PlayerHashGuid == playerDataHashGuid);
            
            if (foundedPlayer != null)
            {
                resultPlayer = new PlayerWithHashDto(foundedPlayer.Nickname, foundedPlayer.PlayerHashGuid);
            }
            else
            {
                resultPlayer = null;
            }

            return resultPlayer;
        }

        public PlayerWithHashDto GetPlayerWithRoomBy(Guid playerDataHashGuid)
        {
            PlayerWithHashDto resultPlayer;

            var foundedPlayer = _gameStreamerContext.Set<JoinedPlayerEntity>()
                .First(p => p.PlayerHashGuid == playerDataHashGuid);

            if (foundedPlayer != null)
            {
                resultPlayer = new PlayerWithHashDto(foundedPlayer.Nickname, foundedPlayer.PlayerHashGuid);
            }
            else
            {
                resultPlayer = null;
            }

            return resultPlayer;
        }

        public PlayerDataResponseDTO UpdateNewPlayer(PlayerWithHashDto updatedPlayer)
        {

            PlayerDataResponseDTO resultDto;

            var playerForUpdate = GetNewPlayerEntityBy(updatedPlayer.PlayerDataHashGuid);

            if (playerForUpdate != null)
            {
                playerForUpdate.Nickname = updatedPlayer.NickName;
                Save();

                resultDto = new PlayerDataResponseDTO() { NickName = updatedPlayer.NickName };
            }
            else
            {
                resultDto = null;
            }

            return resultDto;
        }

        public PlayerDataResponseDTO UpdatePlayerWithRoom(PlayerWithHashDto updatedPlayer)
        {
            PlayerDataResponseDTO resultDto;

            var playerForUpdate = GetJoinedPlayerEntityBy(updatedPlayer.PlayerDataHashGuid);
            
            if (playerForUpdate != null)
            {
                playerForUpdate.Nickname = updatedPlayer.NickName;
                Save();

                resultDto = new PlayerDataResponseDTO() { NickName = updatedPlayer.NickName };
            }
            else
            {
                resultDto = null;
            }

            return resultDto;
        }

        #endregion

        private NewPlayerEntity GetNewPlayerEntityBy(Guid playerDataHashGuid) => _gameStreamerContext.Set<NewPlayerEntity>()
            .First(p => p.PlayerHashGuid == playerDataHashGuid);

        private JoinedPlayerEntity GetJoinedPlayerEntityBy(Guid playerDataHashGuid) => _gameStreamerContext.Set<JoinedPlayerEntity>()
            .First(p => p.PlayerHashGuid == playerDataHashGuid);

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