using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace sistema_gestor_de_tiquetes_aereos.Migrations
{
    /// <inheritdoc />
    public partial class AddForeignKeysOnly : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Remaining schema alignment needed for FK creation
            migrationBuilder.RenameTable(name: "flightseats", newName: "flight_seats");
            migrationBuilder.RenameColumn(name: "Id", table: "flight_seats", newName: "id");
            migrationBuilder.RenameColumn(name: "FlightId", table: "flight_seats", newName: "flight_id");
            migrationBuilder.RenameColumn(name: "SeatCode", table: "flight_seats", newName: "seat_code");
            migrationBuilder.RenameColumn(name: "CabinTypeId", table: "flight_seats", newName: "cabin_type_id");
            migrationBuilder.RenameColumn(name: "LocationTypeId", table: "flight_seats", newName: "location_type_id");
            migrationBuilder.RenameColumn(name: "IsOccupied", table: "flight_seats", newName: "is_occupied");

            migrationBuilder.RenameColumn(name: "Id", table: "flight_crew_assignments", newName: "id");
            migrationBuilder.RenameColumn(name: "FlightId", table: "flight_crew_assignments", newName: "flight_id");
            migrationBuilder.RenameColumn(name: "StaffId", table: "flight_crew_assignments", newName: "staff_id");
            migrationBuilder.RenameColumn(name: "FlightRoleId", table: "flight_crew_assignments", newName: "crew_role_id");

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

            migrationBuilder.RenameColumn(name: "departure_date", table: "flights", newName: "departure_at");
            migrationBuilder.RenameColumn(name: "estimated_arrival_date", table: "flights", newName: "estimated_arrival_at");

            migrationBuilder.RenameColumn(name: "start_row", table: "cabin_configurations", newName: "row_start");
            migrationBuilder.RenameColumn(name: "end_row", table: "cabin_configurations", newName: "row_end");

            migrationBuilder.RenameColumn(name: "origin_status_id", table: "flight_status_transitions", newName: "from_status_id");
            migrationBuilder.RenameColumn(name: "destination_status_id", table: "flight_status_transitions", newName: "to_status_id");

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

            migrationBuilder.AddForeignKey("FK_payments_bookings_booking_id", "payments", "booking_id", "bookings", principalColumn: "id", onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey("FK_payments_payment_methods_payment_method_id", "payments", "payment_method_id", "payment_methods", principalColumn: "id", onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey("FK_payments_payment_statuses_payment_status_id", "payments", "payment_status_id", "payment_statuses", principalColumn: "id", onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey("FK_persons_addresses_address_id", "persons", "address_id", "addresses", principalColumn: "id", onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey("FK_persons_document_types_document_type_id", "persons", "document_type_id", "document_types", principalColumn: "id", onDelete: ReferentialAction.Restrict);

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
            migrationBuilder.DropForeignKey("FK_payments_payment_methods_payment_method_id", "payments");
            migrationBuilder.DropForeignKey("FK_payments_payment_statuses_payment_status_id", "payments");
            migrationBuilder.DropForeignKey("FK_persons_addresses_address_id", "persons");
            migrationBuilder.DropForeignKey("FK_persons_document_types_document_type_id", "persons");
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

