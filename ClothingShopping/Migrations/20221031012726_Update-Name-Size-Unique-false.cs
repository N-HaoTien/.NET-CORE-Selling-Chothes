using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClothingShopping.Migrations
{
    public partial class UpdateNameSizeUniquefalse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Size_Name",
                table: "Size");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Size",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Size",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Size_Name",
                table: "Size",
                column: "Name",
                unique: true);
        }
    }
}
