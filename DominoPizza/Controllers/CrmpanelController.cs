﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DominosPizza.Models;
using DominoPizza.Models;
using System.Web.Security;
using System.Net;
using System.Data.Entity;

namespace DominosPizza.Controllers
{
    public class CrmpanelController : Controller
    {
        private DominosContext _db = new DominosContext();
        
        // GET: Crmpanel Administrator Block
        [Authorize]
        public ActionResult UserProfile()
        {
            return View();
        }

        [Authorize] // (Roles ="Administrator") только авторизированный пользователь может получить доступ к странице управления CRM
        public ActionResult Manage() // страница управления пиццерией
        {
            return View();
        }
        
        [Authorize] // (Roles ="Administrator") только авторизированный пользователь может получить доступ к странице управления CRM
        public ActionResult Active_Orders() // страница управления пиццерией
        {
            return View(_db.Tasks.ToList());
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
                Customer user = null;
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
        public ActionResult Edit([Bind(Include = "CustomerId,CustomerFirstName,CustomerPatronymic,CustomerLastName,CustomerBirthDate,CustomerSex,CustomerPhone,CustomerEmail,CustomerPassword,CustomerPasswordConfirm")] Customer customer)
        {
                _db.Entry(customer).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Manage","Crmpanel");
        }

        [Authorize]
        [ValidateAntiForgeryToken]
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
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
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
            return View();
        }

        [Authorize] // (Roles = "Cook") только авторизированный пользователь может получить доступ к странице управления CRM
        public ActionResult Kitchen() // страница управления пиццерией
        {
            ViewBag.Title = "Dominos Pizza | Кухня";
            return View();
        }

        [Authorize] // (Roles = "Courier") только авторизированный пользователь может получить доступ к странице управления CRM
        public ActionResult Delivery() // страница управления пиццерией
        {
            ViewBag.Title = "Dominos Pizza | Доставка";
            return View();
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult LogOut()
        //{
        //    FormsAuthentication.SignOut();
        //    return RedirectToAction("Index", "Customer");
        //}
    }
}