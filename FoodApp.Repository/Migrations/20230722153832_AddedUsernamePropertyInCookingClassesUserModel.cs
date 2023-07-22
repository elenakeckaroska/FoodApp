using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodApp.Repository.Migrations
{
    public partial class AddedUsernamePropertyInCookingClassesUserModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "CookingClassesUser",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Username",
                table: "CookingClassesUser");
        }
    }
}
