using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookEnd.Models
{
    public class BookStor
    {
        [Key]
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public int Price { get; set; }
        public int Stock { get; set; }
        public string File { get; set; }
        public int NumOfPages { get; set; }
        public short Weight { get; set; }
        public string ISBN { get; set; }
        public byte[] Image { get; set; }
        public bool IsPublish { get; set; }
        public DateTime? PublishDate { get; set; }
        public int PublishYear { get; set; }
        public bool Delete { get; set; }
        public int PublisherID { get; set; }
        public int LanguageID { get; set; }
        public Languge Languge {get; set;}
        public List<Translator_Book> Translator_Books { get; set;}
        public List<Auther_book> Auther_Books { get; set; }
        public List<Category_Book> Category_Books { get; set; }
        public Publisher Publisher { get; set; }
        public List<OrdeeDetails> OrdeeDetails { get; set; }
    }

    public class Category_Book
    {
        public int BookId { get; set; }
        public int CategoryID { get; set; }
        public BookStor BookStor { get; set; }
        public Category Category { get; set; }
    }
    public class Category
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public int? PCategory { get; set; }
        public List<SubCategory> SubCategories { get; set; }
        public List<Category_Book> Category_Books { get; set; }
    }

    public class SubCategory
    {
        public int SubCategoryID { get; set; }
        public string SubCategoryName { get; set; }
        public Category Category { get; set; }
        public List<BookStor> BookStors { get; set; }
    }

    public class Publisher
    {
       public int PublisherId { get; set; }
       public string PublisherName { get; set; }
        public List<BookStor> BookStors { get; set; }
    }
    public class Languge
    {
        public int LangugeId { get; set; }
        public string LanguegeName { get; set; }
        public List<BookStor> Books { get; set; }
    }

    public class Translator_Book
    {
        public int BookId { get; set; }
        public int TranslaorId { get; set; }
        public Translator Translator { get; set; }
        public BookStor BookStor {get; set;}
    }
    public class Translator
    {
        public int TranslaorId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public List<Translator_Book> Translator_Books { get; set; }
    }
    public class Auther_book
    {
        public int BookId { get; set; }
        public int AutherId { get; set; }
        public Auther Auther { get; set; }
        public BookStor BookStor { get; set; }
    }
    public class Auther
    {
        public int AutherId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public List<Auther_book> Auther_Books { get; set; }
    }
}
