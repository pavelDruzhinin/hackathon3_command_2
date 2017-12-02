using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DominosPizza.Models;

namespace DominosPizza.Controllers
{
    public class HomeController : Controller
    {
        //DominosContext db = new DominosContext();
        /* public ActionResult Index()
         {
             Cart cart = new Cart();
             if ((Cart)Session["cart"] != null)
             {
                 cart = (Cart)Session["cart"];
             }
             IEnumerable<Products> products = db.ProductsDbSet;
             ViewBag.products = products;
             ViewBag.cartindicator = cart.Counter;
             return View(products);
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
         public ActionResult Cart() 
         {
             Cart cart = new Cart();
             if ((Cart)Session["cart"] != null)
             {
                 cart = (Cart)Session["cart"];
             }
             ViewBag.cartList = cart.cartlist;
             IEnumerable<Products> products = db.ProductsDbSet;
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
                 IQueryable<Products> product = db.ProductsDbSet
                                                     .Where(c => c.ProductId == keyValue.Key)
                                                     .Select(c => c);
                 orderTableRow.ProductName = product.FirstOrDefault().ProductName;
                 orderTableRow.ProductPrice = (int)product.FirstOrDefault().ProductPrice;
                 table.Add(orderTableRow);
             }






             return View(table);
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
         public RedirectToRouteResult NewTaskFromCart(string TaskDeliveryCustomerAddress, string TaskDeliveryCustomerPhone, string TaskDeliveryCustomerName, string TaskPaymentMethod)
         {
             //TaskDeliveryDateTime
             //IEnumerable<Tasks> Tasks = db.TasksDbSet;
             Tasks task = new Tasks();
             Cart cart = new Cart();
             if ((Cart)Session["cart"] != null)
             {
                 cart = (Cart)Session["cart"];
             }
             int userId = 0;
             task.TaskStatusId = 0;
             task.TaskDeliveryCustomerAddress = TaskDeliveryCustomerAddress;
             task.TaskDeliveryCustomerPhone = TaskDeliveryCustomerPhone;
             task.TaskDeliveryDateTime = DateTime.Now;
             task.TaskDeliveryCustomerName = TaskDeliveryCustomerName;
             task.TaskPaymentMethod = TaskPaymentMethod;
             task.TaskStatusChangeHistory.Add(userId, DateTime.Now);
             db.TasksDbSet.Add(task);
             db.SaveChanges();
             IEnumerable<Tasks> Tasks = db.TasksDbSet;
             var tid = Tasks.Last().TasksId;
             foreach (KeyValuePair<int, int> keyValue in cart.cartlist)
             {
                 TaskList productList = new TaskList();
                 productList.TaskId = tid;
                 productList.ProductId = keyValue.Key;
                 productList.ProductQuantity = keyValue.Value;
                 db.TaskListsDbSet.Add(productList);
             }
             db.SaveChanges();
             Session["cart"] = null;
             Session["orderSuccess"] = true; //надо сделать проверку добавления заказа
             return RedirectToRoute(new { controller = "Home", action = "Index" });
         }
         */
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
            int i = 0; // индикатор останется таким если у нас есть и контакт и клиент
            if ((Cart)Session["cart"] != null)
            {
                cart = (Cart)Session["cart"];
            }
            Contact mycontact = db.Contacts.FirstOrDefault(e => e.ContactAddress == TaskDeliveryCustomerAddress);
            if (mycontact == null)
            {
                i = 2;
                mycontact = new Contact { ContactAddress = TaskDeliveryCustomerAddress, ContactDateLatestOrder = DateTime.Now, Customers = new List<Customer>() { } };
            }
            Customer mycustomer = db.Customers.FirstOrDefault(e => e.CustomerPhone == TaskDeliveryCustomerPhone);
            if (mycustomer == null)
            {
                if (i == 2) //нет контакта есть клиент
                {
                    i = 3; //нет ни того ни другого
                }
                else
                {
                    i = 1; //есть контакт нет клиента
                }
                mycustomer = new Customer { CustomerFirstName = TaskDeliveryCustomerName, CustomerPhone = TaskDeliveryCustomerPhone, CustomerBirthDate = DateTime.Now, Contacts = new List<Contact>() {mycontact} };
                db.Customers.Add(mycustomer); //Пока всегда добавляем нового касмомера с неполными данными (как хотели), потом когда будет готова авторизация надо пересмотреть чтобы брал текущего
            }
            if (i > 0)
            {
                mycontact.Customers.Add(mycustomer);
            //    mycustomer.Contacts.Add(mycontact);
                db.Contacts.Add(mycontact);
            }
            db.SaveChanges();
            task.Contact = mycontact; 
            task.ContactId = mycontact.ContactId;
            //int userId = 0;
            double sum = 0;
            task.TaskStatus = Status.processed.ToString();
            task.TaskDate = DateTime.Now;
            task.TaskPayMethod = Convert.ToInt32(TaskPaymentMethod);
            // task.taskStatusChangeHistory.Add(userId, DateTime.Now, 0); Надо разобраться как мы будем хранить историю
            foreach (var m in cart.cartlist) {
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