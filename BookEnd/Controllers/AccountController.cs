using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BookEnd.Areas.Identity.Data;
using BookEnd.Clasess;
using BookEnd.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;

namespace BookEnd.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<BookUser> _userManager;
        private readonly SignInManager<BookUser> _signInManager;
        public AccountController(UserManager<BookUser> userManager, SignInManager<BookUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        #region Rigistet
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel viewModel)
        {
            if (viewModel!=null)
            {
                var User = new BookUser
                {
                    FirstName = viewModel.Name,
                    LastName = viewModel.Family,
                    Email = viewModel.Email,
                    UserName = viewModel.UserName,
                    BirthDate = viewModel.BirthDay
                };
                var result = await _userManager.CreateAsync(User, viewModel.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(User, "مشتری");
                    return RedirectToAction("Index");
                }
            }
            return View();
        }
        #endregion
        #region Sining
        [HttpGet]
        public IActionResult Sining()
        {
            return View();
        }
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Sining(Sining viewmodel)
        {
            if (Captcha.ValidateCaptchaCode(viewmodel.CaptchaCode,HttpContext))
            {
                var result = await _signInManager.PasswordSignInAsync(viewmodel.UserName, viewmodel.Password,false,false);
                if (result.Succeeded)
                {
                    return Redirect("/Admin/Store");
                }
                ModelState.AddModelError(string.Empty, "اطلاعات صحیح نمیباشد");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "کد امنیتی درست نیست");
            }

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> sinOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        [Route("get-captcha-image")]
        public IActionResult GetCaptchaImage()
        {
            int width = 100;
            int height = 36;
            var captchaCode = Captcha.GenerateCaptchaCode();
            var result = Captcha.GenerateCaptchaImage(width, height, captchaCode);
            HttpContext.Session.SetString("CaptchaCode", result.CaptchaCode);
            Stream s = new MemoryStream(result.CaptchaByteData);
            return new FileStreamResult(s, "image/png");
        }
        #endregion
    }
}