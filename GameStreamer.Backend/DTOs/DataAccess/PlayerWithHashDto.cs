namespace GameStreamer.Backend.DTOs.DataAccess
{
    public class PlayerWithHashDto : PlayerBaseDto
    {
        public PlayerWithHashDto(string nickName, Guid hashGuid) : base(nickName)
        {
            this.PlayerDataHashGuid = hashGuid;
        }

        public Guid PlayerDataHashGuid { get; private set; }

        public void SetNewHashGuid(Guid newHashGuid) => PlayerDataHashGuid = newHashGuid;

    }
}