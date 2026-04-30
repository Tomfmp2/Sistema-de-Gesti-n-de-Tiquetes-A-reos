using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sistema_gestor_de_tiquetes_aereos.Migrations
{
    /// <inheritdoc />
    public partial class AddMilesToRoute : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Miles",
                table: "routes",
                type: "decimal(65,30)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Miles",
                table: "routes");
        }
    }
}
