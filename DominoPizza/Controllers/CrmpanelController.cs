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
        // GET: Crmpanel
        [Authorize] // только авторизированный пользователь может получить доступ к странице управления CRM
        public ActionResult Manage() // страница управления пиццерией
        {
            return View();
        }

        [HttpGet]
        public ActionResult Index() // страница логина в CRM panel
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(LoginModel model) // метод авторизации
        {
            if (ModelState.IsValid)
            {
                User user = null;
                using (DominosContext db = new DominosContext())
                {
                    user = db.Users.FirstOrDefault(u => u.Login == model.Name && u.Password == model.Password);
                }
                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(model.Name, true);
                    return RedirectToAction("Manage", "Crmpanel"); // при успешной авторизации перенаправляем пользователя в админку
                }
                else
                {
                    ModelState.AddModelError("", "Пользователь с таким логином и паролем не существует.");
                }
            }
            return View(model);
        }

        [Authorize] // только авторизированный пользователь может зарегистрировать в системе сотрудника, доступ будет дан только Управляющему пиццерией
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model) // метод регистрации сотрудника, указывается логин, пароль, ФИО, роль
        {
            if (ModelState.IsValid)
            {
                User user = null;
                using (DominosContext db = new DominosContext())
                {
                    user = db.Users.FirstOrDefault(u => u.Login == model.Name);
                }
                if (user == null)
                {
                    using (DominosContext db = new DominosContext())
                    {
                        db.Users.Add(new User { Login = model.Name, Password = model.Password, UserRoleId = model.Role, UserFirstName = model.FirstName, UserLastName = model.LastName, UserPatronymic = model.Patronymic });
                        db.SaveChanges();

                        user = db.Users.Where(u => u.Login == model.Name && u.Password == model.Password).FirstOrDefault();
                    }

                    //if (user != null) // проверка что сотрудника добавили отключим, добавляет только Управляющий
                    //{
                    //    FormsAuthentication.SetAuthCookie(model.Name, true);
                    //    return RedirectToAction("Index", "Crmpanel"); 
                    //}
                }
                else
                {
                    ModelState.AddModelError("", "Пользователь с таким логином существует.");
                }
            }
            return View(model);
        }
    }
}