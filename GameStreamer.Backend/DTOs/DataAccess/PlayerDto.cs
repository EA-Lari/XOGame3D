namespace GameStreamer.Backend.DTOs.DataAccess
{
    public class PlayerDto : PlayerBaseDto
    {

        private Guid _playerDataHashGuid;
        public Guid PlayerDataHashGuid
        {
            get => _playerDataHashGuid;
            set => SetNewHashGuid(value);
        }

        private bool _isReadyForGame;

        public bool IsReadyForGame
        {
            get => _isReadyForGame;
            set => SetNewGameReadyFlag(value);
        }

        private bool _isRandomGameMode;

        public bool IsRandomGameMode
        {
            get => _isRandomGameMode;
            set => SetNewRandomGameFlag(value);
        }

        public string ChatHubId { get; set; }

        public string GameHubId { get; set; }

        public string RoomHubId { get; set; }

        public PlayerDto(string nickName) : base(nickName)
        { }

        private void SetNewHashGuid(Guid newHashGuid) => _playerDataHashGuid = newHashGuid;

        private void SetNewGameReadyFlag(bool gameReadyFlag) => _isReadyForGame = gameReadyFlag;

        private void SetNewRandomGameFlag(bool randomGameFlag) => _isRandomGameMode = randomGameFlag;
    }
}