using CommunityToolkit.Maui.Views;
using TodoList.ViewModels;
using System.Diagnostics;

namespace TodoList.Pages
{
	public partial class AddTaskPage : Popup
	{
		public AddTaskPage()
		{
			InitializeComponent();
			BindingContext = new AddTaskViewModel();
        }
	}
}
