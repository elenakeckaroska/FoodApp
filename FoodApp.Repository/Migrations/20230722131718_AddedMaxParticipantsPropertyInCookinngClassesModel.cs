using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodApp.Repository.Migrations
{
    public partial class AddedMaxParticipantsPropertyInCookinngClassesModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MaxParticipants",
                table: "CookingClasses",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxParticipants",
                table: "CookingClasses");
        }
    }
}
