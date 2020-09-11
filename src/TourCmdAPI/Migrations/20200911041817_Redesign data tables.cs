using Microsoft.EntityFrameworkCore.Migrations;

namespace TourCmdAPI.Migrations
{
    public partial class Redesigndatatables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shows_TourItems_TourId",
                table: "Shows");

            migrationBuilder.DropForeignKey(
                name: "FK_TourItems_Band_BandId",
                table: "TourItems");

            migrationBuilder.DropForeignKey(
                name: "FK_TourItems_Manager_ManagerId",
                table: "TourItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TourItems",
                table: "TourItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Manager",
                table: "Manager");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Band",
                table: "Band");

            migrationBuilder.RenameTable(
                name: "TourItems",
                newName: "Tours");

            migrationBuilder.RenameTable(
                name: "Manager",
                newName: "Managers");

            migrationBuilder.RenameTable(
                name: "Band",
                newName: "Bands");

            migrationBuilder.RenameIndex(
                name: "IX_TourItems_ManagerId",
                table: "Tours",
                newName: "IX_Tours_ManagerId");

            migrationBuilder.RenameIndex(
                name: "IX_TourItems_BandId",
                table: "Tours",
                newName: "IX_Tours_BandId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tours",
                table: "Tours",
                column: "TourId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Managers",
                table: "Managers",
                column: "ManagerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bands",
                table: "Bands",
                column: "BandId");

            migrationBuilder.AddForeignKey(
                name: "FK_Shows_Tours_TourId",
                table: "Shows",
                column: "TourId",
                principalTable: "Tours",
                principalColumn: "TourId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tours_Bands_BandId",
                table: "Tours",
                column: "BandId",
                principalTable: "Bands",
                principalColumn: "BandId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tours_Managers_ManagerId",
                table: "Tours",
                column: "ManagerId",
                principalTable: "Managers",
                principalColumn: "ManagerId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shows_Tours_TourId",
                table: "Shows");

            migrationBuilder.DropForeignKey(
                name: "FK_Tours_Bands_BandId",
                table: "Tours");

            migrationBuilder.DropForeignKey(
                name: "FK_Tours_Managers_ManagerId",
                table: "Tours");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tours",
                table: "Tours");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Managers",
                table: "Managers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bands",
                table: "Bands");

            migrationBuilder.RenameTable(
                name: "Tours",
                newName: "TourItems");

            migrationBuilder.RenameTable(
                name: "Managers",
                newName: "Manager");

            migrationBuilder.RenameTable(
                name: "Bands",
                newName: "Band");

            migrationBuilder.RenameIndex(
                name: "IX_Tours_ManagerId",
                table: "TourItems",
                newName: "IX_TourItems_ManagerId");

            migrationBuilder.RenameIndex(
                name: "IX_Tours_BandId",
                table: "TourItems",
                newName: "IX_TourItems_BandId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TourItems",
                table: "TourItems",
                column: "TourId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Manager",
                table: "Manager",
                column: "ManagerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Band",
                table: "Band",
                column: "BandId");

            migrationBuilder.AddForeignKey(
                name: "FK_Shows_TourItems_TourId",
                table: "Shows",
                column: "TourId",
                principalTable: "TourItems",
                principalColumn: "TourId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TourItems_Band_BandId",
                table: "TourItems",
                column: "BandId",
                principalTable: "Band",
                principalColumn: "BandId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TourItems_Manager_ManagerId",
                table: "TourItems",
                column: "ManagerId",
                principalTable: "Manager",
                principalColumn: "ManagerId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
