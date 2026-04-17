using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sistema_gestor_de_tiquetes_aereos.Migrations
{
    /// <inheritdoc />
    public partial class CreateFaresAndFlightStatusesTransitions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // flight_statuses ya la crea 20260416215013_CreateFlightStatuses; aquí solo fares + transiciones.
            migrationBuilder.Sql(
                """
                CREATE TABLE IF NOT EXISTS `fares` (
                    `id` int NOT NULL AUTO_INCREMENT,
                    `route_id` int NOT NULL,
                    `cabin_type_id` int NOT NULL,
                    `passenger_type_id` int NOT NULL,
                    `season_id` int NOT NULL,
                    `base_price` decimal(18,2) NOT NULL,
                    `valid_from` date NULL,
                    `valid_to` date NULL,
                    CONSTRAINT `PK_fares` PRIMARY KEY (`id`)
                ) CHARACTER SET=utf8mb4;

                CREATE TABLE IF NOT EXISTS `flight_status_transitions` (
                    `id` int NOT NULL AUTO_INCREMENT,
                    `origin_status_id` int NOT NULL,
                    `destination_status_id` int NOT NULL,
                    CONSTRAINT `PK_flight_status_transitions` PRIMARY KEY (`id`)
                ) CHARACTER SET=utf8mb4;
                """
            );

            migrationBuilder.Sql(
                """
                SET @fk := (
                  SELECT CONSTRAINT_NAME FROM information_schema.TABLE_CONSTRAINTS
                  WHERE CONSTRAINT_SCHEMA = DATABASE()
                    AND TABLE_NAME = 'flight_status_transitions'
                    AND CONSTRAINT_NAME = 'FK_flight_status_transitions_flight_statuses_destination_status~'
                    AND CONSTRAINT_TYPE = 'FOREIGN KEY'
                  LIMIT 1);
                SET @stmt := IF(@fk IS NULL,
                  'ALTER TABLE `flight_status_transitions` ADD CONSTRAINT `FK_flight_status_transitions_flight_statuses_destination_status~` FOREIGN KEY (`destination_status_id`) REFERENCES `flight_statuses` (`id`) ON DELETE RESTRICT',
                  'SELECT 1');
                PREPARE p FROM @stmt; EXECUTE p; DEALLOCATE PREPARE p;

                SET @fk := (
                  SELECT CONSTRAINT_NAME FROM information_schema.TABLE_CONSTRAINTS
                  WHERE CONSTRAINT_SCHEMA = DATABASE()
                    AND TABLE_NAME = 'flight_status_transitions'
                    AND CONSTRAINT_NAME = 'FK_flight_status_transitions_flight_statuses_origin_status_id'
                    AND CONSTRAINT_TYPE = 'FOREIGN KEY'
                  LIMIT 1);
                SET @stmt := IF(@fk IS NULL,
                  'ALTER TABLE `flight_status_transitions` ADD CONSTRAINT `FK_flight_status_transitions_flight_statuses_origin_status_id` FOREIGN KEY (`origin_status_id`) REFERENCES `flight_statuses` (`id`) ON DELETE RESTRICT',
                  'SELECT 1');
                PREPARE p FROM @stmt; EXECUTE p; DEALLOCATE PREPARE p;
                """
            );

            migrationBuilder.Sql(
                """
                SET @exists := (SELECT COUNT(*) FROM information_schema.STATISTICS WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'flight_status_transitions' AND INDEX_NAME = 'IX_flight_status_transitions_destination_status_id');
                SET @stmt := IF(@exists = 0, 'CREATE INDEX `IX_flight_status_transitions_destination_status_id` ON `flight_status_transitions` (`destination_status_id`)', 'SELECT 1');
                PREPARE p FROM @stmt; EXECUTE p; DEALLOCATE PREPARE p;

                SET @exists := (SELECT COUNT(*) FROM information_schema.STATISTICS WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'flight_status_transitions' AND INDEX_NAME = 'IX_flight_status_transitions_origin_status_id_destination_statu~');
                SET @stmt := IF(@exists = 0, 'CREATE UNIQUE INDEX `IX_flight_status_transitions_origin_status_id_destination_statu~` ON `flight_status_transitions` (`origin_status_id`, `destination_status_id`)', 'SELECT 1');
                PREPARE p FROM @stmt; EXECUTE p; DEALLOCATE PREPARE p;

                SET @exists := (SELECT COUNT(*) FROM information_schema.STATISTICS WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'flight_statuses' AND INDEX_NAME = 'IX_flight_statuses_name');
                SET @stmt := IF(@exists = 0, 'CREATE UNIQUE INDEX `IX_flight_statuses_name` ON `flight_statuses` (`name`)', 'SELECT 1');
                PREPARE p FROM @stmt; EXECUTE p; DEALLOCATE PREPARE p;
                """
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "fares");

            migrationBuilder.DropTable(
                name: "flight_status_transitions");
        }
    }
}
