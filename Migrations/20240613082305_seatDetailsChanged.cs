using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Simplifly.Migrations
{
    public partial class seatDetailsChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "lBusinessClassSeatPrice",
                table: "Flights",
                newName: "BusinessClassSeatPrice");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BusinessClassSeatPrice",
                table: "Flights",
                newName: "lBusinessClassSeatPrice");
        }
    }
}
