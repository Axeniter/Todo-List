using SQLite;
using System.Diagnostics;

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

        public async Task<IEnumerable<TodoTask>> GetAllItems()
        {
            var items = await _connection.Table<TodoTask>().ToListAsync();
            return items;
        }

        public async Task<IEnumerable<string>> GetAllTags()
        {
            var allTasks = await _connection.Table<TodoTask>().ToListAsync();
            return allTasks
                .Where(t => !string.IsNullOrEmpty(t.Tag))
                .Select(t => t.Tag)
                .Distinct()
                .OrderBy(tag => tag);
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

