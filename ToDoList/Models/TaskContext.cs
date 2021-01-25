using System.Data.Entity;

namespace ToDoList.Models
{
    public class TaskContext : DbContext
    {
        public TaskContext()
            : base("DbConnection") { }

        public DbSet<Task> Tasks { get; set; }
    }
}