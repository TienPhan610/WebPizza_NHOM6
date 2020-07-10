using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FoodShopOnline.Controllers
{
    public class MenuController : Controller
    {
        // GET: Menu
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult KhuyenMai()
        {
            return View();
        }
        [HttpGet]
        public ActionResult CuaHang()
        {
            return View();
        }
    }
}