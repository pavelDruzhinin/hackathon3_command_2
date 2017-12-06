using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DominosPizza.Models;
using DominoPizza.Controllers;

namespace DominosPizza.Controllers
{
    public class HomeController : Controller
    {
        DominosContext db = new DominosContext();

        public ActionResult Index()
        {
            Cart cart = new Cart();
            if ((Cart)Session["cart"] != null)
            {
                cart = (Cart)Session["cart"];
            }
            IEnumerable<Product> products = db.Products;
            return View(products);
        }

        public ActionResult Stock()
        {
            ViewBag.Message = "Акции";

            return View();
        }

        public ActionResult Delivery()
        {
            //ViewBag.Message = "Информация как заказать";
            ViewBag.Phone = "(8142) xx-xx-xx";
            return View();
        }

        public ActionResult Contacts()
        {
            ViewBag.Message = "Контактная информация";

            return View();
        }

        public ActionResult Vacancies()
        {
            ViewBag.Message = "Вакансии";

            return View();
        }

        public ActionResult Cart()
        {
            if (User.Identity.IsAuthenticated)
            {

                ViewBag.Message = "Оформление заказа";

                Cart cart = new Cart();
                if ((Cart)Session["cart"] != null)
                {
                    cart = (Cart)Session["cart"];
                }
                ViewBag.cartList = cart.cartlist;
                IEnumerable<Product> products = db.Products;
                ViewBag.products = products;

                Dictionary<int, string> productNames = new Dictionary<int, string>();
                foreach (var temp in products)
                {
                    productNames.Add(temp.ProductId, temp.ProductName);
                }
                ViewBag.prod = productNames;
                ViewBag.cartindicator = cart.Counter;


                List<OrderTable> table = new List<OrderTable>();
                int i = 1;
                foreach (KeyValuePair<int, int> keyValue in cart.cartlist)
                {
                    OrderTable orderTableRow = new OrderTable();
                    orderTableRow.OrderTableId = i++;
                    orderTableRow.ProductId = keyValue.Key;
                    orderTableRow.ProductQuantity = keyValue.Value;
                    IQueryable<Product> product = db.Products
                                                        .Where(c => c.ProductId == keyValue.Key)
                                                        .Select(c => c);
                    orderTableRow.ProductName = product.FirstOrDefault().ProductName;
                    orderTableRow.ProductPrice = (int)product.FirstOrDefault().ProductPrice;
                    table.Add(orderTableRow);
                }
                return View(table);
            }
            else
            {
                return RedirectToAction("Auth", "Customers");
            }
        }

        public JsonResult FindLastContact()
        {
            var hiscontacts = db.Customers.Where(e => e.CustomerEmail == User.Identity.Name).Include(e => e.Contacts);
            Customer mycustomer = hiscontacts.First(e => e.CustomerEmail == User.Identity.Name);
            DateTime lastdate = DateTime.MinValue;
            string lastcont = null;
            foreach (var p in mycustomer.Contacts)
            {
                if (p.ContactDateLatestOrder > lastdate)
                {
                    lastdate = p.ContactDateLatestOrder;
                    lastcont = p.ContactAddress;
                }
            }
            if (lastcont == null)
            {
                lastcont = "Шотмана 13, Росквартал";
            }
            return Json(data: new { Data = lastcont }, behavior: JsonRequestBehavior.AllowGet);
        }

        public ActionResult Rules()
        {
            ViewBag.Message = "Правовая информация";

            return View();
        }


        public ActionResult Offer()
        {
            ViewBag.Message = "Публичная оферта о продаже товаров дистанционным способом (действует с 25 ноября 2017 года)";

            return View();
        }

        public ActionResult UserAgreement()
        {
            ViewBag.Message = "Пользовательское соглашение (действует с 25 ноября 2017 года)";

            return View();
        }

        public ActionResult Feedback()
        {
            ViewBag.Message = "Напишите нам";

            return View();
        }

        [HttpPost]
        public ActionResult SendMail(FeedbackMail Feedback)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    new FeedbackController().SendEmail(Feedback).Deliver();
                    Feedback.MailDateCreate = DateTime.Now;
                    db.FeedBacks.Add(Feedback);
                    db.SaveChanges();

                    return RedirectToAction("SuccessSend");
                }
                catch (Exception)
                {
                    return RedirectToAction("ErrorSend");
                }

                /*  new FeedbackController().SendEmail(Feedback).Deliver();
                  Feedback.MailDateCreate = DateTime.Now;
                  db.FeedBacks.Add(Feedback);
                  db.SaveChanges();*/
            }
            //return RedirectToRoute(new { controller = "Home", action = "Index" });
            return View();
        }

        public ActionResult SuccessSend()
        {
            return View();
        }

        public ActionResult ErrorSend()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddtoCart(int productId, int amount)
        {
            Cart cart = new Cart();
            if ((Cart)Session["cart"] != null)
            {
                cart = (Cart)Session["cart"];
            }
            int counter = cart.AddDishToCart(productId, amount);
            Session["cart"] = cart;
            return Json(data: new { Data = counter }, behavior: JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult CartChangeQuantity(int productId, int amount)
        {
            Cart cart = new Cart();
            if ((Cart)Session["cart"] != null)
            {
                cart = (Cart)Session["cart"];
            }
            int counter = cart.EditCartList(productId, amount);
            Session["cart"] = cart;
            return Json(data: new { Data = counter }, behavior: JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public RedirectToRouteResult NewTaskFromCart(string TaskDeliveryCustomerAddress, string TaskDeliveryCustomerPhone, string TaskDeliveryCustomerName, string TaskPaymentMethod, string CustomerComment)
        {
            //TaskDeliveryDateTime
            //IEnumerable<Tasks> Tasks = db.TasksDbSet;
            Task task = new Task();
            Cart cart = new Cart();
            Contact contact = new Contact();
            Customer customer = new Customer();
            int i = 0; // индикатор использования пользователем контакта
            if ((Cart)Session["cart"] != null)
            {
                cart = (Cart)Session["cart"];
            }
            var hiscontacts = db.Customers.Where(e => e.CustomerEmail == User.Identity.Name).Include(e => e.Contacts);
            Customer mycustomer = hiscontacts.First(e => e.CustomerEmail == User.Identity.Name);
            Contact mycontact = db.Contacts.FirstOrDefault(e => e.ContactAddress == TaskDeliveryCustomerAddress);
            if (mycontact == null) //если такого контакта не существует то добавляем со связью с текущим пользователем
            {
                //    i = 2;
                mycontact = new Contact { ContactAddress = TaskDeliveryCustomerAddress, ContactDateLatestOrder = DateTime.Now, Customers = new List<Customer>() { mycustomer } };
                db.Contacts.Add(mycontact);
                db.SaveChanges();
            }
            else if (!(mycustomer.Contacts.Contains(mycontact)))
            {
                mycontact.Customers = new List<Customer> { mycustomer };
                mycontact.ContactDateLatestOrder = DateTime.Now;
                db.Entry(mycontact).State = EntityState.Modified;
                db.SaveChanges();
            }
            else
            {
                mycontact.ContactDateLatestOrder = DateTime.Now;
                db.Entry(mycontact).State = EntityState.Modified;
                db.SaveChanges();
            }
            //if (mycustomer == null) // это блок для незалогиненых пользователей, вдруг решим вернуть
            //{
            //    if (i == 2) //нет контакта есть клиент
            //    {
            //        i = 3; //нет ни того ни другого
            //    }
            //    else
            //    {
            //        i = 1; //есть контакт нет клиента
            //    }
            //    mycustomer = new Customer { CustomerFirstName = TaskDeliveryCustomerName, CustomerPhone = TaskDeliveryCustomerPhone, CustomerBirthDate = DateTime.Now, Contacts = new List<Contact>() { mycontact } };
            //    db.Customers.Add(mycustomer); //Пока всегда добавляем нового касмомера с неполными данными (как хотели), потом когда будет готова авторизация надо пересмотреть чтобы брал текущего
            //}

            task.Contact = mycontact;
            task.ContactId = mycontact.ContactId;
            double sum = 0;
            task.TaskStatus = Status.processed.ToString();
            task.TaskDate = DateTime.Now;
            task.TaskPayMethod = Convert.ToInt32(TaskPaymentMethod);
            // task.taskStatusChangeHistory.Add(userId, DateTime.Now, 0); Надо разобраться как мы будем хранить историю
            foreach (var m in cart.cartlist)
            {
                var mydish = db.Products.FirstOrDefault(k => k.ProductId == m.Key);
                sum = mydish.ProductPrice * m.Value + sum;
            }
            task.TaskTotalSum = sum;
            task.TaskCustomerComment = CustomerComment;
            db.Tasks.Add(task);
            db.SaveChanges();
            IEnumerable<Task> tasks = db.Tasks;
            var tid = tasks.Last().TaskId;
            foreach (KeyValuePair<int, int> keyValue in cart.cartlist)
            {
                TaskRow productList = new TaskRow();
                productList.TaskId = tid;
                productList.ProductId = keyValue.Key;
                productList.Quantity = keyValue.Value;
                db.TaskRows.Add(productList);
            }
            db.SaveChanges();
            Session["cart"] = null;
            Session["orderSuccess"] = true; //надо сделать проверку добавления заказа
            return RedirectToRoute(new { controller = "Home", action = "Index" });
        }
    }
}