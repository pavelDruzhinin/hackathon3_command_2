using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using DominosPizza.Models;
using DominoPizza.Models;
using System.Web.Security;
using System.Net;
using System.Data.Entity;
using System.Data.Common;
using System.Data;
using System;


namespace DominosPizza.Controllers
{
    public class CrmpanelController : Controller
    {
        private DominosContext _db = new DominosContext();
        
        // GET: Crmpanel Administrator Block
        [Authorize]
        public ActionResult UserProfile()
        {
            if (User.Identity.IsAuthenticated == false)
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);

            var customer = _db.Customers.FirstOrDefault(m => m.CustomerEmail == User.Identity.Name);
            return View(customer);
        }

        [Authorize] // (Roles ="Administrator") только авторизированный пользователь может получить доступ к странице управления CRM
        public ActionResult Manage() // страница управления пиццерией
        {
            return View();
        }
        
        [Authorize] // (Roles ="Administrator") только авторизированный пользователь может получить доступ к странице управления CRM
        public ActionResult Active_Orders() // страница управления пиццерией
        {
            return View(_db.Tasks.ToList()); //
        }

        [Authorize] // только авторизированный пользователь может зарегистрировать в системе сотрудника, доступ будет дан только Управляющему пиццерией или учетной записи администратора
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model) // метод регистрации сотрудника, указывается логин, пароль, ФИО, роль
        {
            if (ModelState.IsValid)
            {
                Customer user;
                using (var db = new DominosContext())
                {
                    user = db.Customers.FirstOrDefault(u => u.CustomerEmail == model.Email);
                }
                if (user == null)
                {
                    using (var db = new DominosContext())
                    {
                        db.Customers.Add(new Customer { CustomerEmail = model.Email, CustomerPassword = model.Password, CustomerPasswordConfirm = model.ConfirmPassword, CustomerRoleId = model.RoleId, CustomerFirstName = model.FirstName, CustomerLastName = model.LastName, CustomerPatronymic = model.Patronymic, CustomerBirthDate = model.BirthDay, CustomerPhone = model.Phone, CustomerSex = model.Sex });
                        db.SaveChanges();

                        user = db.Customers.FirstOrDefault(u => u.CustomerEmail == model.Email && u.CustomerPassword == model.Password);
                    }

                    if (user != null)
                    {
                        return RedirectToAction("Manage", "Crmpanel");
                    }
                }
                else
                {
                    ModelState.AddModelError("", @"Пользователь с таким логином существует.");
                }
            }
            return View(model);
        }

        [Authorize]
        public ActionResult EditCustomer(int? id)
        {
            if (User.Identity.IsAuthenticated == false)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var customer = _db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCustomer(Customer customer)
        {
            if (User.Identity.IsAuthenticated == false)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }
            _db.Entry(customer).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Manage","Crmpanel");
        }

        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var customer = _db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var customer = _db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            _db.Customers.Remove(customer);
            _db.SaveChanges();
            return RedirectToAction("Manage","Crmpanel");
        }


        [Authorize] // (Roles ="Administrator")
        public ActionResult Users()
        {
            return View(_db.Customers.ToList());
        }

        // CRM Panel Sales Manager Block
        [Authorize] // (Roles = "Manager") только авторизированный пользователь может получить доступ к странице управления CRM
        public ActionResult Manager() // страница управления пиццерией
        {
            ViewBag.Title = "Dominos Pizza | Обработка заказов";
            return View(_db.Tasks.ToList());
        }

        [Authorize] // (Roles = "Manager") только авторизированный пользователь может получить доступ к странице управления CRM
        [HttpGet]
        public ActionResult OrderDetails(int id) // страница управления пиццерией
        {
            ViewBag.Title = "Dominos Pizza | Карточка заказа";
            var task = _db.Tasks.Find(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            ViewBag.Order = task.TaskId;
            var customer = _db.Customers.Find(task.CustomerId);
            if (customer != null)
            {
                ViewBag.FullName = customer.CustomerFullName();
                ViewBag.Telephone = customer.CustomerPhone;
            }
            var adress = _db.Contacts.Find(task.ContactId);
            if (adress != null)
            {
                ViewBag.Adress = adress.ContactAddress;
            }
            ViewBag.OrderDate = task.TaskDate.ToShortDateString();
            ViewBag.OrderTime = task.TaskDate.ToShortTimeString();
            ViewBag.DeliveryTime = null;
            int payment = task.TaskPayMethod;
            if (payment == 1)
            {
                ViewBag.Payment = "Оплачено";
            } else if (payment == 2)
            {
                ViewBag.Payment = "картой курьеру";
            } else if (payment == 3)
            {
                ViewBag.Payment = "наличными курьеру";
            }
            else
            {
                ViewBag.Payment = "нет данных";
            }

            ViewBag.TotalSumm = task.TaskTotalSum;
            ViewBag.Comment = task.TaskCustomerComment;
            //TaskRow
            var row = _db.TaskRows.Find(task.TaskId);
            return View(_db.TaskRows);
        }

        [Authorize]
        [HttpPost]
        public ActionResult OrderDetails(int id, string unusedValue = "")
        {
            var task = _db.Tasks.Find(id);
            if (task != null)
            {
                task.TaskStatus = "kitchen";
                _db.SaveChanges();
            }
            return RedirectToAction("Manager", "Crmpanel");
        }

        [Authorize] // (Roles = "Manager") только авторизированный пользователь может получить доступ к странице управления CRM
        public ActionResult EditOrder(int id) // страница управления пиццерией
        {
            ViewBag.Title = "Dominos Pizza | Редактирование карточки заказа";
            var task = _db.Tasks.Find(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(task);
        }

        [Authorize] // (Roles = "Cook") только авторизированный пользователь может получить доступ к странице управления CRM
        public ActionResult Kitchen() // страница управления пиццерией
        {
            ViewBag.Title = "Dominos Pizza | Кухня";
            return View(_db.Tasks.ToList());
        }

        [Authorize] // (Roles = "Cook") только авторизированный пользователь может получить доступ к странице управления CRM
        [HttpPost]
        public ActionResult Kitchen(int id) // страница управления пиццерией
        {
            ViewBag.Title = "Dominos Pizza | Кухня";
            var task = _db.Tasks.Find(id);
            if (task != null)
            {
                task.TaskStatus = "delivery";
                _db.SaveChanges();
            }
            return RedirectToAction("Kitchen", "Crmpanel");
            //return View(_db.Tasks.ToList());
        }

        [Authorize] // (Roles = "Courier") только авторизированный пользователь может получить доступ к странице управления CRM
        public ActionResult Delivery() // страница управления пиццерией
        {
            ViewBag.Title = "Dominos Pizza | Доставка";
            return View(_db.Tasks.ToList());
        }

        [Authorize]
        public ActionResult ChangePassword()
        {
            ViewBag.Title = "Dominos Pizza | Смена пароля";
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ChangePass model)
        {
            if (User.Identity.Name == null) return View();
            var customer = _db.Customers.FirstOrDefault(u => u.CustomerEmail == User.Identity.Name);

            if (customer != null)
            {
                customer.CustomerPassword = model.Password;
                customer.CustomerPasswordConfirm = model.PasswordConfirm;
            }
            if (customer != null && customer.CustomerPassword != customer.CustomerPasswordConfirm)
            {
                return View();
            }
            _db.Entry(customer).State = EntityState.Modified;
            _db.SaveChanges();

            if (customer != null && customer.CustomerRoleId == 2)
            {
                return RedirectToAction("Manage", "Crmpanel");
            }
            if (customer != null && customer.CustomerRoleId == 3)
            {
                return RedirectToAction("Manager", "Crmpanel");
            }
            if (customer != null && customer.CustomerRoleId == 4)
            {
                return RedirectToAction("Kitchen", "Crmpanel");
            }
            if (customer != null && customer.CustomerRoleId == 5)
            {
                return RedirectToAction("Delivery", "Crmpanel");
            }
            return RedirectToAction("PersonalArea", "Customers");
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
        [Authorize]
        public ActionResult Receipt(int TaskId)
        {
            //IEnumerable<Task> tasks = _db.Tasks.Where(x=>x.TaskId==TaskId);
            IEnumerable<TaskRow> taskrows = _db.TaskRows.Where(x => x.TaskId == TaskId).Include(x=>x.Product);
            IEnumerable<Task> tasks = _db.Tasks.Where(x => x.TaskId == TaskId);
            var task = tasks.First(x => x.TaskId == TaskId);
            var sum = task.TaskTotalSum;
            ViewBag.Sum = sum;
            //var product = _db.TaskRows.Where(x => x.TaskId == TaskId).Include(x => x.Product);
            //ViewBag.product = product;
            return View(taskrows);
            //return View(_db.Tasks.ToList());
        }

        [Authorize]
        [HttpPost]
        public ActionResult TookDeliv(int[] selectedOrd)
        {
            DominosContext db = new DominosContext();

            IEnumerable<Task> tasks = db.Tasks.ToList();
            //IEnumerable<Customer> cust = _db.Customers.ToList();
            foreach (int item in selectedOrd)
            {
                foreach (var iTask in tasks)
                {
                    
                    if (iTask.TaskId == item)
                    {
                        var task = db.Tasks.Find(iTask.TaskId);
                        task.TaskStatus = "delivery";
                        var customer = db.Customers.First(e => e.CustomerEmail == User.Identity.Name);


                        db.StatusHistories.Add(new StatusHistory { StatusChangeTime = DateTime.Now, StatusChangedTo = Status.delivery.ToString(), ForTask = iTask, DominosUser = customer });
                        db.SaveChanges();
                    }

                }
                
            }


            return RedirectToAction("DeliveryCour");
        }

        public ActionResult DeliveryCour()
        {
            DominosContext db = new DominosContext();
            return View(db.Tasks.ToList());
        }

        [Authorize]
        [HttpPost]
        public ActionResult FinDeliv(int[] selectedOrd)
        {
            DominosContext db = new DominosContext();

            IEnumerable<Task> tasks = db.Tasks.ToList();
            //IEnumerable<Customer> cust = _db.Customers.ToList();
            foreach (int item in selectedOrd)
            {
                foreach (var iTask in tasks)
                {

                    if (iTask.TaskId == item)
                    {
                        var task = db.Tasks.Find(iTask.TaskId);
                        task.TaskStatus = "done";
                        var customer = db.Customers.First(e => e.CustomerEmail == User.Identity.Name);


                        db.StatusHistories.Add(new StatusHistory { StatusChangeTime = DateTime.Now, StatusChangedTo = Status.delivery.ToString(), ForTask = iTask, DominosUser = customer });
                        db.SaveChanges();
                    }

                }

            }


            return RedirectToAction("FinDelivery");
        }

        public ActionResult FinDelivery()
        {
            DominosContext db = new DominosContext();

            return View(db.Tasks.ToList());
           
        }
    }
}