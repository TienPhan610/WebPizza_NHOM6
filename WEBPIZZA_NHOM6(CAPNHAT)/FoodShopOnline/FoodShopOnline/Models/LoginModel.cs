using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FoodShopOnline.Models
{
    public class LoginModel
    {
        [Key]
        [Display(Name = "Tên Người Dùng")]
        [Required(ErrorMessage = "Mời nhập user name")]
        public string UserName { set; get; }

        [Display(Name = "Mật Khẩu")]
        [Required(ErrorMessage = "Mời nhập password")]
        public string Password { set; get; }
    }
}