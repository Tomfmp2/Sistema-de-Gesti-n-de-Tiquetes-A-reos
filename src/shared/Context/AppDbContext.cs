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

namespace sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;

public class AppDbContext : DbContext
{
    public DbSet<ContinentEntity> Continents { get; set; }
    public DbSet<CountryEntity> Countries { get; set; }
    public DbSet<RegionEntity> Regions { get; set; }
    public DbSet<CityEntity> Cities { get; set; }
    public DbSet<FlightStatusEntity> FlightStatuses { get; set; }
    public DbSet<FlightStatusTransitionEntity> FlightStatusTransitions { get; set; }
    public DbSet<FlightEntity> Flights { get; set; }
    public DbSet<FareEntity> Fares { get; set; }
    public DbSet<ReservationStatusEntity> ReservationStatuses { get; set; }
    public DbSet<ReservationStatusTransitionEntity> ReservationStatusTransitions { get; set; }
    public DbSet<ReservationEntity> Reservations { get; set; }
    public DbSet<ReservationFlightEntity> ReservationFlights { get; set; }
    public DbSet<ReservationPassengerEntity> ReservationPassengers { get; set; }
    public DbSet<TicketStatusEntity> TicketStatuses { get; set; }
    public DbSet<TicketEntity> Tickets { get; set; }
    public DbSet<CheckinStatusEntity> CheckinStatuses { get; set; }
    public DbSet<CheckinEntity> Checkins { get; set; }
    public DbSet<InvoiceItemTypeEntity> InvoiceItemTypes { get; set; }
    public DbSet<InvoiceEntity> Invoices { get; set; }
    public DbSet<InvoiceItemEntity> InvoiceItems { get; set; }
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
