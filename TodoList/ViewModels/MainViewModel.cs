using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.Collections.ObjectModel;
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
        private ObservableCollection<string> _tags = new() {"Все"};

        public MainViewModel()
        {
            _todoTaskData = new TodoTaskDataBase();

            WeakReferenceMessenger.Default.Register<TaskCreatedMessage>(this, (recipient, message) 
                => CreateTaskCommand.Execute(message.Value));
            Task.Run(async () =>
            {
                await LoadTasks();
                await UpdateTags();
                _ = OverDueChecker();
            });
            CurrentTagFilter = "Все";
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
            var newTags = await _todoTaskData.GetAllTags();
            var currentSelectedTag = CurrentTagFilter;

            var tagsToRemove = Tags.Skip(1).Where(oldTag => !newTags.Contains(oldTag)).ToList();
            foreach (var tagToRemove in tagsToRemove)
            {
                Tags.Remove(tagToRemove);
            }

            var tagsToAdd = newTags.Where(newTag => !Tags.Contains(newTag)).ToList();
            foreach (var tagToAdd in tagsToAdd)
            {
                Tags.Add(tagToAdd);
            }

            if (currentSelectedTag != null && Tags.Contains(currentSelectedTag))
            {
                CurrentTagFilter = currentSelectedTag;
            }
            else
            {
                CurrentTagFilter = "Все";
            }
        }

        private async Task CheckOverDue()
        {
            var tasksToOverDue = PendingTasks.Where(task => task.TryOverDue()).ToList();

            foreach (var task in tasksToOverDue)
            {
                await _todoTaskData.SaveItem(task); 
                OverDueTasks.Add(task);
                PendingTasks.Remove(task);
                _ = ShowOverdueNotification(task);
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
                CompletedTasks.Add(task);
                PendingTasks.Remove(task);
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

        private async Task ShowOverdueNotification(TodoTask task)
        {
            await MainThread.InvokeOnMainThreadAsync(async () =>
            {
                await Application.Current.MainPage.DisplayAlert(
                    "⚠️ Задача просрочена",
                    $"Задача \"{task.Name}\" просрочена!\nСрок: {task.DueTime:dd/MM/yyyy HH:mm}",
                    "Понятно");
            });
        }

        ~MainViewModel()
        {
            WeakReferenceMessenger.Default.UnregisterAll(this);
        }
    }
}