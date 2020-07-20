using Microsoft.EntityFrameworkCore.Migrations;

namespace ShoppingCart.Data.Migrations
{
    public partial class removeUserId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "User");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "User",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
