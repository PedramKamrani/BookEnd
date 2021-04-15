using BookEnd.Attribuite;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookEnd.Models.ViewModels
{
    public class BookCreateViewModel
    {
        public BookCreateViewModel(SubCategory _SubCategory)
        {
            _SubCategory=SubCategory;
        }
        public BookCreateViewModel()
        {

        }
        public IEnumerable<TreeViewModel> Category { get; set; }
        public SubCategory SubCategory { get; set; }
        public int BookId { get; set; }
        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "این {0} پرشود")]
        public string Title { get; set; }
        [Display(Name = "خلاصه")]
        public string Summary { get; set; }
        [Display(Name = "قیـمت")]
        public int Price { get; set; }
        [Display(Name = "تعداد")]
        public int Stock { get; set; }
        [Display(Name = "فایل")]
        [UploadFileExtensionsAttribute(".pdf,.jpg",ErrorMessage ="فایل معتبر نیست")]
        public IFormFile File { get; set; }
        public string FileName { get; set; }
        [Display(Name = "صفحه")]
        public int NumOfPages { get; set; }
        [Display(Name ="تصویر")]
        [UploadFileExtensionsAttribute(".jpg", ErrorMessage = "فایل معتبر نیست")]
        public IFormFile Image { get; set; }
        public byte[] Images { get; set; }
        [Display(Name = "وزن")]
        public short Weight { get; set; }
        [Display(Name = "شابک")]
        public string ISBN { get; set; }
        [Display(Name = "منتشر شده ")]
        public bool IsPublish { get; set; }
        [Display(Name = "تاریخ انتشار")]
        public DateTime? PublishDate { get; set; }
        [Display(Name = "سال انتشار")]
        public int PublishYear { get; set; }

        [Display(Name = "زبان")]
        public int LanguageID { get; set; }
        public string Language { get; set; }
        [Display(Name = "نویسندگان")]
        public int[] AuthorID { get; set; }
        [Display(Name = "مترجمان")]
        public int[] TranslatorID { get; set; }
        public int[] CategoryID { get; set; }
        [Display(Name = "ناشر")]
        public int PublisherID { get; set; }
        public string Publisher { get; set; }
        public bool RecentIsPublish { get; set; }
    }

    public class ListTranslator
    {
        public int TranslatorId { get; set; }
        public string NameFamily { get; set; }
    }
    public class ListAuther
    {
        public int AutherId { get; set; }
        public string NameFamily { get; set; }
    }
    public class SubCategory
    {
        public SubCategory(List<TreeViewModel> _Categores, int[] _Category)
        {
            _Categores = Categores;
            _Category = Category;
        }
        public List<TreeViewModel> Categores { get; set; }
        public int[] Category { get; set; }
     }
}
