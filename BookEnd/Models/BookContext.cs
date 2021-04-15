using BookEnd.Mapping;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookEnd.Models
{
    public class BookContext:DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.;DataBase=BookEnd;Trusted_Connection=True");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PublisherMap());
               
            modelBuilder.Entity<BookStor>().HasKey(b => b.BookId);
            modelBuilder.Entity<BookStor>()
                .HasOne(o => o.Languge)
                .WithMany(l => l.Books)
                .HasForeignKey(p => p.LanguageID);
            //modelBuilder.Entity<BookStor>()
            //    .HasOne(b => b.SubCategory)
            //    .WithMany(c => c.BookStors)
            //    .HasForeignKey(p => p.SubCategoryID);

            modelBuilder.Entity<Category>().HasKey(c => c.CategoryID);
            modelBuilder.Entity<Translator>().HasKey(t => t.TranslaorId);
            modelBuilder.Entity<Auther>().HasKey(t => t.AutherId);
            modelBuilder.Entity<SubCategory>().HasKey(s => s.SubCategoryID);
            modelBuilder.Entity<SubCategory>().
               HasOne(o => o.Category)
               .WithMany(p => p.SubCategories)
               .HasForeignKey(u => u.SubCategoryID);
            modelBuilder.Entity<Auther_book>().HasKey(r => new { r.AutherId, r.BookId });
            modelBuilder.Entity<Auther_book>()
                .HasOne(a => a.Auther)
                .WithMany(b => b.Auther_Books)
                .HasForeignKey(c => c.AutherId);

            modelBuilder.Entity<Auther_book>()
                .HasOne(p => p.BookStor)
                .WithMany(o => o.Auther_Books)
                .HasForeignKey(h => h.BookId);
            modelBuilder.Entity<Translator_Book>().HasKey(r => new { r.TranslaorId, r.BookId });
            modelBuilder.Entity<Translator_Book>()
                .HasOne(t => t.Translator)
                .WithMany(o => o.Translator_Books)
                .HasForeignKey(m => m.TranslaorId);


            modelBuilder.Entity<Translator_Book>()
                .HasOne(t => t.BookStor)
                .WithMany(o => o.Translator_Books)
                .HasForeignKey(m => m.BookId);
            modelBuilder.Entity<Category_Book>().HasKey(r => new { r.BookId, r.CategoryID });
            modelBuilder.Entity<Category_Book>()
                .HasOne(a => a.BookStor)
                .WithMany(b => b.Category_Books)
                .HasForeignKey(c => c.BookId);
            modelBuilder.Entity<Category_Book>()
                .HasOne(a => a.Category)
                .WithMany(b => b.Category_Books)
                .HasForeignKey(c => c.CategoryID);
           

        }
        public DbSet<BookStor> BookStors { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<Languge> Languges { get; set; }
        public DbSet<Auther_book> Auther_Books { get; set; }
        public DbSet<Auther> Authers { get; set; }
        public DbSet<Translator_Book> Translator_Books { get; set; }
        public DbSet<Translator> Translators { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Category_Book> Category_Books { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrdeeDetails> OrdeeDetails { get; set; }
    }
}
