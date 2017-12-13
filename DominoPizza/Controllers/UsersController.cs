using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DominosPizza.Models;
using System.Web.Security;

namespace DominoPizza.Controllers
{
    public class UsersController : Controller
    {
        private DominosContext db = new DominosContext();

        // GET: Customers
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User customer = db.Users.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            return View();
        }
        public ActionResult Auth()
        {
            ViewBag.Message = "Вход";
            if (Request.UrlReferrer != null) {
                Session["backUrl"] = Request.UrlReferrer.ToString(); }
            return View();
        }

        public ActionResult EditPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AuthBlock()
        {
            Session["backUrl"] = Request.UrlReferrer.ToString();
            return PartialView("_AuthPartial");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditPassword(string CustomerPassword,string CustomerPasswordConfirm)
        {
            //using (DominosContext db = new DominosContext())
            //{
            //    customerPas = db.Customers.FirstOrDefault(u => u.CustomerEmail == User.Identity.Name);
            //}
            User customerPas = db.Users.FirstOrDefault(u => u.Email == User.Identity.Name);
            customerPas.Password = CustomerPassword;
            customerPas.PasswordConfirm = CustomerPasswordConfirm;

            if (ModelState.IsValid)
            {
                db.Entry(customerPas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("PersonalArea", "Customers");
            }
            return View(customerPas);
        }
       

        public ActionResult PersonalArea()
        {
            User customer = null;
            using (DominosContext db = new DominosContext())
            {
                customer = db.Users.FirstOrDefault(u => u.Email == User.Identity.Name);
            }
            return View(customer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Auth(UserLogin model)
        {
            if (ModelState.IsValid)
            {
                User user = null;
                using (DominosContext db = new DominosContext())
                {
                    user = db.Users.FirstOrDefault(u => u.Email == model.Email && u.Password == model.Password);
                }
                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(model.Email, true);
                    Session["user"] = user;
                    if (user.UserRoleId == 5)
                    {
                        return RedirectToRoute(new { controller = "Crmpanel", action = "Manage" });
                    }
                    if (user.UserRoleId == 2)
                    {
                        return RedirectToRoute(new { controller = "Manager", action = "Index" });
                    }
                    if (user.UserRoleId == 3)
                    {
                        return RedirectToRoute(new { controller = "Kitchen", action = "Index" });
                    }
                    if (user.UserRoleId == 4)
                    {
                        return RedirectToRoute(new { controller = "Delivery", action = "Index" });
                    }

                    if ((string)Session["backUrl"] != null)
                    {
                        string backUrl = (string)Session["backUrl"];
                        Session["backUrl"] = null;
                        return Redirect(backUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Пользователя с таким логином и паролем нет");
                }
            }
            return View();
        }

        public ActionResult Logoff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
        // POST: Customers/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserId,FirstName,Patronymic,LastName,BirthDate,Sex,Phone,Email,Password,PasswordConfirm")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Auth","Customers");
            }
            return View(user);
        }
       
        

            // GET: Customers/Edit/5
            public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

            

        // POST: Customers/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CustomerId,CustomerFirstName,CustomerPatronymic,CustomerLastName,CustomerBirthDate,CustomerSex,CustomerPhone,CustomerEmail,CustomerPassword,CustomerPasswordConfirm")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
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
