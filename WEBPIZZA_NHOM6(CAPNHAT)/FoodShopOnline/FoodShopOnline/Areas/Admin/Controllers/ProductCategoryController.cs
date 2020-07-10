using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.Func;

namespace FoodShopOnline.Areas.Admin.Controllers
{
    public class ProductCategoryController : Controller
    {
        // GET: Admin/ProductCategory
        public ActionResult Index()
        {
            var ProCate = new ProductCategoryFunc();
            var model = ProCate.ListAllProCate();
            return View(model);
        }
    }
}