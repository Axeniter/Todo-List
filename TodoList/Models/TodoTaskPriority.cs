using System.ComponentModel;

namespace TodoList.Models
{
    public enum TodoTaskPriority
    {
        [Description("Высокий")]
        High,
        [Description("Средний")]
        Medium,
        [Description("Низкий")]
        Low,
        [Description("Без приоритета")]
        None
    }
}
