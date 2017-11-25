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
        DominosContext db = new DominosContext();
        public ActionResult Index()
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


        public ActionResult Vacations()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult HowToOrder()
        {
            return View();
        }

        public ActionResult Rules()
        {
            return View();
        }
    }
}