using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Data.Migrations
{
    /// <inheritdoc />
    public partial class addingHolidays : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "WeeklyHolidays",
                table: "GeneralSettings",
                newName: "WeeklyHolidays2");

            migrationBuilder.AddColumn<int>(
                name: "WeeklyHolidays1",
                table: "GeneralSettings",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WeeklyHolidays1",
                table: "GeneralSettings");

            migrationBuilder.RenameColumn(
                name: "WeeklyHolidays2",
                table: "GeneralSettings",
                newName: "WeeklyHolidays");
        }
    }
}
