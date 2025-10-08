using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using TodoList.Messages;
using TodoList.Models;

namespace TodoList.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly TodoTaskDataBase _todoTaskData;

        [ObservableProperty]
        private string _currentTagFilter;
        [ObservableProperty]
        private ObservableCollection<TodoTask> _pendingTasks;
        [ObservableProperty]
        private ObservableCollection<TodoTask> _overDueTasks;
        [ObservableProperty]
        private ObservableCollection<TodoTask> _completedTasks;
        [ObservableProperty]
        private ObservableCollection<string> _tags;

        public MainViewModel()
        {
            _todoTaskData = new TodoTaskDataBase();
            LoadTasksCommand.Execute(null);
            UpdateTagsCommand.Execute(null);

            PendingTasks = new ObservableCollection<TodoTask>();
            OverDueTasks = new ObservableCollection<TodoTask>();
            CompletedTasks = new ObservableCollection<TodoTask>();
            Tags = new ObservableCollection<string>();

            WeakReferenceMessenger.Default.Register<TaskCreatedMessage>(this, (recipient, message) 
                => CreateTaskCommand.Execute(message.Value));
            CurrentTagFilter = "Все";
            _ = OverDueChecker();
        }

        [RelayCommand]
        private async Task LoadTasks()
        {
            var allTasks = await _todoTaskData.GetAllItems();
            PendingTasks = new(allTasks.Where(i => i.Status == TodoTaskStatus.Pending));
            OverDueTasks = new(allTasks.Where(i => i.Status == TodoTaskStatus.OverDue));
            CompletedTasks = new(allTasks.Where(i => i.Status == TodoTaskStatus.Completed));
        }

        [RelayCommand]
        private async Task UpdateTags()
        {
            var tags = await _todoTaskData.GetAllTags();
            Tags = new(tags);
            Tags.Insert(0, "Все");
        }

        private async Task CheckOverDue()
        {
            var tasksToOverDue = PendingTasks.Where(task => task.TryOverDue()).ToList();

            foreach (var task in tasksToOverDue)
            {
                await _todoTaskData.SaveItem(task);
                PendingTasks.Remove(task);
                OverDueTasks.Add(task);
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
            await UpdateTags();
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
            await UpdateTags();
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

        ~MainViewModel()
        {
            WeakReferenceMessenger.Default.UnregisterAll(this);
        }
    }
}