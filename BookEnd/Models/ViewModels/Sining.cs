using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookEnd.Models.ViewModels
{
    public class Sining
    {
        [Display(Name ="کاربری")]
        public string UserName { get; set; }
        [Display(Name ="رمز عبور")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "امنیت")]
        [StringLength(4,ErrorMessage ="طول کد زیاده")]
        [Required(ErrorMessage ="پرشود")]
        public string CaptchaCode { get; set; }
    }
}

