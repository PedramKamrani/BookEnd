using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BookEnd.Clasess;
using BookEnd.Models;
using BookEnd.Models.Repasitory;
using BookEnd.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using ReflectionIT.Mvc.Paging;
using SubCategory = BookEnd.Models.ViewModels.SubCategory;

namespace BookEnd.Areas.Admin.Controllers
{
    [Area("Admin")]
   [Authorize]
   [AutoValidateAntiforgeryToken]
    public class StoreController : Controller
    {
        private readonly BookContext _context;
        private readonly BookRepasitory _repasitory;
        private readonly IHostingEnvironment _evn;
        public StoreController(BookContext context, BookRepasitory repasitory, IHostingEnvironment evn)
        {
            _context = context;
            _repasitory = repasitory;
            _evn = evn;
        }
        [Authorize]
        public IActionResult Index(string title="",int row=2,int pager = 1)
        {
            string userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string role = User.FindFirstValue(ClaimTypes.Role);
            var Row =new List<int>
            {
                2,5,10,15
            };
            ViewBag.item = new SelectList(Row,row);
            ViewBag.pageNumber = (pager - 1) * row + 1;
            title = String.IsNullOrEmpty(title) ? "" : title;
            string AuthersName = "";
            List<BookIndexViewModel> viewModels = new List<BookIndexViewModel>();
            var Books = (from b in _context.BookStors
                         join p in _context.Publishers on b.PublisherID equals p.PublisherId
                         join ab in _context.Auther_Books on b.BookId equals ab.BookId
                         join au in _context.Authers on ab.AutherId equals au.AutherId
                         where (b.Delete == false&& b.Title.Contains(title.TrimStart().TrimEnd()))
                         select new BookIndexViewModel
                         {
                             Title = b.Title,
                             ISBN = b.ISBN,
                             BookId = b.BookId,
                             Price = b.Price,
                             Stock = b.Stock,
                             IsPublish = b.IsPublish,
                             PublishDate = b.PublishDate,
                             Publisher = p.PublisherName,
                             Auther = au.Name + " " + au.LastName,
                         })
                         .GroupBy(b => b.BookId).Select(g =>new {g.Key, BookGroups = g }).ToList();

            foreach (var item in Books)
            {
                AuthersName = "";
                foreach (var group in item.BookGroups)
                {
                    if (AuthersName == "")
                        AuthersName = group.Auther;
                    else
                        AuthersName = AuthersName + " - " + group.Auther;
                }

                BookIndexViewModel VM = new BookIndexViewModel()
                {
                    Auther = AuthersName,
                    BookId = item.BookGroups.First().BookId,
                    ISBN = item.BookGroups.First().ISBN,
                    Title = item.BookGroups.First().Title,
                    Price = item.BookGroups.First().Price,
                    IsPublish = item.BookGroups.First().IsPublish,
                    PublishDate = item.BookGroups.First().PublishDate,
                    Publisher = item.BookGroups.First().Publisher,
                    Stock = item.BookGroups.First().Stock,
                    Image= item.BookGroups.First().Image,
                };
                viewModels.Add(VM);
            }
            var PagingModel = PagingList.Create(viewModels, row, pager);
            PagingModel.RouteValue = new RouteValueDictionary
            {
                { "row",row}
            };
            return View(PagingModel);
        }
            
       
        [HttpGet]
        [Authorize(Roles = "مدیریت")]
        public IActionResult Creat()
        {
            ViewBag.AuthorID = new SelectList(_context.Authers.Select(r => new ListAuther { AutherId = r.AutherId, NameFamily = r.Name + " " + r.LastName }), "AutherId", "NameFamily");
            ViewBag.LanguageID = new SelectList(_context.Languges, "LangugeId", "LanguegeName");
            ViewBag.PublisherID = new SelectList(_context.Publishers, "PublisherId", "PublisherName");
            ViewBag.TranslatorID = new SelectList(_context.Translators.Select(r => new ListTranslator { TranslatorId = r.TranslaorId, NameFamily = r.Name + " " + r.LastName }), "TranslatorId", "NameFamily");
            BookCreateViewModel viewModel = new BookCreateViewModel();
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Creat(BookCreateViewModel viewModel)
        {

            if (ModelState.IsValid)
            {
                if (viewModel.File!=null)
                {
                    string FileName = viewModel.File.FileName;
                    string Extension = Path.GetExtension(FileName);
                    var Type = FileExtentions.FileType.PDF;
                    using(var memory=new MemoryStream())
                    {
                        await viewModel.File.CopyToAsync(memory);
                       //result = FileExtentions.IsValidFile(memory.ToArray(), Type, Extension.Replace('.', ' '));
                        string NewFileName = String.Concat(Guid.NewGuid().ToString(), Extension);
                        var path = $"{_evn.WebRootPath}/BookFile/{NewFileName}";
                        using (var Stream = new FileStream(path, FileMode.Create))
                        {
                            await viewModel.File.CopyToAsync(Stream);
                        }
                        viewModel.FileName = NewFileName;
                    }
                   
                }
                DateTime? PublishDate1 = null;
                if (viewModel.IsPublish == true)
                {
                    PublishDate1 = DateTime.Now;
                }
                BookStor book = new BookStor
                {
                    BookId = viewModel.BookId,
                    Delete = false,
                    ISBN = viewModel.ISBN,
                    LanguageID = viewModel.LanguageID,
                    IsPublish = viewModel.IsPublish,
                    Price = viewModel.Price,
                    NumOfPages = viewModel.NumOfPages,
                    PublishYear = viewModel.PublishYear,
                    Stock = viewModel.Stock,
                    Summary = viewModel.Summary,
                    Title = viewModel.Title,
                    Weight = viewModel.Weight,
                    PublishDate = PublishDate1,
                    PublisherID = viewModel.PublisherID,
                    File=viewModel.FileName,
                   
                };
                if (viewModel.Image != null)
                {
                    using (var Memory = new MemoryStream())
                    {
                       await viewModel.Image.CopyToAsync(Memory);
                        book.Image= Memory.ToArray();
                    }
                }
                await _context.BookStors.AddAsync(book);
                List<Auther_book> authors = new List<Auther_book>();
                if (viewModel.AuthorID != null)
                {
                    for (int i = 0; i < viewModel.AuthorID.Length; i++)
                    {
                        Auther_book author = new Auther_book()
                        {
                            BookId = book.BookId,
                            AutherId = viewModel.AuthorID[i],
                        };

                        authors.Add(author);
                    }

                    await _context.Auther_Books.AddRangeAsync(authors);
                }
                List<Translator_Book> translators = new List<Translator_Book>();
                if (viewModel.TranslatorID != null)
                {
                    for (int i = 0; i < viewModel.TranslatorID.Length; i++)
                    {
                        Translator_Book translator = new Translator_Book
                        {
                            BookId = book.BookId,
                            TranslaorId = viewModel.TranslatorID[i]
                        };
                        translators.Add(translator);
                    }
                    await _context.Translator_Books.AddRangeAsync(translators);
                }
                List<Category_Book> categories = new List<Category_Book>();
                if (viewModel.CategoryID != null)
                {
                    for (int i = 0; i < viewModel.CategoryID.Length; i++)
                    {
                        Category_Book category = new Category_Book
                        {
                            BookId = book.BookId,
                            CategoryID = viewModel.CategoryID[i]
                        };
                    }
                }


                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.AuthorID = new SelectList(_context.Authers.Select(r => new ListAuther { AutherId = r.AutherId, NameFamily = r.Name + " " + r.LastName }), "AutherId", "NameFamily");
                ViewBag.LanguageID = new SelectList(_context.Languges, "LangugeId", "LanguegeName");
                ViewBag.PublisherID = new SelectList(_context.Publishers, "PublisherId", "PublisherName");
                ViewBag.TranslatorID = new SelectList(_context.Translators.Select(r => new ListTranslator { TranslatorId = r.TranslaorId, NameFamily = r.Name + " " + r.LastName }), "TranslatorId", "NameFamily");
                viewModel.Category = _repasitory.GetAllCategory();
                return View(viewModel);
            }

        }
        [AllowAnonymous]
        public IActionResult Detail(int id)
        {
            var Book = _context.BookStors.Include(p=>p.Publisher).Include(l=>l.Languge).First(p=>p.BookId==id);
                
            return PartialView(Book);
        }
        [HttpGet]
        [Authorize(Roles = "مدیریت")]
        public IActionResult Delete(int? id)
        {
            var Book = _context.BookStors.Find(id);
            return View(Book);
        }
        public async Task<IActionResult> ViewImage(int? id)
        {
            var book = await _context.BookStors.FindAsync(id);
            if (id == null)
            {
                return NotFound();
            }
            else
            {
                var memory = new MemoryStream(book.Image);
                return new FileStreamResult(memory, "image/png");
               
            }
        }
        [HttpPost]
        public IActionResult Deleted(BookStor book)
        {
            var books = _context.BookStors.FirstOrDefault(b => b.BookId == book.BookId);
            if (book.Delete==false)
            {
                books.Delete=book.Delete=true;
            }
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        [Authorize(Roles = "مدیریت")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (ModelState.IsValid)
            {
                if (id == null)
                {
                    return NotFound();
                }
                else
                {
                    var book =await _context.BookStors.FindAsync(id);
                    if (book == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        var ViewModel = (from b in _context.BookStors
                                         join l in _context.Languges on b.LanguageID equals l.LangugeId
                                         join p in _context.Publishers on b.PublisherID equals p.PublisherId
                                         where (b.BookId == id)
                                         select new BookCreateViewModel
                                         {
                                             BookId = b.BookId,
                                             ISBN = b.ISBN,
                                             IsPublish = b.IsPublish,
                                             NumOfPages = b.NumOfPages,
                                             Price = b.Price,
                                             PublishYear = b.PublishYear,
                                             Stock = b.Stock,
                                             Summary = b.Summary,
                                             Title = b.Title,
                                             Weight = b.Weight,
                                             Language = l.LanguegeName,
                                             Publisher = p.PublisherName,
                                             Images=b.Image
                                         }).FirstOrDefault();

                        int[] Auther = (from a in _context.Auther_Books
                                        where (a.BookId == id)
                                        select (a.AutherId)).ToArray();

                        int[] Translator = (from t in _context.Translator_Books
                                            where (t.BookId == id)
                                            select (t.TranslaorId)).ToArray();

                        int[] Category = (from c in _context.Category_Books
                                          where (c.BookId == id)
                                          select (c.CategoryID)
                                        ).ToArray();

                        ViewModel.AuthorID = Auther;
                        ViewModel.TranslatorID = Translator;
                        ViewModel.CategoryID = Category;

                        ViewBag.AuthorID = new SelectList(_context.Authers.Select(r => new ListAuther { AutherId = r.AutherId, NameFamily = r.Name + " " + r.LastName }), "AutherId", "NameFamily");
                        ViewBag.LanguageID = new SelectList(_context.Languges, "LangugeId", "LanguegeName");
                        ViewBag.PublisherID = new SelectList(_context.Publishers, "PublisherId", "PublisherName");
                        ViewBag.TranslatorID = new SelectList(_context.Translators.Select(r => new ListTranslator { TranslatorId = r.TranslaorId, NameFamily = r.Name + " " + r.LastName }), "TranslatorId", "NameFamily");
                       ViewModel.Category = _repasitory.GetAllCategory();
                        ViewModel.SubCategory = new SubCategory(_repasitory.GetAllCategory(),null);
                        await _context.SaveChangesAsync();
                        return View(ViewModel);
                    }
                }
            }
            else
            {
                ViewBag.Error = "Error";
            }
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(BookCreateViewModel ViewModel)
        {

           
            if (!ModelState.IsValid)
            {
                try
                {
                    DateTime? PublishDate;
                    if (ViewModel.IsPublish == true && ViewModel.RecentIsPublish == false)
                    {
                        PublishDate = DateTime.Now;
                    }
                    else if (ViewModel.RecentIsPublish == true && ViewModel.IsPublish == false)
                    {
                        PublishDate = null;
                    }

                    else
                    {
                        PublishDate = ViewModel.PublishDate;
                    }

                    BookStor book = new BookStor()
                    {
                        BookId = ViewModel.BookId,
                        Title = ViewModel.Title,
                        ISBN = ViewModel.ISBN,
                        NumOfPages = ViewModel.NumOfPages,
                        Price = ViewModel.Price,
                        Stock = ViewModel.Stock,
                        IsPublish = ViewModel.IsPublish,
                        LanguageID = ViewModel.LanguageID,
                        PublisherID = ViewModel.PublisherID,
                        PublishYear = ViewModel.PublishYear,
                        Summary = ViewModel.Summary,
                        Weight = ViewModel.Weight,
                        PublishDate = PublishDate,
                        Delete = false,
                    };

                    _context.Update(book);

                    var RecentAuthors = (from a in _context.Auther_Books
                                         where (a.BookId == ViewModel.BookId)
                                         select a.AutherId).ToArray();

                    var RecentTranslators = (from a in _context.Translator_Books
                                             where (a.BookId == ViewModel.BookId)
                                             select a.TranslaorId).ToArray();

                

                    var DeletedAuthors = RecentAuthors.Except(ViewModel.AuthorID);
                    var DeletedTranslators = RecentTranslators.Except(ViewModel.TranslatorID);
                    

                    var AddedAuthors = ViewModel.AuthorID.Except(RecentAuthors);
                    var AddedTranslators = ViewModel.TranslatorID.Except(RecentTranslators);
              

                    if (DeletedAuthors.Count() != 0)
                        _context.RemoveRange(DeletedAuthors.Select(a => new Auther_book { AutherId = a, BookId = ViewModel.BookId }).ToList());

                    if (DeletedTranslators.Count() != 0)
                        _context.RemoveRange(DeletedTranslators.Select(a => new Translator_Book { TranslaorId = a, BookId = ViewModel.BookId }).ToList());

                   
                    if (AddedAuthors.Count() != 0)
                        _context.AddRange(AddedAuthors.Select(a => new Auther_book { AutherId = a, BookId = ViewModel.BookId }).ToList());

                    if (AddedTranslators.Count() != 0)
                        _context.AddRange(AddedTranslators.Select(a => new Translator_Book { TranslaorId = a, BookId = ViewModel.BookId }).ToList());

                    await _context.SaveChangesAsync();

                    ViewBag.MsgSuccess = "ذخیره تغییرات با موفقیت انجام شد.";
                    return View(ViewModel);
                }

                catch
                {
                    ViewBag.MsgFailed = "در ذخیره تغییرات خطایی رخ داده است.";
                   
                }
            }

            ViewBag.AuthorID = new SelectList(_context.Authers.Select(r => new ListAuther { AutherId = r.AutherId, NameFamily = r.Name + " " + r.LastName }), "AutherId", "NameFamily");
            ViewBag.LanguageID = new SelectList(_context.Languges, "LanguegeName", "LanguegeName");
            ViewBag.PublisherID = new SelectList(_context.Publishers, "PublisherId", "PublisherName");
            ViewBag.TranslatorID = new SelectList(_context.Translators.Select(r => new ListTranslator { TranslatorId = r.TranslaorId, NameFamily = r.Name + " " + r.LastName }), "TranslatorId", "NameFamily");
            return View(ViewModel);
        }
        public async Task<IActionResult> Download(int? id)
        {
            var Book =await _context.BookStors.FindAsync(id);
            if (Book == null)
            {
                return Content("همچییین موجود نیست");
            }
            else
            {
                if (Book.File==null)
                {
                    return Content("فایل  موجود نیست");
                }
                var path = $"{_evn.WebRootPath}/BookFile/{Book.File}";
                var Memory = new MemoryStream();
                using(var stream= new FileStream(path, FileMode.Open))
                {
                    await stream.CopyToAsync(Memory);
                }
                Memory.Position = 0;
                return File(Memory, FileExtentions.GetContentType(path), Book.File);
            }
            
        }
    }

}