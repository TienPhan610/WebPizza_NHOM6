using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Model.EnityFramework;
using System.Web.Mvc;

namespace FoodShopOnline.Controllers
{
    public class ProductDetailsController : Controller
    {
        private FoodShopOnlineDBContext db = new FoodShopOnlineDBContext();
        // GET: ProductDetails
        public ActionResult Index()
        {
            return View();
        }
        // doi t
        public ActionResult MonAn(int id)
        {
            var monan = db.Products.Where(s => s.ID == id).FirstOrDefault();
            ViewBag.monan = monan;
            return View();
        }
     
    }
}