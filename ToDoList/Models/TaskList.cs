using System.ComponentModel.DataAnnotations;

namespace ToDoList.Models
{
    public class TaskList
    {
        public TaskList(string name)
        {
            Name = name;
        }

        public TaskList()
        {
            Name = "";
        }

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}