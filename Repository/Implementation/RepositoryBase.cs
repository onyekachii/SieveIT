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

    async Task InitAsync() => _connection = await _dbManager.GetConnectionAsync();

    public async Task CreateAsync(T entity)
    {        
        await _connection.InsertAsync(entity);
    }

    public async Task<int> DeleteAsync(T entity) => await _connection.DeleteAsync(entity);

    public async Task<List<T>> FindAll(int page, int limit)
    {
                return await _connection.Table<T>().Skip(page * limit).Take(limit).ToListAsync();
        }
    public AsyncTableQuery<T> FindByCondition(Expression<Func<T, bool>> expression) => _connection.Table<T>().Where(expression);

    public async Task UpdateAsync(T entity) => await _connection.UpdateAsync(entity);

    public async Task Upsert(T entity) 
    {
        if (entity.Id > 0)
            await _connection.UpdateAsync(entity);
        else
            await _connection.InsertAsync(entity);
    }

    public async Task UpsertAll(List<T> entity)
    {
        await _connection.RunInTransactionAsync(async _ => {
            entity.ForEach(async (item) => {
                if(item.Id > 0)
                {
                    item.UpdatedOn = DateTime.UtcNow;
                    await _connection.UpdateAsync(item);
                }
                else
                {
                    item.CreatedOn = DateTime.UtcNow;
                    await _connection.InsertAsync(item);
                }
            });
        });
       
    }
}
