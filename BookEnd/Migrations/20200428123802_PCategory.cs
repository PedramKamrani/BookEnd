using Microsoft.EntityFrameworkCore.Migrations;

namespace BookEnd.Migrations
{
    public partial class PCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PCategory",
                table: "Categories",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PCategory",
                table: "Categories");
        }
    }
}
