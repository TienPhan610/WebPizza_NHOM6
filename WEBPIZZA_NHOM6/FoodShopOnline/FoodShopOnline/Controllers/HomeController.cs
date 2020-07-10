using Model.Func;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FoodShopOnline.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        //
        [HttpGet] //hiển thị trang thứ 2 của mặt hàng pizza
        public ActionResult Pizza2()
        {
            return View();
        }
        //RenderAction thanh Menu trong trang chủ --- (_Layout)
        [ChildActionOnly]
        public ActionResult MainMenu()
        {
            var model = new UserFunc().ListByGroupID(1);
            return PartialView(model);
        }
        [ChildActionOnly]
        public ActionResult TopMenu()
        {
            var model = new UserFunc().ListByGroupID(2);
            return PartialView(model);
        }
    }
}