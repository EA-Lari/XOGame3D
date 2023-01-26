namespace GameStreamer.Backend.Models
{
    public abstract class PlayerBase
    {

        public PlayerBase(string nickName)
        {
            this.NickName = string.IsNullOrEmpty(nickName) ? "Anon" : nickName;
        }

        public string NickName { get; private set; }

        public void SetNewNickName(string nickName) => this.NickName = nickName;
    }
}
