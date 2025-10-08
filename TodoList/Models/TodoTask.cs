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
        public TodoTaskStatus Status { get; set; }

        public TodoTask() : this("Новая задача", string.Empty, "Входящие",
            DateTime.Today.AddHours(23).AddMinutes(59), TodoTaskPriority.None) { }
        public TodoTask(string name, string description, string tag, DateTime dueTime, TodoTaskPriority priority)
        {
            Name = name;
            Description = description;
            Tag = tag;
            DueTime = dueTime;
            Priority = priority;
            Status = TodoTaskStatus.Pending;
            TryOverDue();
        }

        public bool TryComplete()
        {
            if (Status == TodoTaskStatus.Pending)
            {
                Status = TodoTaskStatus.Completed;
                return true;
            }
            return false;
        }

        public bool TryOverDue()
        {
            if (DateTime.Now > DueTime)
            {
                Status = TodoTaskStatus.OverDue;
                return true;
            }
            return false;
        }
    }
}
