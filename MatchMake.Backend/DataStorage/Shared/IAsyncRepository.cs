using MatchMake.Backend.DataStorage.Shared;

namespace MatchMake.Backend.Storage.Contracts
{

    public interface IAsyncRepository<T, TId>
    {

        /// <summary>
        /// Получаем элемент по Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> GetByIdAsync(TId id);

        /// <summary>
        /// Получаем элемент по кастомному условию
        /// </summary>
        /// <param name="customQuery"></param>
        /// <returns></returns>
        Task<T> GetByCustomQueryAsync(ICustomUserQuery<T> customQuery);

        /// <summary>
        /// Получаем все подходящие под кастомное условие элементы
        /// </summary>
        /// <returns></returns>
        Task<List<T>> GetAllByCustomQueryAsListAsync(ICustomUserQuery<T> customQuery);

    }
}