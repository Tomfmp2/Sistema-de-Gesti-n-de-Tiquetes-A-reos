using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sistema_gestor_de_tiquetes_aereos.Migrations
{
    /// <inheritdoc />
    public partial class CreateBaggage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "baggage",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    checkin_id = table.Column<int>(type: "int", nullable: false),
                    baggage_type_id = table.Column<int>(type: "int", nullable: false),
                    weight_kg = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    charged_price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_baggage", x => x.id);
                    table.ForeignKey(
                        name: "FK_baggage_checkins_checkin_id",
                        column: x => x.checkin_id,
                        principalTable: "checkins",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_baggage_baggage_types_baggage_type_id",
                        column: x => x.baggage_type_id,
                        principalTable: "baggage_types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_baggage_baggage_type_id",
                table: "baggage",
                column: "baggage_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_baggage_checkin_id",
                table: "baggage",
                column: "checkin_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "baggage");
        }
    }
}
