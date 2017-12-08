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
        private DominosContext db = new DominosContext();
        Task task = new Task();
        // Delivery
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
            User courier = new User();
            int status = 0;
            if ((User)Session["user"] != null)
            {
                courier = (User)Session["user"];

                List<Task> tasks = db.Tasks
                                  .Where(z => z.RoleId != 4 && z.Status == "delivery")
                                  .Select(z => z).ToList();

                return PartialView("_TasksTableDelivery", tasks);
            }
            else
            {
                return Json(data: new { Data = status }, behavior: JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public ActionResult ShowTasksInWork()
        {
            User courier = new User();
            int status = 0;
            if ((User)Session["user"] != null)
            {
                courier = (User)Session["user"];
                List<TaskStatusChange> changes = db.TaskStatusChanges
                                  .Where(z => z.UserId == courier.UserId && z.In == z.Out)
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
                return PartialView("_TasksTableDeliveryInWork", tasks);
            }
            else
            {
                return Json(data: new { Data = status }, behavior: JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public ActionResult TaskInWork(int taskId)
        {
            User courier = new User();
            int status = 0;
            if ((User)Session["user"] != null)
            {
                courier = (User)Session["user"];
                TaskStatusChange change = new TaskStatusChange();
                change.TaskId = taskId;
                change.UserId = courier.UserId;
                change.In = DateTime.Now;
                change.Out = change.In;
                db.TaskStatusChanges.Add(change);
                Task task = db.Tasks.Where(z => z.TaskId == taskId).Select(z => z).FirstOrDefault();
                task.RoleId = 4;
                db.Entry(task).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                status = change.TaskStatusChangeId;

            }
            return Json(data: new { Data = status }, behavior: JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult TaskDone(int taskId)
        {
            User courier = new User();
            int status = 0;
            if ((User)Session["user"] != null)
            {
                courier = (User)Session["user"];
                TaskStatusChange change = db.TaskStatusChanges
                                                              .Where(z => z.TaskId == taskId && z.UserId == courier.UserId)
                                                              .FirstOrDefault();
                if (change.In == change.Out)
                {
                    change.Out = DateTime.Now;
                }
                db.Entry(change).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                status = change.TaskStatusChangeId;

                Task task = db.Tasks.Where(z => z.TaskId == taskId).FirstOrDefault();
                task.Status = Status.done.ToString();
                db.Entry(task).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            return Json(data: new { Data = status }, behavior: JsonRequestBehavior.AllowGet);
        }
    }
}