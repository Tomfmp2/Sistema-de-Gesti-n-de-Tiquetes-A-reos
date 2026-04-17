using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sistema_gestor_de_tiquetes_aereos.Migrations
{
    /// <inheritdoc />
    public partial class SchemaAlignedWithReferenceDdlEnglish : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Idempotente: la FK puede no existir si el esquema ya se alineó a mano o con otro nombre.
            migrationBuilder.Sql(
                """
                SET @fk := (
                  SELECT CONSTRAINT_NAME FROM information_schema.TABLE_CONSTRAINTS
                  WHERE CONSTRAINT_SCHEMA = DATABASE()
                    AND TABLE_NAME = 'directions'
                    AND CONSTRAINT_NAME = 'FK_directions_cities_ciudad_id'
                    AND CONSTRAINT_TYPE = 'FOREIGN KEY'
                  LIMIT 1);
                SET @stmt := IF(@fk IS NOT NULL,
                  'ALTER TABLE `directions` DROP FOREIGN KEY `FK_directions_cities_ciudad_id`',
                  'SELECT 1');
                PREPARE p FROM @stmt;
                EXECUTE p;
                DEALLOCATE PREPARE p;
                """
            );

            migrationBuilder.Sql(
                """
                SET @fk := (
                  SELECT CONSTRAINT_NAME FROM information_schema.TABLE_CONSTRAINTS
                  WHERE CONSTRAINT_SCHEMA = DATABASE()
                    AND TABLE_NAME = 'users'
                    AND CONSTRAINT_NAME = 'FK_users_system_roles_rol_id'
                    AND CONSTRAINT_TYPE = 'FOREIGN KEY'
                  LIMIT 1);
                SET @stmt := IF(@fk IS NOT NULL,
                  'ALTER TABLE `users` DROP FOREIGN KEY `FK_users_system_roles_rol_id`',
                  'SELECT 1');
                PREPARE p FROM @stmt;
                EXECUTE p;
                DEALLOCATE PREPARE p;
                """
            );

            migrationBuilder.Sql(
                """
                SET @fk := (
                  SELECT CONSTRAINT_NAME FROM information_schema.TABLE_CONSTRAINTS
                  WHERE CONSTRAINT_SCHEMA = DATABASE()
                    AND TABLE_NAME = 'role_permissions'
                    AND CONSTRAINT_NAME = 'FK_role_permissions_permissions_permission_id'
                    AND CONSTRAINT_TYPE = 'FOREIGN KEY'
                  LIMIT 1);
                SET @stmt := IF(@fk IS NOT NULL,
                  'ALTER TABLE `role_permissions` DROP FOREIGN KEY `FK_role_permissions_permissions_permission_id`',
                  'SELECT 1');
                PREPARE p FROM @stmt;
                EXECUTE p;
                DEALLOCATE PREPARE p;
                """
            );

            migrationBuilder.Sql(
                """
                SET @fk := (
                  SELECT CONSTRAINT_NAME FROM information_schema.TABLE_CONSTRAINTS
                  WHERE CONSTRAINT_SCHEMA = DATABASE()
                    AND TABLE_NAME = 'role_permissions'
                    AND CONSTRAINT_NAME = 'FK_role_permissions_system_roles_role_id'
                    AND CONSTRAINT_TYPE = 'FOREIGN KEY'
                  LIMIT 1);
                SET @stmt := IF(@fk IS NOT NULL,
                  'ALTER TABLE `role_permissions` DROP FOREIGN KEY `FK_role_permissions_system_roles_role_id`',
                  'SELECT 1');
                PREPARE p FROM @stmt;
                EXECUTE p;
                DEALLOCATE PREPARE p;
                """
            );

            migrationBuilder.Sql(
                """
                SET @pk_on_id := (
                  SELECT COUNT(*) FROM information_schema.KEY_COLUMN_USAGE k
                  INNER JOIN information_schema.TABLE_CONSTRAINTS t
                    ON k.CONSTRAINT_SCHEMA = t.CONSTRAINT_SCHEMA
                    AND k.CONSTRAINT_NAME = t.CONSTRAINT_NAME
                  WHERE k.TABLE_SCHEMA = DATABASE()
                    AND k.TABLE_NAME = 'role_permissions'
                    AND t.CONSTRAINT_TYPE = 'PRIMARY KEY'
                    AND k.COLUMN_NAME = 'id');
                SET @pk := (
                  SELECT CONSTRAINT_NAME FROM information_schema.TABLE_CONSTRAINTS
                  WHERE CONSTRAINT_SCHEMA = DATABASE()
                    AND TABLE_NAME = 'role_permissions'
                    AND CONSTRAINT_TYPE = 'PRIMARY KEY'
                  LIMIT 1);
                SET @stmt := IF(@pk IS NOT NULL AND @pk_on_id = 0,
                  'ALTER TABLE `role_permissions` DROP PRIMARY KEY',
                  'SELECT 1');
                PREPARE p FROM @stmt;
                EXECUTE p;
                DEALLOCATE PREPARE p;
                """
            );

            migrationBuilder.Sql(
                """
                SET @has_id := (
                  SELECT COUNT(*) FROM information_schema.COLUMNS
                  WHERE TABLE_SCHEMA = DATABASE()
                    AND TABLE_NAME = 'role_permissions'
                    AND COLUMN_NAME = 'id');
                SET @stmt := IF(@has_id = 0,
                  'ALTER TABLE `role_permissions` ADD COLUMN `id` int NOT NULL AUTO_INCREMENT, ADD PRIMARY KEY (`id`)',
                  'SELECT 1');
                PREPARE p FROM @stmt;
                EXECUTE p;
                DEALLOCATE PREPARE p;
                """
            );

            migrationBuilder.Sql(
                """
                SET @exists := (SELECT COUNT(*) FROM information_schema.STATISTICS
                  WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'phone_codes' AND INDEX_NAME = 'IX_phone_codes_dial_code');
                SET @stmt := IF(@exists > 0, 'ALTER TABLE `phone_codes` DROP INDEX `IX_phone_codes_dial_code`', 'SELECT 1');
                PREPARE p FROM @stmt; EXECUTE p; DEALLOCATE PREPARE p;
                SET @exists := (SELECT COUNT(*) FROM information_schema.STATISTICS
                  WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'person_emails' AND INDEX_NAME = 'IX_person_emails_email');
                SET @stmt := IF(@exists > 0, 'ALTER TABLE `person_emails` DROP INDEX `IX_person_emails_email`', 'SELECT 1');
                PREPARE p FROM @stmt; EXECUTE p; DEALLOCATE PREPARE p;
                SET @exists := (SELECT COUNT(*) FROM information_schema.STATISTICS
                  WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'permissions' AND INDEX_NAME = 'IX_permissions_code');
                SET @stmt := IF(@exists > 0, 'ALTER TABLE `permissions` DROP INDEX `IX_permissions_code`', 'SELECT 1');
                PREPARE p FROM @stmt; EXECUTE p; DEALLOCATE PREPARE p;
                """
            );

            migrationBuilder.Sql(
                """
                SET @c := (SELECT COUNT(*) FROM information_schema.COLUMNS WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'sessions' AND COLUMN_NAME = 'emitida_en_utc');
                SET @stmt := IF(@c > 0, 'ALTER TABLE `sessions` DROP COLUMN `emitida_en_utc`', 'SELECT 1');
                PREPARE p FROM @stmt; EXECUTE p; DEALLOCATE PREPARE p;
                SET @c := (SELECT COUNT(*) FROM information_schema.COLUMNS WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'sessions' AND COLUMN_NAME = 'refresh_token');
                SET @stmt := IF(@c > 0, 'ALTER TABLE `sessions` DROP COLUMN `refresh_token`', 'SELECT 1');
                PREPARE p FROM @stmt; EXECUTE p; DEALLOCATE PREPARE p;
                SET @c := (SELECT COUNT(*) FROM information_schema.COLUMNS WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'phone_codes' AND COLUMN_NAME = 'dial_code');
                SET @stmt := IF(@c > 0, 'ALTER TABLE `phone_codes` DROP COLUMN `dial_code`', 'SELECT 1');
                PREPARE p FROM @stmt; EXECUTE p; DEALLOCATE PREPARE p;
                SET @c := (SELECT COUNT(*) FROM information_schema.COLUMNS WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'phone_codes' AND COLUMN_NAME = 'name');
                SET @stmt := IF(@c > 0, 'ALTER TABLE `phone_codes` DROP COLUMN `name`', 'SELECT 1');
                PREPARE p FROM @stmt; EXECUTE p; DEALLOCATE PREPARE p;
                SET @c := (SELECT COUNT(*) FROM information_schema.COLUMNS WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'person_emails' AND COLUMN_NAME = 'email');
                SET @stmt := IF(@c > 0, 'ALTER TABLE `person_emails` DROP COLUMN `email`', 'SELECT 1');
                PREPARE p FROM @stmt; EXECUTE p; DEALLOCATE PREPARE p;
                SET @c := (SELECT COUNT(*) FROM information_schema.COLUMNS WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'permissions' AND COLUMN_NAME = 'code');
                SET @stmt := IF(@c > 0, 'ALTER TABLE `permissions` DROP COLUMN `code`', 'SELECT 1');
                PREPARE p FROM @stmt; EXECUTE p; DEALLOCATE PREPARE p;
                SET @c := (SELECT COUNT(*) FROM information_schema.COLUMNS WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'sessions' AND COLUMN_NAME = 'expires_at_utc');
                SET @stmt := IF(@c > 0, 'ALTER TABLE `sessions` DROP COLUMN `expires_at_utc`', 'SELECT 1');
                PREPARE p FROM @stmt; EXECUTE p; DEALLOCATE PREPARE p;
                SET @c := (SELECT COUNT(*) FROM information_schema.COLUMNS WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'sessions' AND COLUMN_NAME = 'is_revoked');
                SET @stmt := IF(@c > 0, 'ALTER TABLE `sessions` DROP COLUMN `is_revoked`', 'SELECT 1');
                PREPARE p FROM @stmt; EXECUTE p; DEALLOCATE PREPARE p;
                """
            );

            migrationBuilder.Sql(
                """
                SET @o := (SELECT COUNT(*) FROM information_schema.COLUMNS WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'users' AND COLUMN_NAME = 'rol_id');
                SET @n := (SELECT COUNT(*) FROM information_schema.COLUMNS WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'users' AND COLUMN_NAME = 'system_role_id');
                SET @stmt := IF(@o > 0 AND @n = 0, 'ALTER TABLE `users` RENAME COLUMN `rol_id` TO `system_role_id`', 'SELECT 1');
                PREPARE p FROM @stmt; EXECUTE p; DEALLOCATE PREPARE p;
                SET @o := (SELECT COUNT(*) FROM information_schema.STATISTICS WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'users' AND INDEX_NAME = 'IX_users_rol_id');
                SET @n := (SELECT COUNT(*) FROM information_schema.STATISTICS WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'users' AND INDEX_NAME = 'IX_users_system_role_id');
                SET @stmt := IF(@o > 0 AND @n = 0, 'ALTER TABLE `users` RENAME INDEX `IX_users_rol_id` TO `IX_users_system_role_id`', 'SELECT 1');
                PREPARE p FROM @stmt; EXECUTE p; DEALLOCATE PREPARE p;
                """
            );

            migrationBuilder.Sql(
                """
                SET @c := (SELECT COUNT(*) FROM information_schema.COLUMNS WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'sessions' AND COLUMN_NAME = 'started_at');
                SET @stmt := IF(@c = 0, 'ALTER TABLE `sessions` ADD COLUMN `started_at` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6)', 'SELECT 1');
                PREPARE p FROM @stmt; EXECUTE p; DEALLOCATE PREPARE p;
                SET @c := (SELECT COUNT(*) FROM information_schema.COLUMNS WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'sessions' AND COLUMN_NAME = 'is_active');
                SET @stmt := IF(@c = 0, 'ALTER TABLE `sessions` ADD COLUMN `is_active` tinyint(1) NOT NULL DEFAULT 1', 'SELECT 1');
                PREPARE p FROM @stmt; EXECUTE p; DEALLOCATE PREPARE p;
                """
            );

            migrationBuilder.Sql(
                """
                SET @o := (SELECT COUNT(*) FROM information_schema.COLUMNS WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'persons' AND COLUMN_NAME = 'numero_documento');
                SET @n := (SELECT COUNT(*) FROM information_schema.COLUMNS WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'persons' AND COLUMN_NAME = 'document_number');
                SET @stmt := IF(@o > 0 AND @n = 0, 'ALTER TABLE `persons` RENAME COLUMN `numero_documento` TO `document_number`', 'SELECT 1');
                PREPARE p FROM @stmt; EXECUTE p; DEALLOCATE PREPARE p;
                SET @o := (SELECT COUNT(*) FROM information_schema.COLUMNS WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'persons' AND COLUMN_NAME = 'fecha_nacimiento');
                SET @n := (SELECT COUNT(*) FROM information_schema.COLUMNS WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'persons' AND COLUMN_NAME = 'birth_date');
                SET @stmt := IF(@o > 0 AND @n = 0, 'ALTER TABLE `persons` RENAME COLUMN `fecha_nacimiento` TO `birth_date`', 'SELECT 1');
                PREPARE p FROM @stmt; EXECUTE p; DEALLOCATE PREPARE p;
                SET @o := (SELECT COUNT(*) FROM information_schema.STATISTICS WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'persons' AND INDEX_NAME = 'IX_persons_document_type_id_numero_documento');
                SET @n := (SELECT COUNT(*) FROM information_schema.STATISTICS WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'persons' AND INDEX_NAME = 'IX_persons_document_type_id_document_number');
                SET @stmt := IF(@o > 0 AND @n = 0, 'ALTER TABLE `persons` RENAME INDEX `IX_persons_document_type_id_numero_documento` TO `IX_persons_document_type_id_document_number`', 'SELECT 1');
                PREPARE p FROM @stmt; EXECUTE p; DEALLOCATE PREPARE p;
                SET @o := (SELECT COUNT(*) FROM information_schema.COLUMNS WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'person_phones' AND COLUMN_NAME = 'number');
                SET @n := (SELECT COUNT(*) FROM information_schema.COLUMNS WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'person_phones' AND COLUMN_NAME = 'phone_number');
                SET @stmt := IF(@o > 0 AND @n = 0, 'ALTER TABLE `person_phones` RENAME COLUMN `number` TO `phone_number`', 'SELECT 1');
                PREPARE p FROM @stmt; EXECUTE p; DEALLOCATE PREPARE p;
                SET @o := (SELECT COUNT(*) FROM information_schema.STATISTICS WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'person_phones' AND INDEX_NAME = 'IX_person_phones_person_id_phone_code_id_number');
                SET @n := (SELECT COUNT(*) FROM information_schema.STATISTICS WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'person_phones' AND INDEX_NAME = 'IX_person_phones_person_id_phone_code_id_phone_number');
                SET @stmt := IF(@o > 0 AND @n = 0, 'ALTER TABLE `person_phones` RENAME INDEX `IX_person_phones_person_id_phone_code_id_number` TO `IX_person_phones_person_id_phone_code_id_phone_number`', 'SELECT 1');
                PREPARE p FROM @stmt; EXECUTE p; DEALLOCATE PREPARE p;
                SET @o := (SELECT COUNT(*) FROM information_schema.COLUMNS WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'directions' AND COLUMN_NAME = 'nombre_via');
                SET @n := (SELECT COUNT(*) FROM information_schema.COLUMNS WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'directions' AND COLUMN_NAME = 'street_name');
                SET @stmt := IF(@o > 0 AND @n = 0, 'ALTER TABLE `directions` RENAME COLUMN `nombre_via` TO `street_name`', 'SELECT 1');
                PREPARE p FROM @stmt; EXECUTE p; DEALLOCATE PREPARE p;
                SET @o := (SELECT COUNT(*) FROM information_schema.COLUMNS WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'directions' AND COLUMN_NAME = 'complemento');
                SET @n := (SELECT COUNT(*) FROM information_schema.COLUMNS WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'directions' AND COLUMN_NAME = 'complement');
                SET @stmt := IF(@o > 0 AND @n = 0, 'ALTER TABLE `directions` RENAME COLUMN `complemento` TO `complement`', 'SELECT 1');
                PREPARE p FROM @stmt; EXECUTE p; DEALLOCATE PREPARE p;
                SET @o := (SELECT COUNT(*) FROM information_schema.COLUMNS WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'directions' AND COLUMN_NAME = 'ciudad_id');
                SET @n := (SELECT COUNT(*) FROM information_schema.COLUMNS WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'directions' AND COLUMN_NAME = 'city_id');
                SET @stmt := IF(@o > 0 AND @n = 0, 'ALTER TABLE `directions` RENAME COLUMN `ciudad_id` TO `city_id`', 'SELECT 1');
                PREPARE p FROM @stmt; EXECUTE p; DEALLOCATE PREPARE p;
                SET @o := (SELECT COUNT(*) FROM information_schema.STATISTICS WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'directions' AND INDEX_NAME = 'IX_directions_ciudad_id');
                SET @n := (SELECT COUNT(*) FROM information_schema.STATISTICS WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'directions' AND INDEX_NAME = 'IX_directions_city_id');
                SET @stmt := IF(@o > 0 AND @n = 0, 'ALTER TABLE `directions` RENAME INDEX `IX_directions_ciudad_id` TO `IX_directions_city_id`', 'SELECT 1');
                PREPARE p FROM @stmt; EXECUTE p; DEALLOCATE PREPARE p;
                """
            );

            migrationBuilder.AlterColumn<string>(
                name: "username",
                table: "users",
                type: "varchar(50)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(64)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.Sql(
                """
                SET @c := (SELECT COUNT(*) FROM information_schema.COLUMNS WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'users' AND COLUMN_NAME = 'created_at');
                SET @stmt := IF(@c = 0, 'ALTER TABLE `users` ADD COLUMN `created_at` datetime(6) NOT NULL DEFAULT ''0001-01-01 00:00:00.000000''', 'SELECT 1');
                PREPARE p FROM @stmt; EXECUTE p; DEALLOCATE PREPARE p;
                SET @c := (SELECT COUNT(*) FROM information_schema.COLUMNS WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'users' AND COLUMN_NAME = 'last_access_at');
                SET @stmt := IF(@c = 0, 'ALTER TABLE `users` ADD COLUMN `last_access_at` datetime(6) NULL', 'SELECT 1');
                PREPARE p FROM @stmt; EXECUTE p; DEALLOCATE PREPARE p;
                SET @c := (SELECT COUNT(*) FROM information_schema.COLUMNS WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'users' AND COLUMN_NAME = 'updated_at');
                SET @stmt := IF(@c = 0, 'ALTER TABLE `users` ADD COLUMN `updated_at` datetime(6) NOT NULL DEFAULT ''0001-01-01 00:00:00.000000''', 'SELECT 1');
                PREPARE p FROM @stmt; EXECUTE p; DEALLOCATE PREPARE p;
                """
            );

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "system_roles",
                type: "varchar(50)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(80)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.Sql(
                """
                SET @c := (SELECT COUNT(*) FROM information_schema.COLUMNS WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'system_roles' AND COLUMN_NAME = 'description');
                SET @stmt := IF(@c = 0, 'ALTER TABLE `system_roles` ADD COLUMN `description` varchar(150) NULL', 'SELECT 1');
                PREPARE p FROM @stmt; EXECUTE p; DEALLOCATE PREPARE p;
                """
            );

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "street_types",
                type: "varchar(50)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(80)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.Sql(
                """
                SET @c := (SELECT COUNT(*) FROM information_schema.COLUMNS WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'sessions' AND COLUMN_NAME = 'closed_at');
                SET @stmt := IF(@c = 0, 'ALTER TABLE `sessions` ADD COLUMN `closed_at` datetime(6) NULL', 'SELECT 1');
                PREPARE p FROM @stmt; EXECUTE p; DEALLOCATE PREPARE p;
                SET @c := (SELECT COUNT(*) FROM information_schema.COLUMNS WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'sessions' AND COLUMN_NAME = 'origin_ip');
                SET @stmt := IF(@c = 0, 'ALTER TABLE `sessions` ADD COLUMN `origin_ip` varchar(45) NULL', 'SELECT 1');
                PREPARE p FROM @stmt; EXECUTE p; DEALLOCATE PREPARE p;
                SET @c := (SELECT COUNT(*) FROM information_schema.COLUMNS WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'phone_codes' AND COLUMN_NAME = 'country_dial_code');
                SET @stmt := IF(@c = 0, 'ALTER TABLE `phone_codes` ADD COLUMN `country_dial_code` varchar(5) CHARACTER SET utf8mb4 NOT NULL DEFAULT ''''', 'SELECT 1');
                PREPARE p FROM @stmt; EXECUTE p; DEALLOCATE PREPARE p;
                SET @c := (SELECT COUNT(*) FROM information_schema.COLUMNS WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'phone_codes' AND COLUMN_NAME = 'country_name');
                SET @stmt := IF(@c = 0, 'ALTER TABLE `phone_codes` ADD COLUMN `country_name` varchar(100) CHARACTER SET utf8mb4 NOT NULL DEFAULT ''''', 'SELECT 1');
                PREPARE p FROM @stmt; EXECUTE p; DEALLOCATE PREPARE p;
                """
            );

            migrationBuilder.AlterColumn<string>(
                name: "document_number",
                table: "persons",
                type: "varchar(30)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(32)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.Sql(
                """
                SET @c := (SELECT COUNT(*) FROM information_schema.COLUMNS WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'persons' AND COLUMN_NAME = 'created_at');
                SET @stmt := IF(@c = 0, 'ALTER TABLE `persons` ADD COLUMN `created_at` datetime(6) NOT NULL DEFAULT ''0001-01-01 00:00:00.000000''', 'SELECT 1');
                PREPARE p FROM @stmt; EXECUTE p; DEALLOCATE PREPARE p;
                SET @c := (SELECT COUNT(*) FROM information_schema.COLUMNS WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'persons' AND COLUMN_NAME = 'updated_at');
                SET @stmt := IF(@c = 0, 'ALTER TABLE `persons` ADD COLUMN `updated_at` datetime(6) NOT NULL DEFAULT ''0001-01-01 00:00:00.000000''', 'SELECT 1');
                PREPARE p FROM @stmt; EXECUTE p; DEALLOCATE PREPARE p;
                """
            );

            migrationBuilder.AlterColumn<int>(
                name: "email_domain_id",
                table: "person_emails",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.Sql(
                """
                SET @c := (SELECT COUNT(*) FROM information_schema.COLUMNS WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'person_emails' AND COLUMN_NAME = 'email_local_part');
                SET @stmt := IF(@c = 0, 'ALTER TABLE `person_emails` ADD COLUMN `email_local_part` varchar(100) CHARACTER SET utf8mb4 NOT NULL DEFAULT ''''', 'SELECT 1');
                PREPARE p FROM @stmt; EXECUTE p; DEALLOCATE PREPARE p;
                SET @c := (SELECT COUNT(*) FROM information_schema.COLUMNS WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'person_emails' AND COLUMN_NAME = 'is_primary');
                SET @stmt := IF(@c = 0, 'ALTER TABLE `person_emails` ADD COLUMN `is_primary` tinyint(1) NOT NULL DEFAULT 0', 'SELECT 1');
                PREPARE p FROM @stmt; EXECUTE p; DEALLOCATE PREPARE p;
                SET @c := (SELECT COUNT(*) FROM information_schema.COLUMNS WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'permissions' AND COLUMN_NAME = 'name');
                SET @stmt := IF(@c = 0, 'ALTER TABLE `permissions` ADD COLUMN `name` varchar(100) CHARACTER SET utf8mb4 NOT NULL DEFAULT ''''', 'SELECT 1');
                PREPARE p FROM @stmt; EXECUTE p; DEALLOCATE PREPARE p;
                """
            );

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "passenger_types",
                type: "varchar(50)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(80)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.Sql(
                """
                SET @c := (SELECT COUNT(*) FROM information_schema.COLUMNS WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'passenger_types' AND COLUMN_NAME = 'max_age');
                SET @stmt := IF(@c = 0, 'ALTER TABLE `passenger_types` ADD COLUMN `max_age` int NULL', 'SELECT 1');
                PREPARE p FROM @stmt; EXECUTE p; DEALLOCATE PREPARE p;
                SET @c := (SELECT COUNT(*) FROM information_schema.COLUMNS WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'passenger_types' AND COLUMN_NAME = 'min_age');
                SET @stmt := IF(@c = 0, 'ALTER TABLE `passenger_types` ADD COLUMN `min_age` int NULL', 'SELECT 1');
                PREPARE p FROM @stmt; EXECUTE p; DEALLOCATE PREPARE p;
                """
            );

            migrationBuilder.AlterColumn<string>(
                name: "domain",
                table: "email_domains",
                type: "varchar(100)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "document_types",
                type: "varchar(50)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(80)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.Sql(
                """
                SET @c := (SELECT COUNT(*) FROM information_schema.COLUMNS WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'document_types' AND COLUMN_NAME = 'code');
                SET @stmt := IF(@c = 0, 'ALTER TABLE `document_types` ADD COLUMN `code` varchar(10) CHARACTER SET utf8mb4 NOT NULL DEFAULT ''''', 'SELECT 1');
                PREPARE p FROM @stmt; EXECUTE p; DEALLOCATE PREPARE p;
                """
            );

            migrationBuilder.AlterColumn<string>(
                name: "street_number",
                table: "directions",
                type: "varchar(20)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(20)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "street_name",
                table: "directions",
                type: "varchar(100)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(150)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.Sql(
                """
                SET @c := (SELECT COUNT(*) FROM information_schema.COLUMNS WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'clients' AND COLUMN_NAME = 'created_at');
                SET @stmt := IF(@c = 0, 'ALTER TABLE `clients` ADD COLUMN `created_at` datetime(6) NOT NULL DEFAULT ''0001-01-01 00:00:00.000000''', 'SELECT 1');
                PREPARE p FROM @stmt; EXECUTE p; DEALLOCATE PREPARE p;
                """
            );

            migrationBuilder.CreateIndex(
                name: "IX_system_roles_name",
                table: "system_roles",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_role_permissions_role_id_permission_id",
                table: "role_permissions",
                columns: new[] { "role_id", "permission_id" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_role_permissions_permissions_permission_id",
                table: "role_permissions",
                column: "permission_id",
                principalTable: "permissions",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_role_permissions_system_roles_role_id",
                table: "role_permissions",
                column: "role_id",
                principalTable: "system_roles",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.CreateIndex(
                name: "IX_phone_codes_country_dial_code",
                table: "phone_codes",
                column: "country_dial_code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_person_emails_person_id_email_local_part_email_domain_id",
                table: "person_emails",
                columns: new[] { "person_id", "email_local_part", "email_domain_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_permissions_name",
                table: "permissions",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_passenger_types_name",
                table: "passenger_types",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_document_types_code",
                table: "document_types",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_continents_name",
                table: "continents",
                column: "name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_directions_cities_city_id",
                table: "directions",
                column: "city_id",
                principalTable: "cities",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_users_system_roles_system_role_id",
                table: "users",
                column: "system_role_id",
                principalTable: "system_roles",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_directions_cities_city_id",
                table: "directions");

            migrationBuilder.DropForeignKey(
                name: "FK_users_system_roles_system_role_id",
                table: "users");

            migrationBuilder.DropIndex(
                name: "IX_system_roles_name",
                table: "system_roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_role_permissions",
                table: "role_permissions");

            migrationBuilder.DropIndex(
                name: "IX_role_permissions_role_id_permission_id",
                table: "role_permissions");

            migrationBuilder.DropIndex(
                name: "IX_phone_codes_country_dial_code",
                table: "phone_codes");

            migrationBuilder.DropIndex(
                name: "IX_person_emails_person_id_email_local_part_email_domain_id",
                table: "person_emails");

            migrationBuilder.DropIndex(
                name: "IX_permissions_name",
                table: "permissions");

            migrationBuilder.DropIndex(
                name: "IX_passenger_types_name",
                table: "passenger_types");

            migrationBuilder.DropIndex(
                name: "IX_document_types_code",
                table: "document_types");

            migrationBuilder.DropIndex(
                name: "IX_continents_name",
                table: "continents");

            migrationBuilder.DropColumn(
                name: "created_at",
                table: "users");

            migrationBuilder.DropColumn(
                name: "last_access_at",
                table: "users");

            migrationBuilder.DropColumn(
                name: "updated_at",
                table: "users");

            migrationBuilder.DropColumn(
                name: "description",
                table: "system_roles");

            migrationBuilder.DropColumn(
                name: "closed_at",
                table: "sessions");

            migrationBuilder.DropColumn(
                name: "origin_ip",
                table: "sessions");

            migrationBuilder.DropColumn(
                name: "id",
                table: "role_permissions");

            migrationBuilder.DropColumn(
                name: "country_dial_code",
                table: "phone_codes");

            migrationBuilder.DropColumn(
                name: "country_name",
                table: "phone_codes");

            migrationBuilder.DropColumn(
                name: "created_at",
                table: "persons");

            migrationBuilder.DropColumn(
                name: "updated_at",
                table: "persons");

            migrationBuilder.DropColumn(
                name: "email_local_part",
                table: "person_emails");

            migrationBuilder.DropColumn(
                name: "is_primary",
                table: "person_emails");

            migrationBuilder.DropColumn(
                name: "name",
                table: "permissions");

            migrationBuilder.DropColumn(
                name: "max_age",
                table: "passenger_types");

            migrationBuilder.DropColumn(
                name: "min_age",
                table: "passenger_types");

            migrationBuilder.DropColumn(
                name: "code",
                table: "document_types");

            migrationBuilder.DropColumn(
                name: "created_at",
                table: "clients");

            migrationBuilder.RenameColumn(
                name: "system_role_id",
                table: "users",
                newName: "rol_id");

            migrationBuilder.RenameIndex(
                name: "IX_users_system_role_id",
                table: "users",
                newName: "IX_users_rol_id");

            migrationBuilder.DropColumn(
                name: "started_at",
                table: "sessions");

            migrationBuilder.DropColumn(
                name: "is_active",
                table: "sessions");

            migrationBuilder.AddColumn<DateTime>(
                name: "expires_at_utc",
                table: "sessions",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "is_revoked",
                table: "sessions",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.RenameColumn(
                name: "document_number",
                table: "persons",
                newName: "numero_documento");

            migrationBuilder.RenameColumn(
                name: "birth_date",
                table: "persons",
                newName: "fecha_nacimiento");

            migrationBuilder.RenameIndex(
                name: "IX_persons_document_type_id_document_number",
                table: "persons",
                newName: "IX_persons_document_type_id_numero_documento");

            migrationBuilder.RenameColumn(
                name: "phone_number",
                table: "person_phones",
                newName: "number");

            migrationBuilder.RenameIndex(
                name: "IX_person_phones_person_id_phone_code_id_phone_number",
                table: "person_phones",
                newName: "IX_person_phones_person_id_phone_code_id_number");

            migrationBuilder.RenameColumn(
                name: "street_name",
                table: "directions",
                newName: "nombre_via");

            migrationBuilder.RenameColumn(
                name: "complement",
                table: "directions",
                newName: "complemento");

            migrationBuilder.RenameColumn(
                name: "city_id",
                table: "directions",
                newName: "ciudad_id");

            migrationBuilder.RenameIndex(
                name: "IX_directions_city_id",
                table: "directions",
                newName: "IX_directions_ciudad_id");

            migrationBuilder.AlterColumn<string>(
                name: "username",
                table: "users",
                type: "varchar(64)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "system_roles",
                type: "varchar(80)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "street_types",
                type: "varchar(80)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "emitida_en_utc",
                table: "sessions",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "refresh_token",
                table: "sessions",
                type: "varchar(500)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "dial_code",
                table: "phone_codes",
                type: "varchar(10)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "name",
                table: "phone_codes",
                type: "varchar(100)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "numero_documento",
                table: "persons",
                type: "varchar(32)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(30)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "email_domain_id",
                table: "person_emails",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "email",
                table: "person_emails",
                type: "varchar(320)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "code",
                table: "permissions",
                type: "varchar(64)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "passenger_types",
                type: "varchar(80)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "domain",
                table: "email_domains",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(100)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "document_types",
                type: "varchar(80)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "directions",
                keyColumn: "street_number",
                keyValue: null,
                column: "street_number",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "street_number",
                table: "directions",
                type: "varchar(20)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "nombre_via",
                table: "directions",
                type: "varchar(150)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(100)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_role_permissions",
                table: "role_permissions",
                columns: new[] { "role_id", "permission_id" });

            migrationBuilder.CreateIndex(
                name: "IX_phone_codes_dial_code",
                table: "phone_codes",
                column: "dial_code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_person_emails_email",
                table: "person_emails",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_permissions_code",
                table: "permissions",
                column: "code",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_directions_cities_ciudad_id",
                table: "directions",
                column: "ciudad_id",
                principalTable: "cities",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_users_system_roles_rol_id",
                table: "users",
                column: "rol_id",
                principalTable: "system_roles",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
