using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Simplifly.Migrations
{
    public partial class updatedSeats : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TotalSeats",
                table: "Flights",
                newName: "TotalPremiumEconomySeats");

            migrationBuilder.AddColumn<double>(
                name: "EconomySeatPrice",
                table: "Flights",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "PremiumEconomySeatPrice",
                table: "Flights",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "TotalBusinessClassSeats",
                table: "Flights",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalEconomySeats",
                table: "Flights",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "lBusinessClassSeatPrice",
                table: "Flights",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EconomySeatPrice",
                table: "Flights");

            migrationBuilder.DropColumn(
                name: "PremiumEconomySeatPrice",
                table: "Flights");

            migrationBuilder.DropColumn(
                name: "TotalBusinessClassSeats",
                table: "Flights");

            migrationBuilder.DropColumn(
                name: "TotalEconomySeats",
                table: "Flights");

            migrationBuilder.DropColumn(
                name: "lBusinessClassSeatPrice",
                table: "Flights");

            migrationBuilder.RenameColumn(
                name: "TotalPremiumEconomySeats",
                table: "Flights",
                newName: "TotalSeats");
        }
    }
}
