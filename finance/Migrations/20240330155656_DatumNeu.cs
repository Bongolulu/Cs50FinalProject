using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace finance.Migrations
{
    /// <inheritdoc />
    public partial class DatumNeu : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Kommentar",
                table: "Transaktionen");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Kommentar",
                table: "Transaktionen",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
