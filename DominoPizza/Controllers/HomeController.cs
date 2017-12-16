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
            List<string> ingridients  = new List<string>()
            {
                "томатный соус",
                "моцарелла",
                "блючиз",
                "чеддар",
                "пармезан",
                "пепперони",
                "шампиньоны",
                "маслины",
                "орегано",
                "цыпленок",
                "ветчина",
                "ранч",
                "томаты",
                "чеснок",
                "охотничьи колбаски",
                "бекон",
                "молоко сгущенное",
                "брусника",
                "ананасы",
                "сладкий перец",
                 "лук красный",
                 "острый перец халапеньо",
                "соус барбекю",
                 "креветки",
                "кубики брынзы",
                "говядина",
                "сырный соус",
                "огурцы консервированные",
                "чоризо"
            };
            ViewBag.ingridients = ingridients;
            return View(products);
        }
        [HttpPost]
        public ActionResult FilterProducts(object[] idYes, object[] idNo)
        {
            List<string> ingridients = new List<string>()
            {
                "томатный соус",
                "моцарелла",
                "блючиз",
                "чеддар",
                "пармезан",
                "пепперони",
                "шампиньоны",
                "маслины",
                "орегано",
                "цыпленок",
                "ветчина",
                "ранч",
                "томаты",
                "чеснок",
                "охотничьи колбаски",
                "бекон",
                "молоко сгущенное",
                "брусника",
                "ананасы",
                "сладкий перец",
                 "лук красный",
                 "острый перец халапеньо",
                "соус барбекю",
                 "креветки",
                "кубики брынзы",
                "говядина",
                "сырный соус",
                "огурцы консервированные",
                "чоризо"
            };
            List<string> ingY = new List<string>();
            List<string> ingN = new List<string>();
            List<Product> products = db.Products.ToList();
            int indexx = 0;
            if (idYes != null) { 
                foreach (var id in idYes)
                {
                    indexx = Int32.Parse((string)id);
                    //var temp = ingridients[indexx];
                    var temp = ingridients.ElementAtOrDefault(indexx);
                    ingY.Add(temp);
                }
                foreach (var ing in ingY)
                {
                    products = products.Where(z => z.ProductDescription.ToLower().Contains(ing)).Select(z => z).ToList();
                }
            }
            if (idNo != null)
            {
                foreach (var id in idNo)
                {
                    indexx = Int32.Parse((string)id);
                    //var temp = ingridients[indexx];
                    var temp = ingridients.ElementAtOrDefault(indexx);
                    ingN.Add(temp);
                }

                foreach (var ingn in ingN)
                {
                    products = products.Where(z => !z.ProductDescription.ToLower().Contains(ingn)).ToList();
                }
            }
            return PartialView("_FilterProductsPartial", products);
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

        public ActionResult Cook()
        {
            ViewBag.Message = "Повар";

            return View();
        }

        public ActionResult Courier()
        {
            ViewBag.Message = "Курьер";

            return View();
        }

        public ActionResult Manager()
        {
            ViewBag.Message = "Менеджер";

            return View();
        }

        public ActionResult Resume()
        {
            ViewBag.Message = "Резюме";

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
            return Json(data: new { Data = lastcont}, behavior: JsonRequestBehavior.AllowGet);
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
            string viewmailname = "Email";

            Feedback.To = "dominospizzaptz@yandex.ru";
            if (ModelState.IsValid)
            {
                try
                {
                    new FeedbackController().SendEmail(Feedback, viewmailname).Deliver();
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
        
        public JsonResult GetBirthDay()
        {
            Customer customer = db.Customers.First(e => e.CustomerEmail == User.Identity.Name);
            DateTime birthday = customer.CustomerBirthDate;
            DateTime date = DateTime.Now;
            Boolean bd = false;
            //if (DateTime.Now.Date == birthday)
                if (birthday.Day == date.Day && birthday.Month == date.Month)
                {
                    bd = true;
                }
            return Json(data: new { Data = bd }, behavior: JsonRequestBehavior.AllowGet);
        }

        public JsonResult IsItPie()
        {
            Cart cart = new Cart();
            if ((Cart)Session["cart"] != null)
            {
                cart = (Cart)Session["cart"];
            }
            Boolean pie = false;
            foreach (var c in cart.cartlist)
            {
                if (c.Key == 7 && c.Value == 1)
                {
                    pie = true;
                }
            }
            return Json(data: new { Data = pie }, behavior: JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public RedirectToRouteResult NewTaskFromCart(string TaskDeliveryCustomerAddress, string TaskDeliveryCustomerPhone, string TaskDeliveryCustomerName, string TaskPaymentMethod, string CustomerComment)
        {

            //TaskDeliveryDateTime
            //IEnumerable<Tasks> Tasks = db.TasksDbSet;
            Task task = new Task();
            Cart cart = new Cart();
            //Contact contact = new Contact();
            //Customer customer = new Customer();
            //int i = 0; // индикатор использования пользователем контакта
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
            ////if (mycustomer == null) // это блок для незалогиненых пользователей, вдруг решим вернуть
            ////{
            ////    if (i == 2) //нет контакта есть клиент
            ////    {
            ////        i = 3; //нет ни того ни другого
            ////    }
            ////    else
            ////    {
            ////        i = 1; //есть контакт нет клиента
            ////    }
            ////    mycustomer = new Customer { CustomerFirstName = TaskDeliveryCustomerName, CustomerPhone = TaskDeliveryCustomerPhone, CustomerBirthDate = DateTime.Now, Contacts = new List<Contact>() { mycontact } };
            ////    db.Customers.Add(mycustomer); //Пока всегда добавляем нового касмомера с неполными данными (как хотели), потом когда будет готова авторизация надо пересмотреть чтобы брал текущего
            ////}

            task.Contact = mycontact;
            task.ContactId = mycontact.ContactId;
            double sum = 0;
            task.TaskStatus = Status.processed.ToString();
            db.StatusHistories.Add(new StatusHistory { StatusChangeTime = DateTime.Now, StatusChangedTo = Status.processed.ToString(), ForTask=task, DominosUser=mycustomer });
            task.TaskDate = DateTime.Now;
            task.TaskPayMethod = Convert.ToInt32(TaskPaymentMethod);
            // task.taskStatusChangeHistory.Add(userId, DateTime.Now, 0); Надо разобраться как мы будем хранить историю
            foreach (var m in cart.cartlist)
            {
                var mydish = db.Products.FirstOrDefault(k => k.ProductId == m.Key);
                DateTime birthday = mycustomer.CustomerBirthDate;
                DateTime date = DateTime.Now;
                //int k = 0;
                if (birthday.Day == date.Day && birthday.Month == date.Month && m.Key == 7 && m.Value == 1 && cart.Counter == 1)
                {
                    sum = mydish.ProductPrice * m.Value * 0.7;
                }
                else if (birthday.Day == date.Day && birthday.Month == date.Month && m.Key != 7 && m.Value == 1 && cart.Counter == 1)
                {
                    sum = mydish.ProductPrice * m.Value * 0.85;
                }
                else if (date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday && date.Hour > 11 && date.Hour < 16)
                {
                    sum = mydish.ProductPrice * m.Value * 0.9 + sum;
                }
                else if ((date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday) && (m.Value == 2 || cart.Counter == 2))
                {
                    sum = mydish.ProductPrice * m.Value * 0.85 + sum;
                }
                else
                {
                    sum = mydish.ProductPrice * m.Value + sum;
                }
                
            }
            task.TaskTotalSum = sum;
            task.TaskCustomerComment = CustomerComment;
            task.Customers = new List<Customer>() { mycustomer };
            task.CustomerId = mycustomer.CustomerId;
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

            Session["orderSuccess"] = true; //надо сделать проверку добавления заказа

            if (Convert.ToInt32(TaskPaymentMethod) != 1)
            {
                //заказ отправлен менеджеру
                Session["cart"] = null;
                return RedirectToRoute(new { controller = "Home", action = "SuccessOrder" });
            }
            else
            {   //переход к онлайн оплате
                return RedirectToRoute(new { controller = "Home", action = "PayOnline" });
            }


        }
        public ActionResult PayOnline()
        {
            ViewBag.Message = "On-line оплата";

            Cart cart = new Cart();
            if ((Cart)Session["cart"] != null)
            {
                cart = (Cart)Session["cart"];
            }

            //Task task = new Task();
            var histask = db.Customers.Where(e => e.CustomerEmail == User.Identity.Name).Include(e => e.Tasks);
            Customer mycustomer = histask.First(e => e.CustomerEmail == User.Identity.Name);
            var lasttask = mycustomer.Tasks.Last();
            var sum = lasttask.TaskTotalSum;
            var ordernumber = lasttask.TaskId;

            /*IEnumerable<Product> products = db.Products;
           
            Dictionary<int, string> productNames = new Dictionary<int, string>();
            foreach (var temp in products)
            {
                productNames.Add(temp.ProductId, temp.ProductName);
            }*/
            // ViewBag.prod = productNames;
            //ViewBag.cartindicator = cart.Counter;

            //IEnumerable<Task> tasks = db.Tasks;

            ViewBag.LastTask = ordernumber;
            ViewBag.TotalS = sum;

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
            Session["cart"] = null;
            return View(table);
        }

        public ActionResult SuccessOrder()
        {
            IEnumerable<Task> tasks = db.Tasks;
            ViewBag.LastTask = tasks.Last().TaskId;
            return View();
        }

        [HttpPost]
        public ActionResult PayMethod(int paymentType)
        {
            if (Convert.ToInt32(paymentType) == 1)
            {


                //прошла ли оплата = 4
                // Session["cart"] = null;

                return RedirectToRoute(new { controller = "Home", action = "PayCard" });
            }
            else
            {
                //другие платежные системы
                return RedirectToRoute(new { controller = "Home", action = "PayTypes" });
            }
        }

        public ActionResult PayCard()
        {
            ViewBag.Message = "On-line оплата банковской картой";

            return View();
        }
        public ActionResult SendReceiptMail(string ReplyreceiptMail, string cardholder, FeedbackMail Feedback)
        {
            IEnumerable<Task> tasks = db.Tasks;
            ViewBag.LastTask = tasks.Last().TaskId;
            /*   ViewBag.TotalS = tasks.Last().TaskTotalSum;*/

            IEnumerable<TaskRow> LastTaskRows = db.TaskRows;
            IEnumerable<Product> Prod = db.Products;

            Dictionary<int, string> productNames = new Dictionary<int, string>();
            foreach (var temp in Prod)
            {
                productNames.Add(temp.ProductId, temp.ProductName);
            }

            Dictionary<string, int> listpr = new Dictionary<string, int>();

            foreach (var LastTaskRow in LastTaskRows)
            {
                if (LastTaskRow.TaskId == ViewBag.LastTask)
                {
                    foreach (KeyValuePair<int, string> keyValue in productNames)
                    {
                        if (keyValue.Key == LastTaskRow.ProductId)
                        {
                            listpr.Add(keyValue.Value, LastTaskRow.Quantity);
                        }
                    }

                }
            }
            string viewmailname = "Receipt";

            FeedbackMail receiptmail = new FeedbackMail();

            receiptmail.To = ReplyreceiptMail; //сделать отправку человеку
            receiptmail.FeedbackName = cardholder;
            receiptmail.Subject = "чек";
            string mailtext = "№ заказа: " + tasks.Last().TaskId + " на сумму: " + tasks.Last().TaskTotalSum + "р. Ваш заказ: ";

            string Totaltext = " ";
            foreach (KeyValuePair<string, int> keyValue in listpr)
            {
                string namepz = keyValue.Key;
                int qpz = keyValue.Value;

                Totaltext = Totaltext + " " + namepz + " - " + qpz + " шт., ";
            }
            receiptmail.Body = mailtext + Totaltext;

            receiptmail.ReplyEmail = ReplyreceiptMail;

            //try
            //{
            new FeedbackController().SendEmail(receiptmail, viewmailname).Deliver();
            receiptmail.MailDateCreate = DateTime.Now;
            db.FeedBacks.Add(receiptmail);
            tasks.Last().TaskPayMethod = 4;
            db.SaveChanges();


            return RedirectToAction("SuccessOrderSend");
        
        /*   catch (Exception)
           {
               return RedirectToAction("ErrorSend");
           }*/

    
        }
        public ActionResult SuccessOrderSend()
        {

            return View();
        }
        public ActionResult PayTypes()
        {

            return View();
        }
        
    }
}