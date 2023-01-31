using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace puma.Migrations
{
    /// <inheritdoc />
    public partial class final : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_categoryMaster_stationCategories_stationCategoryId",
                table: "categoryMaster");

            migrationBuilder.DropForeignKey(
                name: "FK_StationMasters_stationCategories_stationCategoryId",
                table: "StationMasters");

            migrationBuilder.DropIndex(
                name: "IX_StationMasters_stationCategoryId",
                table: "StationMasters");

            migrationBuilder.DropIndex(
                name: "IX_categoryMaster_stationCategoryId",
                table: "categoryMaster");

            migrationBuilder.DropColumn(
                name: "stationCategoryId",
                table: "StationMasters");

            migrationBuilder.DropColumn(
                name: "stationCategoryId",
                table: "categoryMaster");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "stationCategoryId",
                table: "StationMasters",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "stationCategoryId",
                table: "categoryMaster",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StationMasters_stationCategoryId",
                table: "StationMasters",
                column: "stationCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_categoryMaster_stationCategoryId",
                table: "categoryMaster",
                column: "stationCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_categoryMaster_stationCategories_stationCategoryId",
                table: "categoryMaster",
                column: "stationCategoryId",
                principalTable: "stationCategories",
                principalColumn: "stationCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_StationMasters_stationCategories_stationCategoryId",
                table: "StationMasters",
                column: "stationCategoryId",
                principalTable: "stationCategories",
                principalColumn: "stationCategoryId");
        }
    }
}
