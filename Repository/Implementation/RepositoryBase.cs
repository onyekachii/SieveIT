using SeiveIT.Entities;
using SeiveIT.Repository.Interface;
using SQLite;
using System.Linq.Expressions;

namespace SeiveIT.Repository.Implementation;

internal abstract class RepositoryBase<T> : IRepositoryBase<T> where T : BaseEntity, new()
{
    DatabaseManager _dbManager;
    SQLiteAsyncConnection _connection;

    protected RepositoryBase(DatabaseManager dbManager)
    {
        _dbManager = dbManager;
        InitAsync().Wait();
    }

    public Task GetByEntityAsync(BaseEntity entity) => _connection.FindAsync<BaseEntity>(entity.Id);

    async Task InitAsync() => _connection = await _dbManager.GetConnectionAsync();

    public async Task CreateAsync(T entity, bool checkExisting = true)
    {
        if (checkExisting)
        {
            var value = GetByEntityAsync(entity);
            if (value != null) throw new Exception("Already exist, duplicate data has been prevented");
        }
        await _connection.InsertAsync(entity);
    }

    public async Task<int> DeleteAsync(T entity) => await _connection.DeleteAsync(entity);

    public AsyncTableQuery<T> FindAll() => _connection.Table<T>();

    public AsyncTableQuery<T> FindByCondition(Expression<Func<T, bool>> expression) => _connection.Table<T>().Where(expression);

    public async Task UpdateAsync(T entity) => await _connection.UpdateAsync(entity);
}
