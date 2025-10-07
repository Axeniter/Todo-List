using CommunityToolkit.Maui.Extensions;
using TodoList.ViewModels;

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
    }
}