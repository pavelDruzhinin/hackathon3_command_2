using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DominosPizza.Models;

namespace DominosPizza.Controllers
{
    public class ManagerController : Controller
    {
        private DominosContext db = new DominosContext();
        Tasks task = new Tasks();
        // GET: Manager
        public ActionResult Index()
        {
            IEnumerable<Products> products = db.ProductsDbSet;
            ViewBag.products = products;
            IEnumerable<Users> users = db.UsersDbSet;
            ViewBag.users = users;
            IEnumerable<Tasks> tasks = db.TasksDbSet;
            ViewBag.tasks = tasks;


            Dictionary<int, string> productNames = new Dictionary<int, string>();

            foreach (var temp in products)
            {
                productNames.Add(temp.ProductId, temp.ProductName);
            }
            ViewBag.prod = productNames;

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
            return View();
        }

        [HttpPost] 
        public ActionResult AddtoCart(int productId,  int amount)
        {

            if ((Tasks)Session["task"] != null)
            {
                task = (Tasks)Session["task"];
            }

            int counter = task.AddDishToTaskList(productId, amount);
            List<OrderTable> table = new List<OrderTable>();
            int i = 1;
            foreach (KeyValuePair < int, int > keyValue in task.TaskList)
            {
                OrderTable orderTableRow = new OrderTable();
                orderTableRow.OrderTableId = i++;
                orderTableRow.ProductId = keyValue.Key;
                orderTableRow.ProductQuantity = keyValue.Value;
                IQueryable<Products> product = db.ProductsDbSet
                                                    .Where(c => c.ProductId == keyValue.Key)
                                                    .Select(c => c);
                orderTableRow.ProductName = product.FirstOrDefault().ProductName;
                orderTableRow.ProductPrice = (int)product.FirstOrDefault().ProductPrice;
                table.Add(orderTableRow);
            }

            Session["task"] = task;

            return PartialView("_OrderTablePartial", table);
           // return Json(data: new { Data = counter }, behavior: JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public RedirectToRouteResult NewTaskFromWeb(int TasksId, int userId)
        {
            //1. Get  from DB
            Tasks task = db.TasksDbSet.FirstOrDefault(u => u.TasksId == TasksId);
            //Tasks  task = db.TasksDbSet.Where(s => s.TasksId == TasksId).FirstOrDefault();
            int TaskStatusId = 1;
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
            return RedirectToRoute(new { controller = "Manager", action = "Index" });
        }
        [HttpPost]
        public RedirectToRouteResult NewTask(int userId, string TaskDeliveryCustomerAddress, string TaskDeliveryCustomerPhone, string TaskDeliveryCustomerName, string TaskPaymentMethod)
        {
            //TaskDeliveryDateTime
            //IEnumerable<Tasks> Tasks = db.TasksDbSet;
            Tasks task = new Tasks();
            if ((Tasks)Session["task"] != null)
            {
                task = (Tasks)Session["task"];
            }
            task.TaskStatusId = 1;
            task.TaskDeliveryCustomerAddress = TaskDeliveryCustomerAddress;
            task.TaskDeliveryCustomerPhone = TaskDeliveryCustomerPhone;
            task.TaskDeliveryDateTime = DateTime.Now;
            task.TaskDeliveryCustomerName = TaskDeliveryCustomerName;
            task.TaskPaymentMethod = TaskPaymentMethod;
            task.TaskStatusChangeHistory.Add(userId, DateTime.Now);
            db.TasksDbSet.Add(task);
            db.SaveChanges();
            IEnumerable<Tasks> Tasks = db.TasksDbSet;
            var tid = Tasks.Last().TasksId;
            foreach (KeyValuePair<int, int> keyValue in task.taskList)
            {
                TaskList productList = new TaskList();
                productList.TaskId = tid;
                productList.ProductId = keyValue.Key;
                productList.ProductQuantity = keyValue.Value;
                db.TaskListsDbSet.Add(productList);
            }
            db.SaveChanges();
            Session["task"] = null;
            return RedirectToRoute(new { controller = "Manager", action = "Index" });
        }

        [HttpPost]
        public ActionResult TaskCommentsShow(int taskId)
        {
            IQueryable<TaskComments> table = db.TaskCommentsDbSet
                                                    .Where(c => c.TaskId == taskId)
                                                    .Select(c => c);
            return PartialView("_CommentsWindowPartial", table);
        }

        [HttpPost]
        public ActionResult AddNewComment(int TasksId, int userId, string CommentText)
        {
            TaskComments newComment = new TaskComments();
            newComment.TaskId = TasksId;
            newComment.UserId = userId;
            newComment.CommentText = CommentText;
            newComment.SetCommentTime(DateTime.Now);
            IQueryable<Users> table = db.UsersDbSet
                                        .Where(c => c.UsersId == userId)
                                        .Select(c => c);
            newComment.UserName = table.FirstOrDefault().UserName;

            db.TaskCommentsDbSet.Add(newComment);
            db.SaveChanges();
            IQueryable<TaskComments> table2 = db.TaskCommentsDbSet
                                                    .Where(c => c.TaskId == TasksId)
                                                    .Select(c => c);
            return PartialView("_CommentsWindowPartial", table2);
        }




        // GET: Manager/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tasks tasks = db.TasksDbSet.Find(id);
            if (tasks == null)
            {
                return HttpNotFound();
            }
            return View(tasks);
        }

        // GET: Manager/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Manager/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TasksId,TaskDeliveryCustomerAddress,TaskDeliveryCustomerPhone,TaskDeliveryCustomerName,TaskDeliveryDateTime,TaskPaymentMethod")] Tasks tasks)
        {
            if (ModelState.IsValid)
            {
                db.TasksDbSet.Add(tasks);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tasks);
        }

        // GET: Manager/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tasks tasks = db.TasksDbSet.Find(id);
            if (tasks == null)
            {
                return HttpNotFound();
            }
            return View(tasks);
        }

        // POST: Manager/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TasksId,TaskDeliveryCustomerAddress,TaskDeliveryCustomerPhone,TaskDeliveryCustomerName,TaskDeliveryDateTime,TaskPaymentMethod")] Tasks tasks)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tasks).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tasks);
        }

        // GET: Manager/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tasks tasks = db.TasksDbSet.Find(id);
            if (tasks == null)
            {
                return HttpNotFound();
            }
            return View(tasks);
        }

        // POST: Manager/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tasks tasks = db.TasksDbSet.Find(id);
            db.TasksDbSet.Remove(tasks);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
