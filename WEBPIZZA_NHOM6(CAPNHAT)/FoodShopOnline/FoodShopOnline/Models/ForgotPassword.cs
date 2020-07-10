using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Net;
using System.Net.Configuration;
using System.Net.Mail;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace FoodShopOnline.Models
{
    public class ForgotPassword
    {
        [Key]
        [Display(Name = "Email khách hàng")]
        [Required(ErrorMessage = "Mời nhập Email")]
        public string Email { set; get; }

        //Tạo thuộc tính để lấy giá trị email trong web.config
        public static String FormAddress
        {
            get
            {
                SmtpSection cfg = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
                return cfg.Network.UserName;
            }
        }

        //Hàm send mail
        public string Send(string subject, string body, string to, bool isHtml, bool isSSL)
        {
            try
            {
                using (MailMessage mail = new MailMessage())
                {
                    SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                    mail.From = new MailAddress("17110377@student.hcmute.edu.vn", "Quản lý");
                    mail.To.Add(to);
                    mail.Subject = subject;
                    mail.Body = body;
                    mail.IsBodyHtml = isHtml;
                    SmtpServer.Port = 587;
                    SmtpServer.Credentials = new System.Net.NetworkCredential("17110377@student.hcmute.edu.vn", "Gtien610");
                    SmtpServer.EnableSsl = true;
                    SmtpServer.Send(mail);
                    
                }
            }
            catch
            {
                return "Gửi mail thất bại!";
            }
            return "Gửi mail thành công!";
        }
        //Lấy lại mật khẩu
        public string BodyMail_LayLaiMatKhau(string Email, string Pass)
        {
            StringBuilder mailbody = new StringBuilder();
            mailbody.Append("<html><head><title>Lấy lại mật khẩu</title></head>");
            mailbody.Append("<body>");
            mailbody.Append("<p>Chào bạn</p>");
            mailbody.Append("<p>Thông tin tài khoản của bạn: </p>");
            mailbody.Append("<p><b>Email: </b>" + Email);
            mailbody.Append("</p><p><b>Mật khẩu: </b>" + Pass);
            mailbody.Append("<p> Chân thành cảm ơn!");
            return mailbody.ToString();
        }
    }
    

}