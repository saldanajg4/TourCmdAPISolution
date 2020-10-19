using Microsoft.EntityFrameworkCore.Migrations;

namespace TourCmdAPI.Migrations
{
    public partial class AddedEstimateCostasdecimaltoItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "EstimatedCost",
                table: "Items",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "EstimatedCost",
                table: "Items",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(decimal));
        }
    }
}
