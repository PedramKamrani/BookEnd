using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using BookEnd.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using BookEnd.Clasess;
using BookEnd.Models;
using BookEnd.Models.ViewModels;

namespace BookEnd.Areas.Identity.Pages.Account
{
    //[Authorize(Roles ="مدیریت")]
    //[Area("Identity")]
    public class RegisterModel : PageModel
    {
        private readonly UserManager<BookUser> _userManager;
        private readonly IApplicationRoleManager _roleManager;
        private readonly ILogger<RegisterModel> _logger;
        
        public RegisterModel(
            UserManager<BookUser> userManager,IApplicationRoleManager roleManager,
          ILogger<RegisterModel> logger
          
            )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; }
        [BindProperty]
        public IEnumerable<AplicationRole> GetRoles { get; set; }

        [BindProperty]
        public string[] UserRoles { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "وارد نمودن {0} الزامی است.")]
            [EmailAddress(ErrorMessage = "ایمیل شما نامعتبر است.")]
            [Display(Name = "ایمیل")]
            public string Email { get; set; }

            [Required(ErrorMessage = "وارد نمودن {0} الزامی است.")]
            [StringLength(100, ErrorMessage = "{0} باید دارای حداقل {2} کاراکتر و حداکثر دارای {1} کاراکتر باشد.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "کلمه عبور")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "تکرار کلمه عبور")]
            [Compare("Password", ErrorMessage = "کلمه عبور وارد شده با تکرار کلمه عبور مطابقت ندارد.")]
            public string ConfirmPassword { get; set; }

            [Display(Name = "نام")]
            [Required(ErrorMessage = "وارد نمودن {0} الزامی است.")]
            public string Name { get; set; }

            [Display(Name = "نام خانوادگی")]
            [Required(ErrorMessage = "وارد نمودن {0} الزامی است.")]
            public string Family { get; set; }

            [Display(Name = "نام کاربری")]
            [Required(ErrorMessage = "وارد نمودن {0} الزامی است.")]
            public string UserName { get; set; }

            //[Display(Name = "شماره موبایل")]
            //[Required(ErrorMessage = "وارد نمودن {0} الزامی است.")]
            //public string PhoneNumber { get; set; }

        }

        public void OnGet(string returnUrl = null)
        {
            GetRoles = _roleManager.GetAllRoles();
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/Admin/Rolse/Index");
            if (ModelState.IsValid)
            {
                //ConvertDateTime convert = new ConvertDateTime();
               
                var user = new BookUser { UserName = Input.UserName, Email = Input.Email, FirstName = Input.Name, LastName = Input.Family, NormalizedEmail = Input.ConfirmPassword,IsActive=true,EmailConfirmed=true };
                var result = await _userManager.CreateAsync(user,Input.Password);
                
                if (result.Succeeded)
                {

                   _logger.LogInformation("User created a new account with password.");

                    if (UserRoles != null)
                    {
                        await _userManager.AddToRolesAsync(user, UserRoles);
                    }
                   
                    return LocalRedirect(returnUrl);
                }

                foreach (var error in result.Errors)
                {
                     ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return Page();
        }
    }
}
