using Model.Func;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FoodShopOnline.Areas.Admin.Controllers
{
    public class BillController : Controller
    {
        // GET: Admin/Bill
        [HttpGet]
        public ActionResult Index()
        {
            var BillOrder = new OrderFunc();
            var model = BillOrder.ListOrder();
            return View(model);
        }
        [HttpGet]
        public ActionResult OrderDetail()
        {
            var Detail = new OrderDetailFunc();
            var model = Detail.ListOrderDetail();
            return View(model);
        }
    }
}