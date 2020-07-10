using Model.Func;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FoodShopOnline.Models;
using Model.EnityFramework;

namespace FoodShopOnline.Controllers
{
    public class HomeController : Controller
    {
        private FoodShopOnlineDBContext db = new FoodShopOnlineDBContext();
        
        // GET: Home
        public ActionResult Index(string sort)
        {
            
            
            if (sort == "1" || sort == null)
            {
                var monan = db.Products.OrderBy(s=>s.Price);
                ViewBag.monan = monan;
            }
            else if (sort == "-1")
            {
                var monan = db.Products.OrderByDescending(s => s.Price);
                ViewBag.monan = monan;
            }
            
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
        public ActionResult Search(string search)
        {
            List<Product> list = new List<Product>();
            if (ModelState.IsValid)
            {
                var product_id = db.Products.Where(s=>s.Name.Contains(search)).ToList();
                list = product_id;  
                
            }
            return View(list);
        }

       
    }
}