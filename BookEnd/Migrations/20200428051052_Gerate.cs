using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BookEnd.Migrations
{
    public partial class Gerate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Authers",
                columns: table => new
                {
                    AutherId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authers", x => x.AutherId);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CategoryName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryID);
                });

            migrationBuilder.CreateTable(
                name: "Languges",
                columns: table => new
                {
                    LangugeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LanguegeName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languges", x => x.LangugeId);
                });

            migrationBuilder.CreateTable(
                name: "Publishers",
                columns: table => new
                {
                    PublisherId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PublisherName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publishers", x => x.PublisherId);
                });

            migrationBuilder.CreateTable(
                name: "Translators",
                columns: table => new
                {
                    TranslaorId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Translators", x => x.TranslaorId);
                });

            migrationBuilder.CreateTable(
                name: "SubCategories",
                columns: table => new
                {
                    SubCategoryID = table.Column<int>(nullable: false),
                    SubCategoryName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubCategories", x => x.SubCategoryID);
                    table.ForeignKey(
                        name: "FK_SubCategories_Categories_SubCategoryID",
                        column: x => x.SubCategoryID,
                        principalTable: "Categories",
                        principalColumn: "CategoryID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookStors",
                columns: table => new
                {
                    BookId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: true),
                    Summary = table.Column<string>(nullable: true),
                    Price = table.Column<int>(nullable: false),
                    Stock = table.Column<int>(nullable: false),
                    File = table.Column<string>(nullable: true),
                    NumOfPages = table.Column<int>(nullable: false),
                    Weight = table.Column<short>(nullable: false),
                    ISBN = table.Column<string>(nullable: true),
                    IsPublish = table.Column<bool>(nullable: false),
                    PublishDate = table.Column<DateTime>(nullable: true),
                    PublishYear = table.Column<int>(nullable: false),
                    Delete = table.Column<bool>(nullable: false),
                    PublisherID = table.Column<int>(nullable: false),
                    LanguageID = table.Column<int>(nullable: false),
                    SubCategoryID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookStors", x => x.BookId);
                    table.ForeignKey(
                        name: "FK_BookStors_Languges_LanguageID",
                        column: x => x.LanguageID,
                        principalTable: "Languges",
                        principalColumn: "LangugeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookStors_Publishers_PublisherID",
                        column: x => x.PublisherID,
                        principalTable: "Publishers",
                        principalColumn: "PublisherId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookStors_SubCategories_SubCategoryID",
                        column: x => x.SubCategoryID,
                        principalTable: "SubCategories",
                        principalColumn: "SubCategoryID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Auther_Books",
                columns: table => new
                {
                    BookId = table.Column<int>(nullable: false),
                    AutherId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Auther_Books", x => new { x.AutherId, x.BookId });
                    table.ForeignKey(
                        name: "FK_Auther_Books_Authers_AutherId",
                        column: x => x.AutherId,
                        principalTable: "Authers",
                        principalColumn: "AutherId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Auther_Books_BookStors_BookId",
                        column: x => x.BookId,
                        principalTable: "BookStors",
                        principalColumn: "BookId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Translator_Books",
                columns: table => new
                {
                    BookId = table.Column<int>(nullable: false),
                    TranslaorId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Translator_Books", x => new { x.TranslaorId, x.BookId });
                    table.ForeignKey(
                        name: "FK_Translator_Books_BookStors_BookId",
                        column: x => x.BookId,
                        principalTable: "BookStors",
                        principalColumn: "BookId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Translator_Books_Translators_TranslaorId",
                        column: x => x.TranslaorId,
                        principalTable: "Translators",
                        principalColumn: "TranslaorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Auther_Books_BookId",
                table: "Auther_Books",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_BookStors_LanguageID",
                table: "BookStors",
                column: "LanguageID");

            migrationBuilder.CreateIndex(
                name: "IX_BookStors_PublisherID",
                table: "BookStors",
                column: "PublisherID");

            migrationBuilder.CreateIndex(
                name: "IX_BookStors_SubCategoryID",
                table: "BookStors",
                column: "SubCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Translator_Books_BookId",
                table: "Translator_Books",
                column: "BookId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Auther_Books");

            migrationBuilder.DropTable(
                name: "Translator_Books");

            migrationBuilder.DropTable(
                name: "Authers");

            migrationBuilder.DropTable(
                name: "BookStors");

            migrationBuilder.DropTable(
                name: "Translators");

            migrationBuilder.DropTable(
                name: "Languges");

            migrationBuilder.DropTable(
                name: "Publishers");

            migrationBuilder.DropTable(
                name: "SubCategories");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
