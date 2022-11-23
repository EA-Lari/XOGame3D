namespace GameStreamer.Backend.Models
{

    /// <summary>
    /// Модель данных нового игрока
    /// </summary>
    public class Player
    {
        public Player(string connectionId, string nickName)
        {
            this.ConnectionId = connectionId;
            this.NickName = string.IsNullOrEmpty(nickName) ? "Anon" : nickName;
        }

        public string ConnectionId { get; }
        public string NickName { get; }

    }
}