using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddForginKeyToPublicHoliday : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_publicHolidays_GeneralSettings_GeneralSettingsId",
                table: "publicHolidays");

            migrationBuilder.AlterColumn<int>(
                name: "GeneralSettingsId",
                table: "publicHolidays",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_publicHolidays_GeneralSettings_GeneralSettingsId",
                table: "publicHolidays",
                column: "GeneralSettingsId",
                principalTable: "GeneralSettings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_publicHolidays_GeneralSettings_GeneralSettingsId",
                table: "publicHolidays");

            migrationBuilder.AlterColumn<int>(
                name: "GeneralSettingsId",
                table: "publicHolidays",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_publicHolidays_GeneralSettings_GeneralSettingsId",
                table: "publicHolidays",
                column: "GeneralSettingsId",
                principalTable: "GeneralSettings",
                principalColumn: "Id");
        }
    }
}
