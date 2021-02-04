using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    public class HomeController : Controller
    {
        static ViewModel ViewModel = new ViewModel();
        public ActionResult Index()
        {

            return View(GetViewModel());
        }

        public ActionResult OpenList(int Id)
        {
            using (TaskListContext db = new TaskListContext())
            {
                var targetList = (from list in db.TaskLists
                                  where list.Id == Id
                                  select list)?.First();

                ViewModel.CurrentList = targetList;
                return View("Index", GetViewModel());
            }
        }

        public ActionResult AddList(string name)
        {
            using (TaskListContext db = new TaskListContext())
            {
                db.TaskLists.Add(new TaskList(name));
                db.SaveChanges();

                return View("Index", GetViewModel());
            }
        }
        public ActionResult DeleteList(int Id)
        {
            if (ViewModel.CurrentList != null)
                if (ViewModel.CurrentList.Id == Id)
                    ViewModel.CurrentList = null;

            using (TaskListContext db = new TaskListContext())
            {
                var targetList = (from t in db.TaskLists
                                  where t.Id == Id
                                  select t)?.First();

                db.TaskLists.Remove(targetList);
                db.SaveChanges();

                return View("Index", GetViewModel());
            }
        }

        public ActionResult AddTask(string description, string color, string date)
        {
            using (TaskContext db = new TaskContext())
            {
                Task NewTask = new Task(ViewModel.CurrentList.Id, description, color, date);
                db.Tasks.Add(NewTask);
                db.SaveChanges();

                return View("Index", GetViewModel());
            }
        }

        public ActionResult DeleteTask(int Id)
        {
            using (TaskContext db = new TaskContext())
            {
                var task = (from t in db.Tasks
                            where t.Id == Id
                            select t)?.First();

                db.Tasks.Remove(task);
                db.SaveChanges();

                return View("Index", GetViewModel());
            }

        }

        public static List<Task> GetTasks()
        {
            using (TaskContext db = new TaskContext())
            {
                List<Task> tasks = new List<Task>();

                foreach (var task in db.Tasks)
                {
                    if (ViewModel.CurrentList != null)
                        if (task.ListId == ViewModel.CurrentList.Id)
                            tasks.Add(task);
                }

                return tasks;
            }
        }

        public static List<TaskList> GetTaskLists()
        {
            using (TaskListContext db = new TaskListContext())
            {
                List<TaskList> taskLists = db.TaskLists?.ToList();

                return taskLists;
            }
        }

        public static ViewModel GetViewModel()
        {
            ViewModel.TaskList = GetTasks();
            ViewModel.ListCatalog = GetTaskLists();

            return ViewModel;
        }
    }
}