using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace puma.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "stationCategories",
                columns: table => new
                {
                    stationCategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StationId = table.Column<int>(type: "int", nullable: false),
                    categoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_stationCategories", x => x.stationCategoryId);
                });

            migrationBuilder.CreateTable(
                name: "categoryMaster",
                columns: table => new
                {
                    categoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    categoryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    isdisabled = table.Column<bool>(type: "bit", nullable: false),
                    stationCategoryId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categoryMaster", x => x.categoryId);
                    table.ForeignKey(
                        name: "FK_categoryMaster_stationCategories_stationCategoryId",
                        column: x => x.stationCategoryId,
                        principalTable: "stationCategories",
                        principalColumn: "stationCategoryId");
                });

            migrationBuilder.CreateTable(
                name: "stationMaster",
                columns: table => new
                {
                    StationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    area = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    city = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    latitude = table.Column<double>(type: "float", nullable: false),
                    longtitude = table.Column<double>(type: "float", nullable: false),
                    stationCategoryId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_stationMaster", x => x.StationId);
                    table.ForeignKey(
                        name: "FK_stationMaster_stationCategories_stationCategoryId",
                        column: x => x.stationCategoryId,
                        principalTable: "stationCategories",
                        principalColumn: "stationCategoryId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_categoryMaster_stationCategoryId",
                table: "categoryMaster",
                column: "stationCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_stationMaster_stationCategoryId",
                table: "stationMaster",
                column: "stationCategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "categoryMaster");

            migrationBuilder.DropTable(
                name: "stationMaster");

            migrationBuilder.DropTable(
                name: "stationCategories");
        }
    }
}
