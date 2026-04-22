using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional
#pragma warning disable CS0162 // Unreachable code (intentional: snapshot-only migration)

namespace sistema_gestor_de_tiquetes_aereos.Migrations
{
    /// <inheritdoc />
    public partial class SyncModelSnapshot : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Snapshot-only migration: no schema operations.
            return;
            migrationBuilder.DropForeignKey(
                name: "FK_addresses_cities_city_id",
                table: "addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_addresses_street_types_street_type_id",
                table: "addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_aircraft_aircraft_models_model_id",
                table: "aircraft");

            migrationBuilder.DropForeignKey(
                name: "FK_aircraft_airlines_airline_id",
                table: "aircraft");

            migrationBuilder.DropForeignKey(
                name: "FK_aircraft_models_aircraft_manufacturers_manufacturer_id",
                table: "aircraft_models");

            migrationBuilder.DropForeignKey(
                name: "FK_airlines_countries_origin_country_id",
                table: "airlines");

            migrationBuilder.DropForeignKey(
                name: "FK_airports_cities_city_id",
                table: "airports");

            migrationBuilder.DropForeignKey(
                name: "FK_baggage_baggage_types_baggage_type_id",
                table: "baggage");

            migrationBuilder.DropForeignKey(
                name: "FK_cabin_configurations_cabin_types_cabin_type_id",
                table: "cabin_configurations");

            migrationBuilder.DropForeignKey(
                name: "FK_check_ins_checkin_statuses_checkin_status_id",
                table: "check_ins");

            migrationBuilder.DropForeignKey(
                name: "FK_check_ins_flight_seats_flight_seat_id",
                table: "check_ins");

            migrationBuilder.DropForeignKey(
                name: "FK_check_ins_staff_staff_id",
                table: "check_ins");

            migrationBuilder.DropForeignKey(
                name: "FK_check_ins_tickets_ticket_id",
                table: "check_ins");

            migrationBuilder.DropForeignKey(
                name: "FK_cities_regions_region_id",
                table: "cities");

            migrationBuilder.DropForeignKey(
                name: "FK_clients_persons_person_id",
                table: "clients");

            migrationBuilder.DropForeignKey(
                name: "FK_countries_continents_continent_id",
                table: "countries");

            migrationBuilder.DropForeignKey(
                name: "FK_fares_cabin_types_cabin_type_id",
                table: "fares");

            migrationBuilder.DropForeignKey(
                name: "FK_fares_passenger_types_passenger_type_id",
                table: "fares");

            migrationBuilder.DropForeignKey(
                name: "FK_fares_routes_route_id",
                table: "fares");

            migrationBuilder.DropForeignKey(
                name: "FK_fares_seasons_season_id",
                table: "fares");

            migrationBuilder.DropForeignKey(
                name: "FK_flight_seats_cabin_types_cabin_type_id",
                table: "flight_seats");

            migrationBuilder.DropForeignKey(
                name: "FK_flight_seats_flights_flight_id",
                table: "flight_seats");

            migrationBuilder.DropForeignKey(
                name: "FK_flight_seats_seat_location_types_location_type_id",
                table: "flight_seats");

            migrationBuilder.DropForeignKey(
                name: "FK_flight_status_transitions_flight_statuses_from_status_id",
                table: "flight_status_transitions");

            migrationBuilder.DropForeignKey(
                name: "FK_flight_status_transitions_flight_statuses_to_status_id",
                table: "flight_status_transitions");

            migrationBuilder.DropForeignKey(
                name: "FK_flights_aircraft_aircraft_id",
                table: "flights");

            migrationBuilder.DropForeignKey(
                name: "FK_flights_airlines_airline_id",
                table: "flights");

            migrationBuilder.DropForeignKey(
                name: "FK_flights_flight_statuses_flight_status_id",
                table: "flights");

            migrationBuilder.DropForeignKey(
                name: "FK_flights_routes_route_id",
                table: "flights");

            migrationBuilder.DropForeignKey(
                name: "FK_invoice_items_invoice_item_types_invoice_item_type_id",
                table: "invoice_items");

            migrationBuilder.DropForeignKey(
                name: "FK_passengers_passenger_types_passenger_type_id",
                table: "passengers");

            migrationBuilder.DropForeignKey(
                name: "FK_passengers_persons_person_id",
                table: "passengers");

            migrationBuilder.DropForeignKey(
                name: "FK_payment_methods_card_issuers_card_issuer_id",
                table: "payment_methods");

            migrationBuilder.DropForeignKey(
                name: "FK_payment_methods_card_types_card_type_id",
                table: "payment_methods");

            migrationBuilder.DropForeignKey(
                name: "FK_payment_methods_payment_method_types_payment_method_type_id",
                table: "payment_methods");

            migrationBuilder.DropForeignKey(
                name: "FK_payments_payment_methods_payment_method_id",
                table: "payments");

            migrationBuilder.DropForeignKey(
                name: "FK_payments_payment_statuses_payment_status_id",
                table: "payments");

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
                name: "FK_routes_airports_destination_airport_id",
                table: "routes");

            migrationBuilder.DropForeignKey(
                name: "FK_routes_airports_origin_airport_id",
                table: "routes");

            migrationBuilder.DropForeignKey(
                name: "FK_staff_staff_positions_position_id",
                table: "staff");

            migrationBuilder.DropForeignKey(
                name: "FK_tickets_ticket_statuses_ticket_status_id",
                table: "tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_users_system_roles_role_id",
                table: "users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_continents",
                table: "continents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ticket_statuses",
                table: "ticket_statuses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_system_roles",
                table: "system_roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_street_types",
                table: "street_types");

            migrationBuilder.DropPrimaryKey(
                name: "PK_staff_positions",
                table: "staff_positions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_seat_location_types",
                table: "seat_location_types");

            migrationBuilder.DropPrimaryKey(
                name: "PK_role_permissions",
                table: "role_permissions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_phone_codes",
                table: "phone_codes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_person_phones",
                table: "person_phones");

            migrationBuilder.DropPrimaryKey(
                name: "PK_person_emails",
                table: "person_emails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_payment_statuses",
                table: "payment_statuses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_payment_methods",
                table: "payment_methods");

            migrationBuilder.DropPrimaryKey(
                name: "PK_payment_method_types",
                table: "payment_method_types");

            migrationBuilder.DropPrimaryKey(
                name: "PK_passenger_types",
                table: "passenger_types");

            migrationBuilder.DropPrimaryKey(
                name: "PK_invoice_item_types",
                table: "invoice_item_types");

            migrationBuilder.DropPrimaryKey(
                name: "PK_flight_statuses",
                table: "flight_statuses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_flight_seats",
                table: "flight_seats");

            migrationBuilder.DropPrimaryKey(
                name: "PK_email_domains",
                table: "email_domains");

            migrationBuilder.DropPrimaryKey(
                name: "PK_document_types",
                table: "document_types");

            migrationBuilder.DropPrimaryKey(
                name: "PK_checkin_statuses",
                table: "checkin_statuses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_card_types",
                table: "card_types");

            migrationBuilder.DropPrimaryKey(
                name: "PK_card_issuers",
                table: "card_issuers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_cabin_types",
                table: "cabin_types");

            migrationBuilder.DropPrimaryKey(
                name: "PK_baggage_types",
                table: "baggage_types");

            migrationBuilder.DropPrimaryKey(
                name: "PK_aircraft_manufacturers",
                table: "aircraft_manufacturers");

            migrationBuilder.RenameTable(
                name: "continents",
                newName: "Continents");

            migrationBuilder.RenameTable(
                name: "ticket_statuses",
                newName: "TicketStatuses");

            migrationBuilder.RenameTable(
                name: "system_roles",
                newName: "SystemRoles");

            migrationBuilder.RenameTable(
                name: "street_types",
                newName: "StreetTypes");

            migrationBuilder.RenameTable(
                name: "staff_positions",
                newName: "StaffPositions");

            migrationBuilder.RenameTable(
                name: "seat_location_types",
                newName: "SeatLocationTypes");

            migrationBuilder.RenameTable(
                name: "role_permissions",
                newName: "RolePermissions");

            migrationBuilder.RenameTable(
                name: "phone_codes",
                newName: "PhoneCodes");

            migrationBuilder.RenameTable(
                name: "person_phones",
                newName: "PersonPhones");

            migrationBuilder.RenameTable(
                name: "person_emails",
                newName: "PersonEmails");

            migrationBuilder.RenameTable(
                name: "payment_statuses",
                newName: "PaymentStatuses");

            migrationBuilder.RenameTable(
                name: "payment_methods",
                newName: "PaymentMethods");

            migrationBuilder.RenameTable(
                name: "payment_method_types",
                newName: "PaymentMethodTypes");

            migrationBuilder.RenameTable(
                name: "passenger_types",
                newName: "PassengerTypes");

            migrationBuilder.RenameTable(
                name: "invoice_item_types",
                newName: "InvoiceItemTypes");

            migrationBuilder.RenameTable(
                name: "flight_statuses",
                newName: "FlightStatuses");

            migrationBuilder.RenameTable(
                name: "flight_seats",
                newName: "FlightSeats");

            migrationBuilder.RenameTable(
                name: "email_domains",
                newName: "EmailDomains");

            migrationBuilder.RenameTable(
                name: "document_types",
                newName: "DocumentTypes");

            migrationBuilder.RenameTable(
                name: "checkin_statuses",
                newName: "CheckinStatuses");

            migrationBuilder.RenameTable(
                name: "card_types",
                newName: "CardTypes");

            migrationBuilder.RenameTable(
                name: "card_issuers",
                newName: "CardIssuers");

            migrationBuilder.RenameTable(
                name: "cabin_types",
                newName: "CabinTypes");

            migrationBuilder.RenameTable(
                name: "baggage_types",
                newName: "BaggageTypes");

            migrationBuilder.RenameTable(
                name: "aircraft_manufacturers",
                newName: "AircraftManufacturers");

            migrationBuilder.RenameColumn(
                name: "ip_address",
                table: "sessions",
                newName: "origin_ip");

            migrationBuilder.RenameColumn(
                name: "ended_at",
                table: "sessions",
                newName: "closed_at");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "seasons",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "seasons",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_seasons_name",
                table: "seasons",
                newName: "IX_seasons_Name");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "routes",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "origin_airport_id",
                table: "routes",
                newName: "Origin_airportId");

            migrationBuilder.RenameColumn(
                name: "destination_airport_id",
                table: "routes",
                newName: "Destination_airportId");

            migrationBuilder.RenameIndex(
                name: "IX_routes_origin_airport_id_destination_airport_id",
                table: "routes",
                newName: "IX_routes_Origin_airportId_Destination_airportId");

            migrationBuilder.RenameIndex(
                name: "IX_routes_destination_airport_id",
                table: "routes",
                newName: "IX_routes_Destination_airportId");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "regions",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "regions",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "country_id",
                table: "regions",
                newName: "CountryId");

            migrationBuilder.RenameIndex(
                name: "IX_regions_country_id",
                table: "regions",
                newName: "IX_regions_CountryId");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "permissions",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "permissions",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_permissions_name",
                table: "permissions",
                newName: "IX_permissions_Name");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "passengers",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "person_id",
                table: "passengers",
                newName: "PersonId");

            migrationBuilder.RenameColumn(
                name: "passenger_type_id",
                table: "passengers",
                newName: "PassengerTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_passengers_person_id",
                table: "passengers",
                newName: "IX_passengers_PersonId");

            migrationBuilder.RenameIndex(
                name: "IX_passengers_passenger_type_id",
                table: "passengers",
                newName: "IX_passengers_PassengerTypeId");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "flights",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "flights",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "route_id",
                table: "flights",
                newName: "RouteId");

            migrationBuilder.RenameColumn(
                name: "flight_status_id",
                table: "flights",
                newName: "FlightstatusId");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "flights",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "airline_id",
                table: "flights",
                newName: "AirlineId");

            migrationBuilder.RenameColumn(
                name: "aircraft_id",
                table: "flights",
                newName: "AircraftId");

            migrationBuilder.RenameIndex(
                name: "IX_flights_route_id",
                table: "flights",
                newName: "IX_flights_RouteId");

            migrationBuilder.RenameIndex(
                name: "IX_flights_flight_status_id",
                table: "flights",
                newName: "IX_flights_FlightstatusId");

            migrationBuilder.RenameIndex(
                name: "IX_flights_airline_id",
                table: "flights",
                newName: "IX_flights_AirlineId");

            migrationBuilder.RenameIndex(
                name: "IX_flights_aircraft_id",
                table: "flights",
                newName: "IX_flights_AircraftId");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "fares",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "valid_to",
                table: "fares",
                newName: "ValidTo");

            migrationBuilder.RenameColumn(
                name: "valid_from",
                table: "fares",
                newName: "ValidFrom");

            migrationBuilder.RenameColumn(
                name: "season_id",
                table: "fares",
                newName: "SeasonId");

            migrationBuilder.RenameColumn(
                name: "route_id",
                table: "fares",
                newName: "RouteId");

            migrationBuilder.RenameColumn(
                name: "passenger_type_id",
                table: "fares",
                newName: "PassengerTypeId");

            migrationBuilder.RenameColumn(
                name: "cabin_type_id",
                table: "fares",
                newName: "CabinTypeId");

            migrationBuilder.RenameColumn(
                name: "base_price",
                table: "fares",
                newName: "BasePrice");

            migrationBuilder.RenameIndex(
                name: "IX_fares_season_id",
                table: "fares",
                newName: "IX_fares_SeasonId");

            migrationBuilder.RenameIndex(
                name: "IX_fares_route_id",
                table: "fares",
                newName: "IX_fares_RouteId");

            migrationBuilder.RenameIndex(
                name: "IX_fares_passenger_type_id",
                table: "fares",
                newName: "IX_fares_PassengerTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_fares_cabin_type_id",
                table: "fares",
                newName: "IX_fares_CabinTypeId");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "countries",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "countries",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "continent_id",
                table: "countries",
                newName: "ContinentId");

            migrationBuilder.RenameColumn(
                name: "code_iso",
                table: "countries",
                newName: "CodeIso");

            migrationBuilder.RenameIndex(
                name: "IX_countries_continent_id",
                table: "countries",
                newName: "IX_countries_ContinentId");

            migrationBuilder.RenameIndex(
                name: "IX_countries_code_iso",
                table: "countries",
                newName: "IX_countries_CodeIso");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Continents",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Continents",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_continents_name",
                table: "Continents",
                newName: "IX_Continents_Name");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "clients",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "person_id",
                table: "clients",
                newName: "PersonId");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "clients",
                newName: "CreatedAt");

            migrationBuilder.RenameIndex(
                name: "IX_clients_person_id",
                table: "clients",
                newName: "IX_clients_PersonId");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "cities",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "cities",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "region_id",
                table: "cities",
                newName: "RegionId");

            migrationBuilder.RenameIndex(
                name: "IX_cities_region_id",
                table: "cities",
                newName: "IX_cities_RegionId");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "check_ins",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ticket_id",
                table: "check_ins",
                newName: "TicketId");

            migrationBuilder.RenameColumn(
                name: "staff_id",
                table: "check_ins",
                newName: "StaffId");

            migrationBuilder.RenameColumn(
                name: "flight_seat_id",
                table: "check_ins",
                newName: "Flight_seatId");

            migrationBuilder.RenameColumn(
                name: "checkin_status_id",
                table: "check_ins",
                newName: "CheckinstatusId");

            migrationBuilder.RenameIndex(
                name: "IX_check_ins_ticket_id",
                table: "check_ins",
                newName: "IX_check_ins_TicketId");

            migrationBuilder.RenameIndex(
                name: "IX_check_ins_staff_id",
                table: "check_ins",
                newName: "IX_check_ins_StaffId");

            migrationBuilder.RenameIndex(
                name: "IX_check_ins_flight_seat_id",
                table: "check_ins",
                newName: "IX_check_ins_Flight_seatId");

            migrationBuilder.RenameIndex(
                name: "IX_check_ins_checkin_status_id",
                table: "check_ins",
                newName: "IX_check_ins_CheckinstatusId");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "airports",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "airports",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "city_id",
                table: "airports",
                newName: "CityId");

            migrationBuilder.RenameIndex(
                name: "IX_airports_city_id",
                table: "airports",
                newName: "IX_airports_CityId");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "airlines",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "airlines",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "airlines",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "origin_country_id",
                table: "airlines",
                newName: "Origin_countryId");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "airlines",
                newName: "CreatedAt");

            migrationBuilder.RenameIndex(
                name: "IX_airlines_origin_country_id",
                table: "airlines",
                newName: "IX_airlines_Origin_countryId");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "aircraft_models",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "manufacturer_id",
                table: "aircraft_models",
                newName: "ManufacturerId");

            migrationBuilder.RenameIndex(
                name: "IX_aircraft_models_manufacturer_id",
                table: "aircraft_models",
                newName: "IX_aircraft_models_ManufacturerId");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "aircraft",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "model_id",
                table: "aircraft",
                newName: "ModelId");

            migrationBuilder.RenameColumn(
                name: "airline_id",
                table: "aircraft",
                newName: "AirlineId");

            migrationBuilder.RenameIndex(
                name: "IX_aircraft_model_id",
                table: "aircraft",
                newName: "IX_aircraft_ModelId");

            migrationBuilder.RenameIndex(
                name: "IX_aircraft_airline_id",
                table: "aircraft",
                newName: "IX_aircraft_AirlineId");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "addresses",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "street_type_id",
                table: "addresses",
                newName: "Street_typeId");

            migrationBuilder.RenameColumn(
                name: "city_id",
                table: "addresses",
                newName: "CityId");

            migrationBuilder.RenameIndex(
                name: "IX_addresses_street_type_id",
                table: "addresses",
                newName: "IX_addresses_Street_typeId");

            migrationBuilder.RenameIndex(
                name: "IX_addresses_city_id",
                table: "addresses",
                newName: "IX_addresses_CityId");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "TicketStatuses",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "TicketStatuses",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "SystemRoles",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "SystemRoles",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_system_roles_name",
                table: "SystemRoles",
                newName: "IX_SystemRoles_Name");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "StreetTypes",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "StreetTypes",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_staff_positions_Name",
                table: "StaffPositions",
                newName: "IX_StaffPositions_Name");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "SeatLocationTypes",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "SeatLocationTypes",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_seat_location_types_name",
                table: "SeatLocationTypes",
                newName: "IX_SeatLocationTypes_Name");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "RolePermissions",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "role_id",
                table: "RolePermissions",
                newName: "RoleId");

            migrationBuilder.RenameColumn(
                name: "permission_id",
                table: "RolePermissions",
                newName: "PermissionId");

            migrationBuilder.RenameIndex(
                name: "IX_role_permissions_role_id_permission_id",
                table: "RolePermissions",
                newName: "IX_RolePermissions_RoleId_PermissionId");

            migrationBuilder.RenameIndex(
                name: "IX_role_permissions_permission_id",
                table: "RolePermissions",
                newName: "IX_RolePermissions_PermissionId");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "PhoneCodes",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_phone_codes_country_code",
                table: "PhoneCodes",
                newName: "IX_PhoneCodes_country_code");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "PersonPhones",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "phone_code_id",
                table: "PersonPhones",
                newName: "PhonecodeId");

            migrationBuilder.RenameColumn(
                name: "person_id",
                table: "PersonPhones",
                newName: "PersonId");

            migrationBuilder.RenameIndex(
                name: "IX_person_phones_phone_code_id",
                table: "PersonPhones",
                newName: "IX_PersonPhones_PhonecodeId");

            migrationBuilder.RenameIndex(
                name: "IX_person_phones_person_id_phone_code_id_phone_number",
                table: "PersonPhones",
                newName: "IX_PersonPhones_PersonId_PhonecodeId_phone_number");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "PersonEmails",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "person_id",
                table: "PersonEmails",
                newName: "PersonId");

            migrationBuilder.RenameColumn(
                name: "email_domain_id",
                table: "PersonEmails",
                newName: "Email_domainId");

            migrationBuilder.RenameIndex(
                name: "IX_person_emails_person_id_email_local_part_email_domain_id",
                table: "PersonEmails",
                newName: "IX_PersonEmails_PersonId_email_local_part_Email_domainId");

            migrationBuilder.RenameIndex(
                name: "IX_person_emails_person_id",
                table: "PersonEmails",
                newName: "IX_PersonEmails_PersonId");

            migrationBuilder.RenameIndex(
                name: "IX_person_emails_email_domain_id",
                table: "PersonEmails",
                newName: "IX_PersonEmails_Email_domainId");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "PaymentStatuses",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "PaymentStatuses",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_payment_statuses_name",
                table: "PaymentStatuses",
                newName: "IX_PaymentStatuses_Name");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "PaymentMethods",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "payment_method_type_id",
                table: "PaymentMethods",
                newName: "Payment_method_typeId");

            migrationBuilder.RenameColumn(
                name: "card_type_id",
                table: "PaymentMethods",
                newName: "Card_typeId");

            migrationBuilder.RenameColumn(
                name: "card_issuer_id",
                table: "PaymentMethods",
                newName: "Card_issuerId");

            migrationBuilder.RenameIndex(
                name: "IX_payment_methods_payment_method_type_id",
                table: "PaymentMethods",
                newName: "IX_PaymentMethods_Payment_method_typeId");

            migrationBuilder.RenameIndex(
                name: "IX_payment_methods_commercial_name",
                table: "PaymentMethods",
                newName: "IX_PaymentMethods_commercial_name");

            migrationBuilder.RenameIndex(
                name: "IX_payment_methods_card_type_id",
                table: "PaymentMethods",
                newName: "IX_PaymentMethods_Card_typeId");

            migrationBuilder.RenameIndex(
                name: "IX_payment_methods_card_issuer_id",
                table: "PaymentMethods",
                newName: "IX_PaymentMethods_Card_issuerId");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "PaymentMethodTypes",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "PaymentMethodTypes",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_payment_method_types_name",
                table: "PaymentMethodTypes",
                newName: "IX_PaymentMethodTypes_Name");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "PassengerTypes",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "PassengerTypes",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_passenger_types_name",
                table: "PassengerTypes",
                newName: "IX_PassengerTypes_Name");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "InvoiceItemTypes",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "InvoiceItemTypes",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_invoice_item_types_name",
                table: "InvoiceItemTypes",
                newName: "IX_InvoiceItemTypes_Name");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "FlightStatuses",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "FlightStatuses",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_flight_statuses_name",
                table: "FlightStatuses",
                newName: "IX_FlightStatuses_Name");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "FlightSeats",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "location_type_id",
                table: "FlightSeats",
                newName: "LocationtypeId");

            migrationBuilder.RenameColumn(
                name: "flight_id",
                table: "FlightSeats",
                newName: "FlightId");

            migrationBuilder.RenameColumn(
                name: "cabin_type_id",
                table: "FlightSeats",
                newName: "CabinTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_flight_seats_location_type_id",
                table: "FlightSeats",
                newName: "IX_FlightSeats_LocationtypeId");

            migrationBuilder.RenameIndex(
                name: "IX_flight_seats_flight_id_seat_code",
                table: "FlightSeats",
                newName: "IX_FlightSeats_FlightId_seat_code");

            migrationBuilder.RenameIndex(
                name: "IX_flight_seats_cabin_type_id",
                table: "FlightSeats",
                newName: "IX_FlightSeats_CabinTypeId");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "EmailDomains",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_email_domains_domain",
                table: "EmailDomains",
                newName: "IX_EmailDomains_domain");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "DocumentTypes",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "DocumentTypes",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_document_types_code",
                table: "DocumentTypes",
                newName: "IX_DocumentTypes_code");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "CheckinStatuses",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "CheckinStatuses",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "CardTypes",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "CardTypes",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_card_types_name",
                table: "CardTypes",
                newName: "IX_CardTypes_Name");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "CardIssuers",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "CardIssuers",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_card_issuers_name",
                table: "CardIssuers",
                newName: "IX_CardIssuers_Name");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "CabinTypes",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "CabinTypes",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_cabin_types_name",
                table: "CabinTypes",
                newName: "IX_CabinTypes_Name");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "BaggageTypes",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "BaggageTypes",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "max_weight_kg",
                table: "BaggageTypes",
                newName: "MaxWeightKg");

            migrationBuilder.RenameColumn(
                name: "base_price",
                table: "BaggageTypes",
                newName: "BasePrice");

            migrationBuilder.RenameIndex(
                name: "IX_baggage_types_name",
                table: "BaggageTypes",
                newName: "IX_BaggageTypes_Name");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "AircraftManufacturers",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "country",
                table: "AircraftManufacturers",
                newName: "Country");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "AircraftManufacturers",
                newName: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Continents",
                table: "Continents",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TicketStatuses",
                table: "TicketStatuses",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SystemRoles",
                table: "SystemRoles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StreetTypes",
                table: "StreetTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StaffPositions",
                table: "StaffPositions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SeatLocationTypes",
                table: "SeatLocationTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RolePermissions",
                table: "RolePermissions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PhoneCodes",
                table: "PhoneCodes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PersonPhones",
                table: "PersonPhones",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PersonEmails",
                table: "PersonEmails",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PaymentStatuses",
                table: "PaymentStatuses",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PaymentMethods",
                table: "PaymentMethods",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PaymentMethodTypes",
                table: "PaymentMethodTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PassengerTypes",
                table: "PassengerTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InvoiceItemTypes",
                table: "InvoiceItemTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FlightStatuses",
                table: "FlightStatuses",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FlightSeats",
                table: "FlightSeats",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmailDomains",
                table: "EmailDomains",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DocumentTypes",
                table: "DocumentTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CheckinStatuses",
                table: "CheckinStatuses",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CardTypes",
                table: "CardTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CardIssuers",
                table: "CardIssuers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CabinTypes",
                table: "CabinTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BaggageTypes",
                table: "BaggageTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AircraftManufacturers",
                table: "AircraftManufacturers",
                column: "Id");

            migrationBuilder.InsertData(
                table: "AircraftManufacturers",
                columns: new[] { "Id", "Country", "Name" },
                values: new object[,]
                {
                    { 1, "Francia", "Airbus" },
                    { 2, "Estados Unidos", "Boeing" },
                    { 3, "Brasil", "Embraer" },
                    { 4, "Francia", "ATR" },
                    { 5, "Canadá", "De Havilland Canada" }
                });

            migrationBuilder.InsertData(
                table: "BaggageTypes",
                columns: new[] { "Id", "MaxWeightKg", "Name" },
                values: new object[,]
                {
                    { 1, 5.00m, "Artículo personal" },
                    { 2, 10.00m, "Equipaje de mano" }
                });

            migrationBuilder.InsertData(
                table: "BaggageTypes",
                columns: new[] { "Id", "BasePrice", "MaxWeightKg", "Name" },
                values: new object[,]
                {
                    { 3, 60000.00m, 23.00m, "Bodega estándar" },
                    { 4, 120000.00m, 32.00m, "Bodega extra" }
                });

            migrationBuilder.InsertData(
                table: "CabinTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Económica" },
                    { 2, "Premium Economy" },
                    { 3, "Ejecutiva" },
                    { 4, "Primera clase" }
                });

            migrationBuilder.InsertData(
                table: "CardIssuers",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Visa" },
                    { 2, "Mastercard" },
                    { 3, "American Express" },
                    { 4, "Diners Club" }
                });

            migrationBuilder.InsertData(
                table: "CardTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Crédito" },
                    { 2, "Débito" },
                    { 3, "Prepago" }
                });

            migrationBuilder.InsertData(
                table: "CheckinStatuses",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Pendiente" },
                    { 2, "Realizado" },
                    { 3, "Cerrado" },
                    { 4, "Cancelado" }
                });

            migrationBuilder.InsertData(
                table: "Continents",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "América" },
                    { 2, "Europa" },
                    { 3, "Asia" },
                    { 4, "África" },
                    { 5, "Oceanía" },
                    { 6, "Antártida" }
                });

            migrationBuilder.InsertData(
                table: "DocumentTypes",
                columns: new[] { "Id", "code", "Name" },
                values: new object[,]
                {
                    { 1, "CC", "Cédula de ciudadanía" },
                    { 2, "CE", "Cédula de extranjería" },
                    { 3, "PAS", "Pasaporte" },
                    { 4, "TI", "Tarjeta de identidad" },
                    { 5, "NIT", "NIT" }
                });

            migrationBuilder.InsertData(
                table: "EmailDomains",
                columns: new[] { "Id", "domain" },
                values: new object[,]
                {
                    { 1, "gmail.com" },
                    { 2, "outlook.com" },
                    { 3, "hotmail.com" },
                    { 4, "yahoo.com" },
                    { 5, "icloud.com" },
                    { 6, "proton.me" }
                });

            migrationBuilder.InsertData(
                table: "FlightStatuses",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Programado" },
                    { 2, "Abordando" },
                    { 3, "En vuelo" },
                    { 4, "Aterrizado" },
                    { 5, "Retrasado" },
                    { 6, "Cancelado" }
                });

            migrationBuilder.InsertData(
                table: "InvoiceItemTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Tarifa aérea" },
                    { 2, "Impuestos" },
                    { 3, "Equipaje" },
                    { 4, "Servicio" },
                    { 5, "Descuento" }
                });

            migrationBuilder.InsertData(
                table: "PassengerTypes",
                columns: new[] { "Id", "max_age", "min_age", "Name" },
                values: new object[,]
                {
                    { 1, 1, 0, "Infante" },
                    { 2, 11, 2, "Niño" },
                    { 3, null, 12, "Adulto" },
                    { 4, null, 60, "Adulto mayor" }
                });

            migrationBuilder.InsertData(
                table: "PaymentMethodTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Tarjeta" },
                    { 2, "Efectivo" },
                    { 3, "Transferencia bancaria" },
                    { 4, "Billetera digital" }
                });

            migrationBuilder.InsertData(
                table: "PaymentStatuses",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Pendiente" },
                    { 2, "Aprobado" },
                    { 3, "Rechazado" },
                    { 4, "Reembolsado" },
                    { 5, "Anulado" }
                });

            migrationBuilder.InsertData(
                table: "PhoneCodes",
                columns: new[] { "Id", "country_code", "country_name" },
                values: new object[,]
                {
                    { 1, "+57", "Colombia" },
                    { 2, "+1", "Estados Unidos / Canadá" },
                    { 3, "+52", "México" },
                    { 4, "+55", "Brasil" },
                    { 5, "+54", "Argentina" },
                    { 6, "+56", "Chile" },
                    { 7, "+51", "Perú" },
                    { 8, "+34", "España" },
                    { 9, "+33", "Francia" },
                    { 10, "+44", "Reino Unido" },
                    { 11, "+49", "Alemania" },
                    { 12, "+39", "Italia" },
                    { 13, "+81", "Japón" },
                    { 14, "+86", "China" },
                    { 15, "+61", "Australia" },
                    { 16, "+27", "Sudáfrica" }
                });

            migrationBuilder.InsertData(
                table: "SeatLocationTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Ventana" },
                    { 2, "Pasillo" },
                    { 3, "Centro" }
                });

            migrationBuilder.InsertData(
                table: "StaffPositions",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Piloto" },
                    { 2, "Copiloto" },
                    { 3, "Tripulante de cabina" },
                    { 4, "Agente de puerta" },
                    { 5, "Técnico de mantenimiento" }
                });

            migrationBuilder.InsertData(
                table: "StreetTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Calle" },
                    { 2, "Carrera" },
                    { 3, "Avenida" },
                    { 4, "Diagonal" },
                    { 5, "Transversal" },
                    { 6, "Autopista" }
                });

            migrationBuilder.InsertData(
                table: "SystemRoles",
                columns: new[] { "Id", "description", "Name" },
                values: new object[,]
                {
                    { 1, "Acceso completo al sistema", "Administrador" },
                    { 2, "Gestión de ventas, reservas y atención al cliente", "Agente" },
                    { 3, "Acceso de autoservicio para pasajeros", "Cliente" },
                    { 4, "Gestión operativa de vuelos y tripulación", "Operaciones" }
                });

            migrationBuilder.InsertData(
                table: "TicketStatuses",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Emitido" },
                    { 2, "Usado" },
                    { 3, "Cancelado" },
                    { 4, "Reembolsado" },
                    { 5, "No presentado" }
                });

            migrationBuilder.InsertData(
                table: "permissions",
                columns: new[] { "Id", "description", "Name" },
                values: new object[,]
                {
                    { 1, "Gestionar reservas", "reservations.manage" },
                    { 2, "Gestionar vuelos", "flights.manage" },
                    { 3, "Gestionar catálogos del sistema", "catalogs.manage" },
                    { 4, "Gestionar pagos", "payments.manage" },
                    { 5, "Consultar reportes", "reports.view" }
                });

            migrationBuilder.InsertData(
                table: "seasons",
                columns: new[] { "Id", "description", "Name", "price_factor" },
                values: new object[,]
                {
                    { 1, "Temporada de menor demanda", "Baja", 0.9000m },
                    { 2, "Temporada regular", "Media", 1.0000m },
                    { 3, "Temporada de alta demanda", "Alta", 1.2500m },
                    { 4, "Temporada de festivos y vacaciones", "Festiva", 1.4000m }
                });

            migrationBuilder.InsertData(
                table: "PaymentMethods",
                columns: new[] { "Id", "Card_issuerId", "Card_typeId", "commercial_name", "Payment_method_typeId" },
                values: new object[,]
                {
                    { 1, 1, 1, "Visa crédito", 1 },
                    { 2, 2, 1, "Mastercard crédito", 1 },
                    { 3, 1, 2, "Visa débito", 1 },
                    { 4, 3, 1, "American Express crédito", 1 },
                    { 5, null, null, "Efectivo en oficina", 2 },
                    { 6, null, null, "Transferencia bancaria", 3 },
                    { 7, null, null, "Billetera digital", 4 }
                });

            migrationBuilder.InsertData(
                table: "RolePermissions",
                columns: new[] { "Id", "PermissionId", "RoleId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 2, 1 },
                    { 3, 3, 1 },
                    { 4, 4, 1 },
                    { 5, 5, 1 },
                    { 6, 1, 2 },
                    { 7, 4, 2 },
                    { 8, 1, 3 },
                    { 9, 2, 4 },
                    { 10, 5, 4 }
                });

            migrationBuilder.InsertData(
                table: "aircraft_models",
                columns: new[] { "Id", "cruising_altitude_ft", "cruising_speed_kmh", "fuel_consumption_kg_h", "ManufacturerId", "max_capacity", "max_takeoff_weight_kg", "model_name" },
                values: new object[,]
                {
                    { 1, 39000, 840, 2500.00m, 1, 180, 78000.00m, "Airbus A320" },
                    { 2, 41000, 871, 5500.00m, 1, 277, 242000.00m, "Airbus A330" },
                    { 3, 41000, 842, 2600.00m, 2, 189, 79015.00m, "Boeing 737-800" },
                    { 4, 43000, 903, 4900.00m, 2, 242, 227930.00m, "Boeing 787-8" },
                    { 5, 41000, 829, 1500.00m, 3, 114, 51800.00m, "Embraer E190" }
                });

            migrationBuilder.InsertData(
                table: "countries",
                columns: new[] { "Id", "CodeIso", "ContinentId", "Name" },
                values: new object[,]
                {
                    { 1, "COL", 1, "Colombia" },
                    { 2, "USA", 1, "Estados Unidos" },
                    { 3, "MEX", 1, "México" },
                    { 4, "BRA", 1, "Brasil" },
                    { 5, "ARG", 1, "Argentina" },
                    { 6, "CHL", 1, "Chile" },
                    { 7, "PER", 1, "Perú" },
                    { 8, "CAN", 1, "Canadá" },
                    { 9, "ESP", 2, "España" },
                    { 10, "FRA", 2, "Francia" },
                    { 11, "GBR", 2, "Reino Unido" },
                    { 12, "DEU", 2, "Alemania" },
                    { 13, "ITA", 2, "Italia" },
                    { 14, "JPN", 3, "Japón" },
                    { 15, "CHN", 3, "China" },
                    { 16, "KOR", 3, "Corea del Sur" },
                    { 17, "AUS", 5, "Australia" },
                    { 18, "ZAF", 4, "Sudáfrica" }
                });

            migrationBuilder.InsertData(
                table: "airlines",
                columns: new[] { "Id", "CreatedAt", "iata_code", "is_active", "Name", "Origin_countryId", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "AV", true, "Avianca", 1, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 2, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "LA", true, "LATAM Airlines", 6, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 3, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "AA", true, "American Airlines", 2, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 4, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "IB", true, "Iberia", 9, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 5, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "AF", true, "Air France", 10, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "regions",
                columns: new[] { "Id", "CountryId", "Name", "type" },
                values: new object[,]
                {
                    { 1, 1, "Cundinamarca", "Departamento" },
                    { 2, 1, "Antioquia", "Departamento" },
                    { 3, 1, "Valle del Cauca", "Departamento" },
                    { 4, 1, "Atlantico", "Departamento" },
                    { 5, 2, "Florida", "Estado" },
                    { 6, 2, "Nueva York", "Estado" },
                    { 7, 2, "California", "Estado" },
                    { 8, 3, "Ciudad de Mexico", "Entidad federativa" },
                    { 9, 9, "Comunidad de Madrid", "Comunidad autonoma" },
                    { 10, 10, "Ile-de-France", "Region" },
                    { 11, 11, "Inglaterra", "Nacion constituyente" },
                    { 12, 4, "Sao Paulo", "Estado" },
                    { 13, 5, "Buenos Aires", "Provincia" },
                    { 14, 6, "Region Metropolitana", "Region" },
                    { 15, 7, "Lima", "Departamento" },
                    { 16, 8, "Ontario", "Provincia" },
                    { 17, 14, "Tokio", "Prefectura" },
                    { 18, 17, "Nueva Gales del Sur", "Estado" }
                });

            migrationBuilder.InsertData(
                table: "aircraft",
                columns: new[] { "Id", "AirlineId", "is_active", "manufacturing_date", "ModelId", "registration" },
                values: new object[,]
                {
                    { 1, 1, true, new DateOnly(2018, 5, 15), 1, "HK-5310" },
                    { 2, 1, true, new DateOnly(2019, 8, 20), 1, "HK-5321" },
                    { 3, 2, true, new DateOnly(2017, 3, 10), 3, "CC-BGA" },
                    { 4, 3, true, new DateOnly(2020, 2, 5), 4, "N801AA" },
                    { 5, 4, true, new DateOnly(2016, 11, 18), 2, "EC-MKI" },
                    { 6, 5, true, new DateOnly(2015, 6, 25), 2, "F-GZCA" }
                });

            migrationBuilder.InsertData(
                table: "cities",
                columns: new[] { "Id", "Name", "RegionId" },
                values: new object[,]
                {
                    { 1, "Bogota", 1 },
                    { 2, "Medellin", 2 },
                    { 3, "Cali", 3 },
                    { 4, "Barranquilla", 4 },
                    { 5, "Miami", 5 },
                    { 6, "Nueva York", 6 },
                    { 7, "Los Angeles", 7 },
                    { 8, "Ciudad de Mexico", 8 },
                    { 9, "Madrid", 9 },
                    { 10, "Paris", 10 },
                    { 11, "Londres", 11 },
                    { 12, "Sao Paulo", 12 },
                    { 13, "Buenos Aires", 13 },
                    { 14, "Santiago", 14 },
                    { 15, "Lima", 15 },
                    { 16, "Toronto", 16 },
                    { 17, "Tokio", 17 },
                    { 18, "Sidney", 18 }
                });

            migrationBuilder.InsertData(
                table: "airports",
                columns: new[] { "Id", "CityId", "iata_code", "icao_code", "Name" },
                values: new object[,]
                {
                    { 1, 1, "BOG", "SKBO", "Aeropuerto Internacional El Dorado" },
                    { 2, 2, "MDE", "SKRG", "Aeropuerto Internacional Jose Maria Cordova" },
                    { 3, 3, "CLO", "SKCL", "Aeropuerto Internacional Alfonso Bonilla Aragon" },
                    { 4, 4, "BAQ", "SKBQ", "Aeropuerto Internacional Ernesto Cortissoz" },
                    { 5, 5, "MIA", "KMIA", "Miami International Airport" },
                    { 6, 6, "JFK", "KJFK", "John F. Kennedy International Airport" },
                    { 7, 7, "LAX", "KLAX", "Los Angeles International Airport" },
                    { 8, 8, "MEX", "MMMX", "Aeropuerto Internacional Benito Juarez" },
                    { 9, 9, "MAD", "LEMD", "Aeropuerto Adolfo Suarez Madrid-Barajas" },
                    { 10, 10, "CDG", "LFPG", "Paris Charles de Gaulle Airport" },
                    { 11, 11, "LHR", "EGLL", "London Heathrow Airport" },
                    { 12, 12, "GRU", "SBGR", "Sao Paulo Guarulhos International Airport" },
                    { 13, 13, "EZE", "SAEZ", "Aeropuerto Internacional Ezeiza" },
                    { 14, 14, "SCL", "SCEL", "Aeropuerto Internacional Arturo Merino Benitez" },
                    { 15, 15, "LIM", "SPJC", "Aeropuerto Internacional Jorge Chavez" },
                    { 16, 16, "YYZ", "CYYZ", "Toronto Pearson International Airport" },
                    { 17, 17, "HND", "RJTT", "Tokyo Haneda Airport" },
                    { 18, 18, "SYD", "YSSY", "Sydney Kingsford Smith Airport" }
                });

            migrationBuilder.InsertData(
                table: "routes",
                columns: new[] { "Id", "Destination_airportId", "distance_km", "estimated_duration_min", "Origin_airportId" },
                values: new object[,]
                {
                    { 1, 2, 215, 55, 1 },
                    { 2, 3, 280, 60, 1 },
                    { 3, 4, 695, 95, 1 },
                    { 4, 5, 2435, 225, 1 },
                    { 5, 8, 3160, 270, 1 },
                    { 6, 9, 8030, 620, 1 },
                    { 7, 15, 1880, 185, 1 },
                    { 8, 6, 1760, 180, 5 },
                    { 9, 11, 5540, 415, 6 },
                    { 10, 10, 1060, 125, 9 },
                    { 11, 11, 1245, 145, 9 },
                    { 12, 13, 1720, 175, 12 },
                    { 13, 14, 1140, 125, 13 },
                    { 14, 15, 2460, 220, 14 },
                    { 15, 18, 7800, 575, 17 }
                });

            migrationBuilder.InsertData(
                table: "fares",
                columns: new[] { "Id", "BasePrice", "CabinTypeId", "PassengerTypeId", "RouteId", "SeasonId", "ValidFrom", "ValidTo" },
                values: new object[,]
                {
                    { 1, 180000.00m, 1, 3, 1, 2, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 2, 210000.00m, 1, 3, 2, 2, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 3, 260000.00m, 1, 3, 3, 2, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 4, 950000.00m, 1, 3, 4, 2, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 5, 1100000.00m, 1, 3, 5, 2, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 6, 2800000.00m, 1, 3, 6, 2, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 7, 780000.00m, 1, 3, 7, 2, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 8, 720000.00m, 1, 3, 8, 2, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 9, 2200000.00m, 1, 3, 9, 2, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 10, 420000.00m, 1, 3, 10, 2, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 11, 480000.00m, 1, 3, 11, 2, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 12, 690000.00m, 1, 3, 12, 2, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 13, 510000.00m, 1, 3, 13, 2, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 14, 850000.00m, 1, 3, 14, 2, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 15, 3100000.00m, 1, 3, 15, 2, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null }
                });

            migrationBuilder.InsertData(
                table: "flights",
                columns: new[] { "Id", "AircraftId", "AirlineId", "available_seats", "CreatedAt", "departure_at", "estimated_arrival_at", "flight_code", "FlightstatusId", "rescheduled_at", "RouteId", "total_capacity", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, 1, 1, 6, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 6, 1, 8, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 6, 1, 8, 55, 0, 0, DateTimeKind.Unspecified), "AV101", 1, null, 1, 6, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 2, 2, 1, 6, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 6, 1, 9, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 6, 1, 12, 45, 0, 0, DateTimeKind.Unspecified), "AV201", 1, null, 4, 6, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 3, 3, 2, 6, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 6, 2, 7, 30, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 6, 2, 11, 10, 0, 0, DateTimeKind.Unspecified), "LA301", 1, null, 14, 6, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 4, 4, 3, 6, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 6, 2, 13, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 6, 2, 16, 0, 0, 0, DateTimeKind.Unspecified), "AA401", 1, null, 8, 6, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 5, 5, 4, 6, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 6, 3, 10, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 6, 3, 12, 25, 0, 0, DateTimeKind.Unspecified), "IB501", 1, null, 11, 6, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "FlightSeats",
                columns: new[] { "Id", "CabinTypeId", "FlightId", "is_occupied", "LocationtypeId", "seat_code" },
                values: new object[,]
                {
                    { 1, 1, 1, false, 1, "1A" },
                    { 2, 1, 1, false, 3, "1B" },
                    { 3, 1, 1, false, 2, "1C" },
                    { 4, 1, 1, false, 2, "1D" },
                    { 5, 1, 1, false, 3, "1E" },
                    { 6, 1, 1, false, 1, "1F" },
                    { 7, 1, 2, false, 1, "1A" },
                    { 8, 1, 2, false, 3, "1B" },
                    { 9, 1, 2, false, 2, "1C" },
                    { 10, 1, 2, false, 2, "1D" },
                    { 11, 1, 2, false, 3, "1E" },
                    { 12, 1, 2, false, 1, "1F" },
                    { 13, 1, 3, false, 1, "1A" },
                    { 14, 1, 3, false, 3, "1B" },
                    { 15, 1, 3, false, 2, "1C" },
                    { 16, 1, 3, false, 2, "1D" },
                    { 17, 1, 3, false, 3, "1E" },
                    { 18, 1, 3, false, 1, "1F" },
                    { 19, 1, 4, false, 1, "1A" },
                    { 20, 1, 4, false, 3, "1B" },
                    { 21, 1, 4, false, 2, "1C" },
                    { 22, 1, 4, false, 2, "1D" },
                    { 23, 1, 4, false, 3, "1E" },
                    { 24, 1, 4, false, 1, "1F" },
                    { 25, 1, 5, false, 1, "1A" },
                    { 26, 1, 5, false, 3, "1B" },
                    { 27, 1, 5, false, 2, "1C" },
                    { 28, 1, 5, false, 2, "1D" },
                    { 29, 1, 5, false, 3, "1E" },
                    { 30, 1, 5, false, 1, "1F" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_addresses_StreetTypes_Street_typeId",
                table: "addresses",
                column: "Street_typeId",
                principalTable: "StreetTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_addresses_cities_CityId",
                table: "addresses",
                column: "CityId",
                principalTable: "cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_aircraft_aircraft_models_ModelId",
                table: "aircraft",
                column: "ModelId",
                principalTable: "aircraft_models",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_aircraft_airlines_AirlineId",
                table: "aircraft",
                column: "AirlineId",
                principalTable: "airlines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_aircraft_models_AircraftManufacturers_ManufacturerId",
                table: "aircraft_models",
                column: "ManufacturerId",
                principalTable: "AircraftManufacturers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_airlines_countries_Origin_countryId",
                table: "airlines",
                column: "Origin_countryId",
                principalTable: "countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_airports_cities_CityId",
                table: "airports",
                column: "CityId",
                principalTable: "cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_baggage_BaggageTypes_baggage_type_id",
                table: "baggage",
                column: "baggage_type_id",
                principalTable: "BaggageTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_cabin_configurations_CabinTypes_cabin_type_id",
                table: "cabin_configurations",
                column: "cabin_type_id",
                principalTable: "CabinTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_check_ins_CheckinStatuses_CheckinstatusId",
                table: "check_ins",
                column: "CheckinstatusId",
                principalTable: "CheckinStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_check_ins_FlightSeats_Flight_seatId",
                table: "check_ins",
                column: "Flight_seatId",
                principalTable: "FlightSeats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_check_ins_staff_StaffId",
                table: "check_ins",
                column: "StaffId",
                principalTable: "staff",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_check_ins_tickets_TicketId",
                table: "check_ins",
                column: "TicketId",
                principalTable: "tickets",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_cities_regions_RegionId",
                table: "cities",
                column: "RegionId",
                principalTable: "regions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_clients_persons_PersonId",
                table: "clients",
                column: "PersonId",
                principalTable: "persons",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_countries_Continents_ContinentId",
                table: "countries",
                column: "ContinentId",
                principalTable: "Continents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_fares_CabinTypes_CabinTypeId",
                table: "fares",
                column: "CabinTypeId",
                principalTable: "CabinTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_fares_PassengerTypes_PassengerTypeId",
                table: "fares",
                column: "PassengerTypeId",
                principalTable: "PassengerTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_fares_routes_RouteId",
                table: "fares",
                column: "RouteId",
                principalTable: "routes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_fares_seasons_SeasonId",
                table: "fares",
                column: "SeasonId",
                principalTable: "seasons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_flight_status_transitions_FlightStatuses_from_status_id",
                table: "flight_status_transitions",
                column: "from_status_id",
                principalTable: "FlightStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_flight_status_transitions_FlightStatuses_to_status_id",
                table: "flight_status_transitions",
                column: "to_status_id",
                principalTable: "FlightStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_flights_FlightStatuses_FlightstatusId",
                table: "flights",
                column: "FlightstatusId",
                principalTable: "FlightStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_flights_aircraft_AircraftId",
                table: "flights",
                column: "AircraftId",
                principalTable: "aircraft",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_flights_airlines_AirlineId",
                table: "flights",
                column: "AirlineId",
                principalTable: "airlines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_flights_routes_RouteId",
                table: "flights",
                column: "RouteId",
                principalTable: "routes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FlightSeats_CabinTypes_CabinTypeId",
                table: "FlightSeats",
                column: "CabinTypeId",
                principalTable: "CabinTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FlightSeats_SeatLocationTypes_LocationtypeId",
                table: "FlightSeats",
                column: "LocationtypeId",
                principalTable: "SeatLocationTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FlightSeats_flights_FlightId",
                table: "FlightSeats",
                column: "FlightId",
                principalTable: "flights",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_invoice_items_InvoiceItemTypes_invoice_item_type_id",
                table: "invoice_items",
                column: "invoice_item_type_id",
                principalTable: "InvoiceItemTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_passengers_PassengerTypes_PassengerTypeId",
                table: "passengers",
                column: "PassengerTypeId",
                principalTable: "PassengerTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_passengers_persons_PersonId",
                table: "passengers",
                column: "PersonId",
                principalTable: "persons",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentMethods_CardIssuers_Card_issuerId",
                table: "PaymentMethods",
                column: "Card_issuerId",
                principalTable: "CardIssuers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentMethods_CardTypes_Card_typeId",
                table: "PaymentMethods",
                column: "Card_typeId",
                principalTable: "CardTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentMethods_PaymentMethodTypes_Payment_method_typeId",
                table: "PaymentMethods",
                column: "Payment_method_typeId",
                principalTable: "PaymentMethodTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_payments_PaymentMethods_payment_method_id",
                table: "payments",
                column: "payment_method_id",
                principalTable: "PaymentMethods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_payments_PaymentStatuses_payment_status_id",
                table: "payments",
                column: "payment_status_id",
                principalTable: "PaymentStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonEmails_EmailDomains_Email_domainId",
                table: "PersonEmails",
                column: "Email_domainId",
                principalTable: "EmailDomains",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonEmails_persons_PersonId",
                table: "PersonEmails",
                column: "PersonId",
                principalTable: "persons",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonPhones_PhoneCodes_PhonecodeId",
                table: "PersonPhones",
                column: "PhonecodeId",
                principalTable: "PhoneCodes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonPhones_persons_PersonId",
                table: "PersonPhones",
                column: "PersonId",
                principalTable: "persons",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_persons_DocumentTypes_document_type_id",
                table: "persons",
                column: "document_type_id",
                principalTable: "DocumentTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_regions_countries_CountryId",
                table: "regions",
                column: "CountryId",
                principalTable: "countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RolePermissions_SystemRoles_RoleId",
                table: "RolePermissions",
                column: "RoleId",
                principalTable: "SystemRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RolePermissions_permissions_PermissionId",
                table: "RolePermissions",
                column: "PermissionId",
                principalTable: "permissions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_routes_airports_Destination_airportId",
                table: "routes",
                column: "Destination_airportId",
                principalTable: "airports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_routes_airports_Origin_airportId",
                table: "routes",
                column: "Origin_airportId",
                principalTable: "airports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_staff_StaffPositions_position_id",
                table: "staff",
                column: "position_id",
                principalTable: "StaffPositions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tickets_TicketStatuses_ticket_status_id",
                table: "tickets",
                column: "ticket_status_id",
                principalTable: "TicketStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_users_SystemRoles_role_id",
                table: "users",
                column: "role_id",
                principalTable: "SystemRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Snapshot-only migration: no schema operations.
            return;
            migrationBuilder.DropForeignKey(
                name: "FK_addresses_StreetTypes_Street_typeId",
                table: "addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_addresses_cities_CityId",
                table: "addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_aircraft_aircraft_models_ModelId",
                table: "aircraft");

            migrationBuilder.DropForeignKey(
                name: "FK_aircraft_airlines_AirlineId",
                table: "aircraft");

            migrationBuilder.DropForeignKey(
                name: "FK_aircraft_models_AircraftManufacturers_ManufacturerId",
                table: "aircraft_models");

            migrationBuilder.DropForeignKey(
                name: "FK_airlines_countries_Origin_countryId",
                table: "airlines");

            migrationBuilder.DropForeignKey(
                name: "FK_airports_cities_CityId",
                table: "airports");

            migrationBuilder.DropForeignKey(
                name: "FK_baggage_BaggageTypes_baggage_type_id",
                table: "baggage");

            migrationBuilder.DropForeignKey(
                name: "FK_cabin_configurations_CabinTypes_cabin_type_id",
                table: "cabin_configurations");

            migrationBuilder.DropForeignKey(
                name: "FK_check_ins_CheckinStatuses_CheckinstatusId",
                table: "check_ins");

            migrationBuilder.DropForeignKey(
                name: "FK_check_ins_FlightSeats_Flight_seatId",
                table: "check_ins");

            migrationBuilder.DropForeignKey(
                name: "FK_check_ins_staff_StaffId",
                table: "check_ins");

            migrationBuilder.DropForeignKey(
                name: "FK_check_ins_tickets_TicketId",
                table: "check_ins");

            migrationBuilder.DropForeignKey(
                name: "FK_cities_regions_RegionId",
                table: "cities");

            migrationBuilder.DropForeignKey(
                name: "FK_clients_persons_PersonId",
                table: "clients");

            migrationBuilder.DropForeignKey(
                name: "FK_countries_Continents_ContinentId",
                table: "countries");

            migrationBuilder.DropForeignKey(
                name: "FK_fares_CabinTypes_CabinTypeId",
                table: "fares");

            migrationBuilder.DropForeignKey(
                name: "FK_fares_PassengerTypes_PassengerTypeId",
                table: "fares");

            migrationBuilder.DropForeignKey(
                name: "FK_fares_routes_RouteId",
                table: "fares");

            migrationBuilder.DropForeignKey(
                name: "FK_fares_seasons_SeasonId",
                table: "fares");

            migrationBuilder.DropForeignKey(
                name: "FK_flight_status_transitions_FlightStatuses_from_status_id",
                table: "flight_status_transitions");

            migrationBuilder.DropForeignKey(
                name: "FK_flight_status_transitions_FlightStatuses_to_status_id",
                table: "flight_status_transitions");

            migrationBuilder.DropForeignKey(
                name: "FK_flights_FlightStatuses_FlightstatusId",
                table: "flights");

            migrationBuilder.DropForeignKey(
                name: "FK_flights_aircraft_AircraftId",
                table: "flights");

            migrationBuilder.DropForeignKey(
                name: "FK_flights_airlines_AirlineId",
                table: "flights");

            migrationBuilder.DropForeignKey(
                name: "FK_flights_routes_RouteId",
                table: "flights");

            migrationBuilder.DropForeignKey(
                name: "FK_FlightSeats_CabinTypes_CabinTypeId",
                table: "FlightSeats");

            migrationBuilder.DropForeignKey(
                name: "FK_FlightSeats_SeatLocationTypes_LocationtypeId",
                table: "FlightSeats");

            migrationBuilder.DropForeignKey(
                name: "FK_FlightSeats_flights_FlightId",
                table: "FlightSeats");

            migrationBuilder.DropForeignKey(
                name: "FK_invoice_items_InvoiceItemTypes_invoice_item_type_id",
                table: "invoice_items");

            migrationBuilder.DropForeignKey(
                name: "FK_passengers_PassengerTypes_PassengerTypeId",
                table: "passengers");

            migrationBuilder.DropForeignKey(
                name: "FK_passengers_persons_PersonId",
                table: "passengers");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentMethods_CardIssuers_Card_issuerId",
                table: "PaymentMethods");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentMethods_CardTypes_Card_typeId",
                table: "PaymentMethods");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentMethods_PaymentMethodTypes_Payment_method_typeId",
                table: "PaymentMethods");

            migrationBuilder.DropForeignKey(
                name: "FK_payments_PaymentMethods_payment_method_id",
                table: "payments");

            migrationBuilder.DropForeignKey(
                name: "FK_payments_PaymentStatuses_payment_status_id",
                table: "payments");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonEmails_EmailDomains_Email_domainId",
                table: "PersonEmails");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonEmails_persons_PersonId",
                table: "PersonEmails");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonPhones_PhoneCodes_PhonecodeId",
                table: "PersonPhones");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonPhones_persons_PersonId",
                table: "PersonPhones");

            migrationBuilder.DropForeignKey(
                name: "FK_persons_DocumentTypes_document_type_id",
                table: "persons");

            migrationBuilder.DropForeignKey(
                name: "FK_regions_countries_CountryId",
                table: "regions");

            migrationBuilder.DropForeignKey(
                name: "FK_RolePermissions_SystemRoles_RoleId",
                table: "RolePermissions");

            migrationBuilder.DropForeignKey(
                name: "FK_RolePermissions_permissions_PermissionId",
                table: "RolePermissions");

            migrationBuilder.DropForeignKey(
                name: "FK_routes_airports_Destination_airportId",
                table: "routes");

            migrationBuilder.DropForeignKey(
                name: "FK_routes_airports_Origin_airportId",
                table: "routes");

            migrationBuilder.DropForeignKey(
                name: "FK_staff_StaffPositions_position_id",
                table: "staff");

            migrationBuilder.DropForeignKey(
                name: "FK_tickets_TicketStatuses_ticket_status_id",
                table: "tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_users_SystemRoles_role_id",
                table: "users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Continents",
                table: "Continents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TicketStatuses",
                table: "TicketStatuses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SystemRoles",
                table: "SystemRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StreetTypes",
                table: "StreetTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StaffPositions",
                table: "StaffPositions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SeatLocationTypes",
                table: "SeatLocationTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RolePermissions",
                table: "RolePermissions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PhoneCodes",
                table: "PhoneCodes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PersonPhones",
                table: "PersonPhones");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PersonEmails",
                table: "PersonEmails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PaymentStatuses",
                table: "PaymentStatuses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PaymentMethodTypes",
                table: "PaymentMethodTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PaymentMethods",
                table: "PaymentMethods");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PassengerTypes",
                table: "PassengerTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InvoiceItemTypes",
                table: "InvoiceItemTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FlightStatuses",
                table: "FlightStatuses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FlightSeats",
                table: "FlightSeats");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmailDomains",
                table: "EmailDomains");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DocumentTypes",
                table: "DocumentTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CheckinStatuses",
                table: "CheckinStatuses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CardTypes",
                table: "CardTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CardIssuers",
                table: "CardIssuers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CabinTypes",
                table: "CabinTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BaggageTypes",
                table: "BaggageTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AircraftManufacturers",
                table: "AircraftManufacturers");

            migrationBuilder.DeleteData(
                table: "AircraftManufacturers",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "AircraftManufacturers",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "BaggageTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "BaggageTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "BaggageTypes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "BaggageTypes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "CabinTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "CabinTypes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "CabinTypes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "CardIssuers",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "CardTypes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "CheckinStatuses",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "CheckinStatuses",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "CheckinStatuses",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "CheckinStatuses",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Continents",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "EmailDomains",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "EmailDomains",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "EmailDomains",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "EmailDomains",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "EmailDomains",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "EmailDomains",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "FlightSeats",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "FlightSeats",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "FlightSeats",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "FlightSeats",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "FlightSeats",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "FlightSeats",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "FlightSeats",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "FlightSeats",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "FlightSeats",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "FlightSeats",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "FlightSeats",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "FlightSeats",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "FlightSeats",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "FlightSeats",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "FlightSeats",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "FlightSeats",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "FlightSeats",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "FlightSeats",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "FlightSeats",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "FlightSeats",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "FlightSeats",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "FlightSeats",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "FlightSeats",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "FlightSeats",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "FlightSeats",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "FlightSeats",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "FlightSeats",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "FlightSeats",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "FlightSeats",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "FlightSeats",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "FlightStatuses",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "FlightStatuses",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "FlightStatuses",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "FlightStatuses",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "FlightStatuses",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "InvoiceItemTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "InvoiceItemTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "InvoiceItemTypes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "InvoiceItemTypes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "InvoiceItemTypes",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "PassengerTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PassengerTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "PassengerTypes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "PaymentMethods",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PaymentMethods",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "PaymentMethods",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "PaymentMethods",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "PaymentMethods",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "PaymentMethods",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "PaymentMethods",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "PaymentStatuses",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PaymentStatuses",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "PaymentStatuses",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "PaymentStatuses",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "PaymentStatuses",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "PhoneCodes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PhoneCodes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "PhoneCodes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "PhoneCodes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "PhoneCodes",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "PhoneCodes",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "PhoneCodes",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "PhoneCodes",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "PhoneCodes",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "PhoneCodes",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "PhoneCodes",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "PhoneCodes",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "PhoneCodes",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "PhoneCodes",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "PhoneCodes",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "PhoneCodes",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "StaffPositions",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "StaffPositions",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "StaffPositions",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "StaffPositions",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "StaffPositions",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "StreetTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "StreetTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "StreetTypes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "StreetTypes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "StreetTypes",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "StreetTypes",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "TicketStatuses",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TicketStatuses",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "TicketStatuses",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "TicketStatuses",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "TicketStatuses",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "aircraft",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "aircraft_models",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "airports",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "airports",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "fares",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "fares",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "fares",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "fares",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "fares",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "fares",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "fares",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "fares",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "fares",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "fares",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "fares",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "fares",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "fares",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "fares",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "fares",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "seasons",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "seasons",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "seasons",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "AircraftManufacturers",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "CabinTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "CardIssuers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "CardIssuers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "CardIssuers",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "CardTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "CardTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Continents",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "PassengerTypes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "PaymentMethodTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PaymentMethodTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "PaymentMethodTypes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "PaymentMethodTypes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "SeatLocationTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "SeatLocationTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "SeatLocationTypes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "SystemRoles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "SystemRoles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "SystemRoles",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "SystemRoles",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "airlines",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "flights",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "flights",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "flights",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "flights",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "flights",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "permissions",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "permissions",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "permissions",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "permissions",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "permissions",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "routes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "routes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "routes",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "routes",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "routes",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "routes",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "routes",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "routes",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "routes",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "routes",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "seasons",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "FlightStatuses",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "aircraft",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "aircraft",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "aircraft",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "aircraft",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "aircraft",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "airports",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "airports",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "airports",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "airports",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "airports",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "airports",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "airports",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "airports",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "regions",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "regions",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "routes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "routes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "routes",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "routes",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "routes",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "aircraft_models",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "aircraft_models",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "aircraft_models",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "aircraft_models",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "airlines",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "airlines",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "airlines",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "airlines",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "airports",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "airports",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "airports",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "airports",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "airports",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "airports",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "airports",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "airports",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "AircraftManufacturers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AircraftManufacturers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "cities",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "regions",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "regions",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "regions",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "regions",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "regions",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "regions",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "regions",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "regions",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "regions",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "regions",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "regions",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "regions",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "regions",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "regions",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "regions",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "regions",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Continents",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Continents",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Continents",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Continents",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.RenameTable(
                name: "Continents",
                newName: "continents");

            migrationBuilder.RenameTable(
                name: "TicketStatuses",
                newName: "ticket_statuses");

            migrationBuilder.RenameTable(
                name: "SystemRoles",
                newName: "system_roles");

            migrationBuilder.RenameTable(
                name: "StreetTypes",
                newName: "street_types");

            migrationBuilder.RenameTable(
                name: "StaffPositions",
                newName: "staff_positions");

            migrationBuilder.RenameTable(
                name: "SeatLocationTypes",
                newName: "seat_location_types");

            migrationBuilder.RenameTable(
                name: "RolePermissions",
                newName: "role_permissions");

            migrationBuilder.RenameTable(
                name: "PhoneCodes",
                newName: "phone_codes");

            migrationBuilder.RenameTable(
                name: "PersonPhones",
                newName: "person_phones");

            migrationBuilder.RenameTable(
                name: "PersonEmails",
                newName: "person_emails");

            migrationBuilder.RenameTable(
                name: "PaymentStatuses",
                newName: "payment_statuses");

            migrationBuilder.RenameTable(
                name: "PaymentMethodTypes",
                newName: "payment_method_types");

            migrationBuilder.RenameTable(
                name: "PaymentMethods",
                newName: "payment_methods");

            migrationBuilder.RenameTable(
                name: "PassengerTypes",
                newName: "passenger_types");

            migrationBuilder.RenameTable(
                name: "InvoiceItemTypes",
                newName: "invoice_item_types");

            migrationBuilder.RenameTable(
                name: "FlightStatuses",
                newName: "flight_statuses");

            migrationBuilder.RenameTable(
                name: "FlightSeats",
                newName: "flight_seats");

            migrationBuilder.RenameTable(
                name: "EmailDomains",
                newName: "email_domains");

            migrationBuilder.RenameTable(
                name: "DocumentTypes",
                newName: "document_types");

            migrationBuilder.RenameTable(
                name: "CheckinStatuses",
                newName: "checkin_statuses");

            migrationBuilder.RenameTable(
                name: "CardTypes",
                newName: "card_types");

            migrationBuilder.RenameTable(
                name: "CardIssuers",
                newName: "card_issuers");

            migrationBuilder.RenameTable(
                name: "CabinTypes",
                newName: "cabin_types");

            migrationBuilder.RenameTable(
                name: "BaggageTypes",
                newName: "baggage_types");

            migrationBuilder.RenameTable(
                name: "AircraftManufacturers",
                newName: "aircraft_manufacturers");

            migrationBuilder.RenameColumn(
                name: "origin_ip",
                table: "sessions",
                newName: "ip_address");

            migrationBuilder.RenameColumn(
                name: "closed_at",
                table: "sessions",
                newName: "ended_at");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "seasons",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "seasons",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_seasons_Name",
                table: "seasons",
                newName: "IX_seasons_name");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "routes",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Origin_airportId",
                table: "routes",
                newName: "origin_airport_id");

            migrationBuilder.RenameColumn(
                name: "Destination_airportId",
                table: "routes",
                newName: "destination_airport_id");

            migrationBuilder.RenameIndex(
                name: "IX_routes_Origin_airportId_Destination_airportId",
                table: "routes",
                newName: "IX_routes_origin_airport_id_destination_airport_id");

            migrationBuilder.RenameIndex(
                name: "IX_routes_Destination_airportId",
                table: "routes",
                newName: "IX_routes_destination_airport_id");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "regions",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "regions",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "CountryId",
                table: "regions",
                newName: "country_id");

            migrationBuilder.RenameIndex(
                name: "IX_regions_CountryId",
                table: "regions",
                newName: "IX_regions_country_id");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "permissions",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "permissions",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_permissions_Name",
                table: "permissions",
                newName: "IX_permissions_name");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "passengers",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "PersonId",
                table: "passengers",
                newName: "person_id");

            migrationBuilder.RenameColumn(
                name: "PassengerTypeId",
                table: "passengers",
                newName: "passenger_type_id");

            migrationBuilder.RenameIndex(
                name: "IX_passengers_PersonId",
                table: "passengers",
                newName: "IX_passengers_person_id");

            migrationBuilder.RenameIndex(
                name: "IX_passengers_PassengerTypeId",
                table: "passengers",
                newName: "IX_passengers_passenger_type_id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "flights",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "flights",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "RouteId",
                table: "flights",
                newName: "route_id");

            migrationBuilder.RenameColumn(
                name: "FlightstatusId",
                table: "flights",
                newName: "flight_status_id");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "flights",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "AirlineId",
                table: "flights",
                newName: "airline_id");

            migrationBuilder.RenameColumn(
                name: "AircraftId",
                table: "flights",
                newName: "aircraft_id");

            migrationBuilder.RenameIndex(
                name: "IX_flights_RouteId",
                table: "flights",
                newName: "IX_flights_route_id");

            migrationBuilder.RenameIndex(
                name: "IX_flights_FlightstatusId",
                table: "flights",
                newName: "IX_flights_flight_status_id");

            migrationBuilder.RenameIndex(
                name: "IX_flights_AirlineId",
                table: "flights",
                newName: "IX_flights_airline_id");

            migrationBuilder.RenameIndex(
                name: "IX_flights_AircraftId",
                table: "flights",
                newName: "IX_flights_aircraft_id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "fares",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "ValidTo",
                table: "fares",
                newName: "valid_to");

            migrationBuilder.RenameColumn(
                name: "ValidFrom",
                table: "fares",
                newName: "valid_from");

            migrationBuilder.RenameColumn(
                name: "SeasonId",
                table: "fares",
                newName: "season_id");

            migrationBuilder.RenameColumn(
                name: "RouteId",
                table: "fares",
                newName: "route_id");

            migrationBuilder.RenameColumn(
                name: "PassengerTypeId",
                table: "fares",
                newName: "passenger_type_id");

            migrationBuilder.RenameColumn(
                name: "CabinTypeId",
                table: "fares",
                newName: "cabin_type_id");

            migrationBuilder.RenameColumn(
                name: "BasePrice",
                table: "fares",
                newName: "base_price");

            migrationBuilder.RenameIndex(
                name: "IX_fares_SeasonId",
                table: "fares",
                newName: "IX_fares_season_id");

            migrationBuilder.RenameIndex(
                name: "IX_fares_RouteId",
                table: "fares",
                newName: "IX_fares_route_id");

            migrationBuilder.RenameIndex(
                name: "IX_fares_PassengerTypeId",
                table: "fares",
                newName: "IX_fares_passenger_type_id");

            migrationBuilder.RenameIndex(
                name: "IX_fares_CabinTypeId",
                table: "fares",
                newName: "IX_fares_cabin_type_id");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "countries",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "countries",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "ContinentId",
                table: "countries",
                newName: "continent_id");

            migrationBuilder.RenameColumn(
                name: "CodeIso",
                table: "countries",
                newName: "code_iso");

            migrationBuilder.RenameIndex(
                name: "IX_countries_ContinentId",
                table: "countries",
                newName: "IX_countries_continent_id");

            migrationBuilder.RenameIndex(
                name: "IX_countries_CodeIso",
                table: "countries",
                newName: "IX_countries_code_iso");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "continents",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "continents",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_Continents_Name",
                table: "continents",
                newName: "IX_continents_name");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "clients",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "PersonId",
                table: "clients",
                newName: "person_id");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "clients",
                newName: "created_at");

            migrationBuilder.RenameIndex(
                name: "IX_clients_PersonId",
                table: "clients",
                newName: "IX_clients_person_id");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "cities",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "cities",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "RegionId",
                table: "cities",
                newName: "region_id");

            migrationBuilder.RenameIndex(
                name: "IX_cities_RegionId",
                table: "cities",
                newName: "IX_cities_region_id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "check_ins",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "TicketId",
                table: "check_ins",
                newName: "ticket_id");

            migrationBuilder.RenameColumn(
                name: "StaffId",
                table: "check_ins",
                newName: "staff_id");

            migrationBuilder.RenameColumn(
                name: "Flight_seatId",
                table: "check_ins",
                newName: "flight_seat_id");

            migrationBuilder.RenameColumn(
                name: "CheckinstatusId",
                table: "check_ins",
                newName: "checkin_status_id");

            migrationBuilder.RenameIndex(
                name: "IX_check_ins_TicketId",
                table: "check_ins",
                newName: "IX_check_ins_ticket_id");

            migrationBuilder.RenameIndex(
                name: "IX_check_ins_StaffId",
                table: "check_ins",
                newName: "IX_check_ins_staff_id");

            migrationBuilder.RenameIndex(
                name: "IX_check_ins_Flight_seatId",
                table: "check_ins",
                newName: "IX_check_ins_flight_seat_id");

            migrationBuilder.RenameIndex(
                name: "IX_check_ins_CheckinstatusId",
                table: "check_ins",
                newName: "IX_check_ins_checkin_status_id");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "airports",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "airports",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "CityId",
                table: "airports",
                newName: "city_id");

            migrationBuilder.RenameIndex(
                name: "IX_airports_CityId",
                table: "airports",
                newName: "IX_airports_city_id");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "airlines",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "airlines",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "airlines",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "Origin_countryId",
                table: "airlines",
                newName: "origin_country_id");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "airlines",
                newName: "created_at");

            migrationBuilder.RenameIndex(
                name: "IX_airlines_Origin_countryId",
                table: "airlines",
                newName: "IX_airlines_origin_country_id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "aircraft_models",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "ManufacturerId",
                table: "aircraft_models",
                newName: "manufacturer_id");

            migrationBuilder.RenameIndex(
                name: "IX_aircraft_models_ManufacturerId",
                table: "aircraft_models",
                newName: "IX_aircraft_models_manufacturer_id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "aircraft",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "ModelId",
                table: "aircraft",
                newName: "model_id");

            migrationBuilder.RenameColumn(
                name: "AirlineId",
                table: "aircraft",
                newName: "airline_id");

            migrationBuilder.RenameIndex(
                name: "IX_aircraft_ModelId",
                table: "aircraft",
                newName: "IX_aircraft_model_id");

            migrationBuilder.RenameIndex(
                name: "IX_aircraft_AirlineId",
                table: "aircraft",
                newName: "IX_aircraft_airline_id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "addresses",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Street_typeId",
                table: "addresses",
                newName: "street_type_id");

            migrationBuilder.RenameColumn(
                name: "CityId",
                table: "addresses",
                newName: "city_id");

            migrationBuilder.RenameIndex(
                name: "IX_addresses_Street_typeId",
                table: "addresses",
                newName: "IX_addresses_street_type_id");

            migrationBuilder.RenameIndex(
                name: "IX_addresses_CityId",
                table: "addresses",
                newName: "IX_addresses_city_id");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "ticket_statuses",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ticket_statuses",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "system_roles",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "system_roles",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_SystemRoles_Name",
                table: "system_roles",
                newName: "IX_system_roles_name");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "street_types",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "street_types",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_StaffPositions_Name",
                table: "staff_positions",
                newName: "IX_staff_positions_Name");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "seat_location_types",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "seat_location_types",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_SeatLocationTypes_Name",
                table: "seat_location_types",
                newName: "IX_seat_location_types_name");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "role_permissions",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                table: "role_permissions",
                newName: "role_id");

            migrationBuilder.RenameColumn(
                name: "PermissionId",
                table: "role_permissions",
                newName: "permission_id");

            migrationBuilder.RenameIndex(
                name: "IX_RolePermissions_RoleId_PermissionId",
                table: "role_permissions",
                newName: "IX_role_permissions_role_id_permission_id");

            migrationBuilder.RenameIndex(
                name: "IX_RolePermissions_PermissionId",
                table: "role_permissions",
                newName: "IX_role_permissions_permission_id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "phone_codes",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_PhoneCodes_country_code",
                table: "phone_codes",
                newName: "IX_phone_codes_country_code");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "person_phones",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "PhonecodeId",
                table: "person_phones",
                newName: "phone_code_id");

            migrationBuilder.RenameColumn(
                name: "PersonId",
                table: "person_phones",
                newName: "person_id");

            migrationBuilder.RenameIndex(
                name: "IX_PersonPhones_PhonecodeId",
                table: "person_phones",
                newName: "IX_person_phones_phone_code_id");

            migrationBuilder.RenameIndex(
                name: "IX_PersonPhones_PersonId_PhonecodeId_phone_number",
                table: "person_phones",
                newName: "IX_person_phones_person_id_phone_code_id_phone_number");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "person_emails",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "PersonId",
                table: "person_emails",
                newName: "person_id");

            migrationBuilder.RenameColumn(
                name: "Email_domainId",
                table: "person_emails",
                newName: "email_domain_id");

            migrationBuilder.RenameIndex(
                name: "IX_PersonEmails_PersonId_email_local_part_Email_domainId",
                table: "person_emails",
                newName: "IX_person_emails_person_id_email_local_part_email_domain_id");

            migrationBuilder.RenameIndex(
                name: "IX_PersonEmails_PersonId",
                table: "person_emails",
                newName: "IX_person_emails_person_id");

            migrationBuilder.RenameIndex(
                name: "IX_PersonEmails_Email_domainId",
                table: "person_emails",
                newName: "IX_person_emails_email_domain_id");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "payment_statuses",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "payment_statuses",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_PaymentStatuses_Name",
                table: "payment_statuses",
                newName: "IX_payment_statuses_name");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "payment_method_types",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "payment_method_types",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_PaymentMethodTypes_Name",
                table: "payment_method_types",
                newName: "IX_payment_method_types_name");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "payment_methods",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Payment_method_typeId",
                table: "payment_methods",
                newName: "payment_method_type_id");

            migrationBuilder.RenameColumn(
                name: "Card_typeId",
                table: "payment_methods",
                newName: "card_type_id");

            migrationBuilder.RenameColumn(
                name: "Card_issuerId",
                table: "payment_methods",
                newName: "card_issuer_id");

            migrationBuilder.RenameIndex(
                name: "IX_PaymentMethods_Payment_method_typeId",
                table: "payment_methods",
                newName: "IX_payment_methods_payment_method_type_id");

            migrationBuilder.RenameIndex(
                name: "IX_PaymentMethods_commercial_name",
                table: "payment_methods",
                newName: "IX_payment_methods_commercial_name");

            migrationBuilder.RenameIndex(
                name: "IX_PaymentMethods_Card_typeId",
                table: "payment_methods",
                newName: "IX_payment_methods_card_type_id");

            migrationBuilder.RenameIndex(
                name: "IX_PaymentMethods_Card_issuerId",
                table: "payment_methods",
                newName: "IX_payment_methods_card_issuer_id");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "passenger_types",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "passenger_types",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_PassengerTypes_Name",
                table: "passenger_types",
                newName: "IX_passenger_types_name");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "invoice_item_types",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "invoice_item_types",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_InvoiceItemTypes_Name",
                table: "invoice_item_types",
                newName: "IX_invoice_item_types_name");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "flight_statuses",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "flight_statuses",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_FlightStatuses_Name",
                table: "flight_statuses",
                newName: "IX_flight_statuses_name");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "flight_seats",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "LocationtypeId",
                table: "flight_seats",
                newName: "location_type_id");

            migrationBuilder.RenameColumn(
                name: "FlightId",
                table: "flight_seats",
                newName: "flight_id");

            migrationBuilder.RenameColumn(
                name: "CabinTypeId",
                table: "flight_seats",
                newName: "cabin_type_id");

            migrationBuilder.RenameIndex(
                name: "IX_FlightSeats_LocationtypeId",
                table: "flight_seats",
                newName: "IX_flight_seats_location_type_id");

            migrationBuilder.RenameIndex(
                name: "IX_FlightSeats_FlightId_seat_code",
                table: "flight_seats",
                newName: "IX_flight_seats_flight_id_seat_code");

            migrationBuilder.RenameIndex(
                name: "IX_FlightSeats_CabinTypeId",
                table: "flight_seats",
                newName: "IX_flight_seats_cabin_type_id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "email_domains",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_EmailDomains_domain",
                table: "email_domains",
                newName: "IX_email_domains_domain");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "document_types",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "document_types",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_DocumentTypes_code",
                table: "document_types",
                newName: "IX_document_types_code");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "checkin_statuses",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "checkin_statuses",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "card_types",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "card_types",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_CardTypes_Name",
                table: "card_types",
                newName: "IX_card_types_name");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "card_issuers",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "card_issuers",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_CardIssuers_Name",
                table: "card_issuers",
                newName: "IX_card_issuers_name");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "cabin_types",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "cabin_types",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_CabinTypes_Name",
                table: "cabin_types",
                newName: "IX_cabin_types_name");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "baggage_types",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "baggage_types",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "MaxWeightKg",
                table: "baggage_types",
                newName: "max_weight_kg");

            migrationBuilder.RenameColumn(
                name: "BasePrice",
                table: "baggage_types",
                newName: "base_price");

            migrationBuilder.RenameIndex(
                name: "IX_BaggageTypes_Name",
                table: "baggage_types",
                newName: "IX_baggage_types_name");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "aircraft_manufacturers",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Country",
                table: "aircraft_manufacturers",
                newName: "country");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "aircraft_manufacturers",
                newName: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_continents",
                table: "continents",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ticket_statuses",
                table: "ticket_statuses",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_system_roles",
                table: "system_roles",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_street_types",
                table: "street_types",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_staff_positions",
                table: "staff_positions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_seat_location_types",
                table: "seat_location_types",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_role_permissions",
                table: "role_permissions",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_phone_codes",
                table: "phone_codes",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_person_phones",
                table: "person_phones",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_person_emails",
                table: "person_emails",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_payment_statuses",
                table: "payment_statuses",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_payment_method_types",
                table: "payment_method_types",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_payment_methods",
                table: "payment_methods",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_passenger_types",
                table: "passenger_types",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_invoice_item_types",
                table: "invoice_item_types",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_flight_statuses",
                table: "flight_statuses",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_flight_seats",
                table: "flight_seats",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_email_domains",
                table: "email_domains",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_document_types",
                table: "document_types",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_checkin_statuses",
                table: "checkin_statuses",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_card_types",
                table: "card_types",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_card_issuers",
                table: "card_issuers",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_cabin_types",
                table: "cabin_types",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_baggage_types",
                table: "baggage_types",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_aircraft_manufacturers",
                table: "aircraft_manufacturers",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_addresses_cities_city_id",
                table: "addresses",
                column: "city_id",
                principalTable: "cities",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_addresses_street_types_street_type_id",
                table: "addresses",
                column: "street_type_id",
                principalTable: "street_types",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_aircraft_aircraft_models_model_id",
                table: "aircraft",
                column: "model_id",
                principalTable: "aircraft_models",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_aircraft_airlines_airline_id",
                table: "aircraft",
                column: "airline_id",
                principalTable: "airlines",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_aircraft_models_aircraft_manufacturers_manufacturer_id",
                table: "aircraft_models",
                column: "manufacturer_id",
                principalTable: "aircraft_manufacturers",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_airlines_countries_origin_country_id",
                table: "airlines",
                column: "origin_country_id",
                principalTable: "countries",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_airports_cities_city_id",
                table: "airports",
                column: "city_id",
                principalTable: "cities",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_baggage_baggage_types_baggage_type_id",
                table: "baggage",
                column: "baggage_type_id",
                principalTable: "baggage_types",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_cabin_configurations_cabin_types_cabin_type_id",
                table: "cabin_configurations",
                column: "cabin_type_id",
                principalTable: "cabin_types",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_check_ins_checkin_statuses_checkin_status_id",
                table: "check_ins",
                column: "checkin_status_id",
                principalTable: "checkin_statuses",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_check_ins_flight_seats_flight_seat_id",
                table: "check_ins",
                column: "flight_seat_id",
                principalTable: "flight_seats",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_check_ins_staff_staff_id",
                table: "check_ins",
                column: "staff_id",
                principalTable: "staff",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_check_ins_tickets_ticket_id",
                table: "check_ins",
                column: "ticket_id",
                principalTable: "tickets",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_cities_regions_region_id",
                table: "cities",
                column: "region_id",
                principalTable: "regions",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_clients_persons_person_id",
                table: "clients",
                column: "person_id",
                principalTable: "persons",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_countries_continents_continent_id",
                table: "countries",
                column: "continent_id",
                principalTable: "continents",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_fares_cabin_types_cabin_type_id",
                table: "fares",
                column: "cabin_type_id",
                principalTable: "cabin_types",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_fares_passenger_types_passenger_type_id",
                table: "fares",
                column: "passenger_type_id",
                principalTable: "passenger_types",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_fares_routes_route_id",
                table: "fares",
                column: "route_id",
                principalTable: "routes",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_fares_seasons_season_id",
                table: "fares",
                column: "season_id",
                principalTable: "seasons",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_flight_seats_cabin_types_cabin_type_id",
                table: "flight_seats",
                column: "cabin_type_id",
                principalTable: "cabin_types",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_flight_seats_flights_flight_id",
                table: "flight_seats",
                column: "flight_id",
                principalTable: "flights",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_flight_seats_seat_location_types_location_type_id",
                table: "flight_seats",
                column: "location_type_id",
                principalTable: "seat_location_types",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_flight_status_transitions_flight_statuses_from_status_id",
                table: "flight_status_transitions",
                column: "from_status_id",
                principalTable: "flight_statuses",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_flight_status_transitions_flight_statuses_to_status_id",
                table: "flight_status_transitions",
                column: "to_status_id",
                principalTable: "flight_statuses",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_flights_aircraft_aircraft_id",
                table: "flights",
                column: "aircraft_id",
                principalTable: "aircraft",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_flights_airlines_airline_id",
                table: "flights",
                column: "airline_id",
                principalTable: "airlines",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_flights_flight_statuses_flight_status_id",
                table: "flights",
                column: "flight_status_id",
                principalTable: "flight_statuses",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_flights_routes_route_id",
                table: "flights",
                column: "route_id",
                principalTable: "routes",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_invoice_items_invoice_item_types_invoice_item_type_id",
                table: "invoice_items",
                column: "invoice_item_type_id",
                principalTable: "invoice_item_types",
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
                name: "FK_payment_methods_card_issuers_card_issuer_id",
                table: "payment_methods",
                column: "card_issuer_id",
                principalTable: "card_issuers",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_payment_methods_card_types_card_type_id",
                table: "payment_methods",
                column: "card_type_id",
                principalTable: "card_types",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_payment_methods_payment_method_types_payment_method_type_id",
                table: "payment_methods",
                column: "payment_method_type_id",
                principalTable: "payment_method_types",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_payments_payment_methods_payment_method_id",
                table: "payments",
                column: "payment_method_id",
                principalTable: "payment_methods",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_payments_payment_statuses_payment_status_id",
                table: "payments",
                column: "payment_status_id",
                principalTable: "payment_statuses",
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
                name: "FK_routes_airports_destination_airport_id",
                table: "routes",
                column: "destination_airport_id",
                principalTable: "airports",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_routes_airports_origin_airport_id",
                table: "routes",
                column: "origin_airport_id",
                principalTable: "airports",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_staff_staff_positions_position_id",
                table: "staff",
                column: "position_id",
                principalTable: "staff_positions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tickets_ticket_statuses_ticket_status_id",
                table: "tickets",
                column: "ticket_status_id",
                principalTable: "ticket_statuses",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_users_system_roles_role_id",
                table: "users",
                column: "role_id",
                principalTable: "system_roles",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
