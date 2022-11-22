using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClothingShopping.Migrations
{
    public partial class AddFieldBackground : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UrlPictureBg",
                table: "Product",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UrlPictureBg",
                table: "Product");
        }
    }
}
