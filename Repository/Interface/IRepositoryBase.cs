using SQLite;
using System.Linq.Expressions;

namespace SeiveIT.Repository.Interface
{
    public interface IRepositoryBase<T> where T : new()
    {
        AsyncTableQuery<T> FindAll();
        AsyncTableQuery<T> FindByCondition(Expression<Func<T, bool>> expression);
        Task CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task<int> DeleteAsync(T entity);
    }
}
