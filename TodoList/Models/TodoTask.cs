using SQLite;

namespace TodoList.Models
{
    public class TodoTask
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Tag { get; set; }
        public DateTime DueTime { get; set; }
        public TodoTaskPriority Priority { get; set; }
        public TodoTaskStatus Status { get; private set; } = TodoTaskStatus.Pending;

        public TodoTask() : this("Новая задача", string.Empty, "Входящие",
            DateTime.Today.AddHours(23).AddMinutes(59), TodoTaskPriority.None) { }
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
