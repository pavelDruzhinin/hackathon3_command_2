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
        public string Index()
        {
            string result = "Вы не авторизированы";
            if (User.Identity.IsAuthenticated)
            {
                result = "Ваш логин: " + User.Identity.Name;
            }
            return result;
        }

        [Authorize]
        public ActionResult About()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
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
                    return RedirectToAction("Index", "Crmpanel");
                }
                else
                {
                    ModelState.AddModelError("", "Пользователь с таким логином и паролем не существует.");
                }
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
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

                    if (user != null)
                    {
                        FormsAuthentication.SetAuthCookie(model.Name, true);
                        return RedirectToAction("Index", "Crmpanel");
                    }
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