using DominosPizza.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DominosPizza.Controllers
{
    public class KitchenController : Controller
    {

        private DominosContext db = new DominosContext();
        Task task = new Task();
        // GET: Manager
        public ActionResult Index()
        {
            User user = new User();
            Contact contact = new Contact();
            if ((User)Session["user"] != null)
            {
                user = (User)Session["user"];

            }
            ViewBag.user = user;

            IEnumerable<Product> products = db.Products;

            Dictionary<int, string> productNames = new Dictionary<int, string>();

            foreach (var temp in products)
            {
                productNames.Add(temp.ProductId, temp.Name);
            }
            ViewBag.prod = productNames;

            return View();
        }
        [HttpPost]
        public ActionResult ShowTasksTable()
        {
            User cook = new User();
            int status = 0;
            if ((User)Session["user"] != null)
            {
                cook = (User)Session["user"];
                
                List<Task> tasks = db.Tasks
                                  .Where(z => z.RoleId != 3 && z.Status == "kitchen")
                                  .Select(z => z).ToList();

                return PartialView("_TasksTableKitchen", tasks);
            }
            else
            {
                return Json(data: new { Data = status }, behavior: JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public ActionResult ShowTasksInWork()
        {
            User cook = new User();
            int status = 0;
            if ((User)Session["user"] != null)
            {
                cook = (User)Session["user"];
                List<TaskStatusChange> changes  = db.TaskStatusChanges
                                  .Where(z => z.UserId == cook.UserId && z.In == z.Out)
                                  .Select(z => z).ToList();
                List<Task> tasks = new List<Task>();
                Task temp = null;
                foreach (var change in changes)
                {
                    temp = db.Tasks
                                    .Where(z => z.TaskId == change.TaskId)
                                    .Select(z => z)
                                    .FirstOrDefault();
                    tasks.Add(temp);
                }
                return PartialView("_TasksTableKitchenInWork", tasks);
            }
            else
            {
                return Json(data: new { Data = status }, behavior: JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public ActionResult TaskInWork(int taskId) {
            User cook  = new User();
            int status = 0;
            if ((User)Session["user"] != null)
            {
                cook = (User)Session["user"];
                TaskStatusChange change = new TaskStatusChange();
                change.TaskId = taskId;
                change.UserId = cook.UserId;
                change.In = DateTime.Now;
                change.Out = change.In;
                db.TaskStatusChanges.Add(change);
                Task task = db.Tasks.Where(z => z.TaskId == taskId).Select(z => z).FirstOrDefault();
                task.RoleId = 3; 
                db.Entry(task).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                status = change.TaskStatusChangeId;

            }
            return Json(data: new { Data = status }, behavior: JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult TaskDone(int taskId)
        {
            User cook = new User();
            int status = 0;
            if ((User)Session["user"] != null)
            {
                cook = (User)Session["user"];
                TaskStatusChange change = db.TaskStatusChanges
                                                              .Where(z => z.TaskId == taskId && z.UserId == cook.UserId)
                                                              .FirstOrDefault();
                if (change.In == change.Out)
                {
                    change.Out = DateTime.Now;
                }
                db.Entry(change).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                status = change.TaskStatusChangeId;

                Task task = db.Tasks.Where(z => z.TaskId == taskId).FirstOrDefault();
                task.Status = Status.delivery.ToString();
                db.Entry(task).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            return Json(data: new { Data = status }, behavior: JsonRequestBehavior.AllowGet);
        }
        /*
        DominosContext db = new DominosContext();
        // GET: Kitchen
        public ActionResult Index()
        {
            IEnumerable<Products> products = db.ProductsDbSet;
            Dictionary<int, string> productNames = new Dictionary<int, string>();

            foreach (var temp in products)
            {
                productNames.Add(temp.ProductId, temp.ProductName);
            }
            ViewBag.prod = productNames;
            IEnumerable<Tasks> tasks = db.TasksDbSet;
            IEnumerable<TaskList> tasksList = db.TaskListsDbSet;
            foreach (Tasks task in tasks)
            {
                var tid = task.TasksId; 
                foreach (TaskList tasklist in tasksList)
                {
                    if (tid == tasklist.TaskId)
                    {
                        task.AddDishToTaskList(tasklist.ProductId, tasklist.ProductQuantity);
                    }
                }
            }
            ViewBag.tasks = tasks;
            IEnumerable<Users> users = db.UsersDbSet;
            ViewBag.users = users;
            return View();
        }

        [HttpPost]
        public RedirectToRouteResult KitchenReadyTask(int TasksId, int userId)
        {
            //1. Get  from DB
            Tasks task = db.TasksDbSet.FirstOrDefault(u => u.TasksId == TasksId);
            //Tasks  task = db.TasksDbSet.Where(s => s.TasksId == TasksId).FirstOrDefault();
            int TaskStatusId = 2;
                                                ////нужно не забыть добавить таблицу для TaskStatusChange, а то пока пишет в никуда
            if (task != null)
            {
               if ( task.TaskStatusChange(TaskStatusId, userId, DateTime.Now) == TaskStatusId) {
                    task.TaskStatusId = TaskStatusId;
                                        
                    //3. Mark entity as modified
                    db.Entry(task).State = System.Data.Entity.EntityState.Modified;
                    
                    //4. call SaveChanges
                    db.SaveChanges();
            
                }
            }
            return RedirectToRoute(new { controller = "Kitchen", action = "Index" });
        }*/
    }
}