using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TodoList.Models;

namespace TodoList.ViewModels
{
    public partial class AddTaskViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _name = string.Empty;

        [ObservableProperty]
        private string _description = string.Empty;

        [ObservableProperty]
        private string _tag = string.Empty;

        [ObservableProperty]
        private TodoTaskPriority _priority = TodoTaskPriority.None;

        [ObservableProperty]
        private DateTime _date = DateTime.Today;

        [ObservableProperty]
        private TimeSpan _time = new(23, 59, 0);

        public static Array AllPriorities => Enum.GetValues(typeof(TodoTaskPriority));
        public AddTaskViewModel() { }

        [RelayCommand]
        private void MakeTask()
        {
            var safeName = string.IsNullOrEmpty(Name) ? "Новая задача" : Name;
            var safeTag = string.IsNullOrEmpty(Tag) ? "Входящие" : Tag;
            var task = new TodoTask(safeName, Description, safeTag, Date + Time, Priority);
        }
    }
}
