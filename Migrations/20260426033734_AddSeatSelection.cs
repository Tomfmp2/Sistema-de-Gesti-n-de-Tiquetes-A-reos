using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sistema_gestor_de_tiquetes_aereos.Migrations
{
    /// <inheritdoc />
    public partial class AddSeatSelection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {


            migrationBuilder.AddColumn<string>(
                name: "status",
                table: "flight_seats",
                type: "varchar(20)",
                nullable: false,
                defaultValue: "Disponible")
                .Annotation("MySql:CharSet", "utf8mb4");


            migrationBuilder.AddColumn<int>(
                name: "flight_seat_id",
                table: "booking_passengers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_booking_passengers_flight_seat_id",
                table: "booking_passengers",
                column: "flight_seat_id");

            migrationBuilder.AddForeignKey(
                name: "FK_booking_passengers_flight_seats_flight_seat_id",
                table: "booking_passengers",
                column: "flight_seat_id",
                principalTable: "flight_seats",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_booking_passengers_flight_seats_flight_seat_id",
                table: "booking_passengers");

            migrationBuilder.DropIndex(
                name: "IX_booking_passengers_flight_seat_id",
                table: "booking_passengers");

            migrationBuilder.DropColumn(
                name: "status",
                table: "flight_seats");

            migrationBuilder.DropColumn(
                name: "flight_seat_id",
                table: "booking_passengers");

            migrationBuilder.AddColumn<bool>(
                name: "is_occupied",
                table: "flight_seats",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }
    }
}
