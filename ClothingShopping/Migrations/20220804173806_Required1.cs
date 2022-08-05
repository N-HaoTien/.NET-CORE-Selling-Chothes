using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClothingShopping.Migrations
{
    public partial class Required1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Order",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 5, 0, 38, 6, 723, DateTimeKind.Local).AddTicks(6216),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 5, 0, 28, 55, 719, DateTimeKind.Local).AddTicks(7369));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Order",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 5, 0, 28, 55, 719, DateTimeKind.Local).AddTicks(7369),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 5, 0, 38, 6, 723, DateTimeKind.Local).AddTicks(6216));
        }
    }
}
