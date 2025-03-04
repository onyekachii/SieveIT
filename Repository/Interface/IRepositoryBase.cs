using SeiveIT.Entities;
using SQLite;
using System.Linq.Expressions;

namespace SeiveIT.Repository.Interface
{
    public interface IRepositoryBase<T> where T : BaseEntity, new()
    {
        Task GetByEntityAsync(BaseEntity entity);
        AsyncTableQuery<T> FindAll();
        AsyncTableQuery<T> FindByCondition(Expression<Func<T, bool>> expression);
        Task CreateAsync(T entity, bool checkExisting = true);
        Task UpdateAsync(T entity);
        Task<int> DeleteAsync(T entity);
    }
}
