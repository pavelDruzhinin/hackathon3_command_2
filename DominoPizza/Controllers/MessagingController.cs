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
        public ActionResult ShowDialog(string onlineChatUniqueId)
        {
            IQueryable<OnlineChatRow> rows = db.OnlineChatRows
                                                    .Where(c => c.OnlineChatUniqueId == onlineChatUniqueId)
                                                    .Select(c => c);
            return PartialView("_OnlineChatDialog", rows);
        }

        [HttpPost]
        public ActionResult AddNewMessageToChat(string onlineChatUniqueId, int customerId, int userId, bool messageByUser, string messageText)
        {
            OnlineChatRow newRow = new OnlineChatRow();
            if (onlineChatUniqueId == null) { onlineChatUniqueId = "new"; }
            if (onlineChatUniqueId == "undefined") { onlineChatUniqueId = "new"; }
            newRow.CustomerId = customerId;                         //!!!     0 если незарегистрированный
            newRow.UserId = userId;
            newRow.MessageByUser = messageByUser;
            if (onlineChatUniqueId != "new")
            {
                newRow.Assigned = false;
                newRow.OnlineChatUniqueId = onlineChatUniqueId;
            }
            else
            {
                newRow.Assigned = false;
                newRow.OnlineChatUniqueId = DateTime.Now.ToString("yyyyMMdd.HH:mm.ssfff");
            }
            newRow.MessageText = messageText;
            newRow.MessageDateTime = DateTime.Now;
            newRow.MessageIsNew = true;
            /*           IQueryable<Users> table = db.UsersDbSet
                                                  .Where(c => c.UsersId == userId)
                                                  .Select(c => c);
                      newRow.UserName = table.FirstOrDefault().UserName;*/

            db.OnlineChatRows.Add(newRow);
            db.SaveChanges();
            bool addStatus = false;
            if (newRow.OnlineChatRowId != 0) addStatus = true;

            /*            IQueryable<OnlineChatRow> rows = db.OnlineChatRows
                                                                   .Where(c => c.OnlineChatUniqueId == onlineChatUniqueId)
                                                                   .Select(c => c); */

            return Json(data: new { Data = newRow.OnlineChatUniqueId }, behavior: JsonRequestBehavior.AllowGet);
            //return PartialView("_CommentsWindowPartial", rows);
            /*      

              */



        }
        //вывод чата для менеджера
        [HttpPost]
        public ActionResult ShowChat()
        {
            Dictionary<string, int> chatlist = new Dictionary<string, int>();            //<номер диалога, взят менеджером>
            Dictionary<string, int> dialogsWithNewMessages = new Dictionary<string, int>(); //<номер диалого, кол-во новых сообщений>
            IEnumerable<OnlineChatRow> rows = db.OnlineChatRows
                                                    /*.Where(c => c.OnlineChatUniqueId == onlineChatUniqueId)
                                                    .Select(c => c)
                                                    .OrderByDescending(c => c.MessageDateTime)
                                                    .Select(c => c)*/;
            foreach (var row in rows)
            {
                if (!(chatlist.ContainsKey(row.OnlineChatUniqueId)))
                    chatlist.Add(row.OnlineChatUniqueId, row.Assigned ? 1 : 0);

                if (row.MessageIsNew && !(row.MessageByUser))
                {
                    if (dialogsWithNewMessages.ContainsKey(row.OnlineChatUniqueId))
                    {
                        dialogsWithNewMessages[row.OnlineChatUniqueId]++;
                    }
                    else
                    {
                        dialogsWithNewMessages.Add(row.OnlineChatUniqueId, 1);
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
                IEnumerable<OnlineChatRow> rows2 = db.OnlineChatRows;
                /* .Where(c => c.OnlineChatUniqueId == onlineChatUniqueId)
                 .Select(c => c);*/
                onlineChatUniqueId = rows2.LastOrDefault().OnlineChatUniqueId;
            }

            IQueryable<OnlineChatRow> rows = db.OnlineChatRows
                                        .Where(c => c.OnlineChatUniqueId == onlineChatUniqueId)
                                        .Select(c => c);
            /*               if (!(rows.FirstOrDefault().Assigned))
                           {
                               foreach (var row in rows)
                               {
                                   row.Assigned = true;
                                   db.Entry(row).State = System.Data.Entity.EntityState.Modified;
                               }
                               db.SaveChanges();
                           }*/

            foreach (var row in rows)
            {
                row.Assigned = true;
                if (!(row.MessageByUser))
                {
                    row.MessageIsNew = false;
                }
                db.Entry(row).State = System.Data.Entity.EntityState.Modified;
            }
            db.SaveChanges();
            /*           //1. Get  from DB
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
            }*/


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

            int indicator2 = db.OnlineChatRows
                                        .Where(c => c.MessageIsNew == true && c.MessageByUser == false)
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