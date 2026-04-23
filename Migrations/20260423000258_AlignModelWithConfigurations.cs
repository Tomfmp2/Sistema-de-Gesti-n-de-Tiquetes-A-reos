using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sistema_gestor_de_tiquetes_aereos.Migrations
{
    /// <summary>
    /// Cierra el aviso de "pending model changes" frente al snapshot anterior: el esquema real ya
    /// coincide (p. ej. <c>seasons</c> con <c>id</c>/<c>name</c> desde <c>CreateSeasons</c>). Sin SQL.
    /// </summary>
    public partial class AlignModelWithConfigurations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}
