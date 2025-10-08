using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using TodoList.Models;
using TodoList.Messages;

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
        private async Task MakeTask()
        {
            if (Tag == "Все")
            {
                await Shell.Current.DisplayAlert("Предупреждение", "Тег \"Все\" зарезервирован системой", "OK");
                return;
            }
            var safeName = string.IsNullOrEmpty(Name) ? "Новая задача" : Name;
            var safeTag = string.IsNullOrEmpty(Tag) ? "Входящие" : Tag;
            var task = new TodoTask(safeName, Description, safeTag, Date + Time, Priority);

            WeakReferenceMessenger.Default.Send(new TaskCreatedMessage(task));
            WeakReferenceMessenger.Default.Send(new ClosePopupMessage());

        }
    }
}
