using FoodShopOnline.Common;
using FoodShopOnline.Models;
using Model.EnityFramework;
using Model.Func;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Runtime.Remoting.Contexts;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace FoodShopOnline.Controllers
{
    public class UserController : Controller
    {
        FoodShopOnlineDBContext db = new FoodShopOnlineDBContext();
        //public object IdentityManager { get; private set; }

        // GET: User
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if(ModelState.IsValid)
            {
                var func = new UserFunc();
                if(func.CheckUserName(model.UserName) > 0)
                {
                    ModelState.AddModelError("", "Tên đăng nhập đã tồn tại");
                }
                else if(func.CheckEmail(model.Email) > 0)
                {
                    ModelState.AddModelError("", "Email đã tồn tại");
                }
                else
                {
                    var user = new User();
                    user.UserName = model.UserName;
                    user.Password = model.Password;
                    user.GroupID = "MEMBER";
                    user.Phone = "01234567890";
                    user.CreatedBy = "CUSTOMER";
                    user.ModifiedBy = "CUSTOMER";
                    user.ModifiedDate = DateTime.Now;
                    user.Name = model.Name;
                    user.Address = model.Address;
                    user.Email = model.Email;
                    user.CreatedDate = DateTime.Now;
                    user.Status = true;

                    if(ModelState.IsValid)
                    {
                        Session["User"] = user;
                        string confirmKey = new Random().Next(100000, 1000000).ToString();
                        Session[user.Email] = confirmKey;
                        SendEmail(user.Email, confirmKey);
                        return RedirectToAction("ConfirmAccount", "User");
                    }
                    
                    //int result = func.insert(user.UserName, user.Password, user.GroupID, user.Name, user.Address,
                    //    user.Email, user.Phone, user.Status);
                    //if (result > 0)
                    //{
                    //    ViewBag.Success = "Đăng kí thành công";
                    //    model = new RegisterModel();
                    //}
                    //else
                    //{
                    //    ModelState.AddModelError("", "Đăng kí không thành công");
                    //}
                }
            }
            return View(model);
            //return RedirectToAction("ConfirmAccount", "User");
        }

        public ActionResult ConfirmAccount()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ConfirmAccount(string confirmKey)
        {
            User user = (User)Session["User"];
            if (confirmKey == (string)Session[user.Email])
            {
                Session[user.Email] = null;
                Session["User"] = null;
                //db.Users.Add(user);
                //db.SaveChanges();
                new UserFunc().insert(user.UserName, user.Password, user.GroupID, user.Name, user.Address, user.Email, user.Phone, user.Status);
                // ViewBag.Success = "Đăng kí thành công";
                TempData["DangKyMess"] = "alert('Đăng kí thành công')";
                return RedirectToAction("Login", "User");
            }
            TempData["DangKyMess"] = "alert('Sai mã xác thực')";
            return RedirectToAction("ConfirmAccount", "User");
        }

        public void SendEmail(string address, string confirmKey)
        {
            string email = "17110377@student.hcmute.edu.vn";
            string password = "Gtien610";

            var loginInfo = new NetworkCredential(email, password);
            var msg = new MailMessage();
            var smtpClient = new SmtpClient("smtp.gmail.com", 587);

            msg.From = new MailAddress(email);
            msg.To.Add(new MailAddress(address));
            msg.Subject = "Mã xác thực Royal Pizza";
            msg.Body = confirmKey;
            msg.IsBodyHtml = true;

            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = loginInfo;
            smtpClient.Send(msg);
        }

        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Logout()
        {
            Session[CommonConstants.User_Session] = null;
            return Redirect("/");
        }
        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var Func = new UserFunc();
                var result = Func.Login(model.UserName, model.Password);
                if (result == 1)
                {
                    var user = Func.GetUserByUserName(model.UserName);
                    var usersession = new UserLogin();

                    usersession.UserName = user.UserName;
                    usersession.UserID = user.ID;

                    Session.Add(CommonConstants.User_Session, usersession); 
                    return Redirect("/");
                }
                else if (result == 0)
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
                else
                {
                    ModelState.AddModelError("", "Đăng nhập không thành công");
                }
            }
            return View(model);
        }



        [HttpGet]
        public ActionResult ForgotPassword ()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgotPassword(ForgotPassword model)
        {
            if (db.Users.Any(x => x.Email == model.Email))
            {
                User t = db.Users.Where(x => x.Email == model.Email).FirstOrDefault();
                ForgotPassword mail = new ForgotPassword();
                string bodymail = mail.BodyMail_LayLaiMatKhau(model.Email, t.Password);
                string ThongBao = mail.Send("Lấy lại mật khẩu", bodymail, model.Email, true, true);
                ViewBag.ThongBao = ThongBao;
            }
            return View();
        }

        [HttpGet]
        public ActionResult Feedback()
        {
            return View();     
        }
        [HttpPost]
        public ActionResult FeedBack(Feedback model)
        {
            var session = (UserLogin)Session[CommonConstants.User_Session];
            if (session == null)
            {
                return RedirectToAction("Login");
            }
            
            return View();
        }
        
    }
}