using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoList.Models
{
    public class Task
    {
        public Task(int listId, string description, string color = "table-default", string date = null)
        {
            ListId = listId;
            Description = description;
            Color = color;
            Date = date;
        }
        
        public Task() { }
        [Key]
        public int Id { get; set; }
        public int ListId { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        public string Date { get; set; }
        
    }
}