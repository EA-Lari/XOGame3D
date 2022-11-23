using GameStreamer.Backend.DTOs;

namespace GameStreamer.Backend.Interfaces
{
    public interface IRoomsHub
    {

        #region Channels For Only Data Streaming

        Task NewPlayerJoined(PlayerNickNameResponseDTO playerResponseDto);

        Task NewRoomAdded(GameRoomResponseDTO gameRoomResponseDto);

        #endregion
    }
}