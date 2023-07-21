using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodApp.Repository.Migrations
{
    public partial class AddedOrderAndPayment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CookingClassInOrder",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ClassId = table.Column<Guid>(nullable: false),
                    OrderId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CookingClassInOrder", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CookingClassInOrder_CookingClasses_ClassId",
                        column: x => x.ClassId,
                        principalTable: "CookingClasses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CookingClassInOrder_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CookingClassInOrder_ClassId",
                table: "CookingClassInOrder",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_CookingClassInOrder_OrderId",
                table: "CookingClassInOrder",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CookingClassInOrder");

            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}
