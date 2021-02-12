using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    public class HomeController : Controller
    {
        static ViewModel ViewModel = new ViewModel();
        static TaskContext TaskDb = new TaskContext();
        static TaskListContext ListDb = new TaskListContext();
        public ActionResult Index()
        {

            return View(GetViewModel());
        }

        public ActionResult OpenList(int Id)
        {
            var targetList = (from list in ListDb.TaskLists
                              where list.Id == Id
                              select list)?.First();

            ViewModel.CurrentList = targetList;
            return View("Index", GetViewModel());
        }

        public ActionResult AddList(string name)
        {
            ListDb.TaskLists.Add(new TaskList(name));
            ListDb.SaveChanges();

            return View("Index", GetViewModel());
        }

        public ActionResult DeleteList(int Id)
        {
            if (ViewModel.CurrentList != null)
                if (ViewModel.CurrentList.Id == Id)
                    ViewModel.CurrentList = null;


            var targetList = (from t in ListDb.TaskLists
                              where t.Id == Id
                              select t)?.First();

            ListDb.TaskLists.Remove(targetList);
            ListDb.SaveChanges();

            return View("Index", GetViewModel());
        }

        public ActionResult AddTask(string description, string color, string date)
        {

            Task NewTask = new Task(ViewModel.CurrentList.Id, description, color, date);
            TaskDb.Tasks.Add(NewTask);
            TaskDb.SaveChanges();

            return View("Index", GetViewModel());

        }

        public ActionResult DeleteTask(int Id)
        {
            var task = (from t in TaskDb.Tasks
                        where t.Id == Id
                        select t)?.First();

            TaskDb.Tasks.Remove(task);
            TaskDb.SaveChanges();

            return View("Index", GetViewModel());
        }

        public static List<Task> GetTasks()
        {
            List<Task> tasks = new List<Task>();

            foreach (var task in TaskDb.Tasks)
            {
                if (ViewModel.CurrentList != null)
                    if (task.ListId == ViewModel.CurrentList.Id)
                        tasks.Add(task);
            }

            return tasks;

        }

        public static List<TaskList> GetTaskLists()
        {
            List<TaskList> taskLists = ListDb.TaskLists?.ToList();

            return taskLists;
        }

        public static ViewModel GetViewModel()
        {
            ViewModel.TaskList = GetTasks();
            ViewModel.ListCatalog = GetTaskLists();

            return ViewModel;
        }
    }
}