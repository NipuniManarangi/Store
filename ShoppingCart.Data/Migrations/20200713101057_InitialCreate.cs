using Microsoft.EntityFrameworkCore.Migrations;

namespace ShoppingCart.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Code = table.Column<string>(maxLength: 10, nullable: true),
                    QuantityInStock = table.Column<int>(nullable: true),
                    UnitPrice = table.Column<decimal>(type: "money", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(60)", nullable: false),
                    ContatctNo = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Address_Line1 = table.Column<string>(type: "nvarchar(60)", nullable: false),
                    Address_Line2 = table.Column<string>(type: "nvarchar(60)", nullable: false),
                    State = table.Column<string>(type: "nvarchar(60)", nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(60)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
