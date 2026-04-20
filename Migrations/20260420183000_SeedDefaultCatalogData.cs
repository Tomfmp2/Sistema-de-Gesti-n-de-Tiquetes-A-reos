using System.Linq;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sistema_gestor_de_tiquetes_aereos.Migrations
{
    /// <inheritdoc />
    public partial class SeedDefaultCatalogData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            InsertLookup(migrationBuilder, "continents", "name", "América", "Europa", "Asia", "África", "Oceanía");

            InsertCountries(migrationBuilder);
            InsertRegions(migrationBuilder);
            InsertCities(migrationBuilder);

            InsertLookup(migrationBuilder, "card_types", "name", "Crédito", "Débito", "Prepago");
            InsertLookup(migrationBuilder, "card_issuers", "name", "Visa", "Mastercard", "American Express", "Diners Club");
            InsertLookup(migrationBuilder, "payment_method_types", "name", "Tarjeta", "Efectivo", "Transferencia bancaria", "Billetera digital");
            InsertLookup(migrationBuilder, "payment_statuses", "name", "Pendiente", "Aprobado", "Rechazado", "Reembolsado", "Anulado");
            InsertPaymentMethods(migrationBuilder);

            InsertLookup(migrationBuilder, "reservation_statuses", "name", "Pendiente", "Confirmada", "Cancelada", "Expirada", "Completada");
            InsertLookup(migrationBuilder, "ticket_statuses", "name", "Emitido", "Usado", "Cancelado", "Reembolsado", "No presentado");
            InsertLookup(migrationBuilder, "checkin_statuses", "name", "Pendiente", "Realizado", "Cerrado", "Cancelado");
            InsertLookup(migrationBuilder, "flight_statuses", "name", "Programado", "Abordando", "En vuelo", "Aterrizado", "Retrasado", "Cancelado");

            InsertDocumentType(migrationBuilder, "CC", "Cédula de ciudadanía");
            InsertDocumentType(migrationBuilder, "CE", "Cédula de extranjería");
            InsertDocumentType(migrationBuilder, "PAS", "Pasaporte");
            InsertDocumentType(migrationBuilder, "TI", "Tarjeta de identidad");
            InsertDocumentType(migrationBuilder, "NIT", "NIT");

            InsertLookup(migrationBuilder, "email_domains", "domain", "gmail.com", "outlook.com", "hotmail.com", "yahoo.com", "icloud.com");
            InsertPhoneCodes(migrationBuilder);
            InsertLookup(migrationBuilder, "street_types", "name", "Calle", "Carrera", "Avenida", "Diagonal", "Transversal");
            InsertPassengerTypes(migrationBuilder);

            InsertLookup(migrationBuilder, "cabin_types", "name", "Económica", "Premium Economy", "Ejecutiva", "Primera clase");
            InsertLookup(migrationBuilder, "seat_location_types", "name", "Ventana", "Pasillo", "Centro");
            InsertSeasons(migrationBuilder);
            InsertLookup(migrationBuilder, "invoice_item_types", "name", "Tarifa aérea", "Impuestos", "Equipaje", "Servicio", "Descuento");
            InsertBaggageTypes(migrationBuilder);

            InsertLookup(migrationBuilder, "system_roles", "name", "Administrador", "Agente", "Cliente", "Operaciones");
            UpdateRoleDescription(migrationBuilder, "Administrador", "Acceso completo al sistema");
            UpdateRoleDescription(migrationBuilder, "Agente", "Gestión de ventas, reservas y atención al cliente");
            UpdateRoleDescription(migrationBuilder, "Cliente", "Acceso de autoservicio para pasajeros");
            UpdateRoleDescription(migrationBuilder, "Operaciones", "Gestión operativa de vuelos y tripulación");
            InsertLookup(migrationBuilder, "permissions", "name", "reservations.manage", "flights.manage", "catalogs.manage", "payments.manage", "reports.view");

            InsertLookup(migrationBuilder, "staff_positions", "Name", "Piloto", "Copiloto", "Tripulante de cabina", "Agente de puerta", "Técnico de mantenimiento");
            InsertLookup(migrationBuilder, "availability_statuses", "Name", "Disponible", "Asignado", "En descanso", "Incapacitado", "Vacaciones");
            InsertLookup(migrationBuilder, "flight_roles", "name", "Capitán", "Primer oficial", "Jefe de cabina", "Auxiliar de vuelo");
            InsertAircraftManufacturers(migrationBuilder);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            DeleteAircraftManufacturers(migrationBuilder);
            DeleteLookup(migrationBuilder, "flight_roles", "name", "Capitán", "Primer oficial", "Jefe de cabina", "Auxiliar de vuelo");
            DeleteLookup(migrationBuilder, "availability_statuses", "Name", "Disponible", "Asignado", "En descanso", "Incapacitado", "Vacaciones");
            DeleteLookup(migrationBuilder, "staff_positions", "Name", "Piloto", "Copiloto", "Tripulante de cabina", "Agente de puerta", "Técnico de mantenimiento");

            DeleteLookup(migrationBuilder, "permissions", "name", "reservations.manage", "flights.manage", "catalogs.manage", "payments.manage", "reports.view");
            DeleteLookup(migrationBuilder, "system_roles", "name", "Administrador", "Agente", "Cliente", "Operaciones");
            DeleteBaggageTypes(migrationBuilder);
            DeleteLookup(migrationBuilder, "invoice_item_types", "name", "Tarifa aérea", "Impuestos", "Equipaje", "Servicio", "Descuento");
            DeleteSeasons(migrationBuilder);
            DeleteLookup(migrationBuilder, "seat_location_types", "name", "Ventana", "Pasillo", "Centro");
            DeleteLookup(migrationBuilder, "cabin_types", "name", "Económica", "Premium Economy", "Ejecutiva", "Primera clase");
            DeletePassengerTypes(migrationBuilder);
            DeleteLookup(migrationBuilder, "street_types", "name", "Calle", "Carrera", "Avenida", "Diagonal", "Transversal");
            DeletePhoneCodes(migrationBuilder);
            DeleteLookup(migrationBuilder, "email_domains", "domain", "gmail.com", "outlook.com", "hotmail.com", "yahoo.com", "icloud.com");
            DeleteLookup(migrationBuilder, "document_types", "code", "CC", "CE", "PAS", "TI", "NIT");

            DeleteLookup(migrationBuilder, "flight_statuses", "name", "Programado", "Abordando", "En vuelo", "Aterrizado", "Retrasado", "Cancelado");
            DeleteLookup(migrationBuilder, "checkin_statuses", "name", "Pendiente", "Realizado", "Cerrado", "Cancelado");
            DeleteLookup(migrationBuilder, "ticket_statuses", "name", "Emitido", "Usado", "Cancelado", "Reembolsado", "No presentado");
            DeleteLookup(migrationBuilder, "reservation_statuses", "name", "Pendiente", "Confirmada", "Cancelada", "Expirada", "Completada");
            DeletePaymentMethods(migrationBuilder);
            DeleteLookup(migrationBuilder, "payment_statuses", "name", "Pendiente", "Aprobado", "Rechazado", "Reembolsado", "Anulado");
            DeleteLookup(migrationBuilder, "payment_method_types", "name", "Tarjeta", "Efectivo", "Transferencia bancaria", "Billetera digital");
            DeleteLookup(migrationBuilder, "card_issuers", "name", "Visa", "Mastercard", "American Express", "Diners Club");
            DeleteLookup(migrationBuilder, "card_types", "name", "Crédito", "Débito", "Prepago");

            DeleteCities(migrationBuilder);
            DeleteRegions(migrationBuilder);
            DeleteCountries(migrationBuilder);
        }

        private static void InsertCountries(MigrationBuilder migrationBuilder)
        {
            InsertCountry(migrationBuilder, "Colombia", "COL", "América");
            InsertCountry(migrationBuilder, "Estados Unidos", "USA", "América");
            InsertCountry(migrationBuilder, "México", "MEX", "América");
            InsertCountry(migrationBuilder, "España", "ESP", "Europa");
            InsertCountry(migrationBuilder, "Francia", "FRA", "Europa");
            InsertCountry(migrationBuilder, "Reino Unido", "GBR", "Europa");
            InsertCountry(migrationBuilder, "Alemania", "DEU", "Europa");
            InsertCountry(migrationBuilder, "Brasil", "BRA", "América");
            InsertCountry(migrationBuilder, "Argentina", "ARG", "América");
            InsertCountry(migrationBuilder, "Chile", "CHL", "América");
            InsertCountry(migrationBuilder, "Perú", "PER", "América");
            InsertCountry(migrationBuilder, "Canadá", "CAN", "América");
            InsertCountry(migrationBuilder, "Japón", "JPN", "Asia");
            InsertCountry(migrationBuilder, "China", "CHN", "Asia");
            InsertCountry(migrationBuilder, "Australia", "AUS", "Oceanía");
        }

        private static void InsertRegions(MigrationBuilder migrationBuilder)
        {
            InsertRegion(migrationBuilder, "Cundinamarca", "Departamento", "COL");
            InsertRegion(migrationBuilder, "Antioquia", "Departamento", "COL");
            InsertRegion(migrationBuilder, "Valle del Cauca", "Departamento", "COL");
            InsertRegion(migrationBuilder, "Atlántico", "Departamento", "COL");
            InsertRegion(migrationBuilder, "Florida", "Estado", "USA");
            InsertRegion(migrationBuilder, "Nueva York", "Estado", "USA");
            InsertRegion(migrationBuilder, "California", "Estado", "USA");
            InsertRegion(migrationBuilder, "Ciudad de México", "Entidad federativa", "MEX");
            InsertRegion(migrationBuilder, "Comunidad de Madrid", "Comunidad autónoma", "ESP");
            InsertRegion(migrationBuilder, "Île-de-France", "Región", "FRA");
            InsertRegion(migrationBuilder, "Inglaterra", "Nación constituyente", "GBR");
            InsertRegion(migrationBuilder, "São Paulo", "Estado", "BRA");
            InsertRegion(migrationBuilder, "Buenos Aires", "Provincia", "ARG");
            InsertRegion(migrationBuilder, "Región Metropolitana", "Región", "CHL");
            InsertRegion(migrationBuilder, "Lima", "Departamento", "PER");
            InsertRegion(migrationBuilder, "Ontario", "Provincia", "CAN");
            InsertRegion(migrationBuilder, "Tokio", "Prefectura", "JPN");
            InsertRegion(migrationBuilder, "Nueva Gales del Sur", "Estado", "AUS");
        }

        private static void InsertCities(MigrationBuilder migrationBuilder)
        {
            InsertCity(migrationBuilder, "Bogotá", "Cundinamarca", "COL");
            InsertCity(migrationBuilder, "Medellín", "Antioquia", "COL");
            InsertCity(migrationBuilder, "Cali", "Valle del Cauca", "COL");
            InsertCity(migrationBuilder, "Barranquilla", "Atlántico", "COL");
            InsertCity(migrationBuilder, "Miami", "Florida", "USA");
            InsertCity(migrationBuilder, "Nueva York", "Nueva York", "USA");
            InsertCity(migrationBuilder, "Los Ángeles", "California", "USA");
            InsertCity(migrationBuilder, "Ciudad de México", "Ciudad de México", "MEX");
            InsertCity(migrationBuilder, "Madrid", "Comunidad de Madrid", "ESP");
            InsertCity(migrationBuilder, "París", "Île-de-France", "FRA");
            InsertCity(migrationBuilder, "Londres", "Inglaterra", "GBR");
            InsertCity(migrationBuilder, "São Paulo", "São Paulo", "BRA");
            InsertCity(migrationBuilder, "Buenos Aires", "Buenos Aires", "ARG");
            InsertCity(migrationBuilder, "Santiago", "Región Metropolitana", "CHL");
            InsertCity(migrationBuilder, "Lima", "Lima", "PER");
            InsertCity(migrationBuilder, "Toronto", "Ontario", "CAN");
            InsertCity(migrationBuilder, "Tokio", "Tokio", "JPN");
            InsertCity(migrationBuilder, "Sídney", "Nueva Gales del Sur", "AUS");
        }

        private static void InsertPaymentMethods(MigrationBuilder migrationBuilder)
        {
            InsertPaymentMethod(migrationBuilder, "Visa crédito", "Tarjeta", "Crédito", "Visa");
            InsertPaymentMethod(migrationBuilder, "Mastercard crédito", "Tarjeta", "Crédito", "Mastercard");
            InsertPaymentMethod(migrationBuilder, "Visa débito", "Tarjeta", "Débito", "Visa");
            InsertPaymentMethod(migrationBuilder, "American Express crédito", "Tarjeta", "Crédito", "American Express");
            InsertPaymentMethod(migrationBuilder, "Efectivo en oficina", "Efectivo", null, null);
            InsertPaymentMethod(migrationBuilder, "Transferencia bancaria", "Transferencia bancaria", null, null);
        }

        private static void InsertPhoneCodes(MigrationBuilder migrationBuilder)
        {
            InsertPhoneCode(migrationBuilder, "+57", "Colombia");
            InsertPhoneCode(migrationBuilder, "+1", "Estados Unidos / Canadá");
            InsertPhoneCode(migrationBuilder, "+52", "México");
            InsertPhoneCode(migrationBuilder, "+34", "España");
            InsertPhoneCode(migrationBuilder, "+33", "Francia");
            InsertPhoneCode(migrationBuilder, "+44", "Reino Unido");
            InsertPhoneCode(migrationBuilder, "+55", "Brasil");
            InsertPhoneCode(migrationBuilder, "+54", "Argentina");
            InsertPhoneCode(migrationBuilder, "+56", "Chile");
            InsertPhoneCode(migrationBuilder, "+51", "Perú");
        }

        private static void InsertPassengerTypes(MigrationBuilder migrationBuilder)
        {
            InsertPassengerType(migrationBuilder, "Infante", 0, 1);
            InsertPassengerType(migrationBuilder, "Niño", 2, 11);
            InsertPassengerType(migrationBuilder, "Adulto", 12, null);
            InsertPassengerType(migrationBuilder, "Adulto mayor", 60, null);
        }

        private static void InsertSeasons(MigrationBuilder migrationBuilder)
        {
            InsertSeason(migrationBuilder, "Baja", "Temporada de menor demanda", 0.9000m);
            InsertSeason(migrationBuilder, "Media", "Temporada regular", 1.0000m);
            InsertSeason(migrationBuilder, "Alta", "Temporada de alta demanda", 1.2500m);
            InsertSeason(migrationBuilder, "Festiva", "Temporada de festivos y vacaciones", 1.4000m);
        }

        private static void InsertBaggageTypes(MigrationBuilder migrationBuilder)
        {
            InsertBaggageType(migrationBuilder, "Artículo personal", 5.00m, 0.00m);
            InsertBaggageType(migrationBuilder, "Equipaje de mano", 10.00m, 0.00m);
            InsertBaggageType(migrationBuilder, "Bodega estándar", 23.00m, 60000.00m);
            InsertBaggageType(migrationBuilder, "Bodega extra", 32.00m, 120000.00m);
        }

        private static void InsertAircraftManufacturers(MigrationBuilder migrationBuilder)
        {
            InsertAircraftManufacturer(migrationBuilder, "Airbus", "Francia");
            InsertAircraftManufacturer(migrationBuilder, "Boeing", "Estados Unidos");
            InsertAircraftManufacturer(migrationBuilder, "Embraer", "Brasil");
            InsertAircraftManufacturer(migrationBuilder, "ATR", "Francia");
            InsertAircraftManufacturer(migrationBuilder, "De Havilland Canada", "Canadá");
        }

        private static void InsertLookup(MigrationBuilder migrationBuilder, string table, string column, params string[] values)
        {
            foreach (var value in values)
            {
                migrationBuilder.Sql($"""
                    INSERT INTO `{table}` (`{column}`)
                    SELECT {Sql(value)}
                    WHERE NOT EXISTS (
                        SELECT 1 FROM `{table}` WHERE `{column}` = {Sql(value)}
                    );
                    """);
            }
        }

        private static void DeleteLookup(MigrationBuilder migrationBuilder, string table, string column, params string[] values)
        {
            migrationBuilder.Sql($"""
                DELETE FROM `{table}`
                WHERE `{column}` IN ({string.Join(", ", values.Select(Sql))});
                """);
        }

        private static void InsertCountry(MigrationBuilder migrationBuilder, string name, string codeIso, string continentName)
        {
            migrationBuilder.Sql($"""
                INSERT INTO `countries` (`name`, `code_iso`, `continent_id`)
                SELECT {Sql(name)}, {Sql(codeIso)}, c.`id`
                FROM `continents` c
                WHERE c.`name` = {Sql(continentName)}
                  AND NOT EXISTS (
                      SELECT 1 FROM `countries` x WHERE x.`code_iso` = {Sql(codeIso)}
                  );
                """);
        }

        private static void InsertRegion(MigrationBuilder migrationBuilder, string name, string type, string countryCode)
        {
            migrationBuilder.Sql($"""
                INSERT INTO `regions` (`name`, `type`, `country_id`)
                SELECT {Sql(name)}, {Sql(type)}, c.`id`
                FROM `countries` c
                WHERE c.`code_iso` = {Sql(countryCode)}
                  AND NOT EXISTS (
                      SELECT 1 FROM `regions` r
                      WHERE r.`name` = {Sql(name)} AND r.`country_id` = c.`id`
                  );
                """);
        }

        private static void InsertCity(MigrationBuilder migrationBuilder, string name, string regionName, string countryCode)
        {
            migrationBuilder.Sql($"""
                INSERT INTO `cities` (`name`, `region_id`)
                SELECT {Sql(name)}, r.`id`
                FROM `regions` r
                INNER JOIN `countries` c ON c.`id` = r.`country_id`
                WHERE r.`name` = {Sql(regionName)}
                  AND c.`code_iso` = {Sql(countryCode)}
                  AND NOT EXISTS (
                      SELECT 1 FROM `cities` ci
                      WHERE ci.`name` = {Sql(name)} AND ci.`region_id` = r.`id`
                  )
                LIMIT 1;
                """);
        }

        private static void InsertPaymentMethod(MigrationBuilder migrationBuilder, string commercialName, string methodType, string cardType, string cardIssuer)
        {
            migrationBuilder.Sql($"""
                INSERT INTO `payment_methods` (`payment_method_type_id`, `card_type_id`, `card_issuer_id`, `commercial_name`)
                SELECT pmt.`id`, ct.`id`, ci.`id`, {Sql(commercialName)}
                FROM `payment_method_types` pmt
                LEFT JOIN `card_types` ct ON {NullableSql(cardType)} IS NOT NULL AND ct.`name` = {NullableSql(cardType)}
                LEFT JOIN `card_issuers` ci ON {NullableSql(cardIssuer)} IS NOT NULL AND ci.`name` = {NullableSql(cardIssuer)}
                WHERE pmt.`name` = {Sql(methodType)}
                  AND NOT EXISTS (
                      SELECT 1 FROM `payment_methods` pm WHERE pm.`commercial_name` = {Sql(commercialName)}
                  )
                LIMIT 1;
                """);
        }

        private static void InsertPhoneCode(MigrationBuilder migrationBuilder, string dialCode, string countryName)
        {
            migrationBuilder.Sql($"""
                INSERT INTO `phone_codes` (`country_dial_code`, `country_name`)
                SELECT {Sql(dialCode)}, {Sql(countryName)}
                WHERE NOT EXISTS (
                    SELECT 1 FROM `phone_codes` WHERE `country_dial_code` = {Sql(dialCode)}
                );
                """);
        }

        private static void InsertDocumentType(MigrationBuilder migrationBuilder, string code, string name)
        {
            migrationBuilder.Sql($"""
                INSERT INTO `document_types` (`code`, `name`)
                SELECT {Sql(code)}, {Sql(name)}
                WHERE NOT EXISTS (
                    SELECT 1 FROM `document_types` WHERE `code` = {Sql(code)}
                );
                """);
        }

        private static void InsertPassengerType(MigrationBuilder migrationBuilder, string name, int? minAge, int? maxAge)
        {
            migrationBuilder.Sql($"""
                INSERT INTO `passenger_types` (`name`, `min_age`, `max_age`)
                SELECT {Sql(name)}, {NullableNumber(minAge)}, {NullableNumber(maxAge)}
                WHERE NOT EXISTS (
                    SELECT 1 FROM `passenger_types` WHERE `name` = {Sql(name)}
                );
                """);
        }

        private static void InsertSeason(MigrationBuilder migrationBuilder, string name, string description, decimal priceFactor)
        {
            migrationBuilder.Sql($"""
                INSERT INTO `seasons` (`name`, `description`, `price_factor`)
                SELECT {Sql(name)}, {Sql(description)}, {SqlDecimal(priceFactor)}
                WHERE NOT EXISTS (
                    SELECT 1 FROM `seasons` WHERE `name` = {Sql(name)}
                );
                """);
        }

        private static void InsertBaggageType(MigrationBuilder migrationBuilder, string name, decimal maxWeightKg, decimal basePrice)
        {
            migrationBuilder.Sql($"""
                INSERT INTO `baggage_types` (`name`, `max_weight_kg`, `base_price`)
                SELECT {Sql(name)}, {SqlDecimal(maxWeightKg)}, {SqlDecimal(basePrice)}
                WHERE NOT EXISTS (
                    SELECT 1 FROM `baggage_types` WHERE `name` = {Sql(name)}
                );
                """);
        }

        private static void InsertAircraftManufacturer(MigrationBuilder migrationBuilder, string name, string country)
        {
            migrationBuilder.Sql($"""
                INSERT INTO `aircraft_manufacturers` (`name`, `country`)
                SELECT {Sql(name)}, {Sql(country)}
                WHERE NOT EXISTS (
                    SELECT 1 FROM `aircraft_manufacturers` WHERE `name` = {Sql(name)}
                );
                """);
        }

        private static void UpdateRoleDescription(MigrationBuilder migrationBuilder, string name, string description)
        {
            migrationBuilder.Sql($"""
                UPDATE `system_roles`
                SET `description` = {Sql(description)}
                WHERE `name` = {Sql(name)} AND `description` IS NULL;
                """);
        }

        private static void DeleteCountries(MigrationBuilder migrationBuilder)
        {
            DeleteLookup(migrationBuilder, "countries", "code_iso", "COL", "USA", "MEX", "ESP", "FRA", "GBR", "DEU", "BRA", "ARG", "CHL", "PER", "CAN", "JPN", "CHN", "AUS");
        }

        private static void DeleteRegions(MigrationBuilder migrationBuilder)
        {
            DeleteLookup(migrationBuilder, "regions", "name", "Cundinamarca", "Antioquia", "Valle del Cauca", "Atlántico", "Florida", "Nueva York", "California", "Ciudad de México", "Comunidad de Madrid", "Île-de-France", "Inglaterra", "São Paulo", "Buenos Aires", "Región Metropolitana", "Lima", "Ontario", "Tokio", "Nueva Gales del Sur");
        }

        private static void DeleteCities(MigrationBuilder migrationBuilder)
        {
            DeleteLookup(migrationBuilder, "cities", "name", "Bogotá", "Medellín", "Cali", "Barranquilla", "Miami", "Nueva York", "Los Ángeles", "Ciudad de México", "Madrid", "París", "Londres", "São Paulo", "Buenos Aires", "Santiago", "Lima", "Toronto", "Tokio", "Sídney");
        }

        private static void DeletePaymentMethods(MigrationBuilder migrationBuilder)
        {
            DeleteLookup(migrationBuilder, "payment_methods", "commercial_name", "Visa crédito", "Mastercard crédito", "Visa débito", "American Express crédito", "Efectivo en oficina", "Transferencia bancaria");
        }

        private static void DeletePhoneCodes(MigrationBuilder migrationBuilder)
        {
            DeleteLookup(migrationBuilder, "phone_codes", "country_dial_code", "+57", "+1", "+52", "+34", "+33", "+44", "+55", "+54", "+56", "+51");
        }

        private static void DeletePassengerTypes(MigrationBuilder migrationBuilder)
        {
            DeleteLookup(migrationBuilder, "passenger_types", "name", "Infante", "Niño", "Adulto", "Adulto mayor");
        }

        private static void DeleteSeasons(MigrationBuilder migrationBuilder)
        {
            DeleteLookup(migrationBuilder, "seasons", "name", "Baja", "Media", "Alta", "Festiva");
        }

        private static void DeleteBaggageTypes(MigrationBuilder migrationBuilder)
        {
            DeleteLookup(migrationBuilder, "baggage_types", "name", "Artículo personal", "Equipaje de mano", "Bodega estándar", "Bodega extra");
        }

        private static void DeleteAircraftManufacturers(MigrationBuilder migrationBuilder)
        {
            DeleteLookup(migrationBuilder, "aircraft_manufacturers", "name", "Airbus", "Boeing", "Embraer", "ATR", "De Havilland Canada");
        }

        private static string Sql(string value)
        {
            return $"'{value.Replace("'", "''")}'";
        }

        private static string NullableSql(string value)
        {
            return value is null ? "NULL" : Sql(value);
        }

        private static string NullableNumber(int? value)
        {
            return value?.ToString() ?? "NULL";
        }

        private static string SqlDecimal(decimal value)
        {
            return value.ToString(System.Globalization.CultureInfo.InvariantCulture);
        }
    }
}
