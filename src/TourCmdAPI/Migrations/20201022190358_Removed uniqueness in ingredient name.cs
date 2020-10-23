using Microsoft.EntityFrameworkCore.Migrations;

namespace TourCmdAPI.Migrations
{
    public partial class Removeduniquenessiningredientname : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_IngredientCategories_IngredientCategoryName",
                table: "IngredientCategories");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_IngredientCategories_IngredientCategoryName",
                table: "IngredientCategories",
                column: "IngredientCategoryName",
                unique: true);
        }
    }
}
