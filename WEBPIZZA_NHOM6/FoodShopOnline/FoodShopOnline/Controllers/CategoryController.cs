using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FoodShopOnline.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        public ActionResult Index()
        {
            return View();
        }
        //Category món ăn kèm 1
        [HttpGet]
        public ActionResult MonAnKem1_Category()
        {
            return View();
        }
        //Category món ăn kèm 2
        [HttpGet]
        public ActionResult MonAnKem2_Category()
        {
            return View();
        }
        //Category Mỳ  ý & Cơm
        [HttpGet]
        public ActionResult MyY_Com_Category()
        {
            return View();
        }
        //Category Giải khát
        [HttpGet]
        public ActionResult GiaiKhat()
        {
            return View();
        }
    }
}