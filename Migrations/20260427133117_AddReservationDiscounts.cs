using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sistema_gestor_de_tiquetes_aereos.Migrations
{
    /// <inheritdoc />
    public partial class AddReservationDiscounts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "DiscountPercentage",
                table: "bookings",
                type: "decimal(65,30)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "OriginalTotalValue",
                table: "bookings",
                type: "decimal(65,30)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiscountPercentage",
                table: "bookings");

            migrationBuilder.DropColumn(
                name: "OriginalTotalValue",
                table: "bookings");
        }
    }
}
