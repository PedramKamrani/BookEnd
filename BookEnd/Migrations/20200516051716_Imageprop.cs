using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BookEnd.Migrations
{
    public partial class Imageprop : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "BookStors",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "BookStors");
        }
    }
}
