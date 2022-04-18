namespace MatchMake.Backend.Contracts
{

    /// <summary>
    /// Интерфейс для реализации игровой комнаты
    /// </summary>
    public interface IRoom
    {
        string Add(string userName);
        string Remove(string userName);
        bool Clear();
        IEnumerable<string> GetAllPlayers();
    }
}