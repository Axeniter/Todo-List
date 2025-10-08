using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using TodoList.Models;
using System.Linq;
using System.Diagnostics;

namespace TodoList.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly TodoTaskDataBase _todoTaskData;

        [ObservableProperty]
        private string _currentTagFilter = "Все";

        public ObservableCollection<TodoTask> PendingTasks { get; private set; }
        public ObservableCollection<TodoTask> OverDueTasks { get; private set; }
        public ObservableCollection<TodoTask> CompletedTasks { get; private set; }
        public ObservableCollection<string> Tags { get; private set; }

        public MainViewModel()
        {
            _todoTaskData = new TodoTaskDataBase();
            Init();
        }
        
        private async Task Init()
        {
            await Task.WhenAll(LoadTasks(), UpdateTagsAsync());
            _ = OverDueChecker();
        }

        public async Task LoadTasks()
        {
            var allTasks = await _todoTaskData.GetAllItems();
            PendingTasks = new(allTasks.Where(i => i.Status == TodoTaskStatus.Pending));
            OverDueTasks = new(allTasks.Where(i => i.Status == TodoTaskStatus.OverDue));
            CompletedTasks = new(allTasks.Where(i => i.Status == TodoTaskStatus.Completed));
        }

        private async Task UpdateTagsAsync()
        {
            var tags = await _todoTaskData.GetAllTags();
            Tags = new(tags);
            Tags.Insert(0, "Все");
        }

        private async Task CheckOverDue()
        {
            foreach (var task in PendingTasks)
            {
                if(task.TryOverDue())
                {
                    await _todoTaskData.SaveItem(task);
                    PendingTasks.Remove(task);
                    OverDueTasks.Add(task);
                }
            }
        }

        private async Task OverDueChecker()
        {
            while (true)
            {
                await Task.Delay(TimeSpan.FromSeconds(30));
                await CheckOverDue();
            }
        }
        [RelayCommand]
        private async Task CompleteTask(TodoTask task)
        {
            if (task.TryComplete())
            {
                await _todoTaskData.SaveItem(task);
                PendingTasks.Remove(task);
                CompletedTasks.Add(task);
            }
        }

        [RelayCommand]
        private async Task DeleteTask(TodoTask task)
        {
            await _todoTaskData.DeleteItem(task);
            await UpdateTagsAsync();
            switch (task.Status)
            {
                case TodoTaskStatus.Pending:
                    PendingTasks.Remove(task);
                    break;
                case TodoTaskStatus.OverDue:
                    OverDueTasks.Remove(task);
                    break;
                 case TodoTaskStatus.Completed:
                    CompletedTasks.Remove(task);
                    break;
            }
        }

        [RelayCommand]
        private async Task CreateTask(TodoTask task)
        {
            await _todoTaskData.SaveItem(task);
            await UpdateTagsAsync();
            switch (task.Status)
            {
                case TodoTaskStatus.Pending:
                    PendingTasks.Add(task);
                    break;
                case TodoTaskStatus.OverDue:
                    OverDueTasks.Add(task);
                    break;
                case TodoTaskStatus.Completed:
                    CompletedTasks.Add(task);
                    break;
            }
        }

    }
}