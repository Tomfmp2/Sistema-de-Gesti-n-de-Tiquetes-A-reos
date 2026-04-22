using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sistema_gestor_de_tiquetes_aereos.Migrations
{
    /// <summary>
    /// Alinea el snapshot del modelo EF con el código (entidades / configuraciones) sin emitir SQL.
    /// El esquema en BD lo definen las migraciones previas; esto quita el aviso "pending model changes".
    /// </summary>
    public partial class SyncModelSnapshotWithCodebase : Migration
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
