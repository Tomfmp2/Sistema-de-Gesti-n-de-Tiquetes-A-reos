using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace sistema_gestor_de_tiquetes_aereos.Migrations
{
    /// <inheritdoc />
    public partial class IntialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase().Annotation("MySql:CharSet", "utf8mb4");

            // Crear tablas de forma idempotente para evitar errores si ya existen
            migrationBuilder.Sql("""
                CREATE TABLE IF NOT EXISTS `continents` (
                    `id` int NOT NULL AUTO_INCREMENT,
                    `name` varchar(50) CHARACTER SET utf8mb4 NOT NULL,
                    CONSTRAINT `PK_continents` PRIMARY KEY (`id`)
                ) CHARACTER SET=utf8mb4;
                """);

            migrationBuilder.Sql("""
                CREATE TABLE IF NOT EXISTS `countries` (
                    `id` int NOT NULL AUTO_INCREMENT,
                    `name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
                    `code_iso` varchar(3) CHARACTER SET utf8mb4 NOT NULL,
                    `continent_id` int NOT NULL,
                    CONSTRAINT `PK_countries` PRIMARY KEY (`id`),
                    CONSTRAINT `FK_countries_continents_continent_id` FOREIGN KEY (`continent_id`) REFERENCES `continents` (`id`) ON DELETE RESTRICT
                ) CHARACTER SET=utf8mb4;
                """);

            // Insertar datos solo si no existen (usando INSERT IGNORE para evitar duplicados)
            migrationBuilder.Sql("""
                INSERT IGNORE INTO `continents` (`id`, `name`) VALUES
                (1, 'América'),
                (2, 'Europa'),
                (3, 'Asia'),
                (4, 'África'),
                (5, 'Oceanía');
                """);

            migrationBuilder.Sql("""
                SET @idx_exists = (
                    SELECT COUNT(1)
                    FROM information_schema.STATISTICS
                    WHERE TABLE_SCHEMA = DATABASE()
                      AND TABLE_NAME = 'countries'
                      AND INDEX_NAME = 'IX_countries_continent_id'
                );
                SET @sql = IF(
                    @idx_exists = 0,
                    'CREATE INDEX `IX_countries_continent_id` ON `countries` (`continent_id`)',
                    'SELECT 1'
                );
                PREPARE stmt FROM @sql;
                EXECUTE stmt;
                DEALLOCATE PREPARE stmt;
                """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "countries");

            migrationBuilder.DropTable(name: "continents");
        }
    }
}
