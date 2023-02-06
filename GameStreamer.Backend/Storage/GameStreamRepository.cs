using GameStreamer.Backend.DTOs;
using GameStreamer.Backend.DTOs.DataAccess;
using GameStreamer.Backend.Models;
using GameStreamer.Backend.Storage.GameStreamerDbase;
using GameStreamer.Backend.Storage.GameStreamerDbase.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameStreamer.Backend.Storage
{
    public class GameStreamRepository : IGameStreamRepository
    {

        private GameStreamerContext _gameStreamerContext;

        public GameStreamRepository(GameStreamerContext gameStreamerContext)
        {
            _gameStreamerContext = gameStreamerContext;
        }

        #region Rooms

        public RoomDto? GetFirstIncompleteRoom()
        {

            RoomDto responseDto;

            var incompleteRoomEntity = _gameStreamerContext.Set<RoomEntity>().Include(p => p.JoinedPlayers.Count() < 2).FirstOrDefault();
            var playersDtoList = new List<PlayerDto>();

            if (incompleteRoomEntity != null)
            {

                foreach (var entityPlayer in incompleteRoomEntity.JoinedPlayers)
                {
                    var playerDto = new PlayerDto(entityPlayer.Nickname);

                    playerDto.PlayerDataHashGuid = entityPlayer.PlayerHashGuid;
                    playerDto.ChatHubId = entityPlayer.ChatHubId;
                    playerDto.GameHubId = entityPlayer.GameHubId;
                    playerDto.RoomHubId = entityPlayer.RoomHubId;
                    playerDto.IsReadyForGame = entityPlayer.IsReadyForGame;
                    playerDto.IsRandomGameMode = entityPlayer.IsRandomGameMode;

                    playersDtoList.Add(playerDto);
                }

                responseDto = new RoomDto
                {
                    RoomGuid = incompleteRoomEntity.RoomGuid,
                    HubGroupId = incompleteRoomEntity.HubGroupId,
                    RoomPlayers = playersDtoList
                };

            }

            else
            {
                responseDto = null;
            }

            return responseDto;
        }

        public RoomDto? GetRoomBy(Guid roomGuid)
        {

            RoomDto? resultRoom;

            var foundedRoom = GetRoomEntityBy(roomGuid);

            var playersDtoList = new List<PlayerDto>();

            if (foundedRoom != null)
            {

                foreach (var entityPlayer in foundedRoom.JoinedPlayers)
                {
                    var playerDto = new PlayerDto(entityPlayer.Nickname);

                    playerDto.PlayerDataHashGuid = entityPlayer.PlayerHashGuid;
                    playerDto.ChatHubId = entityPlayer.ChatHubId;
                    playerDto.GameHubId = entityPlayer.GameHubId;
                    playerDto.RoomHubId = entityPlayer.RoomHubId;
                    playerDto.IsReadyForGame = entityPlayer.IsReadyForGame;
                    playerDto.IsRandomGameMode = entityPlayer.IsRandomGameMode;

                    playersDtoList.Add(playerDto);
                }

                resultRoom = new RoomDto
                {
                    RoomGuid = foundedRoom.RoomGuid,
                    HubGroupId = foundedRoom.HubGroupId,
                    RoomPlayers = playersDtoList
                };
            }
            else
            {
                resultRoom = null;
            }

            return resultRoom;
        }

        public void AddRoom(RoomDto roomForAdd)
        {

            var addedPlayersList = new List<PlayerEntity>();

            foreach (var player in roomForAdd.RoomPlayers)
            {
                addedPlayersList.Add(new PlayerEntity()
                {
                    Nickname = player.NickName,
                    PlayerHashGuid = player.PlayerDataHashGuid,
                    ChatHubId = player.ChatHubId,
                    GameHubId = player.GameHubId,
                    RoomHubId = player.RoomHubId,
                    IsRandomGameMode = player.IsRandomGameMode,
                    IsReadyForGame = player.IsReadyForGame,
                    CreatedAt = DateTime.Now,
                });
            }

            var addedRoom = new RoomEntity()
            {
                CreatedAt = DateTime.Now,
                HubGroupId = roomForAdd.HubGroupId,
                RoomGuid = roomForAdd.RoomGuid,
                JoinedPlayers = addedPlayersList
            };

            _gameStreamerContext.Set<RoomEntity>().Add(addedRoom);
            Save();
        }

        public void UpdateRoom(RoomDto roomForUpdate)
        {
            throw new NotImplementedException();
        }

        public void DeleteRoom(RoomDto roomForDelete)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Players

        //public PlayerDataResponseDTO AddPlayer(PlayerDto playerForAdd)
        //{
        //    var newPlayerEntity = new PlayerEntity()
        //    {
        //        Nickname = playerForAdd.NickName,
        //        PlayerHashGuid = playerForAdd.PlayerDataHashGuid,
        //        CreatedAt = DateTime.Now
        //    };

        //    _gameStreamerContext.Set<PlayerEntity>().Add(newPlayerEntity);
        //    Save();

        //    return new PlayerDataResponseDTO { NickName = playerForAdd.NickName };
        //}

        public PlayerDto GetPlayerBy(Guid playerDataHashGuid)
        {
            PlayerDto resultPlayer;

            var foundedPlayer = GetPlayerEntityBy(playerDataHashGuid);

            if (foundedPlayer != null)
            {
                resultPlayer = new PlayerDto(foundedPlayer.Nickname);
            }
            else
            {
                resultPlayer = null;
            }

            return resultPlayer;
        }

        public PlayerDataResponseDTO UpdatePlayer(PlayerDto playerForUpdate)
        {
            PlayerDataResponseDTO resultDto;

            var updatedPlayer = GetPlayerEntityBy(playerForUpdate.PlayerDataHashGuid);

            if (updatedPlayer != null)
            {
                updatedPlayer.Nickname = playerForUpdate.NickName;
                Save();

                resultDto = new PlayerDataResponseDTO() { NickName = playerForUpdate.NickName };
            }
            else
            {
                resultDto = null;
            }

            return resultDto;
        }

        public PlayerDataResponseDTO DeletePlayer(PlayerDto playerForDelete)
        {
            var deletedPlayer = GetPlayerEntityBy(playerForDelete.PlayerDataHashGuid);
            _gameStreamerContext.Remove(deletedPlayer);
            Save();

            return new PlayerDataResponseDTO() { NickName = playerForDelete.NickName };
        }

        #endregion

        private PlayerEntity? GetPlayerEntityBy(Guid playerDataHashGuid) => _gameStreamerContext.Set<PlayerEntity>()
            .FirstOrDefault(p => p.PlayerHashGuid == playerDataHashGuid);

        private RoomEntity? GetRoomEntityBy(Guid roomGuid) => _gameStreamerContext.Set<RoomEntity>()
            .FirstOrDefault(p => p.RoomGuid == roomGuid);

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
            if (!disposed)
            {
                if (disposing)
                {
                    _gameStreamerContext.Dispose();
                }
            }
            disposed = true;
        }

        #endregion

    }
}