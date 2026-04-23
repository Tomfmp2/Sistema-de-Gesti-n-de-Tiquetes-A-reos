using sistema_gestor_de_tiquetes_aereos.Src.Modules.Aircraft.Application.UseCases;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Aircraft.Infrastructure.Repository;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Aircraft.UI;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.AircraftManufacturers.Application.UseCases;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.AircraftManufacturers.Infrastructure.repository;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.AircraftManufacturers.UI;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.AircraftModels.Application.UseCases;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.AircraftModels.Infrastructure.repository;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.AircraftModels.UI;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Airlines.Application.UseCases;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Airlines.Infrastructure.repository;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Airlines.UI;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.AirportAirline.Application.UseCases;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.AirportAirline.Infrastructure.repository;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.AirportAirline.UI;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Airports.Application.UseCases;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Airports.Infrastructure.repository;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Airports.UI;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.AvailabilityStatuses.Application.UseCases;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.AvailabilityStatuses.Infrastructure.Repository;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.AvailabilityStatuses.UI;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Baggage.Application.UseCases;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Baggage.Infrastructure.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Baggage.UI;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.BaggageTypes.Application.UseCases;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.BaggageTypes.Infrastructure.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.BaggageTypes.UI;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.CabinConfiguration.UI;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.CabinTypes.Application.UseCases;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.CabinTypes.Infrastructure.repository;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.CabinTypes.UI;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Checkins.Application.UseCases;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Checkins.Infrastructure.repository;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Checkins.UI;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Fares.Application.UseCases;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Fares.Infrastructure.repository;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Fares.UI;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Flights.Application.UseCases;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Flights.Infrastructure.repository;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Flights.UI;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightAssignments.Application.UseCases;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightAssignments.Infrastructure.repository;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightAssignments.UI;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightRoles.Application.Services;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightRoles.Application.UseCases;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightRoles.Infrastructure.repository;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightRoles.UI;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightSeats.Infrastructure.repository;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightSeats.UI;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatuses.Application.UseCases;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatuses.Infrastructure.repository;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatuses.UI;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.RouteLayovers.Application.UseCases;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.RouteLayovers.Infrastructure.repository;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.RouteLayovers.UI;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Routes.Application.UseCases;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Routes.Infrastructure.repository;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Routes.UI;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Seasons.Application.UseCases;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Seasons.Infrastructure.repository;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Seasons.UI;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.SeatLocationTypes.Application.UseCases;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.SeatLocationTypes.Infrastructure.repository;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.SeatLocationTypes.UI;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Reservations.Application.UseCases;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Reservations.Infrastructure.repository;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Reservations.UI;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationFlights.Application.UseCases;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationFlights.Infrastructure.repository;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationPassengers.Application.UseCases;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationPassengers.Infrastructure.repository;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Passengers.Application.UseCases;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Passengers.Infrastructure.repository;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Payments.Application.UseCases;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Payments.Infrastructure.repository;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Payments.UI;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Staff.Application.UseCases;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Staff.Infrastructure.Repository;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Staff.UI;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.StaffAvailability.Application.UseCases;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.StaffAvailability.Infrastructure.repository;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.StaffAvailability.UI;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.StaffPositions.Application.UseCases;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.StaffPositions.Infrastructure.Repository;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.StaffPositions.UI;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Tickets.Application.UseCases;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Tickets.Infrastructure.repository;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Tickets.UI;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Invoices.Application.UseCases;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Invoices.Infrastructure.repository;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Invoices.UI;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Users.Application.Services;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Users.Application.UseCases;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Users.Infrastructure.repository;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Users.UI;

namespace sistema_gestor_de_tiquetes_aereos.Src.Shared.Ui;

public static class ModuleUiFactory
{
    public static AgentReservationsConsoleUI CreateAgentReservationsUi(AppDbContext ctx)
    {
        var repo = new ReservationRepository(ctx);
        var rfRepo = new ReservationFlightRepository(ctx);
        var rpRepo = new ReservationPassengerRepository(ctx);
        var passengerRepo = new PassengerRepository(ctx);
        return new AgentReservationsConsoleUI(
            ctx,
            new CreateReservationUseCase(repo),
            new GetAllReservationsUseCase(repo),
            new GetReservationByIdUseCase(repo),
            new UpdateReservationUseCase(repo),
            new CreateReservationFlightUseCase(rfRepo),
            new CreateReservationPassengerUseCase(rpRepo),
            new CreatePassengerUseCase(passengerRepo)
        );
    }

    public static AirlineConsoleUI CreateAirlineUi(AppDbContext ctx)
    {
        var repo = new AirlineRepository(ctx);
        return new AirlineConsoleUI(
            ctx,
            new CreateAirlineUseCase(repo),
            new GetAirlineByIdUseCase(repo),
            new GetAllAirlinesUseCase(repo),
            new UpdateAirlineUseCase(repo),
            new DeleteAirlineUseCase(repo)
        );
    }

    public static AirportConsoleUI CreateAirportUi(AppDbContext ctx)
    {
        var repo = new AirportRepository(ctx);
        return new AirportConsoleUI(
            ctx,
            new CreateAirportUseCase(repo),
            new GetAirportByIdUseCase(repo),
            new GetAllAirportsUseCase(repo),
            new UpdateAirportUseCase(repo),
            new DeleteAirportUseCase(repo)
        );
    }

    public static AirportAirlineConsoleUI CreateAirportAirlineUi(AppDbContext ctx)
    {
        var repo = new AirportAirlineRepository(ctx);
        return new AirportAirlineConsoleUI(
            new CreateAirportAirlineUseCase(repo),
            new GetAirportAirlineByIdUseCase(repo),
            new GetAllAirportAirlinesUseCase(repo),
            new UpdateAirportAirlineUseCase(repo),
            new DeleteAirportAirlineUseCase(repo)
        );
    }

    public static FlightConsoleUI CreateFlightUi(AppDbContext ctx)
    {
        var repo = new FlightRepository(ctx);
        return new FlightConsoleUI(
            new CreateFlightUseCase(repo),
            new GetFlightByIdUseCase(repo),
            new GetAllFlightsUseCase(repo),
            new UpdateFlightUseCase(repo),
            new DeleteFlightUseCase(repo)
        );
    }

    public static FareConsoleUI CreateFareUi(AppDbContext ctx)
    {
        var repo = new FareRepository(ctx);
        return new FareConsoleUI(
            new CreateFareUseCase(repo),
            new GetFareByIdUseCase(repo),
            new GetAllFaresUseCase(repo),
            new UpdateFareUseCase(repo),
            new DeleteFareUseCase(repo)
        );
    }

    public static PaymentConsoleUI CreatePaymentUi(AppDbContext ctx)
    {
        var repo = new PaymentRepository(ctx);
        return new PaymentConsoleUI(
            new CreatePaymentUseCase(repo),
            new GetPaymentByIdUseCase(repo),
            new GetAllPaymentsUseCase(repo),
            new UpdatePaymentUseCase(repo),
            new DeletePaymentUseCase(repo)
        );
    }

    public static TicketConsoleUI CreateTicketUi(AppDbContext ctx)
    {
        var repo = new TicketRepository(ctx);
        return new TicketConsoleUI(
            new CreateTicketUseCase(repo),
            new GetTicketByIdUseCase(repo),
            new GetAllTicketsUseCase(repo),
            new UpdateTicketUseCase(repo),
            new DeleteTicketUseCase(repo)
        );
    }

    public static CheckinConsoleUI CreateCheckinUi(AppDbContext ctx)
    {
        var repo = new CheckinRepository(ctx);
        return new CheckinConsoleUI(
            new CreateCheckinUseCase(repo),
            new GetCheckinByIdUseCase(repo),
            new GetAllCheckinsUseCase(repo),
            new UpdateCheckinUseCase(repo),
            new DeleteCheckinUseCase(repo)
        );
    }

    public static InvoiceConsoleUI CreateInvoiceUi(AppDbContext ctx)
    {
        var repo = new InvoiceRepository(ctx);
        return new InvoiceConsoleUI(
            new CreateInvoiceUseCase(repo),
            new GetInvoiceByIdUseCase(repo),
            new GetAllInvoicesUseCase(repo),
            new UpdateInvoiceUseCase(repo),
            new DeleteInvoiceUseCase(repo)
        );
    }

    public static AircraftManufacturerUI CreateAircraftManufacturerUi(AppDbContext ctx)
    {
        var repo = new AircraftManufacturerRepository(ctx);
        return new AircraftManufacturerUI(
            new CreateAircraftManufacturerUseCase(repo),
            new GetAllAircraftManufacturersUseCase(repo),
            new GetAircraftManufacturerByIdUseCase(repo),
            new UpdateAircraftManufacturerUseCase(repo),
            new DeleteAircraftManufacturerUseCase(repo)
        );
    }

    public static AircraftModelUI CreateAircraftModelUi(AppDbContext ctx)
    {
        var repo = new AircraftModelRepository(ctx);
        return new AircraftModelUI(
            new CreateAircraftModelUseCase(repo),
            new GetAllAircraftModelsUseCase(repo),
            new GetAircraftModelByIdUseCase(repo),
            new UpdateAircraftModelUseCase(repo),
            new DeleteAircraftModelUseCase(repo)
        );
    }

    public static AircraftUI CreateAircraftUi(AppDbContext ctx) => new(ctx);

    public static CabinTypeUI CreateCabinTypeUi(AppDbContext ctx) => new(ctx);

    public static CabinConfigurationConsoleUI CreateCabinConfigurationUi(AppDbContext ctx) =>
        new(ctx);

    public static RouteConsoleUI CreateRouteUi(AppDbContext ctx)
    {
        var repo = new RouteRepository(ctx);
        return new RouteConsoleUI(
            new CreateRouteUseCase(repo),
            new GetRouteByIdUseCase(repo),
            new GetAllRoutesUseCase(repo),
            new UpdateRouteUseCase(repo),
            new DeleteRouteUseCase(repo)
        );
    }

    public static RouteLayoverConsoleUI CreateRouteLayoverUi(AppDbContext ctx)
    {
        var repo = new RouteLayoverRepository(ctx);
        return new RouteLayoverConsoleUI(
            new CreateRouteLayoverUseCase(repo),
            new GetRouteLayoverByIdUseCase(repo),
            new GetAllRouteLayoversUseCase(repo),
            new UpdateRouteLayoverUseCase(repo),
            new DeleteRouteLayoverUseCase(repo)
        );
    }

    public static SeasonConsoleUI CreateSeasonUi(AppDbContext ctx)
    {
        var repo = new SeasonRepository(ctx);
        return new SeasonConsoleUI(
            new CreateSeasonUseCase(repo),
            new GetSeasonByIdUseCase(repo),
            new GetAllSeasonsUseCase(repo),
            new UpdateSeasonUseCase(repo),
            new DeleteSeasonUseCase(repo)
        );
    }

    public static SeatLocationTypeConsoleUI CreateSeatLocationTypeUi(AppDbContext ctx)
    {
        var repo = new SeatLocationTypeRepository(ctx);
        return new SeatLocationTypeConsoleUI(
            new CreateSeatLocationTypeUseCase(repo),
            new GetSeatLocationTypeByIdUseCase(repo),
            new GetAllSeatLocationTypesUseCase(repo),
            new UpdateSeatLocationTypeUseCase(repo),
            new DeleteSeatLocationTypeUseCase(repo)
        );
    }

    public static FlightSeatConsoleUI CreateFlightSeatUi(AppDbContext ctx) =>
        new(new FlightSeatRepository(ctx));

    public static FlightAssignmentConsoleUI CreateFlightAssignmentUi(AppDbContext ctx)
    {
        var repo = new FlightAssignmentRepository(ctx);
        return new FlightAssignmentConsoleUI(
            new CreateFlightAssignmentUseCase(repo),
            new GetFlightAssignmentByIdUseCase(repo),
            new GetAllFlightAssignmentsUseCase(repo),
            new UpdateFlightAssignmentUseCase(repo),
            new DeleteFlightAssignmentUseCase(repo)
        );
    }

    public static FlightStatusConsoleUI CreateFlightStatusUi(AppDbContext ctx)
    {
        var repo = new FlightStatusRepository(ctx);
        return new FlightStatusConsoleUI(
            new CreateFlightStatusUseCase(repo),
            new GetFlightStatusByIdUseCase(repo),
            new GetAllFlightStatusesUseCase(repo),
            new UpdateFlightStatusUseCase(repo),
            new DeleteFlightStatusUseCase(repo)
        );
    }

    public static FlightRoleConsoleUI CreateFlightRoleUi(AppDbContext ctx)
    {
        var repo = new FlightRoleRepository(ctx);
        var service = new FlightRoleService(
            new CreateFlightRoleUseCase(repo),
            new GetFlightRoleByIdUseCase(repo),
            new GetAllFlightRolesUseCase(repo),
            new UpdateFlightRoleUseCase(repo),
            new DeleteFlightRoleUseCase(repo)
        );
        return new FlightRoleConsoleUI(service);
    }

    public static StaffConsoleUI CreateStaffUi(AppDbContext ctx)
    {
        var repo = new StaffRepository(ctx);
        return new StaffConsoleUI(
            new CreateStaffUseCase(repo),
            new GetStaffByIdUseCase(repo),
            new GetAllStaffUseCase(repo),
            new UpdateStaffUseCase(repo),
            new DeleteStaffUseCase(repo)
        );
    }

    public static StaffPositionConsoleUI CreateStaffPositionUi(AppDbContext ctx)
    {
        var repo = new StaffPositionRepository(ctx);
        return new StaffPositionConsoleUI(
            new CreateStaffPositionUseCase(repo),
            new GetStaffPositionByIdUseCase(repo),
            new GetAllStaffPositionsUseCase(repo),
            new UpdateStaffPositionUseCase(repo),
            new DeleteStaffPositionUseCase(repo)
        );
    }

    public static AvailabilityStatusConsoleUI CreateAvailabilityStatusUi(AppDbContext ctx)
    {
        var repo = new AvailabilityStatusRepository(ctx);
        return new AvailabilityStatusConsoleUI(
            new CreateAvailabilityStatusUseCase(repo),
            new GetAvailabilityStatusByIdUseCase(repo),
            new GetAllAvailabilityStatusesUseCase(repo),
            new UpdateAvailabilityStatusUseCase(repo),
            new DeleteAvailabilityStatusUseCase(repo)
        );
    }

    public static StaffAvailabilityUI CreateStaffAvailabilityUi(AppDbContext ctx)
    {
        var repo = new StaffAvailabilityRepository(ctx);
        return new StaffAvailabilityUI(
            new CreateStaffAvailabilityUseCase(repo),
            new GetAllStaffAvailabilitiesUseCase(repo),
            new GetStaffAvailabilityByIdUseCase(repo),
            new UpdateStaffAvailabilityUseCase(repo),
            new DeleteStaffAvailabilityUseCase(repo)
        );
    }

    public static BaggageTypeConsoleUI CreateBaggageTypeUi(AppDbContext ctx)
    {
        var repo = new BaggageTypeRepository(ctx);
        return new BaggageTypeConsoleUI(
            new CreateBaggageTypeUseCase(repo),
            new GetBaggageTypeByIdUseCase(repo),
            new GetAllBaggageTypesUseCase(repo),
            new UpdateBaggageTypeUseCase(repo),
            new DeleteBaggageTypeUseCase(repo)
        );
    }

    public static BaggageConsoleUI CreateBaggageUi(AppDbContext ctx)
    {
        var repo = new BaggageRepository(ctx);
        return new BaggageConsoleUI(
            new CreateBaggageUseCase(repo),
            new GetBaggageByIdUseCase(repo),
            new GetAllBaggagesUseCase(repo),
            new UpdateBaggageUseCase(repo),
            new DeleteBaggageUseCase(repo)
        );
    }

    public static MyReservationsConsoleUI CreateMyReservationsUi(AppDbContext ctx, AuthContext auth)
    {
        var repo = new ReservationRepository(ctx);
        return new MyReservationsConsoleUI(
            auth,
            ctx,
            new GetReservationsByClientIdUseCase(repo),
            new CreateReservationUseCase(repo)
        );
    }

    public static CreateMyReservationConsoleUI CreateCreateMyReservationUi(AppDbContext ctx, AuthContext auth)
    {
        var repo = new ReservationRepository(ctx);
        return new CreateMyReservationConsoleUI(auth, new CreateReservationUseCase(repo));
    }

    public static ClientReservationsConsoleUI CreateClientReservationsUi(AppDbContext ctx, AuthContext auth)
    {
        var repo = new ReservationRepository(ctx);
        var rfRepo = new ReservationFlightRepository(ctx);
        var rpRepo = new ReservationPassengerRepository(ctx);
        var passengerRepo = new PassengerRepository(ctx);
        return new ClientReservationsConsoleUI(
            auth,
            ctx,
            new CreateReservationUseCase(repo),
            new GetReservationsByClientIdUseCase(repo),
            new GetReservationByIdUseCase(repo),
            new UpdateReservationUseCase(repo),
            new DeleteReservationUseCase(repo),
            new CreateReservationFlightUseCase(rfRepo),
            new CreateReservationPassengerUseCase(rpRepo),
            new CreatePassengerUseCase(passengerRepo)
        );
    }

    public static UserConsoleUI CreateUserUi(AppDbContext ctx)
    {
        var repo = new UserRepository(ctx);
        var service = new UserService(
            new CreateUserUseCase(repo),
            new GetUserByIdUseCase(repo),
            new GetAllUsersUseCase(repo),
            new UpdateUserUseCase(repo),
            new DeleteUserUseCase(repo)
        );
        return new UserConsoleUI(ctx, service);
    }
}
