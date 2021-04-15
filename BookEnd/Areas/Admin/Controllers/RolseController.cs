using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookEnd.Areas.Identity.Data;
using BookEnd.Models;
using BookEnd.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookEnd.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RolseController : Controller
    {
        private readonly IApplicationRoleManager _roleManager;
        private readonly BookEndContext _context;
        public RolseController(IApplicationRoleManager roleManager,BookEndContext context)
        {
            _roleManager = roleManager;
            _context = context;
        }
        [Authorize]
        public IActionResult Index()
        {
            var Role = _roleManager.GetAllRolesAndUsersCount().OrderBy(r=>r.NameR);
            return View(Role);
        }

        public async Task<IActionResult> DeleteRole(string id)
        {
            var Role = await _roleManager.FindByIdAsync(id);
            _context.Remove(Role);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult AddRole()
        {
            return View();
        }
        [HttpPost]
        [ActionName("AddRole")]
        public async Task<IActionResult> AddRolo(RoleManagerViewModel viewModel)
        {
            if (await _roleManager.RoleExistsAsync(viewModel.NameR))
            {
                ViewBag.Error = "!!!";
            }
            var Result = await _roleManager.CreateAsync(new AplicationRole(viewModel.NameR,viewModel.DiscriptionR));
            if (Result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            return View(viewModel);
        }
    }
}