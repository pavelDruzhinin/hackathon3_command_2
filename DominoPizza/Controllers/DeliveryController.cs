using DominosPizza.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DominosPizza.Controllers
{
    public class DeliveryController : Controller
    {
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
        public RedirectToRouteResult DeliveryReadyTask(int TasksId, int userId) 
        {
            //1. Get  from DB
            Tasks task = db.TasksDbSet.FirstOrDefault(u => u.TasksId == TasksId);
            //Tasks  task = db.TasksDbSet.Where(s => s.TasksId == TasksId).FirstOrDefault();
            int TaskStatusId = 3;
            ////нужно не забыть добавить таблицу для TaskStatusChange, а то пока пишет в никуда
            if (task != null)
            {
                if (task.TaskStatusChange(TaskStatusId, userId, DateTime.Now) == TaskStatusId)
                {
                    task.TaskStatusId = TaskStatusId;

                    //3. Mark entity as modified
                    db.Entry(task).State = System.Data.Entity.EntityState.Modified;

                    //4. call SaveChanges
                    db.SaveChanges();

                }
            }
            return RedirectToRoute(new { controller = "Delivery", action = "Index" });
        }
    }
}