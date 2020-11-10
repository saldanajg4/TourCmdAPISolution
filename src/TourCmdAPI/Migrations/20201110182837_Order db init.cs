using Microsoft.EntityFrameworkCore.Migrations;

namespace TourCmdAPI.Migrations
{
    public partial class Orderdbinit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CardOwnerName",
                table: "PaymentDetails",
                maxLength: 70,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(200)",
                oldMaxLength: 200);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CardOwnerName",
                table: "PaymentDetails",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 70);
        }
    }
}
