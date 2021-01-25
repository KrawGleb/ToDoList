using System.Data.Entity;

namespace ToDoList.Models
{
    public class TaskListContext : DbContext
    {
        public TaskListContext()
            : base("DbConnection") { }

        public DbSet<TaskList> TaskLists {get; set;}
    }
}