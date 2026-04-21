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

namespace sistema_gestor_de_tiquetes_aereos.Src.Shared.Ui;

public static class ModuleUiFactory
{
    public static AirlineConsoleUI CreateAirlineUi(AppDbContext ctx)
    {
        var repo = new AirlineRepository(ctx);
        return new AirlineConsoleUI(
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

    // Nota: módulos de rutas/escalas/temporadas/asientos por vuelo quedan fuera por ahora
    // para mantener la UI básica sin adaptadores adicionales.

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

    // (SeatLocationTypes / FlightSeats / FlightAssignments también se dejan fuera
    // para evitar adaptadores en esta fase visual.)

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

    // ...

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
}
