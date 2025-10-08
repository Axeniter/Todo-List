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
        public ObservableCollection<TodoTask> Tasks { get; } = new();
        public ObservableCollection<string> Tags { get; } = new();
        public MainViewModel()
        {
            Tasks.Add(new TodoTask("Приготовить бутерброды", "Колбаса, сыр, хлеб необязательно", "Дом",
                DateTime.Now, TodoTaskPriority.None));
            Tasks.Add(new TodoTask("Сделать Todo List", "Надо:\n-Подключить базу данных\n-Сделать ViewModel\n-Фильтрация списка\n-Логика задач",
                "Учеба", DateTime.Now, TodoTaskPriority.Medium));
            Tasks.Add(new TodoTask("Сделать веб", "", "Учеба", DateTime.Today, TodoTaskPriority.High));
            Tags.Add("fff");
            Tags.Add("ga");
        }

        [RelayCommand]
        public void DeleteTask(TodoTask task)
        {
            Tasks.Remove(task);
        }

    }
}
