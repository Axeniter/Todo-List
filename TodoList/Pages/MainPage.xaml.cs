using CommunityToolkit.Maui.Extensions;
using TodoList.ViewModels;
using TodoList.Models;

namespace TodoList.Pages
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainViewModel();
        }

        public void OnAddButtonClicked(object sender, EventArgs e)
        {
            this.ShowPopup(new AddTaskPage());
        }

        private void OnCheckBoxCheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            var checkBox = (CheckBox)sender;
            if (checkBox.BindingContext is TodoTask task && BindingContext is MainViewModel viewModel)
            {
                viewModel.CompleteTaskCommand.Execute(task);
                checkBox.IsChecked = false;
            }
        }
    }
}