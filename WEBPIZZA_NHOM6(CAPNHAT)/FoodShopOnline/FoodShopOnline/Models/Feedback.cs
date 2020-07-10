using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FoodShopOnline.Models
{
    public class Feedback
    {
        [Key]
        [Display(Name = "Ý kiến khách hàng")]
        [Required(ErrorMessage = "Mời nhập ý kiếm. Không thể để trống ô này")]
        public string fb { set; get; }
    }
}