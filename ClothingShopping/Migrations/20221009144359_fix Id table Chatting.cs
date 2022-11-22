using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClothingShopping.Migrations
{
    public partial class fixIdtableChatting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chatting_AspNetUsers_FromUserId1",
                table: "Chatting");

            migrationBuilder.DropIndex(
                name: "IX_Chatting_FromUserId1",
                table: "Chatting");

            migrationBuilder.DropColumn(
                name: "FromUserId1",
                table: "Chatting");

            migrationBuilder.CreateIndex(
                name: "IX_Chatting_ToUserId",
                table: "Chatting",
                column: "ToUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Chatting_AspNetUsers_ToUserId",
                table: "Chatting",
                column: "ToUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chatting_AspNetUsers_ToUserId",
                table: "Chatting");

            migrationBuilder.DropIndex(
                name: "IX_Chatting_ToUserId",
                table: "Chatting");

            migrationBuilder.AddColumn<string>(
                name: "FromUserId1",
                table: "Chatting",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Chatting_FromUserId1",
                table: "Chatting",
                column: "FromUserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Chatting_AspNetUsers_FromUserId1",
                table: "Chatting",
                column: "FromUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
