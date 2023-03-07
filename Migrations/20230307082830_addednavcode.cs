using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace puma.Migrations
{
    /// <inheritdoc />
    public partial class addednavcode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "navCode",
                table: "StationMasters",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "navCode",
                table: "StationMasters");
        }
    }
}
