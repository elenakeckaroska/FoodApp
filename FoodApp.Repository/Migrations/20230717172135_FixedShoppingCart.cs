using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodApp.Repository.Migrations
{
    public partial class FixedShoppingCart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Price",
                table: "CookingClasses",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "CookingClasses");
        }
    }
}
