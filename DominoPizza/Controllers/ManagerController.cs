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
            //ViewBag.products = products;
            //IEnumerable<User> users = db.Users;
            //ViewBag.users = users;
            //IEnumerable<Task> tasks = db.Tasks;
            //ViewBag.tasks = tasks;


            Dictionary<int, string> productNames = new Dictionary<int, string>();

            foreach (var temp in products)
            {
                productNames.Add(temp.ProductId, temp.Name);
            }
            ViewBag.prod = productNames;

            //IEnumerable<TaskRow> tasksList = db.TaskRows;
            //foreach (Task task in tasks)
            //{
            //    var tid = task.TaskId;
            //    foreach (TaskRow tasklist in tasksList)
            //    {
            //        if (tid == tasklist.TaskId)
            //        {
            //            task.AddDishToTaskList(tasklist.ProductId, tasklist.ProductQuantity);
            //        }
            //    }
            //}
            //ViewBag.tasks = tasks;
            return View();
        }

        [HttpPost]
        public ActionResult ShowTaskDetails(int taskId) 
        {
            TaskDetails taskdetails = new TaskDetails();
            taskdetails.inWorkFlag = false;
            User staff = new User(); 
            if ((User)Session["user"] != null)
                {
                staff = (User)Session["user"];
                Task task = db.Tasks.Where(z => z.TaskId == taskId).Select(z => z).FirstOrDefault();
                if (staff.UserRoleId == 2 && task.Status == "processed") {
                    TaskStatusChange statuschange2 = db.TaskStatusChanges
                                            .Where(z => z.TaskId == taskId && z.In == z.Out && z.UserId ==staff.UserId)
                                            .Select(z => z).FirstOrDefault();
                    if (statuschange2 == null) {
                        TaskStatusChange statuschange = new TaskStatusChange();
                        statuschange.TaskId = taskId;
                        statuschange.In = DateTime.Now;
                        statuschange.Out = statuschange.In;
                        statuschange.UserId = staff.UserId;
                        db.TaskStatusChanges.Add(statuschange);
                        task.RoleId = 2;
                        db.Entry(task).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }
                    taskdetails.inWorkFlag = true;
                }
                taskdetails.TaskId = taskId;
                taskdetails.userrole = staff.UserRoleId;
                Contact contact = db.Contacts.Where(z=>z.ContactId == task.ContactId).Select(z => z).FirstOrDefault();
                taskdetails.DateTime = task.DateTime;
                taskdetails.Status = task.Status;
                taskdetails.PayMethod = task.PayMethod;
                taskdetails.TotalSum = task.TotalSum;
                taskdetails.ClientFullName = contact.FullName;
                taskdetails.ClientPhone = contact.Phone;
                taskdetails.ClientAddress = contact.Address;
                taskdetails.changes = db.TaskStatusChanges.Where(z => z.TaskId == taskId).Select(z => z).ToList();
                taskdetails.tasklist = db.TaskRows.Where(z => z.TaskId == taskId).Select(z => z).ToList();
                taskdetails.comments = db.UserComments.Where(z => z.TaskId == taskId).Select(z => z).ToList();
                taskdetails.roles = db.UserRoles.Select(z => z).ToList();
                foreach (var change in taskdetails.changes)
                {
                    taskdetails.staff.Add(db.Users.Where(z => z.UserId == change.UserId).Select(z => z).FirstOrDefault());
                }
                foreach (var comm in taskdetails.comments)
                {
                    taskdetails.staff.Add(db.Users.Where(z => z.UserId == comm.UserId).Select(z => z).FirstOrDefault());
                }
                foreach (var row in taskdetails.tasklist)
                {
                    taskdetails.products.Add(db.Products.Where(z => z.ProductId == row.ProductId).Select(z => z).FirstOrDefault());
                }
                return PartialView("_TaskDetails", taskdetails);
            }
            else
            {
                return Json(data: new { Data = 0 }, behavior: JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public ActionResult TaskDone(int taskId)
        {
            //надо добавить комментарий от менеджера 
            User manager = new User();
            int status = 0;
            if ((User)Session["user"] != null)
            {
                manager = (User)Session["user"];
                TaskStatusChange change = db.TaskStatusChanges
                                                              .Where(z => z.TaskId == taskId && z.UserId == manager.UserId)
                                                              .FirstOrDefault();
                if (change.In == change.Out)
                {
                    change.Out = DateTime.Now;
                }
                db.Entry(change).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                status = change.TaskStatusChangeId;

                Task task = db.Tasks.Where(z => z.TaskId == taskId).FirstOrDefault();
                task.Status = Status.kitchen.ToString();
                db.Entry(task).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            return Json(data: new { Data = status }, behavior: JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ShowTasksTable()
        {
            IQueryable<Task> tasks = db.Tasks;
                                          //.Where(c => c.OnlineChatUniqueId == onlineChatUniqueId)
                                                    //.Select(c => c);
            return PartialView("_TasksTable", tasks);
        }

        [HttpPost]
        public ActionResult AddtoCart(int productId, int amount)
        {

            Cart cart = new Cart();
            int counter = 0;
            if ((Cart)Session["cart"] != null)
            {
                cart = (Cart)Session["cart"];
            }

            counter = cart.AddDishToCart(productId, amount);

            List<OrderTable> table = new List<OrderTable>();
                int i = 1;
            foreach (KeyValuePair<int, int> keyValue in cart.cartlist)
            {
                OrderTable orderTableRow = new OrderTable();
                orderTableRow.OrderTableId = i++;
                orderTableRow.ProductId = keyValue.Key;
                orderTableRow.ProductQuantity = keyValue.Value;
                IQueryable<Product> product = db.Products
                                                    .Where(c => c.ProductId == keyValue.Key)
                                                    .Select(c => c);
                orderTableRow.ProductName = product.FirstOrDefault().Name;
                orderTableRow.ProductPrice = (int)product.FirstOrDefault().Price;
                table.Add(orderTableRow);
            }


            Session["cart"] = cart;
                //Cart cart = new Cart();
                //if ((Cart)Session["cart"] != null)
                //{
                //    cart = (Cart)Session["cart"];
                //}
                //int counter = cart.AddDishToCart(productId, amount);
                //Session["cart"] = cart;
                //return Json(data: new { Data = counter }, behavior: JsonRequestBehavior.AllowGet);

                return PartialView("_OrderTablePartial", table);
            
            // return Json(data: new { Data = counter }, behavior: JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public ActionResult CartChangeQuantity(int productId, int amount)
        {
            Cart cart = new Cart();
            if ((Cart)Session["cart"] != null)
            {
                cart = (Cart)Session["cart"];
            }
            int counter = cart.EditCartList(productId, amount);
            Session["cart"] = cart;
            return Json(data: new { Data = counter }, behavior: JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public RedirectToRouteResult NewTaskFromCart(string TaskDeliveryCustomerAddress, string TaskDeliveryCustomerPhone, string TaskDeliveryCustomerName, string TaskPaymentMethod, string CustomerComment)
        {

            Task task = new Task();
            Cart cart = new Cart();
            User manager = new User();
            
            int contactId = 0;
            double sum = 0;
            if ((Cart)Session["cart"] != null && (User)Session["user"] != null)
            {

                manager = (User)Session["user"];
                Contact contact = new Contact();
                contact = db.Contacts.FirstOrDefault(u => u.Address == TaskDeliveryCustomerAddress &&
                                                          u.Phone == TaskDeliveryCustomerPhone &&
                                                          u.FullName == TaskDeliveryCustomerName);
                if (contact != null)
                {
                    contact.OrderDateTime = DateTime.Now;
                    db.Entry(contact).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    contactId = contact.ContactId;
                }
                else
                {
                    Contact newcontact = new Contact();
                    newcontact.Address = TaskDeliveryCustomerAddress;
                    newcontact.Phone = TaskDeliveryCustomerPhone;
                    newcontact.FullName = TaskDeliveryCustomerName;
                    newcontact.OrderDateTime = DateTime.Now;
                    newcontact.UserId = 2; //незарегистрированный клиент
                    db.Contacts.Add(newcontact);
                    db.SaveChanges();
                    contactId = newcontact.ContactId;
                }

                cart = (Cart)Session["cart"];

                foreach (var m in cart.cartlist)
                {
                    var mydish = db.Products.FirstOrDefault(k => k.ProductId == m.Key);
                    sum = mydish.Price * m.Value + sum;
                }

                task.TotalSum = sum;
                task.Status = Status.kitchen.ToString();
                task.DateTime = DateTime.Now; // НАДО переделать на запланированное время доставки + на вьюхе тоже
                task.PayMethod = Convert.ToInt32(TaskPaymentMethod);
                task.UserComment = 0;
                if (CustomerComment.Length > 0)
                {
                    task.UserComment = 1; //Переделал в ИНТ! Это поле счётчик комментов. Все комменты в UserComments.cs
                }
                task.ContactId = contactId;
                task.RoleId = 2;
                db.Tasks.Add(task);
                db.SaveChanges();

                var TaskId = task.TaskId;

                if (CustomerComment.Length > 0)
                {
                    UserComment comment = new UserComment();
                    comment.Text = CustomerComment;
                    comment.TaskId = TaskId;
                    comment.UserId = manager.UserId;
                    comment.DateTime = DateTime.Now;
                    db.UserComments.Add(comment);
                    db.SaveChanges();
                }
                TaskStatusChange change = new TaskStatusChange();
                change.TaskId = TaskId;
                change.UserId = manager.UserId;
                change.In = DateTime.Now;
                change.Out = DateTime.Now;
                db.TaskStatusChanges.Add(change);

                foreach (KeyValuePair<int, int> keyValue in cart.cartlist)
                {
                    TaskRow productList = new TaskRow();
                    productList.TaskId = TaskId;
                    productList.ProductId = keyValue.Key;
                    productList.Quantity = keyValue.Value;
                    db.TaskRows.Add(productList);
                }
                db.SaveChanges();
                Session["cart"] = null;
                Session["orderSuccess"] = true; //надо сделать проверку добавления заказа
            }
            else
            {
                Session["orderSuccess"] = false;
            }
            return RedirectToRoute(new {controller = "Manager", action = "Index" });
        }


        /*      [HttpPost]
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
             }*/
    }
}
