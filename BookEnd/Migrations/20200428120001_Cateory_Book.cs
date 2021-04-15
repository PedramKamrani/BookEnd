using Microsoft.EntityFrameworkCore.Migrations;

namespace BookEnd.Migrations
{
    public partial class Cateory_Book : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookStors_SubCategories_SubCategoryID",
                table: "BookStors");

            migrationBuilder.AlterColumn<int>(
                name: "SubCategoryID",
                table: "BookStors",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateTable(
                name: "Category_Books",
                columns: table => new
                {
                    BookId = table.Column<int>(nullable: false),
                    CategoryID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category_Books", x => new { x.BookId, x.CategoryID });
                    table.ForeignKey(
                        name: "FK_Category_Books_BookStors_BookId",
                        column: x => x.BookId,
                        principalTable: "BookStors",
                        principalColumn: "BookId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Category_Books_Categories_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "Categories",
                        principalColumn: "CategoryID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Category_Books_CategoryID",
                table: "Category_Books",
                column: "CategoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_BookStors_SubCategories_SubCategoryID",
                table: "BookStors",
                column: "SubCategoryID",
                principalTable: "SubCategories",
                principalColumn: "SubCategoryID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookStors_SubCategories_SubCategoryID",
                table: "BookStors");

            migrationBuilder.DropTable(
                name: "Category_Books");

            migrationBuilder.AlterColumn<int>(
                name: "SubCategoryID",
                table: "BookStors",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BookStors_SubCategories_SubCategoryID",
                table: "BookStors",
                column: "SubCategoryID",
                principalTable: "SubCategories",
                principalColumn: "SubCategoryID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
