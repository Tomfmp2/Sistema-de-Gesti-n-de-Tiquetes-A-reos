using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sistema_gestor_de_tiquetes_aereos.Migrations
{
    /// <inheritdoc />
    public partial class CreateFlightsAndRelatedTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Idempotente: tablas pueden existir por migraciones previas mal ordenadas o por reaplicación parcial.
            migrationBuilder.Sql(
                """
                CREATE TABLE IF NOT EXISTS `checkin_statuses` (
                    `id` int NOT NULL AUTO_INCREMENT,
                    `name` varchar(50) CHARACTER SET utf8mb4 NOT NULL,
                    CONSTRAINT `PK_checkin_statuses` PRIMARY KEY (`id`)
                ) CHARACTER SET=utf8mb4;

                CREATE TABLE IF NOT EXISTS `flights` (
                    `id` int NOT NULL AUTO_INCREMENT,
                    `flight_code` varchar(10) CHARACTER SET utf8mb4 NOT NULL,
                    `airline_id` int NOT NULL,
                    `route_id` int NOT NULL,
                    `aircraft_id` int NOT NULL,
                    `departure_date` datetime NOT NULL,
                    `estimated_arrival_date` datetime NOT NULL,
                    `total_capacity` int NOT NULL,
                    `available_seats` int NOT NULL,
                    `flight_status_id` int NOT NULL,
                    `rescheduled_at` datetime NULL,
                    `created_at` datetime NOT NULL,
                    `updated_at` datetime NOT NULL,
                    CONSTRAINT `PK_flights` PRIMARY KEY (`id`)
                ) CHARACTER SET=utf8mb4;

                CREATE TABLE IF NOT EXISTS `tickets` (
                    `id` int NOT NULL AUTO_INCREMENT,
                    `reservation_passenger_id` int NOT NULL,
                    `ticket_code` varchar(30) CHARACTER SET utf8mb4 NOT NULL,
                    `issue_date` datetime NOT NULL,
                    `ticket_status_id` int NOT NULL,
                    `created_at` datetime NOT NULL,
                    `updated_at` datetime NOT NULL,
                    CONSTRAINT `PK_tickets` PRIMARY KEY (`id`)
                ) CHARACTER SET=utf8mb4;

                CREATE TABLE IF NOT EXISTS `checkins` (
                    `id` int NOT NULL AUTO_INCREMENT,
                    `ticket_id` int NOT NULL,
                    `staff_id` int NOT NULL,
                    `flight_seat_id` int NOT NULL,
                    `checkin_date` datetime NOT NULL,
                    `checkin_status_id` int NOT NULL,
                    `boarding_pass_number` varchar(20) CHARACTER SET utf8mb4 NOT NULL,
                    `checked_baggage` tinyint(1) NOT NULL,
                    `baggage_weight_kg` decimal(5,2) NULL,
                    CONSTRAINT `PK_checkins` PRIMARY KEY (`id`)
                ) CHARACTER SET=utf8mb4;
                """
            );

            migrationBuilder.Sql(
                """
                SET @fk := (
                  SELECT CONSTRAINT_NAME FROM information_schema.TABLE_CONSTRAINTS
                  WHERE CONSTRAINT_SCHEMA = DATABASE()
                    AND TABLE_NAME = 'tickets'
                    AND CONSTRAINT_NAME = 'FK_tickets_reservation_passengers_reservation_passenger_id'
                    AND CONSTRAINT_TYPE = 'FOREIGN KEY'
                  LIMIT 1);
                SET @stmt := IF(@fk IS NULL,
                  'ALTER TABLE `tickets` ADD CONSTRAINT `FK_tickets_reservation_passengers_reservation_passenger_id` FOREIGN KEY (`reservation_passenger_id`) REFERENCES `reservation_passengers` (`id`) ON DELETE RESTRICT',
                  'SELECT 1');
                PREPARE p FROM @stmt; EXECUTE p; DEALLOCATE PREPARE p;

                SET @fk := (
                  SELECT CONSTRAINT_NAME FROM information_schema.TABLE_CONSTRAINTS
                  WHERE CONSTRAINT_SCHEMA = DATABASE()
                    AND TABLE_NAME = 'tickets'
                    AND CONSTRAINT_NAME = 'FK_tickets_ticket_statuses_ticket_status_id'
                    AND CONSTRAINT_TYPE = 'FOREIGN KEY'
                  LIMIT 1);
                SET @stmt := IF(@fk IS NULL,
                  'ALTER TABLE `tickets` ADD CONSTRAINT `FK_tickets_ticket_statuses_ticket_status_id` FOREIGN KEY (`ticket_status_id`) REFERENCES `ticket_statuses` (`id`) ON DELETE RESTRICT',
                  'SELECT 1');
                PREPARE p FROM @stmt; EXECUTE p; DEALLOCATE PREPARE p;

                SET @fk := (
                  SELECT CONSTRAINT_NAME FROM information_schema.TABLE_CONSTRAINTS
                  WHERE CONSTRAINT_SCHEMA = DATABASE()
                    AND TABLE_NAME = 'checkins'
                    AND CONSTRAINT_NAME = 'FK_checkins_checkin_statuses_checkin_status_id'
                    AND CONSTRAINT_TYPE = 'FOREIGN KEY'
                  LIMIT 1);
                SET @stmt := IF(@fk IS NULL,
                  'ALTER TABLE `checkins` ADD CONSTRAINT `FK_checkins_checkin_statuses_checkin_status_id` FOREIGN KEY (`checkin_status_id`) REFERENCES `checkin_statuses` (`id`) ON DELETE RESTRICT',
                  'SELECT 1');
                PREPARE p FROM @stmt; EXECUTE p; DEALLOCATE PREPARE p;

                SET @fk := (
                  SELECT CONSTRAINT_NAME FROM information_schema.TABLE_CONSTRAINTS
                  WHERE CONSTRAINT_SCHEMA = DATABASE()
                    AND TABLE_NAME = 'checkins'
                    AND CONSTRAINT_NAME = 'FK_checkins_tickets_ticket_id'
                    AND CONSTRAINT_TYPE = 'FOREIGN KEY'
                  LIMIT 1);
                SET @stmt := IF(@fk IS NULL,
                  'ALTER TABLE `checkins` ADD CONSTRAINT `FK_checkins_tickets_ticket_id` FOREIGN KEY (`ticket_id`) REFERENCES `tickets` (`id`) ON DELETE RESTRICT',
                  'SELECT 1');
                PREPARE p FROM @stmt; EXECUTE p; DEALLOCATE PREPARE p;
                """
            );

            migrationBuilder.Sql(
                """
                SET @exists := (SELECT COUNT(*) FROM information_schema.STATISTICS WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'checkins' AND INDEX_NAME = 'IX_checkins_boarding_pass_number');
                SET @stmt := IF(@exists = 0, 'CREATE UNIQUE INDEX `IX_checkins_boarding_pass_number` ON `checkins` (`boarding_pass_number`)', 'SELECT 1');
                PREPARE p FROM @stmt; EXECUTE p; DEALLOCATE PREPARE p;

                SET @exists := (SELECT COUNT(*) FROM information_schema.STATISTICS WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'checkins' AND INDEX_NAME = 'IX_checkins_checkin_status_id');
                SET @stmt := IF(@exists = 0, 'CREATE INDEX `IX_checkins_checkin_status_id` ON `checkins` (`checkin_status_id`)', 'SELECT 1');
                PREPARE p FROM @stmt; EXECUTE p; DEALLOCATE PREPARE p;

                SET @exists := (SELECT COUNT(*) FROM information_schema.STATISTICS WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'checkins' AND INDEX_NAME = 'IX_checkins_flight_seat_id');
                SET @stmt := IF(@exists = 0, 'CREATE UNIQUE INDEX `IX_checkins_flight_seat_id` ON `checkins` (`flight_seat_id`)', 'SELECT 1');
                PREPARE p FROM @stmt; EXECUTE p; DEALLOCATE PREPARE p;

                SET @exists := (SELECT COUNT(*) FROM information_schema.STATISTICS WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'checkins' AND INDEX_NAME = 'IX_checkins_ticket_id');
                SET @stmt := IF(@exists = 0, 'CREATE UNIQUE INDEX `IX_checkins_ticket_id` ON `checkins` (`ticket_id`)', 'SELECT 1');
                PREPARE p FROM @stmt; EXECUTE p; DEALLOCATE PREPARE p;

                SET @exists := (SELECT COUNT(*) FROM information_schema.STATISTICS WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'tickets' AND INDEX_NAME = 'IX_tickets_reservation_passenger_id');
                SET @stmt := IF(@exists = 0, 'CREATE UNIQUE INDEX `IX_tickets_reservation_passenger_id` ON `tickets` (`reservation_passenger_id`)', 'SELECT 1');
                PREPARE p FROM @stmt; EXECUTE p; DEALLOCATE PREPARE p;

                SET @exists := (SELECT COUNT(*) FROM information_schema.STATISTICS WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'tickets' AND INDEX_NAME = 'IX_tickets_ticket_code');
                SET @stmt := IF(@exists = 0, 'CREATE UNIQUE INDEX `IX_tickets_ticket_code` ON `tickets` (`ticket_code`)', 'SELECT 1');
                PREPARE p FROM @stmt; EXECUTE p; DEALLOCATE PREPARE p;

                SET @exists := (SELECT COUNT(*) FROM information_schema.STATISTICS WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'tickets' AND INDEX_NAME = 'IX_tickets_ticket_status_id');
                SET @stmt := IF(@exists = 0, 'CREATE INDEX `IX_tickets_ticket_status_id` ON `tickets` (`ticket_status_id`)', 'SELECT 1');
                PREPARE p FROM @stmt; EXECUTE p; DEALLOCATE PREPARE p;
                """
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "checkins");

            migrationBuilder.DropTable(
                name: "flights");

            migrationBuilder.DropTable(
                name: "checkin_statuses");

            migrationBuilder.DropTable(
                name: "tickets");
        }
    }
}
