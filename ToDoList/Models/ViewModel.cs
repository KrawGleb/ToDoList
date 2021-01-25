using System.Collections.Generic;

namespace ToDoList.Models
{
    public class ViewModel
    {
        
        public TaskList CurrentList { get; set; } = null;
        public List<Task> TaskList; 
        public List<TaskList> ListCatalog;
    }
}