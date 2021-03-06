// <auto-generated />
using System;
using BookEnd.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BookEnd.Migrations
{
    [DbContext(typeof(BookContext))]
    [Migration("20200428123802_PCategory")]
    partial class PCategory
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BookEnd.Models.Auther", b =>
                {
                    b.Property<int>("AutherId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("LastName");

                    b.Property<string>("Name");

                    b.HasKey("AutherId");

                    b.ToTable("Authers");
                });

            modelBuilder.Entity("BookEnd.Models.Auther_book", b =>
                {
                    b.Property<int>("AutherId");

                    b.Property<int>("BookId");

                    b.HasKey("AutherId", "BookId");

                    b.HasIndex("BookId");

                    b.ToTable("Auther_Books");
                });

            modelBuilder.Entity("BookEnd.Models.BookStor", b =>
                {
                    b.Property<int>("BookId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Delete");

                    b.Property<string>("File");

                    b.Property<string>("ISBN");

                    b.Property<bool>("IsPublish");

                    b.Property<int>("LanguageID");

                    b.Property<int>("NumOfPages");

                    b.Property<int>("Price");

                    b.Property<DateTime?>("PublishDate");

                    b.Property<int>("PublishYear");

                    b.Property<int>("PublisherID");

                    b.Property<int>("Stock");

                    b.Property<int?>("SubCategoryID");

                    b.Property<string>("Summary");

                    b.Property<string>("Title");

                    b.Property<short>("Weight");

                    b.HasKey("BookId");

                    b.HasIndex("LanguageID");

                    b.HasIndex("PublisherID");

                    b.HasIndex("SubCategoryID");

                    b.ToTable("BookStors");
                });

            modelBuilder.Entity("BookEnd.Models.Category", b =>
                {
                    b.Property<int>("CategoryID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CategoryName");

                    b.Property<int>("PCategory");

                    b.HasKey("CategoryID");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("BookEnd.Models.Category_Book", b =>
                {
                    b.Property<int>("BookId");

                    b.Property<int>("CategoryID");

                    b.HasKey("BookId", "CategoryID");

                    b.HasIndex("CategoryID");

                    b.ToTable("Category_Books");
                });

            modelBuilder.Entity("BookEnd.Models.Languge", b =>
                {
                    b.Property<int>("LangugeId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("LanguegeName");

                    b.HasKey("LangugeId");

                    b.ToTable("Languges");
                });

            modelBuilder.Entity("BookEnd.Models.Publisher", b =>
                {
                    b.Property<int>("PublisherId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("PublisherName");

                    b.HasKey("PublisherId");

                    b.ToTable("Publishers");
                });

            modelBuilder.Entity("BookEnd.Models.SubCategory", b =>
                {
                    b.Property<int>("SubCategoryID");

                    b.Property<string>("SubCategoryName");

                    b.HasKey("SubCategoryID");

                    b.ToTable("SubCategories");
                });

            modelBuilder.Entity("BookEnd.Models.Translator", b =>
                {
                    b.Property<int>("TranslaorId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("LastName");

                    b.Property<string>("Name");

                    b.HasKey("TranslaorId");

                    b.ToTable("Translators");
                });

            modelBuilder.Entity("BookEnd.Models.Translator_Book", b =>
                {
                    b.Property<int>("TranslaorId");

                    b.Property<int>("BookId");

                    b.HasKey("TranslaorId", "BookId");

                    b.HasIndex("BookId");

                    b.ToTable("Translator_Books");
                });

            modelBuilder.Entity("BookEnd.Models.Auther_book", b =>
                {
                    b.HasOne("BookEnd.Models.Auther", "Auther")
                        .WithMany("Auther_Books")
                        .HasForeignKey("AutherId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BookEnd.Models.BookStor", "BookStor")
                        .WithMany("Auther_Books")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BookEnd.Models.BookStor", b =>
                {
                    b.HasOne("BookEnd.Models.Languge", "Languge")
                        .WithMany("Books")
                        .HasForeignKey("LanguageID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BookEnd.Models.Publisher", "Publisher")
                        .WithMany("BookStors")
                        .HasForeignKey("PublisherID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BookEnd.Models.SubCategory")
                        .WithMany("BookStors")
                        .HasForeignKey("SubCategoryID");
                });

            modelBuilder.Entity("BookEnd.Models.Category_Book", b =>
                {
                    b.HasOne("BookEnd.Models.BookStor", "BookStor")
                        .WithMany("Category_Books")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BookEnd.Models.Category", "Category")
                        .WithMany("Category_Books")
                        .HasForeignKey("CategoryID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BookEnd.Models.SubCategory", b =>
                {
                    b.HasOne("BookEnd.Models.Category", "Category")
                        .WithMany("SubCategories")
                        .HasForeignKey("SubCategoryID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BookEnd.Models.Translator_Book", b =>
                {
                    b.HasOne("BookEnd.Models.BookStor", "BookStor")
                        .WithMany("Translator_Books")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BookEnd.Models.Translator", "Translator")
                        .WithMany("Translator_Books")
                        .HasForeignKey("TranslaorId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
