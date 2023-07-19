using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodApp.Repository.Migrations
{
    public partial class AddedRelationBetweenUserAndCookingClasses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CookingClassesUser",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CookingClassesID = table.Column<Guid>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CookingClassesUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CookingClassesUser_CookingClasses_CookingClassesID",
                        column: x => x.CookingClassesID,
                        principalTable: "CookingClasses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CookingClassesUser_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CookingClassesUser_CookingClassesID",
                table: "CookingClassesUser",
                column: "CookingClassesID");

            migrationBuilder.CreateIndex(
                name: "IX_CookingClassesUser_UserId",
                table: "CookingClassesUser",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CookingClassesUser");
        }
    }
}
