using SQLite;

namespace TodoList.Models
{
    public class TodoTask
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; } = "Новая задача";
        public string Description { get; set; } = string.Empty;
        public string Tag { get; set; } = "Входящие";
        public DateTime DueTime { get; set; } = DateTime.Today.AddHours(23).AddMinutes(59);
        public TodoTaskPriority Priority { get; set; } = TodoTaskPriority.None;
        public TodoTaskStatus Status { get; set; } = TodoTaskStatus.Pending;

        public TodoTask()
        { }
        public TodoTask(string name, string description, string tag, DateTime dueTime, TodoTaskPriority priority)
        {
            Name = name;
            Description = description;
            Tag = tag;
            DueTime = dueTime;
            Priority = priority;
        }
    }
}
