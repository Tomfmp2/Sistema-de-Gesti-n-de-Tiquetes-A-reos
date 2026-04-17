using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sistema_gestor_de_tiquetes_aereos.Migrations
{
    /// <inheritdoc />
    public partial class CreateFaresAndFlightStatusesTransitions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "fares",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    route_id = table.Column<int>(type: "int", nullable: false),
                    cabin_type_id = table.Column<int>(type: "int", nullable: false),
                    passenger_type_id = table.Column<int>(type: "int", nullable: false),
                    season_id = table.Column<int>(type: "int", nullable: false),
                    base_price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    valid_from = table.Column<DateTime>(type: "date", nullable: true),
                    valid_to = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fares", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "flight_statuses",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(50)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_flight_statuses", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "flight_status_transitions",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    origin_status_id = table.Column<int>(type: "int", nullable: false),
                    destination_status_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_flight_status_transitions", x => x.id);
                    table.ForeignKey(
                        name: "FK_flight_status_transitions_flight_statuses_destination_status~",
                        column: x => x.destination_status_id,
                        principalTable: "flight_statuses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_flight_status_transitions_flight_statuses_origin_status_id",
                        column: x => x.origin_status_id,
                        principalTable: "flight_statuses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_flight_status_transitions_destination_status_id",
                table: "flight_status_transitions",
                column: "destination_status_id");

            migrationBuilder.CreateIndex(
                name: "IX_flight_status_transitions_origin_status_id_destination_statu~",
                table: "flight_status_transitions",
                columns: new[] { "origin_status_id", "destination_status_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_flight_statuses_name",
                table: "flight_statuses",
                column: "name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "fares");

            migrationBuilder.DropTable(
                name: "flight_status_transitions");

            migrationBuilder.DropTable(
                name: "flight_statuses");
        }
    }
}
