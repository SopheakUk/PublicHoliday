using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PublicHoliday.Migrations
{
    public partial class _1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SupportedCountry",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FromDay = table.Column<int>(type: "int", nullable: false),
                    FromMonth = table.Column<int>(type: "int", nullable: false),
                    FromYear = table.Column<int>(type: "int", nullable: false),
                    ToDay = table.Column<int>(type: "int", nullable: false),
                    ToMonth = table.Column<int>(type: "int", nullable: false),
                    ToYear = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupportedCountry", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HolidayType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SupportedCountryId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HolidayType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HolidayType_SupportedCountry_SupportedCountryId",
                        column: x => x.SupportedCountryId,
                        principalTable: "SupportedCountry",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Region",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SupportedCountryId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Region", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Region_SupportedCountry_SupportedCountryId",
                        column: x => x.SupportedCountryId,
                        principalTable: "SupportedCountry",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_HolidayType_SupportedCountryId",
                table: "HolidayType",
                column: "SupportedCountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Region_SupportedCountryId",
                table: "Region",
                column: "SupportedCountryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HolidayType");

            migrationBuilder.DropTable(
                name: "Region");

            migrationBuilder.DropTable(
                name: "SupportedCountry");
        }
    }
}
