using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Simplifly.Migrations
{
    public partial class updatedPassenger : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Passengers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Passengers_UserId",
                table: "Passengers",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Passengers_Customers_UserId",
                table: "Passengers",
                column: "UserId",
                principalTable: "Customers",
                principalColumn: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Passengers_Customers_UserId",
                table: "Passengers");

            migrationBuilder.DropIndex(
                name: "IX_Passengers_UserId",
                table: "Passengers");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Passengers");
        }
    }
}
