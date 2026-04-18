using Microsoft.EntityFrameworkCore;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.CardIssuers.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.CardTypes.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.CheckinStatuses.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Checkins.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Cities.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Continents.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Countries.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Fares.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Flights.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatuses.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatusTransitions.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.InvoiceItems.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.InvoiceItemTypes.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Invoices.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentMethods.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentMethodTypes.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Payments.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentStatuses.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Regions.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationFlights.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationPassengers.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationStatuses.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationStatusTransitions.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Reservations.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Tickets.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.TicketStatuses.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Airlines.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Airports.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.AirportAirline.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.StaffPositions.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Staff.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.AvailabilityStatuses.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.StaffAvailability.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.AircraftManufacturers.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.AircraftModels.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Aircraft.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.CabinTypes.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.CabinConfiguration.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Routes.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.RouteLayovers.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Seasons.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.SeatLocationTypes.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightAssignments.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightSeats.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Persons.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.DocumentTypes.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PersonEmails.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PersonPhones.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.EmailDomains.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PhoneCodes.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.StreetsTypes.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Directions.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Clients.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Passengers.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PassengerTypes.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Users.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.SystemRoles.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Permissions.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.RolePermissions.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Sessions.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightRoles.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.BaggageTypes.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;

public class AppDbContext : DbContext
{
    // Geography
    public DbSet<ContinentEntity> Continents { get; set; }
    public DbSet<CountryEntity> Countries { get; set; }
    public DbSet<RegionEntity> Regions { get; set; }
    public DbSet<CityEntity> Cities { get; set; }

    // Persons & Related
    public DbSet<DocumentTypeEntity> DocumentTypes { get; set; }
    public DbSet<PersonEntity> Persons { get; set; }
    public DbSet<PersonEmailEntity> PersonEmails { get; set; }
    public DbSet<PersonPhoneEntity> PersonPhones { get; set; }
    public DbSet<EmailDomainEntity> EmailDomains { get; set; }
    public DbSet<PhoneCodeEntity> PhoneCodes { get; set; }

    // Addresses
    public DbSet<StreetTypeEntity> StreetTypes { get; set; }
    public DbSet<DirectionEntity> Directions { get; set; }

    // Users & Authorization
    public DbSet<ClientEntity> Clients { get; set; }
    public DbSet<PassengerEntity> Passengers { get; set; }
    public DbSet<PassengerTypeEntity> PassengerTypes { get; set; }
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<SystemRoleEntity> SystemRoles { get; set; }
    public DbSet<PermissionEntity> Permissions { get; set; }
    public DbSet<RolePermissionEntity> RolePermissions { get; set; }
    public DbSet<SessionEntity> Sessions { get; set; }

    // Airlines & Airports
    public DbSet<AirlineEntity> Airlines { get; set; }
    public DbSet<AirportEntity> Airports { get; set; }
    public DbSet<AirportAirlineEntity> AirportAirlines { get; set; }

    // Staff
    public DbSet<StaffPositionEntity> StaffPositions { get; set; }
    public DbSet<StaffEntity> Staff { get; set; }
    public DbSet<AvailabilityStatusEntity> AvailabilityStatuses { get; set; }
    public DbSet<StaffAvailabilityEntity> StaffAvailabilities { get; set; }

    // Aircraft & Cabin
    public DbSet<AircraftManufacturerEntity> AircraftManufacturers { get; set; }
    public DbSet<AircraftModelEntity> AircraftModels { get; set; }
    public DbSet<AircraftEntity> Aircraft { get; set; }
    public DbSet<CabinTypeEntity> CabinTypes { get; set; }
    public DbSet<CabinConfigurationEntity> CabinConfiguration { get; set; }

    // Routes
    public DbSet<RouteEntity> Routes { get; set; }
    public DbSet<RouteLayoverEntity> RouteLayovers { get; set; }

    // Flights
    public DbSet<SeasonEntity> Seasons { get; set; }
    public DbSet<FareEntity> Fares { get; set; }
    public DbSet<FlightStatusEntity> FlightStatuses { get; set; }
    public DbSet<FlightStatusTransitionEntity> FlightStatusTransitions { get; set; }
    public DbSet<FlightEntity> Flights { get; set; }
    public DbSet<SeatLocationTypeEntity> SeatLocationTypes { get; set; }
    public DbSet<FlightSeatEntity> FlightSeats { get; set; }

    // Flight Crew
    public DbSet<FlightRoleEntity> FlightRoles { get; set; }
    public DbSet<FlightAssignmentEntity> FlightAssignments { get; set; }

    // Reservations
    public DbSet<ReservationStatusEntity> ReservationStatuses { get; set; }
    public DbSet<ReservationStatusTransitionEntity> ReservationStatusTransitions { get; set; }
    public DbSet<ReservationEntity> Reservations { get; set; }
    public DbSet<ReservationFlightEntity> ReservationFlights { get; set; }
    public DbSet<ReservationPassengerEntity> ReservationPassengers { get; set; }

    // Tickets & Check-in
    public DbSet<TicketStatusEntity> TicketStatuses { get; set; }
    public DbSet<TicketEntity> Tickets { get; set; }
    public DbSet<CheckinStatusEntity> CheckinStatuses { get; set; }
    public DbSet<CheckinEntity> Checkins { get; set; }

    // Baggage
    public DbSet<BaggageTypeEntity> BaggageTypes { get; set; }

    // Invoices
    public DbSet<InvoiceItemTypeEntity> InvoiceItemTypes { get; set; }
    public DbSet<InvoiceEntity> Invoices { get; set; }
    public DbSet<InvoiceItemEntity> InvoiceItems { get; set; }

    // Payments
    public DbSet<PaymentStatusEntity> PaymentStatuses { get; set; }
    public DbSet<PaymentMethodTypeEntity> PaymentMethodTypes { get; set; }
    public DbSet<CardTypeEntity> CardTypes { get; set; }
    public DbSet<CardIssuerEntity> CardIssuers { get; set; }
    public DbSet<PaymentMethodEntity> PaymentMethods { get; set; }
    public DbSet<PaymentEntity> Payments { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
