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
    public class CustomersController : Controller
    {
        private DominosContext db = new DominosContext();

        // GET: Customers
        public ActionResult Index()
        {
            return View(db.Customers.ToList());
        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
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

            return View();
        }

        public ActionResult EditPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditPassword(string CustomerPassword,string CustomerPasswordConfirm)
        {
            //using (DominosContext db = new DominosContext())
            //{
            //    customerPas = db.Customers.FirstOrDefault(u => u.CustomerEmail == User.Identity.Name);
            //}
            Customer customerPas = db.Customers.FirstOrDefault(u => u.CustomerEmail == User.Identity.Name);
            customerPas.CustomerPassword = CustomerPassword;
            customerPas.CustomerPasswordConfirm = CustomerPasswordConfirm;

            if (ModelState.IsValid)
            {
                db.Entry(customerPas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("PersonalArea", "Customers");
            }
            return View(customerPas);
        }
       
        public ActionResult CustomerTask()
        {
            ViewBag.Message = "Заказы пользователя";
            return PartialView();
        }

        public ActionResult PersonalArea()
        {
            Customer customer = null;
            using (DominosContext db = new DominosContext())
            {
                customer = db.Customers.FirstOrDefault(u => u.CustomerEmail == User.Identity.Name);
            }
            return View(customer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Auth(CustomerLogin model)
        {
            if (ModelState.IsValid)
            {
                Customer customer = null;
                using (DominosContext db = new DominosContext())
                {
                    customer = db.Customers.FirstOrDefault(u => u.CustomerEmail == model.CustomerEmail && u.CustomerPassword == model.CustomerPassword);

                }
                if (customer != null)
                {
                    FormsAuthentication.SetAuthCookie(model.CustomerEmail, true);
                    Session["user"] = customer;
                    if ((Cart)Session["cart"] != null)
                        return RedirectToAction("Cart", "Home");
                    else
                        return RedirectToAction("Index", "Home");
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
        public ActionResult Create([Bind(Include = "CustomerId,CustomerFirstName,CustomerPatronymic,CustomerLastName,CustomerBirthDate,CustomerSex,CustomerPhone,CustomerEmail,CustomerPassword,CustomerPasswordConfirm")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Auth","Customers");
            }

            return View(customer);
        }
       
        

            // GET: Customers/Edit/5
            public ActionResult Edit(int? id)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Customer customer = db.Customers.Find(id);
                if (customer == null)
                {
                    return HttpNotFound();
                }
                return View(customer);
            }

            

        // POST: Customers/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CustomerId,CustomerFirstName,CustomerPatronymic,CustomerLastName,CustomerBirthDate,CustomerSex,CustomerPhone,CustomerEmail,CustomerPassword,CustomerPasswordConfirm")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
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
