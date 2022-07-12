namespace GameStreamer.Backend.Models
{

    /// <summary>
    /// Модель данных нового игрока
    /// </summary>
    public class NewPlayer
    {
        public string ConnectionId { get; set; }
        public string NickName { get; set; }

        public static NewPlayer Create(string connectionId, string nickName)
        {
            return new NewPlayer
            {
                ConnectionId = connectionId,
                NickName = string.IsNullOrEmpty(nickName) ? "Anon" : nickName
            };
        }

    }
}