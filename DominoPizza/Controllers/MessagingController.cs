using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DominosPizza.Models;
using System.Data.Entity;

namespace DominoPizza.Controllers
{
    public class MessagingController : Controller
    {
        DominosContext db = new DominosContext();
        // GET: Messaging
        public ActionResult Index()
        {
            return View();
        }

        //[HttpPost]
        //public ActionResult AddNewComment(int taskId, string CommentText)
        //{
        //    Customer user = new Customer();
        //    if ((Customer)Session["user"] != null)
        //    {
        //        user = (Customer)Session["user"];
        //        UserComment newComment = new UserComment();
        //        newComment.TaskId = taskId;
        //        newComment.UserId = user.UserId;
        //        newComment.Text = CommentText;
        //        newComment.DateTime = DateTime.Now;
        //        db.UserComments.Add(newComment);
        //        Task task = db.Tasks.Where(z => z.TaskId == taskId).Select(z => z).FirstOrDefault();
        //        task.UserComment++;
        //        db.Entry(task).State = System.Data.Entity.EntityState.Modified;
        //        db.SaveChanges();
        //        return Json(data: new { Data = newComment.UserCommentId }, behavior: JsonRequestBehavior.AllowGet);
        //    }
        //    else
        //    {
        //        return Json(data: new { Data = 0 }, behavior: JsonRequestBehavior.AllowGet);
        //    }
        //}

        [HttpPost]
        public ActionResult ShowDialog(string onlineChatUniqueId)
        {
            Customer user = new Customer();
            List<OnlineChatMessage> rows = null;
            if (Session["onlineChatUniqueId"] != null && user.CustomerRoleId != 3)
            {
                onlineChatUniqueId = (string)Session["onlineChatUniqueId"];
            }
            rows = db.OnlineChatMessages
                                                    .Where(c => c.OnlineChatUniqueId == onlineChatUniqueId)
                                                    .Select(c => c).ToList();
            if (rows.Count() > 0) { Session["managerId"] = rows.FirstOrDefault().ManagerId; }
            return PartialView("_OnlineChatDialog", rows);
        }

        [HttpPost]
        public ActionResult AddNewMessageToChat(string onlineChatUniqueId, int userId, bool messageByManager, string messageText)
        {
            Customer user = new Customer();
            user.CustomerRoleId = 1;
            user.CustomerId = 2; //анонимный клиент нужен для работы чата, CustomerId = 2
            int managerId = 1;
            OnlineChatMessage newMessage = new OnlineChatMessage();
            newMessage.Assigned = false;
            if (Session["user"] != null)
            {
                user = (Customer)Session["user"];
                userId = user.CustomerId;
                if (user.CustomerRoleId != 3)
                {
                    if (Session["managerId"] != null) { managerId = (int)Session["managerId"]; newMessage.Assigned = true; }
                }
                else
                {
                    managerId = user.CustomerId;
                }
            }
            else
            {
                if (Session["managerId"] != null) { managerId = (int)Session["managerId"]; newMessage.Assigned = true; }
            }


            if (Session["onlineChatUniqueId"] != null && user.CustomerRoleId != 3)
            {
                onlineChatUniqueId = (string)Session["onlineChatUniqueId"];
            }

            if (onlineChatUniqueId == null) { onlineChatUniqueId = "new"; }
            if (onlineChatUniqueId == "undefined") { onlineChatUniqueId = "new"; }
            newMessage.UserId = userId;
            newMessage.ManagerId = managerId;
            newMessage.IsByManager = messageByManager;
            if (onlineChatUniqueId != "new")
            {

                newMessage.OnlineChatUniqueId = onlineChatUniqueId;
            }
            else
            {
                newMessage.Assigned = false;
                newMessage.OnlineChatUniqueId = DateTime.Now.ToString("yyyyMMdd.HH:mm.ssfff");
                Session["onlineChatUniqueId"] = newMessage.OnlineChatUniqueId;
            }
            newMessage.Text = messageText;
            newMessage.DateTime = DateTime.Now;
            newMessage.IsNew = true;

            db.OnlineChatMessages.Add(newMessage);
            db.SaveChanges();

            return Json(data: new { Data = newMessage.OnlineChatUniqueId }, behavior: JsonRequestBehavior.AllowGet);
            //return PartialView("_CommentsWindowPartial", rows);

        }
        //вывод чата для менеджера
        [HttpPost]
        public ActionResult ShowChat()
        {
            Customer manager = new Customer();
            Dictionary<string, int> chatlist = new Dictionary<string, int>();            //<номер диалога, взят менеджером>
            Dictionary<string, int> dialogsWithNewMessages = new Dictionary<string, int>(); //<номер диалого, кол-во новых сообщений>
            IEnumerable<OnlineChatMessage> rows = null;
            if (Session["user"] != null)
            {
                manager = (Customer)Session["user"];

                rows = db.OnlineChatMessages
                                                        .Where(c => c.ManagerId == manager.CustomerId || c.Assigned == false)
                                                        .Select(c => c);
            }
            else
            {
                rows = db.OnlineChatMessages;
            }
            foreach (var message in rows)
            {
                if (!(chatlist.ContainsKey(message.OnlineChatUniqueId)))
                    chatlist.Add(message.OnlineChatUniqueId, message.Assigned ? 1 : 0);

                if (message.IsNew && !(message.IsByManager))
                {
                    if (dialogsWithNewMessages.ContainsKey(message.OnlineChatUniqueId))
                    {
                        dialogsWithNewMessages[message.OnlineChatUniqueId]++;
                    }
                    else
                    {
                        dialogsWithNewMessages.Add(message.OnlineChatUniqueId, 1);
                    }
                }
            }
            List<Dictionary<string, int>> list = new List<Dictionary<string, int>>();
            list.Add(chatlist);
            list.Add(dialogsWithNewMessages);

            return PartialView("_OnlineChatList", list);
        }
        [HttpPost]
        public ActionResult ShowDialogManager(string onlineChatUniqueId)
        {
            Customer manager = new Customer();
            int managerId = 1;
            if (Session["user"] != null)
            {
                manager = (Customer)Session["user"];
                managerId = manager.CustomerId;
            }
            if (onlineChatUniqueId == "empty" || onlineChatUniqueId == null)
            {
                IEnumerable<OnlineChatMessage> rows2 = db.OnlineChatMessages;
                /* .Where(c => c.OnlineChatUniqueId == onlineChatUniqueId)
                 .Select(c => c);*/
                if (rows2.Where(z => z.ManagerId == managerId || z.Assigned == false).Count() < 1)
                {
                    return Json(data: new { Data = 0 }, behavior: JsonRequestBehavior.AllowGet);
                }
                else
                {
                    onlineChatUniqueId = rows2.Where(z => z.ManagerId == managerId || z.Assigned == false).LastOrDefault().OnlineChatUniqueId;
                }
            }

            IQueryable<OnlineChatMessage> rows = db.OnlineChatMessages
                                        .Where(c => c.OnlineChatUniqueId == onlineChatUniqueId)
                                        .Select(c => c);
            foreach (var message in rows)
            {
                message.Assigned = true;
                message.ManagerId = managerId;
                if (!(message.IsByManager))
                {
                    message.IsNew = false;
                }
                db.Entry(message).State = System.Data.Entity.EntityState.Modified;
            }
            db.SaveChanges();

            return PartialView("_OnlineChatDialogForManager", rows);
        }

        [HttpPost]
        public ActionResult ChatIndicators()
        {
            /*   int indicator1 = db.OnlineChatRows
                                           .Where(c => c.Assigned == false)
                                           .Select(c => c.OnlineChatUniqueId)
                                           .Distinct()
                                           .Count();*/
            int indicator1 = db.OnlineChatMessages
                             .Where(c => c.Assigned == false)
                             .GroupBy(c => c.OnlineChatUniqueId)
                             .Count();
            Customer manager = new Customer();
            int managerId = 1;
            if (Session["user"] != null)
            {
                manager = (Customer)Session["user"];
                managerId = manager.CustomerId;
            }
            int indicator2 = db.OnlineChatMessages
                                        .Where(c => c.IsNew == true && c.IsByManager == false && c.ManagerId == managerId)
                                        .Select(c => c)
                                        .Count();
            return Json(data: new { indicator1 = indicator1, indicator2 = indicator2 }, behavior: JsonRequestBehavior.AllowGet);

        }
    }
}