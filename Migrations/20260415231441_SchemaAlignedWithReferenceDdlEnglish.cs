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
            migrationBuilder.DropForeignKey(
                name: "FK_directions_cities_ciudad_id",
                table: "directions");

            migrationBuilder.DropForeignKey(
                name: "FK_users_system_roles_rol_id",
                table: "users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_role_permissions",
                table: "role_permissions");

            migrationBuilder.DropIndex(
                name: "IX_phone_codes_dial_code",
                table: "phone_codes");

            migrationBuilder.DropIndex(
                name: "IX_person_emails_email",
                table: "person_emails");

            migrationBuilder.DropIndex(
                name: "IX_permissions_code",
                table: "permissions");

            migrationBuilder.DropColumn(
                name: "emitida_en_utc",
                table: "sessions");

            migrationBuilder.DropColumn(
                name: "refresh_token",
                table: "sessions");

            migrationBuilder.DropColumn(
                name: "dial_code",
                table: "phone_codes");

            migrationBuilder.DropColumn(
                name: "name",
                table: "phone_codes");

            migrationBuilder.DropColumn(
                name: "email",
                table: "person_emails");

            migrationBuilder.DropColumn(
                name: "code",
                table: "permissions");

            migrationBuilder.RenameColumn(
                name: "rol_id",
                table: "users",
                newName: "system_role_id");

            migrationBuilder.RenameIndex(
                name: "IX_users_rol_id",
                table: "users",
                newName: "IX_users_system_role_id");

            migrationBuilder.DropColumn(
                name: "expires_at_utc",
                table: "sessions");

            migrationBuilder.DropColumn(
                name: "is_revoked",
                table: "sessions");

            migrationBuilder.AddColumn<DateTime>(
                name: "started_at",
                table: "sessions",
                type: "datetime(6)",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP(6)");

            migrationBuilder.AddColumn<bool>(
                name: "is_active",
                table: "sessions",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: true);

            migrationBuilder.RenameColumn(
                name: "numero_documento",
                table: "persons",
                newName: "document_number");

            migrationBuilder.RenameColumn(
                name: "fecha_nacimiento",
                table: "persons",
                newName: "birth_date");

            migrationBuilder.RenameIndex(
                name: "IX_persons_document_type_id_numero_documento",
                table: "persons",
                newName: "IX_persons_document_type_id_document_number");

            migrationBuilder.RenameColumn(
                name: "number",
                table: "person_phones",
                newName: "phone_number");

            migrationBuilder.RenameIndex(
                name: "IX_person_phones_person_id_phone_code_id_number",
                table: "person_phones",
                newName: "IX_person_phones_person_id_phone_code_id_phone_number");

            migrationBuilder.RenameColumn(
                name: "nombre_via",
                table: "directions",
                newName: "street_name");

            migrationBuilder.RenameColumn(
                name: "complemento",
                table: "directions",
                newName: "complement");

            migrationBuilder.RenameColumn(
                name: "ciudad_id",
                table: "directions",
                newName: "city_id");

            migrationBuilder.RenameIndex(
                name: "IX_directions_ciudad_id",
                table: "directions",
                newName: "IX_directions_city_id");

            migrationBuilder.AlterColumn<string>(
                name: "username",
                table: "users",
                type: "varchar(50)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(64)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "created_at",
                table: "users",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "last_access_at",
                table: "users",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "updated_at",
                table: "users",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "system_roles",
                type: "varchar(50)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(80)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "system_roles",
                type: "varchar(150)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "street_types",
                type: "varchar(50)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(80)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "closed_at",
                table: "sessions",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "origin_ip",
                table: "sessions",
                type: "varchar(45)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "id",
                table: "role_permissions",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<string>(
                name: "country_dial_code",
                table: "phone_codes",
                type: "varchar(5)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "country_name",
                table: "phone_codes",
                type: "varchar(100)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "document_number",
                table: "persons",
                type: "varchar(30)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(32)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "created_at",
                table: "persons",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "updated_at",
                table: "persons",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<int>(
                name: "email_domain_id",
                table: "person_emails",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "email_local_part",
                table: "person_emails",
                type: "varchar(100)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<bool>(
                name: "is_primary",
                table: "person_emails",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "name",
                table: "permissions",
                type: "varchar(100)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "passenger_types",
                type: "varchar(50)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(80)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "max_age",
                table: "passenger_types",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "min_age",
                table: "passenger_types",
                type: "int",
                nullable: true);

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

            migrationBuilder.AddColumn<string>(
                name: "code",
                table: "document_types",
                type: "varchar(10)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

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

            migrationBuilder.AddColumn<DateTime>(
                name: "created_at",
                table: "clients",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_role_permissions",
                table: "role_permissions",
                column: "id");

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
