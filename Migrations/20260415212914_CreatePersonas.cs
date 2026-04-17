using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sistema_gestor_de_tiquetes_aereos.Migrations
{
    /// <inheritdoc />
    public partial class CreatePersonas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "codigos_telefono",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    codigo = table.Column<string>(type: "varchar(10)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    nombre = table.Column<string>(type: "varchar(100)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_codigos_telefono", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "dominios_email",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    dominio = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dominios_email", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tipos_documento",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nombre = table.Column<string>(type: "varchar(80)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tipos_documento", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tipos_pasajero",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nombre = table.Column<string>(type: "varchar(80)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tipos_pasajero", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "personas",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    tipo_documento_id = table.Column<int>(type: "int", nullable: false),
                    numero_documento = table.Column<string>(type: "varchar(32)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    nombres = table.Column<string>(type: "varchar(100)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    apellidos = table.Column<string>(type: "varchar(100)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    fecha_nacimiento = table.Column<DateTime>(type: "date", nullable: true),
                    genero = table.Column<string>(type: "char(1)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    direccion_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_personas", x => x.id);
                    table.ForeignKey(
                        name: "FK_personas_direcciones_direccion_id",
                        column: x => x.direccion_id,
                        principalTable: "direcciones",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_personas_tipos_documento_tipo_documento_id",
                        column: x => x.tipo_documento_id,
                        principalTable: "tipos_documento",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "clientes",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    persona_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_clientes", x => x.id);
                    table.ForeignKey(
                        name: "FK_clientes_personas_persona_id",
                        column: x => x.persona_id,
                        principalTable: "personas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "pasajeros",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    persona_id = table.Column<int>(type: "int", nullable: false),
                    tipo_pasajero_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pasajeros", x => x.id);
                    table.ForeignKey(
                        name: "FK_pasajeros_personas_persona_id",
                        column: x => x.persona_id,
                        principalTable: "personas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_pasajeros_tipos_pasajero_tipo_pasajero_id",
                        column: x => x.tipo_pasajero_id,
                        principalTable: "tipos_pasajero",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "personas_emails",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    persona_id = table.Column<int>(type: "int", nullable: false),
                    dominio_email_id = table.Column<int>(type: "int", nullable: true),
                    email = table.Column<string>(type: "varchar(320)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_personas_emails", x => x.id);
                    table.ForeignKey(
                        name: "FK_personas_emails_dominios_email_dominio_email_id",
                        column: x => x.dominio_email_id,
                        principalTable: "dominios_email",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_personas_emails_personas_persona_id",
                        column: x => x.persona_id,
                        principalTable: "personas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "personas_telefonos",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    persona_id = table.Column<int>(type: "int", nullable: false),
                    codigo_telefono_id = table.Column<int>(type: "int", nullable: false),
                    numero = table.Column<string>(type: "varchar(20)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    es_principal = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_personas_telefonos", x => x.id);
                    table.ForeignKey(
                        name: "FK_personas_telefonos_codigos_telefono_codigo_telefono_id",
                        column: x => x.codigo_telefono_id,
                        principalTable: "codigos_telefono",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_personas_telefonos_personas_persona_id",
                        column: x => x.persona_id,
                        principalTable: "personas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_clientes_persona_id",
                table: "clientes",
                column: "persona_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_codigos_telefono_codigo",
                table: "codigos_telefono",
                column: "codigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_dominios_email_dominio",
                table: "dominios_email",
                column: "dominio",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_pasajeros_persona_id",
                table: "pasajeros",
                column: "persona_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_pasajeros_tipo_pasajero_id",
                table: "pasajeros",
                column: "tipo_pasajero_id");

            migrationBuilder.CreateIndex(
                name: "IX_personas_direccion_id",
                table: "personas",
                column: "direccion_id");

            migrationBuilder.CreateIndex(
                name: "IX_personas_tipo_documento_id_numero_documento",
                table: "personas",
                columns: new[] { "tipo_documento_id", "numero_documento" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_personas_emails_dominio_email_id",
                table: "personas_emails",
                column: "dominio_email_id");

            migrationBuilder.CreateIndex(
                name: "IX_personas_emails_email",
                table: "personas_emails",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_personas_emails_persona_id",
                table: "personas_emails",
                column: "persona_id");

            migrationBuilder.CreateIndex(
                name: "IX_personas_telefonos_codigo_telefono_id",
                table: "personas_telefonos",
                column: "codigo_telefono_id");

            migrationBuilder.CreateIndex(
                name: "IX_personas_telefonos_persona_id_codigo_telefono_id_numero",
                table: "personas_telefonos",
                columns: new[] { "persona_id", "codigo_telefono_id", "numero" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "clientes");

            migrationBuilder.DropTable(
                name: "pasajeros");

            migrationBuilder.DropTable(
                name: "personas_emails");

            migrationBuilder.DropTable(
                name: "personas_telefonos");

            migrationBuilder.DropTable(
                name: "tipos_pasajero");

            migrationBuilder.DropTable(
                name: "dominios_email");

            migrationBuilder.DropTable(
                name: "codigos_telefono");

            migrationBuilder.DropTable(
                name: "personas");

            migrationBuilder.DropTable(
                name: "tipos_documento");
        }
    }
}
