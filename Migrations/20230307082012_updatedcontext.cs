using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace puma.Migrations
{
    /// <inheritdoc />
    public partial class updatedcontext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tests");

            migrationBuilder.RenameColumn(
                name: "city",
                table: "StationMasters",
                newName: "regionName");

            migrationBuilder.RenameColumn(
                name: "area",
                table: "StationMasters",
                newName: "provinceName");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "StationMasters",
                newName: "districtName");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "StationMasters",
                newName: "Location");

            migrationBuilder.AddColumn<string>(
                name: "CustomerName",
                table: "StationMasters",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerName",
                table: "StationMasters");

            migrationBuilder.RenameColumn(
                name: "regionName",
                table: "StationMasters",
                newName: "city");

            migrationBuilder.RenameColumn(
                name: "provinceName",
                table: "StationMasters",
                newName: "area");

            migrationBuilder.RenameColumn(
                name: "districtName",
                table: "StationMasters",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Location",
                table: "StationMasters",
                newName: "Description");

            migrationBuilder.CreateTable(
                name: "tests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    latitude = table.Column<double>(type: "float", nullable: false),
                    longitude = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tests", x => x.Id);
                });
        }
    }
}
