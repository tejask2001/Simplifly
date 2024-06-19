using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Simplifly.Migrations
{
    public partial class priceModified : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BasePrice",
                table: "Flights");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "BasePrice",
                table: "Flights",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
