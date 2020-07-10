using Model.EnityFramework;
using Model.Func;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FoodShopOnline.Areas.Admin.Controllers
{
    public class ProductController : BaseController
    {
        // GET: Admin/Product
        public ActionResult Index()
        {
            var ProModel = new ProductFunc();
            var model = ProModel.ListAllPro();
            return View(model);
        }
        //hàm hiện thị trang sản phẩm sao khi thêm mới
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        //hàm thêm mới sản phẩm
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product collection)
        {
            try
            {
                var model = new ProductFunc();
                int k = model.Create(collection.Name, collection.Alias, collection.CategoryID, collection.Image, collection.Price,
                    collection.Promotion, collection.Description,collection.CreatedDate,collection.CreatedBy ,collection.Status);
                if (k != 0)
                {
                    SetAlert("Thêm thông tin sản phẩm thành công", "success");
                }
                else
                {
                    return RedirectToAction("Index");
                }
                
                return View(collection);
            }
            catch
            {
                ModelState.AddModelError("", "Xin điền đủ thông tin sản phẩm");
                return View();
            }
        }
        //hàm sửa thông tin sản phẩm
        [HttpPost]
        [ValidateAntiForgeryToken] //Khi form Admin được Post hiển thị lên
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                var Func = new ProductFunc();
                var result = Func.Update(product);
                if (result)
                {
                    SetAlert("Cập nhật thông tin sản phẩm thành công", "success");
                    return RedirectToAction("Index", "Product");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật thông tin sản phẩm thất bại");
                }
            }
            return View("Index");
        }
        public ActionResult Edit(int id)
        {
            var product = new ProductFunc().ViewDetailProduct(id);
            return View(product);
        }
        //
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new ProductFunc().DeleteProduct(id);
            return RedirectToAction("Index");
        }
    }
}