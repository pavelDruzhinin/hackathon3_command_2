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

        [HttpPost]
        public ActionResult AddNewComment(int taskId, string CommentText)
        {
            User user = new User();
            if ((User)Session["user"] != null)
            {
                user = (User)Session["user"];
                UserComment newComment = new UserComment();
                newComment.TaskId = taskId;
                newComment.UserId = user.UserId;
                newComment.Text = CommentText;
                newComment.DateTime = DateTime.Now;
                db.UserComments.Add(newComment);
                Task task = db.Tasks.Where(z => z.TaskId == taskId).Select(z => z).FirstOrDefault();
                task.UserComment++;
                db.Entry(task).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Json(data: new { Data = newComment.UserCommentId }, behavior: JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(data: new { Data = 0 }, behavior: JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult ShowDialog(string onlineChatUniqueId)
        {
            IQueryable<OnlineChatMessage> rows = db.OnlineChatMessages
                                                    .Where(c => c.OnlineChatUniqueId == onlineChatUniqueId)
                                                    .Select(c => c);
            return PartialView("_OnlineChatDialog", rows);
        }

        [HttpPost]
        public ActionResult AddNewMessageToChat(string onlineChatUniqueId, int userId, bool messageByManager, string messageText)
        {
            OnlineChatMessage newMessage = new OnlineChatMessage();
            if (onlineChatUniqueId == null) { onlineChatUniqueId = "new"; }
            if (onlineChatUniqueId == "undefined") { onlineChatUniqueId = "new"; }
            newMessage.UserId = userId;
            newMessage.IsByManager = messageByManager;
            if (onlineChatUniqueId != "new")
            {
                newMessage.Assigned = false;
                newMessage.OnlineChatUniqueId = onlineChatUniqueId;
            }
            else
            {
                newMessage.Assigned = false;
                newMessage.OnlineChatUniqueId = DateTime.Now.ToString("yyyyMMdd.HH:mm.ssfff");
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
            Dictionary<string, int> chatlist = new Dictionary<string, int>();            //<номер диалога, взят менеджером>
            Dictionary<string, int> dialogsWithNewMessages = new Dictionary<string, int>(); //<номер диалого, кол-во новых сообщений>
            IEnumerable<OnlineChatMessage> rows = db.OnlineChatMessages
                                                    /*.Where(c => c.OnlineChatUniqueId == onlineChatUniqueId)
                                                    .Select(c => c)
                                                    .OrderByDescending(c => c.MessageDateTime)
                                                    .Select(c => c)*/;
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
            if (onlineChatUniqueId == "empty" || onlineChatUniqueId == null)
            {
                IEnumerable<OnlineChatMessage> rows2 = db.OnlineChatMessages;
                /* .Where(c => c.OnlineChatUniqueId == onlineChatUniqueId)
                 .Select(c => c);*/
                onlineChatUniqueId = rows2.LastOrDefault().OnlineChatUniqueId;
            }

            IQueryable<OnlineChatMessage> rows = db.OnlineChatMessages
                                        .Where(c => c.OnlineChatUniqueId == onlineChatUniqueId)
                                        .Select(c => c);
            foreach (var message in rows)
            {
                message.Assigned = true;
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
                                           .Count();
              int indicator1 = db.OnlineChatRows
                             .Where(c => c.Assigned == false)
                             .GroupBy(c => c.OnlineChatUniqueId)
                             .Count();*/

            int indicator2 = db.OnlineChatMessages
                                        .Where(c => c.IsNew == true && c.IsByManager == false)
                                        .Select(c => c)
                                        .Count();
            string indicators = $"{indicator2}";
            return Json(data: new { Data = indicators }, behavior: JsonRequestBehavior.AllowGet);

        }
        public ActionResult Muzzle()
        {
            IEnumerable<Product> temp = db.Products;
            List<Product> products = temp.ToList();
            return View(products);
        }
    }
}