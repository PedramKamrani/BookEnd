using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookEnd.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Display(Name = "نام")]
        [Required(ErrorMessage = "وارد نمودن {0} الزامي است.")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "{0} بايد حداقل داراي 3 کاراکتر و حداکثر داراي 20 کاراکتر باشد.")]
        public string Name { get; set; }

        [Display(Name = "نام خانوادگي")]
        [Required(ErrorMessage = "وارد نمودن {0} الزامي است.")]
        [StringLength(40, ErrorMessage = "{0} بايد حداکثر 40 کاراکتر باشد.")]
        public string Family { get; set; }

        [Display(Name = "تاریخ تولد")]
        [Required(ErrorMessage = "وارد نمودن {0} الزامي است.")]
        public DateTime BirthDay { get; set; }

        [Display(Name = "ايميل")]
        [EmailAddress(ErrorMessage = "{0} شما نامعتبر است.")]
        [Required(ErrorMessage = "وارد نمودن {0} الزامي است.")]
        public string Email { get; set; }

        [Display(Name = "نام کاربري")]
        [Required(ErrorMessage = "وارد نمودن {0} الزامي است.")]
        public string UserName { get; set; }

        [Display(Name = "پسورد")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "وارد نمودن {0} الزامي است.")]
        public string Password { get; set; }

        [Display(Name = "تکرار پسورد")]
        [DataType(DataType.Password)]

        [Compare("Password", ErrorMessage = "تکرار پسورد با پسورد وارد شده مطابقت ندارد.")]
        [Required(ErrorMessage = "وارد نمودن {0} الزامي است.")]
        public string ConfirmPassword { get; set; }


        //[Display(Name = "با قوانين سايت موافق هستيد؟")]
        //[Range(typeof(bool), "true", "true", ErrorMessage = "لطفا موافقت خود را با قوانين سايت اعلام کنيد.")]
        //public bool IAccept { get; set; }

    }

        
}
