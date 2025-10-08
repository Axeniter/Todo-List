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

        public Task<List<TodoTask>> GetAllItems()
        {
            return _connection.Table<TodoTask>().ToListAsync();
        }

        public async Task<List<TodoTask>> GetFilteredItems(TodoTaskStatus? status = null, string? tag = null)
        {
            var allTasks = await _connection.Table<TodoTask>().ToListAsync();
            return allTasks
                .Where(i => (string.IsNullOrEmpty(tag) ? true : i.Tag == tag) && (status == null ? true : i.Status == status))
                .ToList();
        }

        public async Task<List<string>> GetAllTags()
        {
            var allTasks = await _connection.Table<TodoTask>().ToListAsync();

            return allTasks
                .Where(t => !string.IsNullOrEmpty(t.Tag))
                .Select(t => t.Tag)
                .Distinct()
                .OrderBy(tag => tag)
                .ToList();
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

