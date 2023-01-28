namespace GameStreamer.Backend.DTOs
{
    public abstract class PlayerBaseDto
    {

        private string _nickName;

        public string NickName
        {
            get => _nickName;
            set => SetNewNickName(value);
        }

        public PlayerBaseDto(string nickName)
        {
            SetNewNickName(nickName);
        }

        private void SetNewNickName(string nickName) => _nickName = nickName;

    }
}