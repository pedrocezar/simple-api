using System.Linq.Expressions;

namespace Simple.DDD.Domain.Interfaces.Services
{
    public interface IBaseService<T>
    {
        Task<List<T>> ObterTodosAsync(Expression<Func<T, bool>> expression);
        Task<T> ObterAsync(Expression<Func<T, bool>> expression);
        Task<List<T>> ObterTodosAsync();
        Task<T> ObterPorIdAsync(int id);
        Task AdicionarAsync(T entity);
        Task DeletarAsync(int id);
        Task AlterarAsync(T entity);
    }
}
