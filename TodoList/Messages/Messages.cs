using CommunityToolkit.Mvvm.Messaging.Messages;
using TodoList.Models;

namespace TodoList.Messages
{
    public class TaskCreatedMessage : ValueChangedMessage<TodoTask>
    {
        public TaskCreatedMessage(TodoTask task) : base(task) { }
    }

    public class ClosePopupMessage { }
}
