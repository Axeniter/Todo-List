using SQLite;

namespace TodoList.Models
{
    class TodoTask
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; private set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Tag { get; set; }
        public DateTime DueTime { get; set; }
        public TodoTaskPriority Priority { get; set; }
        public TodoTaskStatus Status { get; set; } = TodoTaskStatus.Pending;

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
