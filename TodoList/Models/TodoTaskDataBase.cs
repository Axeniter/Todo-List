using SQLite;

namespace TodoList.Models
{
    public class TodoTaskDataBase
    {
        private readonly SQLiteAsyncConnection _connection;

        public TodoTaskDataBase()
        {
            string dataBasePath = Path.Combine(FileSystem.AppDataDirectory, "Todo.db");
            _connection = new SQLiteAsyncConnection(dataBasePath);
            _connection.CreateTableAsync<TodoTask>().Wait();
        }

        public Task<List<TodoTask>> GetItems()
        {
            return _connection.Table<TodoTask>().ToListAsync();
        }

        public async Task SaveItem(TodoTask item)
        {
            if (item.Id == 0)
            {
                await _connection.InsertAsync(item);
            }
            else
            {
                await _connection.UpdateAsync(item);
            }
        }

        public Task<int> DeleteItem(TodoTask item)
        {
            return _connection.DeleteAsync(item);
        }
    }
}

