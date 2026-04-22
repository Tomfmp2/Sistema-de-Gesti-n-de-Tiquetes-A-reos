using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sistema_gestor_de_tiquetes_aereos.Migrations
{
    /// <inheritdoc />
    public partial class CreateReservationStatusTransitions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           
            migrationBuilder.CreateTable(
                name: "reservation_status_transitions",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    origin_status_id = table.Column<int>(type: "int", nullable: false),
                    destination_status_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reservation_status_transitions", x => x.id);
                    table.ForeignKey(
                        name: "FK_reservation_status_transitions_reservation_statuses_destinat~",
                        column: x => x.destination_status_id,
                        principalTable: "reservation_statuses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_reservation_status_transitions_reservation_statuses_origin_s~",
                        column: x => x.origin_status_id,
                        principalTable: "reservation_statuses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_reservation_status_transitions_destination_status_id",
                table: "reservation_status_transitions",
                column: "destination_status_id");

            migrationBuilder.CreateIndex(
                name: "IX_reservation_status_transitions_origin_status_id_destination_~",
                table: "reservation_status_transitions",
                columns: new[] { "origin_status_id", "destination_status_id" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "reservation_status_transitions");

           
        }
    }
}
