using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookEnd.Models.ViewModels
{
    public class BookIndexViewModel
    {
        public int BookId { get; set; }
        [Display(Name = "عنوان")]
        public string Title { get; set; }
        [Display(Name = "قیـمت")]
        public int Price { get; set; }
        [Display(Name = "تعداد")]
        public int Stock { get; set; }
        [Display(Name = "شابک")]
        public string ISBN { get; set; }
        [Display(Name = "منتشر شده ")]
        public bool IsPublish { get; set; }
        [Display(Name = "تاریخ انتشار")]
        public DateTime? PublishDate { get; set; }
        [Display(Name = "ناشر")]
        public string Publisher { get; set; }
        [Display(Name = "تصویر")]
        public IFormFile Image { get; set; }
        [Display(Name ="نویسندگان")]
        public string Auther { get; set; }
    }
}
