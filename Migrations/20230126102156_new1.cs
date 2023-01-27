using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace puma.Migrations
{
    /// <inheritdoc />
    public partial class new1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_stationMaster_stationCategories_stationCategoryId",
                table: "stationMaster");

            migrationBuilder.DropPrimaryKey(
                name: "PK_stationMaster",
                table: "stationMaster");

            migrationBuilder.RenameTable(
                name: "stationMaster",
                newName: "StationMasters");

            migrationBuilder.RenameIndex(
                name: "IX_stationMaster_stationCategoryId",
                table: "StationMasters",
                newName: "IX_StationMasters_stationCategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StationMasters",
                table: "StationMasters",
                column: "StationId");

            migrationBuilder.AddForeignKey(
                name: "FK_StationMasters_stationCategories_stationCategoryId",
                table: "StationMasters",
                column: "stationCategoryId",
                principalTable: "stationCategories",
                principalColumn: "stationCategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StationMasters_stationCategories_stationCategoryId",
                table: "StationMasters");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StationMasters",
                table: "StationMasters");

            migrationBuilder.RenameTable(
                name: "StationMasters",
                newName: "stationMaster");

            migrationBuilder.RenameIndex(
                name: "IX_StationMasters_stationCategoryId",
                table: "stationMaster",
                newName: "IX_stationMaster_stationCategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_stationMaster",
                table: "stationMaster",
                column: "StationId");

            migrationBuilder.AddForeignKey(
                name: "FK_stationMaster_stationCategories_stationCategoryId",
                table: "stationMaster",
                column: "stationCategoryId",
                principalTable: "stationCategories",
                principalColumn: "stationCategoryId");
        }
    }
}
