using GameStreamer.Backend.DTOs;

namespace GameStreamer.Backend.Interfaces
{
    public interface IRoomsHub
    {

        #region Channels For Only Data Streaming

        Task NewPlayerJoined(PlayerDataResponseDTO playerResponseDto);

        Task NewRoomAdded(GameRoomResponseDTO gameRoomResponseDto);

        /// <summary>
        /// Игрок покинул сервер
        /// </summary>
        /// <param name="playerResponseDto">Модель ответной DTO игрока</param>
        /// <returns></returns>
        Task PlayerLeavedServer(PlayerDataResponseDTO playerResponseDto);

        Task UpdatePlayersWithoutRooms(List<PlayerDataResponseDTO> playersWithoutRoomsList);

        Task PlayerChangedNickName(PlayerDataResponseDTO playerResponseDto);

        #endregion
    }
}