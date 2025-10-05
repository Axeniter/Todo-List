using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using TodoList.Models;

namespace TodoList.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        public ObservableCollection<TodoTask> Tasks { get; } = new ObservableCollection<TodoTask>();

        public MainViewModel()
        {
            Tasks.Add(new TodoTask("Приготовить бутерброды", "Колбаса, сыр, хлеб необязательно", "Дом",
                DateTime.Now, TodoTaskPriority.None));
            Tasks.Add(new TodoTask("Сделать Todo List", "Надо:\n-Подключить базу данных\n-Сделать ViewModel\n-Фильтрация списка\n-Логика задач",
                "Учеба", DateTime.Now, TodoTaskPriority.Medium));
            Tasks.Add(new TodoTask("Сделать веб", "", "Учеба", DateTime.Now, TodoTaskPriority.High));
        }

        [RelayCommand]
        public void DeleteTask(TodoTask task)
        {
            Tasks.Remove(task);
        }
    }
}
