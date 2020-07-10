using FoodShopOnline.Models;
using Model.Func;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Model.EnityFramework;
using FoodShopOnline.Common;

namespace FoodShopOnline.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        private const string CartSession = "CartSession";
        public ActionResult Index()
        {
            var cart = Session[CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return View(list);
        }
        public ActionResult AddItem(int productId, int quantity)
        {
            var product = new ProductFunc().ViewDetailProduct(productId);
            var cart = Session[CartSession];
            if (cart != null)
            {
                var list = (List<CartItem>)cart;
                if (list.Exists(x => x.Product.ID == productId))
                {
                    foreach (var item in list)
                    {
                        if (item.Product.ID == productId)
                        {
                            item.Quantity += quantity;
                        }
                    }
                }
                else
                {
                    //tạo mới đối tượng trong cart item
                    var item = new CartItem();
                    item.Product = product;
                    item.Quantity = quantity;
                    list.Add(item);
                }
                //Gán vào session
                Session[CartSession] = list;
            }
            else
            {
                //tạo mới đối tượng trong cart item
                var item = new CartItem();
                item.Product = product;
                item.Quantity = quantity;
                var list = new List<CartItem>();
                list.Add(item);
                //gắn vào session
                Session[CartSession] = list;
            }
            //return Json(new { Product.ID = productId }, JsonRequestBehavior.AllowGet);
            return RedirectToAction("Index");
        }
        public JsonResult DeleteAll()
        {
            Session[CartSession] = null;
            return Json(new
            {
                status = true
            });
        }
        public JsonResult Delete(int id)
        {
            var sessionCart = (List<CartItem>)Session[CartSession];
            sessionCart.RemoveAll(x => x.Product.ID == id);
            Session[CartSession] = sessionCart;
            return Json(new
            {
                status = true
            });
        }
        public JsonResult Update(string cartModel)
        {
            var jsonCart = new JavaScriptSerializer().Deserialize<List<CartItem>>(cartModel);
            var sessionCart = (List<CartItem>)Session[CartSession];

            foreach (var item in sessionCart)
            {
                var jsonItem = jsonCart.SingleOrDefault(x => x.Product.ID == item.Product.ID);
                if (jsonItem != null)
                {
                    item.Quantity = jsonItem.Quantity;
                }
            }
            Session[CartSession] = sessionCart;
            return Json(new
            {
                status = true
            });
        }
        [HttpGet]
        public ActionResult Payment()
        {
            var cart = Session[CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return View(list);
        }
        [HttpPost]
        public ActionResult Payment(string CustomerName, string CustomerAddress, string CustomerEmail, string CustomerMobie)
        {
            var session = (UserLogin)Session[CommonConstants.User_Session];
            if(session != null)
            {
                var order = new Order();
                order.CreatedDate = DateTime.Now;
                order.CustomerID = session.UserID;
                order.CustomerName = CustomerName;
                order.CustomerEmail = CustomerEmail;
                order.CustomerMobile = CustomerMobie;
                order.CustomerAddress = CustomerAddress;
                order.CreatedBy = "CUSTOMER";
                order.PaymentMethod = "Thanh Toán Bằng Tiền Mặt Khi Nhận Hàng";
                order.Status = true;
                order.PaymentStatus = "Chờ Thanh Toán";

                try
                {
                    new OrderFunc().Insert(order);
                    var cart = (List<CartItem>)Session[CartSession];
                    var detailFunc = new OrderDetailFunc();
                    foreach (var item in cart)
                    {
                        var orderDetail = new OrderDetail();
                        orderDetail.ProductID = item.Product.ID;
                        orderDetail.OrderID = order.ID;
                        orderDetail.Quantity = item.Quantity;
                        orderDetail.Price = item.Product.Price;
                        detailFunc.Insert(orderDetail);
                    }
                }
                catch (Exception ex)
                {
                    return Redirect("/Cart/OrderFail");
                }

                return Redirect("/Cart/Success");
            }
            else
            {
                return Redirect("/Cart/NotLoggedIn");
            }
        }
        public ActionResult Success()
        {
            return View();
        }
        public ActionResult OrderFail()
        {
            return View();
        }
        public ActionResult NotLoggedIn()
        {
            return View();
        }
    }
}