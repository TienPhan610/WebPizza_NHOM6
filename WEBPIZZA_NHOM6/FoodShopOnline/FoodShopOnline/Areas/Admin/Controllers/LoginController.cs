using FoodShopOnline.Areas.Admin.Models;
using FoodShopOnline.Common;
using Model.Func;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FoodShopOnline.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Admin/Login
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var Func = new UserFunc();
                var result = Func.Login(model.UserName, model.Password, true);
                if (result == 1)
                {
                    var user = Func.GetUserByUserName(model.UserName);
                    var usersession = new UserLogin();

                    usersession.UserName = user.UserName;
                    usersession.UserID = user.ID;
                    usersession.GroupID = user.GroupID;
                    var listCredentials = Func.GetListCredential(model.UserName);

                    Session.Add(CommonConstants.Session_Credential, listCredentials);
                    Session.Add(CommonConstants.User_Session, usersession);
                    return RedirectToAction("Index", "Home");
                }
                else if(result == 0)
                {
                    ModelState.AddModelError("", "Tài khoản không tồn tại");
                }
                else if (result == -1)
                {
                    ModelState.AddModelError("", "Tài khoản đang bị khóa");
                }
                else if (result == -2)
                {
                    ModelState.AddModelError("", "Sai mật khẩu");
                }
                else if (result == -3)
                {
                    ModelState.AddModelError("", "Tài khoản của bạn không có quyền đăng nhập");
                }
                else
                {
                    ModelState.AddModelError("", "Đăng nhập không thành công");
                }
            }
            return View("Index");
        }
    }
}