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
        // public ActionResult Index()
        // {
        //     return View();
        // }
        [HttpPost]
        public ActionResult OnlineChatShow(int CustomerId)
        {
            IQueryable<OnlineChatRow> rows = db.OnlineChatRows
                                                    .Where(c => c.CustomerId == CustomerId)
                                                    .Select(c => c);
            return PartialView("_CommentsWindowPartial", rows);
        }

        [HttpPost]
        public ActionResult AddNewMessageToChat(string onlineChatUniqueId, int customerId, int userId, bool messageByUser, string messageText)
        {
            OnlineChatRow newRow = new OnlineChatRow();

            newRow.CustomerId = customerId;                         //!!!     0 если незарегистрированный
            newRow.UserId = userId;
            newRow.MessageByUser = messageByUser;
            if (onlineChatUniqueId != null)
            {
                newRow.Assigned = true;
                newRow.OnlineChatUniqueId = onlineChatUniqueId;
            }
            else
            {
                newRow.Assigned = false;
                newRow.OnlineChatUniqueId = DateTime.Now.ToString("yyyyMMddhhmmssfff");
            }
            newRow.MessageText = messageText;
            newRow.MessageDateTime = DateTime.Now;
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

            return Json(data: new { Data = addStatus }, behavior: JsonRequestBehavior.AllowGet);
            //return PartialView("_CommentsWindowPartial", rows);
            /*      

              */



        }

    }
}