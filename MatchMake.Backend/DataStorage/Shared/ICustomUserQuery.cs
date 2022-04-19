using System.Linq.Expressions;

namespace MatchMake.Backend.DataStorage.Shared
{

    /// <summary>
    /// Интерфейс универсального запроса для инкапсуляции запросов (ограничения/фильтрации) к БД
    /// </summary>
    public interface ICustomUserQuery<T>
    {

       /// <summary>
       /// Непосредственно универсальная функция фильтрации/ограничения, будет использоваться как дополнительное выражение, встраиваемое в Linq запрос, и которое помогает строить финальный SQL код
       /// </summary>
       /// <returns></returns>
        Expression<Func<T, bool>> IsSatisfiedBy();

    }
}