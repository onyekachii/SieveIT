using SeiveIT.Entities;
using SQLite;
using System.Linq.Expressions;

namespace SeiveIT.Repository.Interface
{
    public interface IRepositoryBase<T> where T : BaseEntity, new()
    {
        Task<List<T>> FindAll(int page, int limit);
        AsyncTableQuery<T> FindByCondition(Expression<Func<T, bool>> expression);
        Task CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task<int> DeleteAsync(T entity);
        Task Upsert(T entity);
    }
}
