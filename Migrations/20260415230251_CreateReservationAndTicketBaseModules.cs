using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sistema_gestor_de_tiquetes_aereos.Migrations
{
    /// <inheritdoc />
    public partial class CreateReservationAndTicketBaseModules : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "reservation_flights",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    reservation_id = table.Column<int>(type: "int", nullable: false),
                    flight_id = table.Column<int>(type: "int", nullable: false),
                    partial_value = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reservation_flights", x => x.id);
                    table.ForeignKey(
                        name: "FK_reservation_flights_reservations_reservation_id",
                        column: x => x.reservation_id,
                        principalTable: "reservations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ticket_statuses",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(50)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ticket_statuses", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "reservation_passengers",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    reservation_flight_id = table.Column<int>(type: "int", nullable: false),
                    passenger_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reservation_passengers", x => x.id);
                    table.ForeignKey(
                        name: "FK_reservation_passengers_reservation_flights_reservation_fligh~",
                        column: x => x.reservation_flight_id,
                        principalTable: "reservation_flights",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_reservation_flights_reservation_id_flight_id",
                table: "reservation_flights",
                columns: new[] { "reservation_id", "flight_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_reservation_passengers_reservation_flight_id_passenger_id",
                table: "reservation_passengers",
                columns: new[] { "reservation_flight_id", "passenger_id" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "reservation_passengers");

            migrationBuilder.DropTable(
                name: "ticket_statuses");

            migrationBuilder.DropTable(
                name: "reservation_flights");
        }
    }
}
