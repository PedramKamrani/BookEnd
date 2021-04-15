using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BookEnd.Models;
using Microsoft.AspNetCore.Authorization;

namespace BookEnd.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly BookContext _context;
        public HomeController(BookContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            
            return View();
        }
        

        public IActionResult Contat() => View();

       public IActionResult NewPruodct()
        {
            var Books = _context.BookStors.OrderBy(o=>o.PublishDate).Take(7).ToList();
            return View(Books);
        }
        [Authorize]
        public IActionResult Detail(int id)
        {
            var book = _context.BookStors.Find(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }
    }
}
