using Model.EnityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FoodShopOnline.Controllers
{
    public class CategoryController : Controller
    {
        private FoodShopOnlineDBContext db = new FoodShopOnlineDBContext();
        // GET: Category
        public ActionResult Index()
        {
            return View();
        }
        //Pizza
        public ActionResult Pizza(string sort)
        {
            List<Product> list = new List<Product>();
            if (sort == "1" || sort == null)
            {
                list = db.Products.Where(x => x.CategoryID == 1).OrderBy(s=>s.Price).ToList();
            }
            else if (sort == "-1")
            {
                list = db.Products.Where(x => x.CategoryID == 1).OrderByDescending(s => s.Price).ToList();
            }
            
            return View(list);
        }
        //Category món ăn kèm
        [HttpGet]
        public ActionResult MonAnKem1_Category(string sort)
        {
            List<Product> list = new List<Product>();
            if (sort == "1" || sort == null)
            {
                list = db.Products.Where(x => x.CategoryID == 2).OrderBy(s => s.Price).ToList();
            }
            else if (sort == "-1")
            {
                list = db.Products.Where(x => x.CategoryID == 2).OrderByDescending(s => s.Price).ToList();
            }

            return View(list);
        }
        public ActionResult ChiTiet(string id)
        {
            var sp = db.Products.Where(x => x.ID == int.Parse(id));
            return View(sp);
        }
        //Category món ăn kèm 2
        [HttpGet]
        public ActionResult MonAnKem2_Category() 
        {
            return View();
        }
        //Category Mỳ  ý & Cơm
        [HttpGet]
        public ActionResult MyY_Com_Category(string sort)
        {
            List<Product> list = new List<Product>();
            if (sort == "1" || sort == null)
            {
                list = db.Products.Where(x => x.CategoryID == 3).OrderBy(s => s.Price).ToList();
            }
            else if (sort == "-1")
            {
                list = db.Products.Where(x => x.CategoryID == 3).OrderByDescending(s => s.Price).ToList();
            }

            return View(list);
        }
        //Category Giải khát
        [HttpGet]
        public ActionResult GiaiKhat(string sort)
        {
            List<Product> list = new List<Product>();
            if (sort == "1" || sort == null)
            {
                list = db.Products.Where(x => x.CategoryID == 4).OrderBy(s => s.Price).ToList();
            }
            else if (sort == "-1")
            {
                list = db.Products.Where(x => x.CategoryID == 4).OrderByDescending(s => s.Price).ToList();
            }

            return View(list);
        }
        //Category Khuyến mãi
        [HttpGet]
        public ActionResult KhuyenMai(string sort)
        {
            List<Product> list = new List<Product>();
            if (sort == "1" || sort == null)
            {
                list = db.Products.Where(x => x.CategoryID == 5).OrderBy(s => s.Price).ToList();
            }
            else if (sort == "-1")
            {
                list = db.Products.Where(x => x.CategoryID == 5).OrderByDescending(s => s.Price).ToList();
            }

            return View(list);
        }
    }
}