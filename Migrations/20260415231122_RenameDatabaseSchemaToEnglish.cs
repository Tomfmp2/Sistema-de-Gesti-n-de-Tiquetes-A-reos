using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sistema_gestor_de_tiquetes_aereos.Migrations
{
    /// <inheritdoc />
    public partial class RenameDatabaseSchemaToEnglish : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_clientes_personas_persona_id",
                table: "clientes");

            migrationBuilder.DropForeignKey(
                name: "FK_direcciones_cities_ciudad_id",
                table: "direcciones");

            migrationBuilder.DropForeignKey(
                name: "FK_direcciones_tipos_via_tipo_via_id",
                table: "direcciones");

            migrationBuilder.DropForeignKey(
                name: "FK_pasajeros_personas_persona_id",
                table: "pasajeros");

            migrationBuilder.DropForeignKey(
                name: "FK_pasajeros_tipos_pasajero_tipo_pasajero_id",
                table: "pasajeros");

            migrationBuilder.DropForeignKey(
                name: "FK_personas_direcciones_direccion_id",
                table: "personas");

            migrationBuilder.DropForeignKey(
                name: "FK_personas_tipos_documento_tipo_documento_id",
                table: "personas");

            migrationBuilder.DropForeignKey(
                name: "FK_personas_emails_dominios_email_dominio_email_id",
                table: "personas_emails");

            migrationBuilder.DropForeignKey(
                name: "FK_personas_emails_personas_persona_id",
                table: "personas_emails");

            migrationBuilder.DropForeignKey(
                name: "FK_personas_telefonos_codigos_telefono_codigo_telefono_id",
                table: "personas_telefonos");

            migrationBuilder.DropForeignKey(
                name: "FK_personas_telefonos_personas_persona_id",
                table: "personas_telefonos");

            migrationBuilder.DropForeignKey(
                name: "FK_regions_countries_Country_id",
                table: "regions");

            migrationBuilder.DropForeignKey(
                name: "FK_roles_permisos_permisos_permiso_id",
                table: "roles_permisos");

            migrationBuilder.DropForeignKey(
                name: "FK_roles_permisos_roles_sistema_rol_id",
                table: "roles_permisos");

            migrationBuilder.DropForeignKey(
                name: "FK_sesiones_usuarios_usuario_id",
                table: "sesiones");

            migrationBuilder.DropForeignKey(
                name: "FK_usuarios_personas_persona_id",
                table: "usuarios");

            migrationBuilder.DropForeignKey(
                name: "FK_usuarios_roles_sistema_rol_id",
                table: "usuarios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_usuarios",
                table: "usuarios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tipos_via",
                table: "tipos_via");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tipos_pasajero",
                table: "tipos_pasajero");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tipos_documento",
                table: "tipos_documento");

            migrationBuilder.DropPrimaryKey(
                name: "PK_sesiones",
                table: "sesiones");

            migrationBuilder.DropPrimaryKey(
                name: "PK_roles_sistema",
                table: "roles_sistema");

            migrationBuilder.DropPrimaryKey(
                name: "PK_roles_permisos",
                table: "roles_permisos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_personas_telefonos",
                table: "personas_telefonos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_personas_emails",
                table: "personas_emails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_personas",
                table: "personas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_permisos",
                table: "permisos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_pasajeros",
                table: "pasajeros");

            migrationBuilder.DropPrimaryKey(
                name: "PK_dominios_email",
                table: "dominios_email");

            migrationBuilder.DropPrimaryKey(
                name: "PK_direcciones",
                table: "direcciones");

            migrationBuilder.DropPrimaryKey(
                name: "PK_codigos_telefono",
                table: "codigos_telefono");

            migrationBuilder.DropPrimaryKey(
                name: "PK_clientes",
                table: "clientes");

            migrationBuilder.RenameTable(
                name: "usuarios",
                newName: "users");

            migrationBuilder.RenameTable(
                name: "tipos_via",
                newName: "street_types");

            migrationBuilder.RenameTable(
                name: "tipos_pasajero",
                newName: "passenger_types");

            migrationBuilder.RenameTable(
                name: "tipos_documento",
                newName: "document_types");

            migrationBuilder.RenameTable(
                name: "sesiones",
                newName: "sessions");

            migrationBuilder.RenameTable(
                name: "roles_sistema",
                newName: "system_roles");

            migrationBuilder.RenameTable(
                name: "roles_permisos",
                newName: "role_permissions");

            migrationBuilder.RenameTable(
                name: "personas_telefonos",
                newName: "person_phones");

            migrationBuilder.RenameTable(
                name: "personas_emails",
                newName: "person_emails");

            migrationBuilder.RenameTable(
                name: "personas",
                newName: "persons");

            migrationBuilder.RenameTable(
                name: "permisos",
                newName: "permissions");

            migrationBuilder.RenameTable(
                name: "pasajeros",
                newName: "passengers");

            migrationBuilder.RenameTable(
                name: "dominios_email",
                newName: "email_domains");

            migrationBuilder.RenameTable(
                name: "direcciones",
                newName: "directions");

            migrationBuilder.RenameTable(
                name: "codigos_telefono",
                newName: "phone_codes");

            migrationBuilder.RenameTable(
                name: "clientes",
                newName: "clients");

            migrationBuilder.RenameColumn(
                name: "Country_id",
                table: "regions",
                newName: "country_id");

            migrationBuilder.RenameIndex(
                name: "IX_regions_Country_id",
                table: "regions",
                newName: "IX_regions_country_id");

            migrationBuilder.RenameColumn(
                name: "persona_id",
                table: "users",
                newName: "person_id");

            migrationBuilder.RenameColumn(
                name: "nombre_usuario",
                table: "users",
                newName: "username");

            migrationBuilder.RenameColumn(
                name: "activo",
                table: "users",
                newName: "is_active");

            migrationBuilder.RenameIndex(
                name: "IX_usuarios_rol_id",
                table: "users",
                newName: "IX_users_rol_id");

            migrationBuilder.RenameIndex(
                name: "IX_usuarios_persona_id",
                table: "users",
                newName: "IX_users_person_id");

            migrationBuilder.RenameIndex(
                name: "IX_usuarios_nombre_usuario",
                table: "users",
                newName: "IX_users_username");

            migrationBuilder.RenameColumn(
                name: "nombre",
                table: "street_types",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "nombre",
                table: "passenger_types",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "nombre",
                table: "document_types",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "usuario_id",
                table: "sessions",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "revocada",
                table: "sessions",
                newName: "is_revoked");

            migrationBuilder.RenameColumn(
                name: "expira_en_utc",
                table: "sessions",
                newName: "expires_at_utc");

            migrationBuilder.RenameIndex(
                name: "IX_sesiones_usuario_id",
                table: "sessions",
                newName: "IX_sessions_user_id");

            migrationBuilder.RenameColumn(
                name: "nombre",
                table: "system_roles",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "permiso_id",
                table: "role_permissions",
                newName: "permission_id");

            migrationBuilder.RenameColumn(
                name: "rol_id",
                table: "role_permissions",
                newName: "role_id");

            migrationBuilder.RenameIndex(
                name: "IX_roles_permisos_permiso_id",
                table: "role_permissions",
                newName: "IX_role_permissions_permission_id");

            migrationBuilder.RenameColumn(
                name: "persona_id",
                table: "person_phones",
                newName: "person_id");

            migrationBuilder.RenameColumn(
                name: "numero",
                table: "person_phones",
                newName: "number");

            migrationBuilder.RenameColumn(
                name: "es_principal",
                table: "person_phones",
                newName: "is_primary");

            migrationBuilder.RenameColumn(
                name: "codigo_telefono_id",
                table: "person_phones",
                newName: "phone_code_id");

            migrationBuilder.RenameIndex(
                name: "IX_personas_telefonos_persona_id_codigo_telefono_id_numero",
                table: "person_phones",
                newName: "IX_person_phones_person_id_phone_code_id_number");

            migrationBuilder.RenameIndex(
                name: "IX_personas_telefonos_codigo_telefono_id",
                table: "person_phones",
                newName: "IX_person_phones_phone_code_id");

            migrationBuilder.RenameColumn(
                name: "persona_id",
                table: "person_emails",
                newName: "person_id");

            migrationBuilder.RenameColumn(
                name: "dominio_email_id",
                table: "person_emails",
                newName: "email_domain_id");

            migrationBuilder.RenameIndex(
                name: "IX_personas_emails_persona_id",
                table: "person_emails",
                newName: "IX_person_emails_person_id");

            migrationBuilder.RenameIndex(
                name: "IX_personas_emails_email",
                table: "person_emails",
                newName: "IX_person_emails_email");

            migrationBuilder.RenameIndex(
                name: "IX_personas_emails_dominio_email_id",
                table: "person_emails",
                newName: "IX_person_emails_email_domain_id");

            migrationBuilder.RenameColumn(
                name: "tipo_documento_id",
                table: "persons",
                newName: "document_type_id");

            migrationBuilder.RenameColumn(
                name: "nombres",
                table: "persons",
                newName: "first_name");

            migrationBuilder.RenameColumn(
                name: "genero",
                table: "persons",
                newName: "gender");

            migrationBuilder.RenameColumn(
                name: "direccion_id",
                table: "persons",
                newName: "direction_id");

            migrationBuilder.RenameColumn(
                name: "apellidos",
                table: "persons",
                newName: "last_name");

            migrationBuilder.RenameIndex(
                name: "IX_personas_tipo_documento_id_numero_documento",
                table: "persons",
                newName: "IX_persons_document_type_id_numero_documento");

            migrationBuilder.RenameIndex(
                name: "IX_personas_direccion_id",
                table: "persons",
                newName: "IX_persons_direction_id");

            migrationBuilder.RenameColumn(
                name: "descripcion",
                table: "permissions",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "codigo",
                table: "permissions",
                newName: "code");

            migrationBuilder.RenameIndex(
                name: "IX_permisos_codigo",
                table: "permissions",
                newName: "IX_permissions_code");

            migrationBuilder.RenameColumn(
                name: "tipo_pasajero_id",
                table: "passengers",
                newName: "passenger_type_id");

            migrationBuilder.RenameColumn(
                name: "persona_id",
                table: "passengers",
                newName: "person_id");

            migrationBuilder.RenameIndex(
                name: "IX_pasajeros_tipo_pasajero_id",
                table: "passengers",
                newName: "IX_passengers_passenger_type_id");

            migrationBuilder.RenameIndex(
                name: "IX_pasajeros_persona_id",
                table: "passengers",
                newName: "IX_passengers_person_id");

            migrationBuilder.RenameColumn(
                name: "dominio",
                table: "email_domains",
                newName: "domain");

            migrationBuilder.RenameIndex(
                name: "IX_dominios_email_dominio",
                table: "email_domains",
                newName: "IX_email_domains_domain");

            migrationBuilder.RenameColumn(
                name: "tipo_via_id",
                table: "directions",
                newName: "street_type_id");

            migrationBuilder.RenameColumn(
                name: "numero",
                table: "directions",
                newName: "street_number");

            migrationBuilder.RenameColumn(
                name: "codigo_postal",
                table: "directions",
                newName: "postal_code");

            migrationBuilder.RenameIndex(
                name: "IX_direcciones_tipo_via_id",
                table: "directions",
                newName: "IX_directions_street_type_id");

            migrationBuilder.RenameIndex(
                name: "IX_direcciones_ciudad_id",
                table: "directions",
                newName: "IX_directions_ciudad_id");

            migrationBuilder.RenameColumn(
                name: "nombre",
                table: "phone_codes",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "codigo",
                table: "phone_codes",
                newName: "dial_code");

            migrationBuilder.RenameIndex(
                name: "IX_codigos_telefono_codigo",
                table: "phone_codes",
                newName: "IX_phone_codes_dial_code");

            migrationBuilder.RenameColumn(
                name: "persona_id",
                table: "clients",
                newName: "person_id");

            migrationBuilder.RenameIndex(
                name: "IX_clientes_persona_id",
                table: "clients",
                newName: "IX_clients_person_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_users",
                table: "users",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_street_types",
                table: "street_types",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_passenger_types",
                table: "passenger_types",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_document_types",
                table: "document_types",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_sessions",
                table: "sessions",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_system_roles",
                table: "system_roles",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_role_permissions",
                table: "role_permissions",
                columns: new[] { "role_id", "permission_id" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_person_phones",
                table: "person_phones",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_person_emails",
                table: "person_emails",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_persons",
                table: "persons",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_permissions",
                table: "permissions",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_passengers",
                table: "passengers",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_email_domains",
                table: "email_domains",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_directions",
                table: "directions",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_phone_codes",
                table: "phone_codes",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_clients",
                table: "clients",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "IX_countries_code_iso",
                table: "countries",
                column: "code_iso",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_clients_persons_person_id",
                table: "clients",
                column: "person_id",
                principalTable: "persons",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_directions_cities_ciudad_id",
                table: "directions",
                column: "ciudad_id",
                principalTable: "cities",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_directions_street_types_street_type_id",
                table: "directions",
                column: "street_type_id",
                principalTable: "street_types",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_passengers_passenger_types_passenger_type_id",
                table: "passengers",
                column: "passenger_type_id",
                principalTable: "passenger_types",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_passengers_persons_person_id",
                table: "passengers",
                column: "person_id",
                principalTable: "persons",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_person_emails_email_domains_email_domain_id",
                table: "person_emails",
                column: "email_domain_id",
                principalTable: "email_domains",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_person_emails_persons_person_id",
                table: "person_emails",
                column: "person_id",
                principalTable: "persons",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_person_phones_persons_person_id",
                table: "person_phones",
                column: "person_id",
                principalTable: "persons",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_person_phones_phone_codes_phone_code_id",
                table: "person_phones",
                column: "phone_code_id",
                principalTable: "phone_codes",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_persons_directions_direction_id",
                table: "persons",
                column: "direction_id",
                principalTable: "directions",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_persons_document_types_document_type_id",
                table: "persons",
                column: "document_type_id",
                principalTable: "document_types",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_regions_countries_country_id",
                table: "regions",
                column: "country_id",
                principalTable: "countries",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

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

            migrationBuilder.AddForeignKey(
                name: "FK_sessions_users_user_id",
                table: "sessions",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_users_persons_person_id",
                table: "users",
                column: "person_id",
                principalTable: "persons",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_clients_persons_person_id",
                table: "clients");

            migrationBuilder.DropForeignKey(
                name: "FK_directions_cities_ciudad_id",
                table: "directions");

            migrationBuilder.DropForeignKey(
                name: "FK_directions_street_types_street_type_id",
                table: "directions");

            migrationBuilder.DropForeignKey(
                name: "FK_passengers_passenger_types_passenger_type_id",
                table: "passengers");

            migrationBuilder.DropForeignKey(
                name: "FK_passengers_persons_person_id",
                table: "passengers");

            migrationBuilder.DropForeignKey(
                name: "FK_person_emails_email_domains_email_domain_id",
                table: "person_emails");

            migrationBuilder.DropForeignKey(
                name: "FK_person_emails_persons_person_id",
                table: "person_emails");

            migrationBuilder.DropForeignKey(
                name: "FK_person_phones_persons_person_id",
                table: "person_phones");

            migrationBuilder.DropForeignKey(
                name: "FK_person_phones_phone_codes_phone_code_id",
                table: "person_phones");

            migrationBuilder.DropForeignKey(
                name: "FK_persons_directions_direction_id",
                table: "persons");

            migrationBuilder.DropForeignKey(
                name: "FK_persons_document_types_document_type_id",
                table: "persons");

            migrationBuilder.DropForeignKey(
                name: "FK_regions_countries_country_id",
                table: "regions");

            migrationBuilder.DropForeignKey(
                name: "FK_role_permissions_permissions_permission_id",
                table: "role_permissions");

            migrationBuilder.DropForeignKey(
                name: "FK_role_permissions_system_roles_role_id",
                table: "role_permissions");

            migrationBuilder.DropForeignKey(
                name: "FK_sessions_users_user_id",
                table: "sessions");

            migrationBuilder.DropForeignKey(
                name: "FK_users_persons_person_id",
                table: "users");

            migrationBuilder.DropForeignKey(
                name: "FK_users_system_roles_rol_id",
                table: "users");

            migrationBuilder.DropIndex(
                name: "IX_countries_code_iso",
                table: "countries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_users",
                table: "users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_system_roles",
                table: "system_roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_street_types",
                table: "street_types");

            migrationBuilder.DropPrimaryKey(
                name: "PK_sessions",
                table: "sessions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_role_permissions",
                table: "role_permissions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_phone_codes",
                table: "phone_codes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_persons",
                table: "persons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_person_phones",
                table: "person_phones");

            migrationBuilder.DropPrimaryKey(
                name: "PK_person_emails",
                table: "person_emails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_permissions",
                table: "permissions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_passengers",
                table: "passengers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_passenger_types",
                table: "passenger_types");

            migrationBuilder.DropPrimaryKey(
                name: "PK_email_domains",
                table: "email_domains");

            migrationBuilder.DropPrimaryKey(
                name: "PK_document_types",
                table: "document_types");

            migrationBuilder.DropPrimaryKey(
                name: "PK_directions",
                table: "directions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_clients",
                table: "clients");

            migrationBuilder.RenameTable(
                name: "users",
                newName: "usuarios");

            migrationBuilder.RenameTable(
                name: "system_roles",
                newName: "roles_sistema");

            migrationBuilder.RenameTable(
                name: "street_types",
                newName: "tipos_via");

            migrationBuilder.RenameTable(
                name: "sessions",
                newName: "sesiones");

            migrationBuilder.RenameTable(
                name: "role_permissions",
                newName: "roles_permisos");

            migrationBuilder.RenameTable(
                name: "phone_codes",
                newName: "codigos_telefono");

            migrationBuilder.RenameTable(
                name: "persons",
                newName: "personas");

            migrationBuilder.RenameTable(
                name: "person_phones",
                newName: "personas_telefonos");

            migrationBuilder.RenameTable(
                name: "person_emails",
                newName: "personas_emails");

            migrationBuilder.RenameTable(
                name: "permissions",
                newName: "permisos");

            migrationBuilder.RenameTable(
                name: "passengers",
                newName: "pasajeros");

            migrationBuilder.RenameTable(
                name: "passenger_types",
                newName: "tipos_pasajero");

            migrationBuilder.RenameTable(
                name: "email_domains",
                newName: "dominios_email");

            migrationBuilder.RenameTable(
                name: "document_types",
                newName: "tipos_documento");

            migrationBuilder.RenameTable(
                name: "directions",
                newName: "direcciones");

            migrationBuilder.RenameTable(
                name: "clients",
                newName: "clientes");

            migrationBuilder.RenameColumn(
                name: "country_id",
                table: "regions",
                newName: "Country_id");

            migrationBuilder.RenameIndex(
                name: "IX_regions_country_id",
                table: "regions",
                newName: "IX_regions_Country_id");

            migrationBuilder.RenameColumn(
                name: "username",
                table: "usuarios",
                newName: "nombre_usuario");

            migrationBuilder.RenameColumn(
                name: "person_id",
                table: "usuarios",
                newName: "persona_id");

            migrationBuilder.RenameColumn(
                name: "is_active",
                table: "usuarios",
                newName: "activo");

            migrationBuilder.RenameIndex(
                name: "IX_users_username",
                table: "usuarios",
                newName: "IX_usuarios_nombre_usuario");

            migrationBuilder.RenameIndex(
                name: "IX_users_rol_id",
                table: "usuarios",
                newName: "IX_usuarios_rol_id");

            migrationBuilder.RenameIndex(
                name: "IX_users_person_id",
                table: "usuarios",
                newName: "IX_usuarios_persona_id");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "roles_sistema",
                newName: "nombre");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "tipos_via",
                newName: "nombre");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "sesiones",
                newName: "usuario_id");

            migrationBuilder.RenameColumn(
                name: "is_revoked",
                table: "sesiones",
                newName: "revocada");

            migrationBuilder.RenameColumn(
                name: "expires_at_utc",
                table: "sesiones",
                newName: "expira_en_utc");

            migrationBuilder.RenameIndex(
                name: "IX_sessions_user_id",
                table: "sesiones",
                newName: "IX_sesiones_usuario_id");

            migrationBuilder.RenameColumn(
                name: "permission_id",
                table: "roles_permisos",
                newName: "permiso_id");

            migrationBuilder.RenameColumn(
                name: "role_id",
                table: "roles_permisos",
                newName: "rol_id");

            migrationBuilder.RenameIndex(
                name: "IX_role_permissions_permission_id",
                table: "roles_permisos",
                newName: "IX_roles_permisos_permiso_id");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "codigos_telefono",
                newName: "nombre");

            migrationBuilder.RenameColumn(
                name: "dial_code",
                table: "codigos_telefono",
                newName: "codigo");

            migrationBuilder.RenameIndex(
                name: "IX_phone_codes_dial_code",
                table: "codigos_telefono",
                newName: "IX_codigos_telefono_codigo");

            migrationBuilder.RenameColumn(
                name: "last_name",
                table: "personas",
                newName: "apellidos");

            migrationBuilder.RenameColumn(
                name: "gender",
                table: "personas",
                newName: "genero");

            migrationBuilder.RenameColumn(
                name: "first_name",
                table: "personas",
                newName: "nombres");

            migrationBuilder.RenameColumn(
                name: "document_type_id",
                table: "personas",
                newName: "tipo_documento_id");

            migrationBuilder.RenameColumn(
                name: "direction_id",
                table: "personas",
                newName: "direccion_id");

            migrationBuilder.RenameIndex(
                name: "IX_persons_document_type_id_numero_documento",
                table: "personas",
                newName: "IX_personas_tipo_documento_id_numero_documento");

            migrationBuilder.RenameIndex(
                name: "IX_persons_direction_id",
                table: "personas",
                newName: "IX_personas_direccion_id");

            migrationBuilder.RenameColumn(
                name: "phone_code_id",
                table: "personas_telefonos",
                newName: "codigo_telefono_id");

            migrationBuilder.RenameColumn(
                name: "person_id",
                table: "personas_telefonos",
                newName: "persona_id");

            migrationBuilder.RenameColumn(
                name: "number",
                table: "personas_telefonos",
                newName: "numero");

            migrationBuilder.RenameColumn(
                name: "is_primary",
                table: "personas_telefonos",
                newName: "es_principal");

            migrationBuilder.RenameIndex(
                name: "IX_person_phones_phone_code_id",
                table: "personas_telefonos",
                newName: "IX_personas_telefonos_codigo_telefono_id");

            migrationBuilder.RenameIndex(
                name: "IX_person_phones_person_id_phone_code_id_number",
                table: "personas_telefonos",
                newName: "IX_personas_telefonos_persona_id_codigo_telefono_id_numero");

            migrationBuilder.RenameColumn(
                name: "person_id",
                table: "personas_emails",
                newName: "persona_id");

            migrationBuilder.RenameColumn(
                name: "email_domain_id",
                table: "personas_emails",
                newName: "dominio_email_id");

            migrationBuilder.RenameIndex(
                name: "IX_person_emails_person_id",
                table: "personas_emails",
                newName: "IX_personas_emails_persona_id");

            migrationBuilder.RenameIndex(
                name: "IX_person_emails_email_domain_id",
                table: "personas_emails",
                newName: "IX_personas_emails_dominio_email_id");

            migrationBuilder.RenameIndex(
                name: "IX_person_emails_email",
                table: "personas_emails",
                newName: "IX_personas_emails_email");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "permisos",
                newName: "descripcion");

            migrationBuilder.RenameColumn(
                name: "code",
                table: "permisos",
                newName: "codigo");

            migrationBuilder.RenameIndex(
                name: "IX_permissions_code",
                table: "permisos",
                newName: "IX_permisos_codigo");

            migrationBuilder.RenameColumn(
                name: "person_id",
                table: "pasajeros",
                newName: "persona_id");

            migrationBuilder.RenameColumn(
                name: "passenger_type_id",
                table: "pasajeros",
                newName: "tipo_pasajero_id");

            migrationBuilder.RenameIndex(
                name: "IX_passengers_person_id",
                table: "pasajeros",
                newName: "IX_pasajeros_persona_id");

            migrationBuilder.RenameIndex(
                name: "IX_passengers_passenger_type_id",
                table: "pasajeros",
                newName: "IX_pasajeros_tipo_pasajero_id");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "tipos_pasajero",
                newName: "nombre");

            migrationBuilder.RenameColumn(
                name: "domain",
                table: "dominios_email",
                newName: "dominio");

            migrationBuilder.RenameIndex(
                name: "IX_email_domains_domain",
                table: "dominios_email",
                newName: "IX_dominios_email_dominio");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "tipos_documento",
                newName: "nombre");

            migrationBuilder.RenameColumn(
                name: "street_type_id",
                table: "direcciones",
                newName: "tipo_via_id");

            migrationBuilder.RenameColumn(
                name: "street_number",
                table: "direcciones",
                newName: "numero");

            migrationBuilder.RenameColumn(
                name: "postal_code",
                table: "direcciones",
                newName: "codigo_postal");

            migrationBuilder.RenameIndex(
                name: "IX_directions_street_type_id",
                table: "direcciones",
                newName: "IX_direcciones_tipo_via_id");

            migrationBuilder.RenameIndex(
                name: "IX_directions_ciudad_id",
                table: "direcciones",
                newName: "IX_direcciones_ciudad_id");

            migrationBuilder.RenameColumn(
                name: "person_id",
                table: "clientes",
                newName: "persona_id");

            migrationBuilder.RenameIndex(
                name: "IX_clients_person_id",
                table: "clientes",
                newName: "IX_clientes_persona_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_usuarios",
                table: "usuarios",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_roles_sistema",
                table: "roles_sistema",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tipos_via",
                table: "tipos_via",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_sesiones",
                table: "sesiones",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_roles_permisos",
                table: "roles_permisos",
                columns: new[] { "rol_id", "permiso_id" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_codigos_telefono",
                table: "codigos_telefono",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_personas",
                table: "personas",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_personas_telefonos",
                table: "personas_telefonos",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_personas_emails",
                table: "personas_emails",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_permisos",
                table: "permisos",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_pasajeros",
                table: "pasajeros",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tipos_pasajero",
                table: "tipos_pasajero",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_dominios_email",
                table: "dominios_email",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tipos_documento",
                table: "tipos_documento",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_direcciones",
                table: "direcciones",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_clientes",
                table: "clientes",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_clientes_personas_persona_id",
                table: "clientes",
                column: "persona_id",
                principalTable: "personas",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_direcciones_cities_ciudad_id",
                table: "direcciones",
                column: "ciudad_id",
                principalTable: "cities",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_direcciones_tipos_via_tipo_via_id",
                table: "direcciones",
                column: "tipo_via_id",
                principalTable: "tipos_via",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_pasajeros_personas_persona_id",
                table: "pasajeros",
                column: "persona_id",
                principalTable: "personas",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_pasajeros_tipos_pasajero_tipo_pasajero_id",
                table: "pasajeros",
                column: "tipo_pasajero_id",
                principalTable: "tipos_pasajero",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_personas_direcciones_direccion_id",
                table: "personas",
                column: "direccion_id",
                principalTable: "direcciones",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_personas_tipos_documento_tipo_documento_id",
                table: "personas",
                column: "tipo_documento_id",
                principalTable: "tipos_documento",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_personas_emails_dominios_email_dominio_email_id",
                table: "personas_emails",
                column: "dominio_email_id",
                principalTable: "dominios_email",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_personas_emails_personas_persona_id",
                table: "personas_emails",
                column: "persona_id",
                principalTable: "personas",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_personas_telefonos_codigos_telefono_codigo_telefono_id",
                table: "personas_telefonos",
                column: "codigo_telefono_id",
                principalTable: "codigos_telefono",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_personas_telefonos_personas_persona_id",
                table: "personas_telefonos",
                column: "persona_id",
                principalTable: "personas",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_regions_countries_Country_id",
                table: "regions",
                column: "Country_id",
                principalTable: "countries",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_roles_permisos_permisos_permiso_id",
                table: "roles_permisos",
                column: "permiso_id",
                principalTable: "permisos",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_roles_permisos_roles_sistema_rol_id",
                table: "roles_permisos",
                column: "rol_id",
                principalTable: "roles_sistema",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_sesiones_usuarios_usuario_id",
                table: "sesiones",
                column: "usuario_id",
                principalTable: "usuarios",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_usuarios_personas_persona_id",
                table: "usuarios",
                column: "persona_id",
                principalTable: "personas",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_usuarios_roles_sistema_rol_id",
                table: "usuarios",
                column: "rol_id",
                principalTable: "roles_sistema",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
