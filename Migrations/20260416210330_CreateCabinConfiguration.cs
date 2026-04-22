using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sistema_gestor_de_tiquetes_aereos.Migrations
{
    /// <inheritdoc />
    public partial class CreateCabinConfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "cabin_configuration",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    aircraft_id = table.Column<int>(type: "int", nullable: false),
                    cabin_type_id = table.Column<int>(type: "int", nullable: false),
                    start_row = table.Column<int>(type: "int", nullable: false),
                    end_row = table.Column<int>(type: "int", nullable: false),
                    seats_per_row = table.Column<int>(type: "int", nullable: false),
                    seat_letters = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cabin_configuration", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_cabin_configuration_aircraft_id_cabin_type_id",
                table: "cabin_configuration",
                columns: new[] { "aircraft_id", "cabin_type_id" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cabin_configuration");
        }
    }
}
