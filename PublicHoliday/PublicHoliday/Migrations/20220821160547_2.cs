using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PublicHoliday.Migrations
{
    public partial class _2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MonthlyHoliday",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Day = table.Column<int>(type: "int", nullable: false),
                    Month = table.Column<int>(type: "int", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    DayOfWeek = table.Column<int>(type: "int", nullable: false),
                    HolidayType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonthlyHoliday", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MonthlyHolidayName",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Lang = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MonthlyHolidayId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonthlyHolidayName", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MonthlyHolidayName_MonthlyHoliday_MonthlyHolidayId",
                        column: x => x.MonthlyHolidayId,
                        principalTable: "MonthlyHoliday",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_MonthlyHolidayName_MonthlyHolidayId",
                table: "MonthlyHolidayName",
                column: "MonthlyHolidayId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MonthlyHolidayName");

            migrationBuilder.DropTable(
                name: "MonthlyHoliday");
        }
    }
}
