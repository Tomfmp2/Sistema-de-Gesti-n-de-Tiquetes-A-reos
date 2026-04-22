using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace sistema_gestor_de_tiquetes_aereos.Migrations
{
    /// <inheritdoc />
    public partial class AddForeignKeysOnly : Migration
    {
        /// <summary>
        /// Alinea nombres creados por CreateReservations / CreateReservationAndTicketBaseModules (reservation_*)
        /// con el modelo actual (booking_*). Idempotente: no hace nada si <c>booking_flights</c> ya existe.
        /// </summary>
        private const string AlignLegacyReservationNamesToBookingSql = """
            SET @need_reservation_to_booking = (
                SELECT IF(
                    NOT EXISTS (
                        SELECT 1 FROM information_schema.TABLES
                        WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'booking_flights'
                    )
                    AND EXISTS (
                        SELECT 1 FROM information_schema.TABLES
                        WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'reservation_flights'
                    ),
                    1, 0
                )
            );

            SET @cn = NULL;
            SET @q = 'SELECT 1';

            /* 1) Quitar FKs (orden: hijos primero) */
            SET @cn = IF(@need_reservation_to_booking = 1,
                (SELECT rc.CONSTRAINT_NAME
                 FROM information_schema.REFERENTIAL_CONSTRAINTS rc
                 WHERE rc.CONSTRAINT_SCHEMA = DATABASE() AND rc.TABLE_NAME = 'tickets'
                   AND rc.REFERENCED_TABLE_NAME = 'reservation_passengers' LIMIT 1), NULL);
            SET @q = IF(@cn IS NOT NULL, CONCAT('ALTER TABLE `tickets` DROP FOREIGN KEY `', @cn, '`'), 'SELECT 1');
            PREPARE st FROM @q; EXECUTE st; DEALLOCATE PREPARE st;

            SET @cn = IF(@need_reservation_to_booking = 1,
                (SELECT rc.CONSTRAINT_NAME
                 FROM information_schema.REFERENTIAL_CONSTRAINTS rc
                 WHERE rc.CONSTRAINT_SCHEMA = DATABASE() AND rc.TABLE_NAME = 'invoice_items'
                   AND rc.REFERENCED_TABLE_NAME = 'reservation_passengers' LIMIT 1), NULL);
            SET @q = IF(@cn IS NOT NULL, CONCAT('ALTER TABLE `invoice_items` DROP FOREIGN KEY `', @cn, '`'), 'SELECT 1');
            PREPARE st FROM @q; EXECUTE st; DEALLOCATE PREPARE st;

            SET @cn = IF(@need_reservation_to_booking = 1,
                (SELECT rc.CONSTRAINT_NAME
                 FROM information_schema.REFERENTIAL_CONSTRAINTS rc
                 WHERE rc.CONSTRAINT_SCHEMA = DATABASE() AND rc.TABLE_NAME = 'reservation_passengers'
                   AND rc.REFERENCED_TABLE_NAME = 'reservation_flights' LIMIT 1), NULL);
            SET @q = IF(@cn IS NOT NULL, CONCAT('ALTER TABLE `reservation_passengers` DROP FOREIGN KEY `', @cn, '`'), 'SELECT 1');
            PREPARE st FROM @q; EXECUTE st; DEALLOCATE PREPARE st;

            SET @cn = IF(@need_reservation_to_booking = 1,
                (SELECT rc.CONSTRAINT_NAME
                 FROM information_schema.REFERENTIAL_CONSTRAINTS rc
                 WHERE rc.CONSTRAINT_SCHEMA = DATABASE() AND rc.TABLE_NAME = 'reservation_flights'
                   AND rc.REFERENCED_TABLE_NAME = 'reservations' LIMIT 1), NULL);
            SET @q = IF(@cn IS NOT NULL, CONCAT('ALTER TABLE `reservation_flights` DROP FOREIGN KEY `', @cn, '`'), 'SELECT 1');
            PREPARE st FROM @q; EXECUTE st; DEALLOCATE PREPARE st;

            SET @cn = IF(@need_reservation_to_booking = 1,
                (SELECT rc.CONSTRAINT_NAME
                 FROM information_schema.REFERENTIAL_CONSTRAINTS rc
                 WHERE rc.CONSTRAINT_SCHEMA = DATABASE() AND rc.TABLE_NAME = 'invoices'
                   AND rc.REFERENCED_TABLE_NAME = 'reservations' LIMIT 1), NULL);
            SET @q = IF(@cn IS NOT NULL, CONCAT('ALTER TABLE `invoices` DROP FOREIGN KEY `', @cn, '`'), 'SELECT 1');
            PREPARE st FROM @q; EXECUTE st; DEALLOCATE PREPARE st;

            SET @cn = IF(@need_reservation_to_booking = 1,
                (SELECT rc.CONSTRAINT_NAME
                 FROM information_schema.REFERENTIAL_CONSTRAINTS rc
                 WHERE rc.CONSTRAINT_SCHEMA = DATABASE() AND rc.TABLE_NAME = 'payments'
                   AND rc.REFERENCED_TABLE_NAME = 'reservations' LIMIT 1), NULL);
            SET @q = IF(@cn IS NOT NULL, CONCAT('ALTER TABLE `payments` DROP FOREIGN KEY `', @cn, '`'), 'SELECT 1');
            PREPARE st FROM @q; EXECUTE st; DEALLOCATE PREPARE st;

            SET @cn = IF(@need_reservation_to_booking = 1,
                (SELECT rc.CONSTRAINT_NAME
                 FROM information_schema.REFERENTIAL_CONSTRAINTS rc
                 WHERE rc.CONSTRAINT_SCHEMA = DATABASE() AND rc.TABLE_NAME = 'reservations'
                   AND rc.REFERENCED_TABLE_NAME = 'reservation_statuses' LIMIT 1), NULL);
            SET @q = IF(@cn IS NOT NULL, CONCAT('ALTER TABLE `reservations` DROP FOREIGN KEY `', @cn, '`'), 'SELECT 1');
            PREPARE st FROM @q; EXECUTE st; DEALLOCATE PREPARE st;

            SET @cn = IF(@need_reservation_to_booking = 1,
                (SELECT rc.CONSTRAINT_NAME
                 FROM information_schema.REFERENTIAL_CONSTRAINTS rc
                 WHERE rc.CONSTRAINT_SCHEMA = DATABASE() AND rc.TABLE_NAME = 'reservation_status_transitions'
                   AND rc.REFERENCED_TABLE_NAME = 'reservation_statuses' LIMIT 1), NULL);
            SET @q = IF(@cn IS NOT NULL, CONCAT('ALTER TABLE `reservation_status_transitions` DROP FOREIGN KEY `', @cn, '`'), 'SELECT 1');
            PREPARE st FROM @q; EXECUTE st; DEALLOCATE PREPARE st;

            SET @cn = IF(@need_reservation_to_booking = 1,
                (SELECT rc.CONSTRAINT_NAME
                 FROM information_schema.REFERENTIAL_CONSTRAINTS rc
                 WHERE rc.CONSTRAINT_SCHEMA = DATABASE() AND rc.TABLE_NAME = 'reservation_status_transitions'
                   AND rc.REFERENCED_TABLE_NAME = 'reservation_statuses' LIMIT 1), NULL);
            SET @q = IF(@cn IS NOT NULL, CONCAT('ALTER TABLE `reservation_status_transitions` DROP FOREIGN KEY `', @cn, '`'), 'SELECT 1');
            PREPARE st FROM @q; EXECUTE st; DEALLOCATE PREPARE st;

            /* 2) Tablas de catálogo de estado: reservation_statuses -> booking_statuses */
            SET @q = IF(
                @need_reservation_to_booking = 1
                AND EXISTS (SELECT 1 FROM information_schema.TABLES WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'reservation_statuses')
                AND NOT EXISTS (SELECT 1 FROM information_schema.TABLES WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'booking_statuses'),
                'RENAME TABLE `reservation_statuses` TO `booking_statuses`', 'SELECT 1');
            PREPARE st FROM @q; EXECUTE st; DEALLOCATE PREPARE st;

            /* 3) Transiciones: columnas y nombre de tabla */
            SET @q = IF(
                @need_reservation_to_booking = 1
                AND EXISTS (SELECT 1 FROM information_schema.COLUMNS WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'reservation_status_transitions' AND COLUMN_NAME = 'origin_status_id')
                AND NOT EXISTS (SELECT 1 FROM information_schema.COLUMNS WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'reservation_status_transitions' AND COLUMN_NAME = 'from_status_id'),
                'ALTER TABLE `reservation_status_transitions` RENAME COLUMN `origin_status_id` TO `from_status_id`', 'SELECT 1');
            PREPARE st FROM @q; EXECUTE st; DEALLOCATE PREPARE st;

            SET @q = IF(
                @need_reservation_to_booking = 1
                AND EXISTS (SELECT 1 FROM information_schema.COLUMNS WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'reservation_status_transitions' AND COLUMN_NAME = 'destination_status_id')
                AND NOT EXISTS (SELECT 1 FROM information_schema.COLUMNS WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'reservation_status_transitions' AND COLUMN_NAME = 'to_status_id'),
                'ALTER TABLE `reservation_status_transitions` RENAME COLUMN `destination_status_id` TO `to_status_id`', 'SELECT 1');
            PREPARE st FROM @q; EXECUTE st; DEALLOCATE PREPARE st;

            SET @q = IF(
                @need_reservation_to_booking = 1
                AND EXISTS (SELECT 1 FROM information_schema.TABLES WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'reservation_status_transitions')
                AND NOT EXISTS (SELECT 1 FROM information_schema.TABLES WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'booking_status_transitions'),
                'RENAME TABLE `reservation_status_transitions` TO `booking_status_transitions`', 'SELECT 1');
            PREPARE st FROM @q; EXECUTE st; DEALLOCATE PREPARE st;

            /* 4) Reservas -> bookings + columnas */
            SET @q = IF(
                @need_reservation_to_booking = 1
                AND EXISTS (SELECT 1 FROM information_schema.TABLES WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'reservations')
                AND NOT EXISTS (SELECT 1 FROM information_schema.TABLES WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'bookings'),
                'RENAME TABLE `reservations` TO `bookings`', 'SELECT 1');
            PREPARE st FROM @q; EXECUTE st; DEALLOCATE PREPARE st;

            SET @q = IF(
                @need_reservation_to_booking = 1
                AND EXISTS (SELECT 1 FROM information_schema.COLUMNS WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'bookings' AND COLUMN_NAME = 'reservation_status_id'),
                'ALTER TABLE `bookings` RENAME COLUMN `reservation_status_id` TO `booking_status_id`', 'SELECT 1');
            PREPARE st FROM @q; EXECUTE st; DEALLOCATE PREPARE st;

            SET @q = IF(
                @need_reservation_to_booking = 1
                AND EXISTS (SELECT 1 FROM information_schema.COLUMNS WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'bookings' AND COLUMN_NAME = 'reservation_date'),
                'ALTER TABLE `bookings` RENAME COLUMN `reservation_date` TO `booked_at`', 'SELECT 1');
            PREPARE st FROM @q; EXECUTE st; DEALLOCATE PREPARE st;

            SET @q = IF(
                @need_reservation_to_booking = 1
                AND EXISTS (SELECT 1 FROM information_schema.COLUMNS WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'bookings' AND COLUMN_NAME = 'total_value'),
                'ALTER TABLE `bookings` RENAME COLUMN `total_value` TO `total_amount`', 'SELECT 1');
            PREPARE st FROM @q; EXECUTE st; DEALLOCATE PREPARE st;

            /* 5) Vuelos de reserva */
            SET @q = IF(
                @need_reservation_to_booking = 1
                AND EXISTS (SELECT 1 FROM information_schema.TABLES WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'reservation_flights')
                AND NOT EXISTS (SELECT 1 FROM information_schema.TABLES WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'booking_flights'),
                'RENAME TABLE `reservation_flights` TO `booking_flights`', 'SELECT 1');
            PREPARE st FROM @q; EXECUTE st; DEALLOCATE PREPARE st;

            SET @q = IF(
                @need_reservation_to_booking = 1
                AND EXISTS (SELECT 1 FROM information_schema.COLUMNS WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'booking_flights' AND COLUMN_NAME = 'reservation_id'),
                'ALTER TABLE `booking_flights` RENAME COLUMN `reservation_id` TO `booking_id`', 'SELECT 1');
            PREPARE st FROM @q; EXECUTE st; DEALLOCATE PREPARE st;

            SET @q = IF(
                @need_reservation_to_booking = 1
                AND EXISTS (SELECT 1 FROM information_schema.COLUMNS WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'booking_flights' AND COLUMN_NAME = 'partial_value'),
                'ALTER TABLE `booking_flights` RENAME COLUMN `partial_value` TO `partial_amount`', 'SELECT 1');
            PREPARE st FROM @q; EXECUTE st; DEALLOCATE PREPARE st;

            /* 6) Pasajeros de reserva */
            SET @q = IF(
                @need_reservation_to_booking = 1
                AND EXISTS (SELECT 1 FROM information_schema.TABLES WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'reservation_passengers')
                AND NOT EXISTS (SELECT 1 FROM information_schema.TABLES WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'booking_passengers'),
                'RENAME TABLE `reservation_passengers` TO `booking_passengers`', 'SELECT 1');
            PREPARE st FROM @q; EXECUTE st; DEALLOCATE PREPARE st;

            SET @q = IF(
                @need_reservation_to_booking = 1
                AND EXISTS (SELECT 1 FROM information_schema.COLUMNS WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'booking_passengers' AND COLUMN_NAME = 'reservation_flight_id'),
                'ALTER TABLE `booking_passengers` RENAME COLUMN `reservation_flight_id` TO `booking_flight_id`', 'SELECT 1');
            PREPARE st FROM @q; EXECUTE st; DEALLOCATE PREPARE st;

            /* 7) facturas, pagos, tiquetes, items */
            SET @q = IF(
                @need_reservation_to_booking = 1
                AND EXISTS (SELECT 1 FROM information_schema.COLUMNS WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'invoices' AND COLUMN_NAME = 'reservation_id'),
                'ALTER TABLE `invoices` RENAME COLUMN `reservation_id` TO `booking_id`', 'SELECT 1');
            PREPARE st FROM @q; EXECUTE st; DEALLOCATE PREPARE st;

            SET @q = IF(
                @need_reservation_to_booking = 1
                AND EXISTS (SELECT 1 FROM information_schema.COLUMNS WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'payments' AND COLUMN_NAME = 'reservation_id'),
                'ALTER TABLE `payments` RENAME COLUMN `reservation_id` TO `booking_id`', 'SELECT 1');
            PREPARE st FROM @q; EXECUTE st; DEALLOCATE PREPARE st;

            SET @q = IF(
                @need_reservation_to_booking = 1
                AND EXISTS (SELECT 1 FROM information_schema.COLUMNS WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'tickets' AND COLUMN_NAME = 'reservation_passenger_id')
                AND NOT EXISTS (SELECT 1 FROM information_schema.COLUMNS WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'tickets' AND COLUMN_NAME = 'booking_passenger_id'),
                'ALTER TABLE `tickets` RENAME COLUMN `reservation_passenger_id` TO `booking_passenger_id`', 'SELECT 1');
            PREPARE st FROM @q; EXECUTE st; DEALLOCATE PREPARE st;

            SET @q = IF(
                @need_reservation_to_booking = 1
                AND EXISTS (SELECT 1 FROM information_schema.COLUMNS WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'tickets' AND COLUMN_NAME = 'issue_date')
                AND NOT EXISTS (SELECT 1 FROM information_schema.COLUMNS WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'tickets' AND COLUMN_NAME = 'issued_at'),
                'ALTER TABLE `tickets` RENAME COLUMN `issue_date` TO `issued_at`', 'SELECT 1');
            PREPARE st FROM @q; EXECUTE st; DEALLOCATE PREPARE st;

            SET @q = IF(
                @need_reservation_to_booking = 1
                AND EXISTS (SELECT 1 FROM information_schema.COLUMNS WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'invoice_items' AND COLUMN_NAME = 'reservation_passenger_id')
                AND NOT EXISTS (SELECT 1 FROM information_schema.COLUMNS WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'invoice_items' AND COLUMN_NAME = 'booking_passenger_id'),
                'ALTER TABLE `invoice_items` RENAME COLUMN `reservation_passenger_id` TO `booking_passenger_id`', 'SELECT 1');
            PREPARE st FROM @q; EXECUTE st; DEALLOCATE PREPARE st;
            """;

        /// <summary>
        /// <c>CreateFlightsAndRelatedTables</c> crea la tabla <c>checkins</c>; el modelo usa <c>check_ins</c> y
        /// <c>checked_in_at</c> (no <c>checkin_date</c>). Idempotente si <c>check_ins</c> ya existe.
        /// </summary>
        private const string AlignCheckinsToCheckInsSql = """
            SET @need_checkins_to_check_ins = (
                SELECT IF(
                    EXISTS (
                        SELECT 1 FROM information_schema.TABLES
                        WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'checkins'
                    )
                    AND NOT EXISTS (
                        SELECT 1 FROM information_schema.TABLES
                        WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'check_ins'
                    ),
                    1, 0
                )
            );

            /* La FK a checkin_statuses se vuelve a crear como FK_check_ins_checkin_statuses_... en AddForeignKeysOnly */
            SET @cn = IF(@need_checkins_to_check_ins = 1,
                (SELECT rc.CONSTRAINT_NAME
                 FROM information_schema.REFERENTIAL_CONSTRAINTS rc
                 WHERE rc.CONSTRAINT_SCHEMA = DATABASE() AND rc.TABLE_NAME = 'checkins'
                   AND rc.REFERENCED_TABLE_NAME = 'checkin_statuses' LIMIT 1), NULL);
            SET @q = IF(@cn IS NOT NULL, CONCAT('ALTER TABLE `checkins` DROP FOREIGN KEY `', @cn, '`'), 'SELECT 1');
            PREPARE st FROM @q; EXECUTE st; DEALLOCATE PREPARE st;

            SET @q = IF(
                @need_checkins_to_check_ins = 1,
                'RENAME TABLE `checkins` TO `check_ins`',
                'SELECT 1'
            );
            PREPARE st FROM @q; EXECUTE st; DEALLOCATE PREPARE st;

            SET @q = IF(
                EXISTS (SELECT 1 FROM information_schema.COLUMNS WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'check_ins' AND COLUMN_NAME = 'checkin_date')
                AND NOT EXISTS (SELECT 1 FROM information_schema.COLUMNS WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'check_ins' AND COLUMN_NAME = 'checked_in_at'),
                'ALTER TABLE `check_ins` RENAME COLUMN `checkin_date` TO `checked_in_at`',
                'SELECT 1'
            );
            PREPARE st FROM @q; EXECUTE st; DEALLOCATE PREPARE st;
            """;

        /// <summary>
        /// <c>RenameDatabaseSchemaToEnglish</c> dejó <c>directions</c> y <c>persons.direction_id</c>.
        /// El modelo usa <c>addresses</c> y <c>address_id</c>. Quitar la FK antigua, renombrar tabla y columna.
        /// </summary>
        private const string AlignDirectionsToAddressesSql = """
            SET @need_dir_to_addr = (
                SELECT IF(
                    EXISTS (SELECT 1 FROM information_schema.COLUMNS WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'persons' AND COLUMN_NAME = 'direction_id')
                    AND NOT EXISTS (SELECT 1 FROM information_schema.COLUMNS WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'persons' AND COLUMN_NAME = 'address_id'),
                    1, 0
                )
            );

            /* Quitar FK persons -> directions o -> addresses (estado intermedio) */
            SET @cn = IF(@need_dir_to_addr = 1,
                (SELECT rc.CONSTRAINT_NAME
                 FROM information_schema.REFERENTIAL_CONSTRAINTS rc
                 WHERE rc.CONSTRAINT_SCHEMA = DATABASE() AND rc.TABLE_NAME = 'persons'
                   AND rc.REFERENCED_TABLE_NAME IN ('directions', 'addresses') LIMIT 1), NULL);
            SET @q = IF(@cn IS NOT NULL, CONCAT('ALTER TABLE `persons` DROP FOREIGN KEY `', @cn, '`'), 'SELECT 1');
            PREPARE st FROM @q; EXECUTE st; DEALLOCATE PREPARE st;

            SET @q = IF(
                @need_dir_to_addr = 1
                AND EXISTS (SELECT 1 FROM information_schema.TABLES WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'directions')
                AND NOT EXISTS (SELECT 1 FROM information_schema.TABLES WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'addresses'),
                'RENAME TABLE `directions` TO `addresses`',
                'SELECT 1'
            );
            PREPARE st FROM @q; EXECUTE st; DEALLOCATE PREPARE st;

            SET @q = IF(
                @need_dir_to_addr = 1
                AND EXISTS (SELECT 1 FROM information_schema.COLUMNS WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'persons' AND COLUMN_NAME = 'direction_id')
                AND NOT EXISTS (SELECT 1 FROM information_schema.COLUMNS WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'persons' AND COLUMN_NAME = 'address_id'),
                'ALTER TABLE `persons` RENAME COLUMN `direction_id` TO `address_id`',
                'SELECT 1'
            );
            PREPARE st FROM @q; EXECUTE st; DEALLOCATE PREPARE st;

            SET @q = IF(
                @need_dir_to_addr = 1
                AND EXISTS (SELECT 1 FROM information_schema.STATISTICS WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'persons' AND INDEX_NAME = 'IX_persons_direction_id')
                AND NOT EXISTS (SELECT 1 FROM information_schema.STATISTICS WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'persons' AND INDEX_NAME = 'IX_persons_address_id'),
                'ALTER TABLE `persons` RENAME INDEX `IX_persons_direction_id` TO `IX_persons_address_id`',
                'SELECT 1'
            );
            PREPARE st FROM @q; EXECUTE st; DEALLOCATE PREPARE st;
            """;

        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Remaining schema alignment needed for FK creation
            migrationBuilder.Sql("""
                SET @legacy_flight_seats_table = (
                    SELECT TABLE_NAME
                    FROM information_schema.TABLES
                    WHERE TABLE_SCHEMA = DATABASE()
                      AND TABLE_NAME IN ('FlightSeats', 'flightseats')
                      AND NOT EXISTS (
                          SELECT 1
                          FROM information_schema.TABLES
                          WHERE TABLE_SCHEMA = DATABASE()
                            AND TABLE_NAME = 'flight_seats'
                      )
                    LIMIT 1
                );
                SET @sql = IF(
                    @legacy_flight_seats_table IS NOT NULL,
                    CONCAT('RENAME TABLE `', @legacy_flight_seats_table, '` TO `flight_seats`'),
                    'SELECT 1'
                );
                PREPARE stmt FROM @sql;
                EXECUTE stmt;
                DEALLOCATE PREPARE stmt;
                """);

            RenameColumnIfExists("flight_seats", "Id", "id");
            RenameColumnIfExists("flight_seats", "FlightId", "flight_id");
            RenameColumnIfExists("flight_seats", "SeatCode", "seat_code");
            RenameColumnIfExists("flight_seats", "CabinTypeId", "cabin_type_id");
            RenameColumnIfExists("flight_seats", "LocationTypeId", "location_type_id");
            RenameColumnIfExists("flight_seats", "IsOccupied", "is_occupied");

            // CreateFlightAssignments creó la tabla como "FlightAssignments" (PascalCase).
            // El modelo actual usa "flight_crew_assignments"; sin este paso, RenameColumn falla en BD nuevas.
            migrationBuilder.Sql("""
                SET @legacy_fca_table = (
                    SELECT TABLE_NAME
                    FROM information_schema.TABLES
                    WHERE TABLE_SCHEMA = DATABASE()
                      AND TABLE_NAME IN ('FlightAssignments', 'flightassignments')
                      AND NOT EXISTS (
                          SELECT 1
                          FROM information_schema.TABLES
                          WHERE TABLE_SCHEMA = DATABASE()
                            AND TABLE_NAME = 'flight_crew_assignments'
                      )
                    LIMIT 1
                );
                SET @sql = IF(
                    @legacy_fca_table IS NOT NULL,
                    CONCAT('RENAME TABLE `', @legacy_fca_table, '` TO `flight_crew_assignments`'),
                    'SELECT 1'
                );
                PREPARE stmt FROM @sql;
                EXECUTE stmt;
                DEALLOCATE PREPARE stmt;
                """);

            RenameColumnIfExists("flight_crew_assignments", "Id", "id");
            RenameColumnIfExists("flight_crew_assignments", "FlightId", "flight_id");
            RenameColumnIfExists("flight_crew_assignments", "StaffId", "staff_id");
            RenameColumnIfExists("flight_crew_assignments", "FlightRoleId", "crew_role_id");

            // CreateCabinConfiguration (20260416210330) crea la tabla como "cabin_configuration" (singular).
            // El modelo y FKs usan "cabin_configurations" (plural) con columnas row_start/row_end.
            migrationBuilder.Sql("""
                SET @legacy_cc_table = (
                    SELECT TABLE_NAME
                    FROM information_schema.TABLES
                    WHERE TABLE_SCHEMA = DATABASE()
                      AND LOWER(TABLE_NAME) = 'cabin_configuration'
                      AND NOT EXISTS (
                          SELECT 1
                          FROM information_schema.TABLES
                          WHERE TABLE_SCHEMA = DATABASE()
                            AND TABLE_NAME = 'cabin_configurations'
                      )
                    LIMIT 1
                );
                SET @sql = IF(
                    @legacy_cc_table IS NOT NULL,
                    CONCAT('RENAME TABLE `', @legacy_cc_table, '` TO `cabin_configurations`'),
                    'SELECT 1'
                );
                PREPARE stmt FROM @sql;
                EXECUTE stmt;
                DEALLOCATE PREPARE stmt;
                """);

            migrationBuilder.CreateTable(
                name: "flight_crew_roles",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(100)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_flight_crew_roles", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            RenameColumnIfExists("flights", "departure_date", "departure_at");
            RenameColumnIfExists("flights", "estimated_arrival_date", "estimated_arrival_at");

            RenameColumnIfExists("cabin_configurations", "start_row", "row_start");
            RenameColumnIfExists("cabin_configurations", "end_row", "row_end");

            // CreateFaresAndFlightStatusesTransitions ya añadió FKs sobre origin_/destination_status_id.
            // Hay que quitarlas antes de renombrar columnas y volver a crear las FK con nombres del modelo.
            migrationBuilder.Sql("""
                SET @fk1 = (
                    SELECT k.CONSTRAINT_NAME
                    FROM information_schema.KEY_COLUMN_USAGE k
                    INNER JOIN information_schema.REFERENTIAL_CONSTRAINTS r
                        ON k.CONSTRAINT_SCHEMA = r.CONSTRAINT_SCHEMA
                        AND k.CONSTRAINT_NAME = r.CONSTRAINT_NAME
                    WHERE k.TABLE_SCHEMA = DATABASE()
                      AND k.TABLE_NAME = 'flight_status_transitions'
                      AND k.COLUMN_NAME = 'origin_status_id'
                      AND r.REFERENCED_TABLE_NAME = 'flight_statuses'
                    LIMIT 1
                );
                SET @q1 = IF(@fk1 IS NOT NULL,
                    CONCAT('ALTER TABLE `flight_status_transitions` DROP FOREIGN KEY `', @fk1, '`'),
                    'SELECT 1'
                );
                PREPARE s1 FROM @q1;
                EXECUTE s1;
                DEALLOCATE PREPARE s1;

                SET @fk2 = (
                    SELECT k.CONSTRAINT_NAME
                    FROM information_schema.KEY_COLUMN_USAGE k
                    INNER JOIN information_schema.REFERENTIAL_CONSTRAINTS r
                        ON k.CONSTRAINT_SCHEMA = r.CONSTRAINT_SCHEMA
                        AND k.CONSTRAINT_NAME = r.CONSTRAINT_NAME
                    WHERE k.TABLE_SCHEMA = DATABASE()
                      AND k.TABLE_NAME = 'flight_status_transitions'
                      AND k.COLUMN_NAME = 'destination_status_id'
                      AND r.REFERENCED_TABLE_NAME = 'flight_statuses'
                    LIMIT 1
                );
                SET @q2 = IF(@fk2 IS NOT NULL,
                    CONCAT('ALTER TABLE `flight_status_transitions` DROP FOREIGN KEY `', @fk2, '`'),
                    'SELECT 1'
                );
                PREPARE s2 FROM @q2;
                EXECUTE s2;
                DEALLOCATE PREPARE s2;
                """);

            RenameColumnIfExists("flight_status_transitions", "origin_status_id", "from_status_id");
            RenameColumnIfExists("flight_status_transitions", "destination_status_id", "to_status_id");

            // Las migraciones antiguas (CreateReservations, etc.) usan nombres reservation_*. El modelo y este archivo usan booking_*.
            // Sin este bloque, en una BD nueva no existen booking_flights, bookings, etc.
            migrationBuilder.Sql(AlignLegacyReservationNamesToBookingSql);
            migrationBuilder.Sql(AlignCheckinsToCheckInsSql);
            migrationBuilder.Sql(AlignDirectionsToAddressesSql);

            // Note: some base FKs already exist in the DB (e.g., addresses -> cities/street_types, aircraft -> models/airlines, etc.)

            migrationBuilder.AddForeignKey("FK_booking_flights_bookings_booking_id", "booking_flights", "booking_id", "bookings", principalColumn: "id", onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey("FK_booking_flights_flights_flight_id", "booking_flights", "flight_id", "flights", principalColumn: "id", onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey("FK_booking_passengers_booking_flights_booking_flight_id", "booking_passengers", "booking_flight_id", "booking_flights", principalColumn: "id", onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey("FK_booking_passengers_passengers_passenger_id", "booking_passengers", "passenger_id", "passengers", principalColumn: "id", onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey("FK_booking_status_transitions_booking_statuses_from_status_id", "booking_status_transitions", "from_status_id", "booking_statuses", principalColumn: "id", onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey("FK_booking_status_transitions_booking_statuses_to_status_id", "booking_status_transitions", "to_status_id", "booking_statuses", principalColumn: "id", onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey("FK_bookings_booking_statuses_booking_status_id", "bookings", "booking_status_id", "booking_statuses", principalColumn: "id", onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey("FK_bookings_clients_client_id", "bookings", "client_id", "clients", principalColumn: "id", onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey("FK_cabin_configurations_aircraft_aircraft_id", "cabin_configurations", "aircraft_id", "aircraft", principalColumn: "id", onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey("FK_cabin_configurations_cabin_types_cabin_type_id", "cabin_configurations", "cabin_type_id", "cabin_types", principalColumn: "id", onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey("FK_check_ins_checkin_statuses_checkin_status_id", "check_ins", "checkin_status_id", "checkin_statuses", principalColumn: "id", onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey("FK_check_ins_flight_seats_flight_seat_id", "check_ins", "flight_seat_id", "flight_seats", principalColumn: "id", onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey("FK_check_ins_staff_staff_id", "check_ins", "staff_id", "staff", principalColumn: "id", onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey("FK_fares_cabin_types_cabin_type_id", "fares", "cabin_type_id", "cabin_types", principalColumn: "id", onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey("FK_fares_passenger_types_passenger_type_id", "fares", "passenger_type_id", "passenger_types", principalColumn: "id", onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey("FK_fares_routes_route_id", "fares", "route_id", "routes", principalColumn: "id", onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey("FK_fares_seasons_season_id", "fares", "season_id", "seasons", principalColumn: "id", onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey("FK_flight_crew_assignments_flight_crew_roles_crew_role_id", "flight_crew_assignments", "crew_role_id", "flight_crew_roles", principalColumn: "id", onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey("FK_flight_crew_assignments_flights_flight_id", "flight_crew_assignments", "flight_id", "flights", principalColumn: "id", onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey("FK_flight_crew_assignments_staff_staff_id", "flight_crew_assignments", "staff_id", "staff", principalColumn: "id", onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey("FK_flight_seats_cabin_types_cabin_type_id", "flight_seats", "cabin_type_id", "cabin_types", principalColumn: "id", onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey("FK_flight_seats_flights_flight_id", "flight_seats", "flight_id", "flights", principalColumn: "id", onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey("FK_flight_seats_seat_location_types_location_type_id", "flight_seats", "location_type_id", "seat_location_types", principalColumn: "id", onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey("FK_flight_status_transitions_flight_statuses_from_status_id", "flight_status_transitions", "from_status_id", "flight_statuses", principalColumn: "id", onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey("FK_flight_status_transitions_flight_statuses_to_status_id", "flight_status_transitions", "to_status_id", "flight_statuses", principalColumn: "id", onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey("FK_flights_aircraft_aircraft_id", "flights", "aircraft_id", "aircraft", principalColumn: "id", onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey("FK_flights_airlines_airline_id", "flights", "airline_id", "airlines", principalColumn: "id", onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey("FK_flights_flight_statuses_flight_status_id", "flights", "flight_status_id", "flight_statuses", principalColumn: "id", onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey("FK_flights_routes_route_id", "flights", "route_id", "routes", principalColumn: "id", onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey("FK_invoice_items_booking_passengers_booking_passenger_id", "invoice_items", "booking_passenger_id", "booking_passengers", principalColumn: "id", onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey("FK_invoices_bookings_booking_id", "invoices", "booking_id", "bookings", principalColumn: "id", onDelete: ReferentialAction.Restrict);

            // payment_method_id y payment_status_id: ya creadas en CreatePaymentAndCardsEtc (mismas columnas y nombres de FK).
            migrationBuilder.AddForeignKey("FK_payments_bookings_booking_id", "payments", "booking_id", "bookings", principalColumn: "id", onDelete: ReferentialAction.Restrict);

            // document_type_id: ya definida en RenameDatabaseSchemaToEnglish (FK_persons_document_types_document_type_id).
            migrationBuilder.AddForeignKey("FK_persons_addresses_address_id", "persons", "address_id", "addresses", principalColumn: "id", onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey("FK_route_stopovers_airports_stopover_airport_id", "route_stopovers", "stopover_airport_id", "airports", principalColumn: "id", onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey("FK_route_stopovers_routes_route_id", "route_stopovers", "route_id", "routes", principalColumn: "id", onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey("FK_routes_airports_origin_airport_id", "routes", "origin_airport_id", "airports", principalColumn: "id", onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey("FK_routes_airports_destination_airport_id", "routes", "destination_airport_id", "airports", principalColumn: "id", onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey("FK_staff_persons_person_id", "staff", "person_id", "persons", principalColumn: "id", onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey("FK_staff_staff_positions_position_id", "staff", "position_id", "staff_positions", principalColumn: "id", onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey("FK_staff_airlines_airline_id", "staff", "airline_id", "airlines", principalColumn: "id", onDelete: ReferentialAction.SetNull);
            migrationBuilder.AddForeignKey("FK_staff_airports_airport_id", "staff", "airport_id", "airports", principalColumn: "id", onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey("FK_tickets_booking_passengers_booking_passenger_id", "tickets", "booking_passenger_id", "booking_passengers", principalColumn: "id", onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey("FK_users_system_roles_role_id", "users", "role_id", "system_roles", principalColumn: "id", onDelete: ReferentialAction.Restrict);

            void RenameColumnIfExists(string table, string from, string to)
            {
                migrationBuilder.Sql($"""
                    SET @column_exists = (
                        SELECT COUNT(1)
                        FROM information_schema.COLUMNS
                        WHERE TABLE_SCHEMA = DATABASE()
                          AND TABLE_NAME = '{table}'
                          AND COLUMN_NAME = '{from}'
                    );
                    SET @target_exists = (
                        SELECT COUNT(1)
                        FROM information_schema.COLUMNS
                        WHERE TABLE_SCHEMA = DATABASE()
                          AND TABLE_NAME = '{table}'
                          AND COLUMN_NAME = '{to}'
                    );
                    SET @sql = IF(
                        @column_exists > 0 AND @target_exists = 0,
                        'ALTER TABLE `{table}` RENAME COLUMN `{from}` TO `{to}`',
                        'SELECT 1'
                    );
                    PREPARE stmt FROM @sql;
                    EXECUTE stmt;
                    DEALLOCATE PREPARE stmt;
                    """);
            }
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey("FK_booking_flights_bookings_booking_id", "booking_flights");
            migrationBuilder.DropForeignKey("FK_booking_flights_flights_flight_id", "booking_flights");
            migrationBuilder.DropForeignKey("FK_booking_passengers_booking_flights_booking_flight_id", "booking_passengers");
            migrationBuilder.DropForeignKey("FK_booking_passengers_passengers_passenger_id", "booking_passengers");
            migrationBuilder.DropForeignKey("FK_booking_status_transitions_booking_statuses_from_status_id", "booking_status_transitions");
            migrationBuilder.DropForeignKey("FK_booking_status_transitions_booking_statuses_to_status_id", "booking_status_transitions");
            migrationBuilder.DropForeignKey("FK_bookings_booking_statuses_booking_status_id", "bookings");
            migrationBuilder.DropForeignKey("FK_bookings_clients_client_id", "bookings");
            migrationBuilder.DropForeignKey("FK_cabin_configurations_aircraft_aircraft_id", "cabin_configurations");
            migrationBuilder.DropForeignKey("FK_cabin_configurations_cabin_types_cabin_type_id", "cabin_configurations");
            migrationBuilder.DropForeignKey("FK_check_ins_checkin_statuses_checkin_status_id", "check_ins");
            migrationBuilder.DropForeignKey("FK_check_ins_flight_seats_flight_seat_id", "check_ins");
            migrationBuilder.DropForeignKey("FK_check_ins_staff_staff_id", "check_ins");
            migrationBuilder.DropForeignKey("FK_fares_cabin_types_cabin_type_id", "fares");
            migrationBuilder.DropForeignKey("FK_fares_passenger_types_passenger_type_id", "fares");
            migrationBuilder.DropForeignKey("FK_fares_routes_route_id", "fares");
            migrationBuilder.DropForeignKey("FK_fares_seasons_season_id", "fares");
            migrationBuilder.DropForeignKey("FK_flight_crew_assignments_flight_crew_roles_crew_role_id", "flight_crew_assignments");
            migrationBuilder.DropForeignKey("FK_flight_crew_assignments_flights_flight_id", "flight_crew_assignments");
            migrationBuilder.DropForeignKey("FK_flight_crew_assignments_staff_staff_id", "flight_crew_assignments");
            migrationBuilder.DropForeignKey("FK_flight_seats_cabin_types_cabin_type_id", "flight_seats");
            migrationBuilder.DropForeignKey("FK_flight_seats_flights_flight_id", "flight_seats");
            migrationBuilder.DropForeignKey("FK_flight_seats_seat_location_types_location_type_id", "flight_seats");
            migrationBuilder.DropForeignKey("FK_flight_status_transitions_flight_statuses_from_status_id", "flight_status_transitions");
            migrationBuilder.DropForeignKey("FK_flight_status_transitions_flight_statuses_to_status_id", "flight_status_transitions");
            migrationBuilder.DropForeignKey("FK_flights_aircraft_aircraft_id", "flights");
            migrationBuilder.DropForeignKey("FK_flights_airlines_airline_id", "flights");
            migrationBuilder.DropForeignKey("FK_flights_flight_statuses_flight_status_id", "flights");
            migrationBuilder.DropForeignKey("FK_flights_routes_route_id", "flights");
            migrationBuilder.DropForeignKey("FK_invoice_items_booking_passengers_booking_passenger_id", "invoice_items");
            migrationBuilder.DropForeignKey("FK_invoices_bookings_booking_id", "invoices");
            migrationBuilder.DropForeignKey("FK_payments_bookings_booking_id", "payments");
            migrationBuilder.DropForeignKey("FK_persons_addresses_address_id", "persons");
            migrationBuilder.DropForeignKey("FK_route_stopovers_airports_stopover_airport_id", "route_stopovers");
            migrationBuilder.DropForeignKey("FK_route_stopovers_routes_route_id", "route_stopovers");
            migrationBuilder.DropForeignKey("FK_routes_airports_origin_airport_id", "routes");
            migrationBuilder.DropForeignKey("FK_routes_airports_destination_airport_id", "routes");
            migrationBuilder.DropForeignKey("FK_staff_persons_person_id", "staff");
            migrationBuilder.DropForeignKey("FK_staff_staff_positions_position_id", "staff");
            migrationBuilder.DropForeignKey("FK_staff_airlines_airline_id", "staff");
            migrationBuilder.DropForeignKey("FK_staff_airports_airport_id", "staff");
            migrationBuilder.DropForeignKey("FK_tickets_booking_passengers_booking_passenger_id", "tickets");
            migrationBuilder.DropForeignKey("FK_users_system_roles_role_id", "users");
        }
    }
}

