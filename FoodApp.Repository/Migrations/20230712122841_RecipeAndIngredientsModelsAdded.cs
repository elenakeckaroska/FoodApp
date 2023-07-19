using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodApp.Repository.Migrations
{
    public partial class RecipeAndIngredientsModelsAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OwnerOfRecipeId",
                table: "Recipes",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_OwnerOfRecipeId",
                table: "Recipes",
                column: "OwnerOfRecipeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_AspNetUsers_OwnerOfRecipeId",
                table: "Recipes",
                column: "OwnerOfRecipeId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_AspNetUsers_OwnerOfRecipeId",
                table: "Recipes");

            migrationBuilder.DropIndex(
                name: "IX_Recipes_OwnerOfRecipeId",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "OwnerOfRecipeId",
                table: "Recipes");
        }
    }
}
