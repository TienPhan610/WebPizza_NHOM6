using FoodShopOnline.Common;
using Model.EnityFramework;
using Model.Func;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FoodShopOnline.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        // GET: Admin/User
        [HasCredential(RoleID ="VIEW_USER")]
        public ActionResult Index(string searchString, int page = 1, int pagesize = 10)
        {
            var Func = new UserFunc();
            var model = Func.ListAllPaging(searchString, page, pagesize);
            ViewBag.SearchString = searchString;
            return View(model);
        }
        [HttpGet] //Dùng để tải trang
        [HasCredential(RoleID = "ADD_USER")]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost] //Khi form Admin được Post hiển thị lên
        [HasCredential(RoleID = "ADD_USER")]
        public ActionResult Create(User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var Func = new UserFunc();
                    int id = Func.insert(user.UserName, user.Password, user.GroupID, user.Name, user.Address, user.Email, user.Phone, user.Status);
                    if (id != 0)
                    {
                        SetAlert("Thêm User thành công", "success");
                        return RedirectToAction("Index", "User");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Thêm User Không Thành công");
                    }
                }
                return View("Index");
            }
            catch
            {
                ModelState.AddModelError("", "Xin điền đủ thông tin người dùng");
                return View();
            }
        }
        //
        [HttpPost] //Khi form Admin được Post hiển thị lên
        [HasCredential(RoleID = "EDIT_USER")]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                var Func = new UserFunc();
                var result = Func.UpdateUser(user);
                if (result)
                {
                    SetAlert("Cập nhật User thành công", "success");
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật User không Thành công.Xin kiểm tra lại");
                }
            }
            return View("Edit");
        }
        [HasCredential(RoleID = "EDIT_USER")]
        public ActionResult Edit(int id)
        {
            var user = new UserFunc();
            var model = user.ViewDetailUser(id);
            return View(model);
        }
        //
        [HttpDelete]
        [HasCredential(RoleID = "DELETE_USER")]
        public ActionResult Delete(int id)
        {
            var deluser = new UserFunc().DeleteUser(id);
            if (deluser)
            {
                SetAlert("Xóa thành công", "success");
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Xóa User không Thành công. Không được xóa Admin. Tải lại trang để cập nhật lại trang User");
            }
            return View("DeleteError");
            //return Redirect("DeleteError");
        }
        //

        //
        
        [HasCredential(RoleID = "EDIT_USER")]
        [HttpPost]
        public JsonResult ChangeStatus(int id)
        {
            var result = new UserFunc().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }
        //Trang hiển thị nhóm người dùng
        [HttpGet]
        public ActionResult UserGroup()
        {
            var list = new UserFunc();
            var model = list.ListUserGroup();
            return View(model);
        }
    }
}