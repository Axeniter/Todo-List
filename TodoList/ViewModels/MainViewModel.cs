using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using TodoList.Models;

namespace TodoList.ViewModels
{
    class MainViewModel : ObservableObject
    {
        public ObservableCollection<TodoTask> Tasks { get; } = new ObservableCollection<TodoTask>();
    }
}
