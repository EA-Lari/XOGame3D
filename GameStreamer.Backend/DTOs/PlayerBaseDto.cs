namespace GameStreamer.Backend.DTOs
{
    public abstract class PlayerBaseDto
    {

        public PlayerBaseDto(string nickName)
        {
            NickName = nickName;
        }

        public string NickName { get; private set; }

        public void SetNewNickName(string nickName) => NickName = nickName;

    }
}
