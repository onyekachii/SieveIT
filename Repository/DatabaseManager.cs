using SeiveIT.Entities;
using SQLite;

namespace SeiveIT.Repository
{
    public class DatabaseManager
    {
        private readonly string _dbPath;
        private SQLiteAsyncConnection _connection;
        public DatabaseManager(string dbPath)
        {
            _dbPath = dbPath;
            GetConnectionAsync();
        }
        public async Task<SQLiteAsyncConnection> GetConnectionAsync()
        {
            if (_connection == null)
            {
                _connection = new SQLiteAsyncConnection(_dbPath);
                await Init();
            }
            return _connection;
        }

        private async Task Init()
        {
            await _connection.CreateTableAsync<Project>();
            await _connection.CreateTableAsync<Outcrop>();
            await _connection.CreateTableAsync<SeiveData>();
        }
    }
}
