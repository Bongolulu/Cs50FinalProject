using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace finance.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Benutzer",
                columns: table => new
                {
                    BenutzerId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    PasswortHash = table.Column<string>(type: "text", nullable: false),
                    Bargeld = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Benutzer", x => x.BenutzerId);
                });

            migrationBuilder.CreateTable(
                name: "Transaktionen",
                columns: table => new
                {
                    TransaktionId = table.Column<Guid>(type: "uuid", nullable: false),
                    BenutzerId = table.Column<Guid>(type: "uuid", nullable: false),
                    Symbol = table.Column<string>(type: "text", nullable: false),
                    Anzahl = table.Column<int>(type: "integer", nullable: false),
                    Preis = table.Column<decimal>(type: "numeric", nullable: false),
                    DatumZeit = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaktionen", x => x.TransaktionId);
                    table.ForeignKey(
                        name: "FK_Transaktionen_Benutzer_BenutzerId",
                        column: x => x.BenutzerId,
                        principalTable: "Benutzer",
                        principalColumn: "BenutzerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transaktionen_BenutzerId",
                table: "Transaktionen",
                column: "BenutzerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transaktionen");

            migrationBuilder.DropTable(
                name: "Benutzer");
        }
    }
}
