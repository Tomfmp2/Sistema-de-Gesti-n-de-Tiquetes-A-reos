using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sistema_gestor_de_tiquetes_aereos.Migrations;

/// <inheritdoc />
public partial class Baseline_CurrentDatabase : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        // Baseline migration: intentionally empty.
        // The database already contains the schema; we only want EF to start tracking it.
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        // Baseline migration: intentionally empty.
    }
}

