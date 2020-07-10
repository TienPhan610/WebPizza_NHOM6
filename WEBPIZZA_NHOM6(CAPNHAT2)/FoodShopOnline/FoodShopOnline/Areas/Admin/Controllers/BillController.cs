using Model.EnityFramework;
using Model.Func;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace FoodShopOnline.Areas.Admin.Controllers
{
    public class BillController : Controller
    {
        private FoodShopOnlineDBContext db = new FoodShopOnlineDBContext();
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

        [HttpGet]
        public ActionResult XacNhanHoaDon(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            order.Status = false;
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("Index", "Bill");
        }
        [HttpGet]
        public ActionResult XacNhanGiaoHang(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order hoaDon = db.Orders.Find(id);
            if (hoaDon == null)
            {
                return HttpNotFound();
            }
            hoaDon.Status = true;
            if (ModelState.IsValid)
            {
                db.Entry(hoaDon).State = EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("Index", "Bill");
        }
        //public ActionResult Delete(int? id)
        //{
            
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Order order = db.Orders.Find(id);
        //    if (order == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(order);
        //}

        //// POST: Admin/HoaDons/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Order order = db.Orders.Find(id);
        //    db.OrderDetails.RemoveRange(order.OrderDetails);
        //    db.SaveChanges();
        //    db.Orders.Remove(order);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}
    }
}