using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sistema_gestor_de_tiquetes_aereos.Migrations
{
    /// <inheritdoc />
    public partial class AddSoftDeleteToGeoCatalogs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "is_active",
                table: "continents",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "is_active",
                table: "countries",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "is_active",
                table: "regions",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "is_active",
                table: "cities",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: true);

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_active",
                table: "continents");

            migrationBuilder.DropColumn(
                name: "is_active",
                table: "countries");

            migrationBuilder.DropColumn(
                name: "is_active",
                table: "regions");

            migrationBuilder.DropColumn(
                name: "is_active",
                table: "cities");

        }
    }
}
