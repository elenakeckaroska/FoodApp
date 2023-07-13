using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodApp.Web.Data.Migrations
{
    public partial class FavoritesRecepiesPerUser2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteRecipeUser_Recipes_RecipeId",
                table: "FavoriteRecipeUser");

            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteRecipeUser_AspNetUsers_UserId",
                table: "FavoriteRecipeUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FavoriteRecipeUser",
                table: "FavoriteRecipeUser");

            migrationBuilder.RenameTable(
                name: "FavoriteRecipeUser",
                newName: "FavoriteRecipeUsers");

            migrationBuilder.RenameIndex(
                name: "IX_FavoriteRecipeUser_UserId",
                table: "FavoriteRecipeUsers",
                newName: "IX_FavoriteRecipeUsers_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_FavoriteRecipeUser_RecipeId",
                table: "FavoriteRecipeUsers",
                newName: "IX_FavoriteRecipeUsers_RecipeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FavoriteRecipeUsers",
                table: "FavoriteRecipeUsers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteRecipeUsers_Recipes_RecipeId",
                table: "FavoriteRecipeUsers",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteRecipeUsers_AspNetUsers_UserId",
                table: "FavoriteRecipeUsers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteRecipeUsers_Recipes_RecipeId",
                table: "FavoriteRecipeUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteRecipeUsers_AspNetUsers_UserId",
                table: "FavoriteRecipeUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FavoriteRecipeUsers",
                table: "FavoriteRecipeUsers");

            migrationBuilder.RenameTable(
                name: "FavoriteRecipeUsers",
                newName: "FavoriteRecipeUser");

            migrationBuilder.RenameIndex(
                name: "IX_FavoriteRecipeUsers_UserId",
                table: "FavoriteRecipeUser",
                newName: "IX_FavoriteRecipeUser_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_FavoriteRecipeUsers_RecipeId",
                table: "FavoriteRecipeUser",
                newName: "IX_FavoriteRecipeUser_RecipeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FavoriteRecipeUser",
                table: "FavoriteRecipeUser",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteRecipeUser_Recipes_RecipeId",
                table: "FavoriteRecipeUser",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteRecipeUser_AspNetUsers_UserId",
                table: "FavoriteRecipeUser",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
