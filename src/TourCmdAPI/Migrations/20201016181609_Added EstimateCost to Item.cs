using Microsoft.EntityFrameworkCore.Migrations;

namespace TourCmdAPI.Migrations
{
    public partial class AddedEstimateCosttoItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "EstimatedCost",
                table: "Items",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EstimatedCost",
                table: "Items");
        }
    }
}
