using GameStreamer.Backend.DTOs;

namespace GameStreamer.Backend.Interfaces
{
    public interface IRoomsHub
    {

        #region Channels For Only Data Streaming

        Task NewPlayerJoined(PlayerDataResponseDTO playerResponseDto);

        Task NewRoomAdded(GameRoomResponseDTO gameRoomResponseDto);

        Task PlayerLeavedServer(PlayerDataResponseDTO playerResponseDto);

        Task UpdatePlayersWithoutRooms(List<PlayerDataResponseDTO> playersWithoutRoomsList);

        Task PlayerChangedNickName(PlayerDataResponseDTO playerResponseDto);

        #endregion
    }
}