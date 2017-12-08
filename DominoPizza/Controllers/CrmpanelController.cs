using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DominosPizza.Models;
using DominoPizza.Models;
using System.Web.Security;

namespace DominosPizza.Controllers
{
    public class CrmpanelController : Controller
    {
        private DominosContext db = new DominosContext();

        // GET: Crmpanel
        [Authorize] // (Roles ="Administrator") только авторизированный пользователь может получить доступ к странице управления CRM
        public ActionResult Manage() // страница управления пиццерией
        {
            return View(db.Tasks.ToList());
        }

        [Authorize]
        public ActionResult Users()
        {
            //ViewBag.Users = new SelectList(db.Users, "ID", "FullName");

            return View(db.Users.ToList());
        }

        [Authorize] // (Roles = "Manager") только авторизированный пользователь может получить доступ к странице управления CRM
        public ActionResult Manager() // страница управления пиццерией
        {
            return View();
        }

        [Authorize] // (Roles = "Cook") только авторизированный пользователь может получить доступ к странице управления CRM
        public ActionResult Kitchen() // страница управления пиццерией
        {
            return View();
        }

        [Authorize] // (Roles = "Courier") только авторизированный пользователь может получить доступ к странице управления CRM
        public ActionResult Delivery() // страница управления пиццерией
        {
            return View();
        }
        /*
        [HttpGet]
        public ActionResult Index() // страница логина в CRM panel
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(CustomerLogin model) // метод авторизации
        {
            if (ModelState.IsValid)
            {
                Customer user = null;
                using (DominosContext db = new DominosContext())
                {
                    user = db.Customers.FirstOrDefault(u => u.CustomerEmail == model.CustomerEmail && u.CustomerPassword == model.CustomerPassword);
                }
                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(model.CustomerEmail, true); // добавить проверку роли и перенаправление на соответствующую страницу - "Manage" Администратор, "Manager" менеджер, "Kitchen" повар, "Delivery" курьер
                    return RedirectToAction("Manage", "Crmpanel"); // при успешной авторизации перенаправляем пользователя в админку
                }
                else
                {
                    ModelState.AddModelError("", "Неверное имя или пароль");
                }
            }
            return View(model);
        }
        */
       // [Authorize] // только авторизированный пользователь может зарегистрировать в системе сотрудника, доступ будет дан только Управляющему пиццерией или учетной записи администратора
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        /*
        public ActionResult Auth()
        {
            ViewBag.Message = "Вход";

            return View();
        }
        */
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model) // метод регистрации сотрудника, указывается логин, пароль, ФИО, роль
        {
            if (ModelState.IsValid)
            {
                User user = null;
                using (DominosContext db = new DominosContext())
                {
                    user = db.Users.FirstOrDefault(u => u.Email == model.Email);
                }
                if (user == null)
                {
                    using (DominosContext db = new DominosContext())
                    {
                        db.Users.Add(new User { Email = model.Email, Password = model.Password, UserRoleId = model.RoleId, FirstName = model.FirstName, LastName = model.LastName, Patronymic = model.Patronymic });
                        db.SaveChanges();

                        user = db.Users.Where(u => u.Email == model.Email && u.Password == model.Password).FirstOrDefault();
                    }

                    if (user != null) // проверка что сотрудника добавили отключим, добавляет только Управляющий
                    {
                    //    FormsAuthentication.SetAuthCookie(model.Name, true);
                        return RedirectToAction("Manage", "Crmpanel"); 
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Пользователь с таким логином существует.");
                }
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Auth", "Customer");
        }
    }
}