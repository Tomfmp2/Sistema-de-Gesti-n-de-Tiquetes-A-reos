using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sistema_gestor_de_tiquetes_aereos.Migrations
{
    /// <inheritdoc />
    public partial class UpdateExistingRouteMiles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("UPDATE routes SET Miles = distance_km * 10 WHERE distance_km IS NOT NULL AND (Miles = 0 OR Miles IS NULL);");
            migrationBuilder.Sql("UPDATE routes SET Miles = 5000 WHERE distance_km IS NULL AND (Miles = 0 OR Miles IS NULL);");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
