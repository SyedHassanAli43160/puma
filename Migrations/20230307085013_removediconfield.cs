using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace puma.Migrations
{
    /// <inheritdoc />
    public partial class removediconfield : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "categoryIcon",
                table: "categoryMaster");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "categoryIcon",
                table: "categoryMaster",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
