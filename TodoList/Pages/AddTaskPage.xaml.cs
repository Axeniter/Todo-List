using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.Messaging;
using System.Threading.Tasks;
using TodoList.Messages;
using TodoList.ViewModels;

namespace TodoList.Pages
{
	public partial class AddTaskPage : Popup
	{
		public AddTaskPage()
		{
			InitializeComponent();
            BindingContext = new AddTaskViewModel();
            WeakReferenceMessenger.Default.Register<ClosePopupMessage>(this, (recipient, message) => OnCloseRequested());
        }

		private async void OnCloseRequested()
		{
            WeakReferenceMessenger.Default.UnregisterAll(this);
            await CloseAsync();
		}
    }
}
