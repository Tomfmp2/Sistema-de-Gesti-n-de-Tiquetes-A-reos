using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sistema_gestor_de_tiquetes_aereos.Migrations
{
    /// <inheritdoc />
    public partial class CreateFlightsAndRelatedTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "checkin_statuses",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(50)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_checkin_statuses", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "flights",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    flight_code = table.Column<string>(type: "varchar(10)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    airline_id = table.Column<int>(type: "int", nullable: false),
                    route_id = table.Column<int>(type: "int", nullable: false),
                    aircraft_id = table.Column<int>(type: "int", nullable: false),
                    departure_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    estimated_arrival_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    total_capacity = table.Column<int>(type: "int", nullable: false),
                    available_seats = table.Column<int>(type: "int", nullable: false),
                    flight_status_id = table.Column<int>(type: "int", nullable: false),
                    rescheduled_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_flights", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tickets",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    reservation_passenger_id = table.Column<int>(type: "int", nullable: false),
                    ticket_code = table.Column<string>(type: "varchar(30)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    issue_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    ticket_status_id = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tickets", x => x.id);
                    table.ForeignKey(
                        name: "FK_tickets_reservation_passengers_reservation_passenger_id",
                        column: x => x.reservation_passenger_id,
                        principalTable: "reservation_passengers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tickets_ticket_statuses_ticket_status_id",
                        column: x => x.ticket_status_id,
                        principalTable: "ticket_statuses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "checkins",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ticket_id = table.Column<int>(type: "int", nullable: false),
                    staff_id = table.Column<int>(type: "int", nullable: false),
                    flight_seat_id = table.Column<int>(type: "int", nullable: false),
                    checkin_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    checkin_status_id = table.Column<int>(type: "int", nullable: false),
                    boarding_pass_number = table.Column<string>(type: "varchar(20)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    checked_baggage = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    baggage_weight_kg = table.Column<decimal>(type: "decimal(5,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_checkins", x => x.id);
                    table.ForeignKey(
                        name: "FK_checkins_checkin_statuses_checkin_status_id",
                        column: x => x.checkin_status_id,
                        principalTable: "checkin_statuses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_checkins_tickets_ticket_id",
                        column: x => x.ticket_id,
                        principalTable: "tickets",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_checkins_boarding_pass_number",
                table: "checkins",
                column: "boarding_pass_number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_checkins_checkin_status_id",
                table: "checkins",
                column: "checkin_status_id");

            migrationBuilder.CreateIndex(
                name: "IX_checkins_flight_seat_id",
                table: "checkins",
                column: "flight_seat_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_checkins_ticket_id",
                table: "checkins",
                column: "ticket_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tickets_reservation_passenger_id",
                table: "tickets",
                column: "reservation_passenger_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tickets_ticket_code",
                table: "tickets",
                column: "ticket_code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tickets_ticket_status_id",
                table: "tickets",
                column: "ticket_status_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "checkins");

            migrationBuilder.DropTable(
                name: "flights");

            migrationBuilder.DropTable(
                name: "checkin_statuses");

            migrationBuilder.DropTable(
                name: "tickets");
        }
    }
}
