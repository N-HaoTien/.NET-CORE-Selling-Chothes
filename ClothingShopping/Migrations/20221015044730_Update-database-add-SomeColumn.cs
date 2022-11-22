using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClothingShopping.Migrations
{
    public partial class UpdatedatabaseaddSomeColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Product");

            migrationBuilder.RenameColumn(
                name: "receiveddate",
                table: "Order",
                newName: "ReceivedDate");

            migrationBuilder.AddColumn<int>(
                name: "CurrentQuantity",
                table: "ProductSizes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "NewPrice",
                table: "Product",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "OldPrice",
                table: "Product",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "ProductImg",
                table: "OrderItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Size",
                table: "OrderItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Order",
                type: "nvarchar(1)",
                maxLength: 1,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "CheckerUserId",
                table: "Order",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "Order",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RejectContent",
                table: "Order",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "StatusName",
                table: "Order",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Order_CheckerUserId",
                table: "Order",
                column: "CheckerUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_AspNetUsers_CheckerUserId",
                table: "Order",
                column: "CheckerUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_AspNetUsers_CheckerUserId",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Order_CheckerUserId",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "CurrentQuantity",
                table: "ProductSizes");

            migrationBuilder.DropColumn(
                name: "NewPrice",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "OldPrice",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "ProductImg",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "Size",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "CheckerUserId",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "RejectContent",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "StatusName",
                table: "Order");

            migrationBuilder.RenameColumn(
                name: "ReceivedDate",
                table: "Order",
                newName: "receiveddate");

            migrationBuilder.AddColumn<int>(
                name: "Price",
                table: "Product",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Product",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Order",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(1)",
                oldMaxLength: 1);
        }
    }
}
