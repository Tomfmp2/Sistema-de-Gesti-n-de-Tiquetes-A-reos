using Microsoft.EntityFrameworkCore;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Continents.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Airlines.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Airports.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.AirportAirline.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.StaffPositions.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Staff.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.AvailabilityStatuses.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.StaffAvailability.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.AircraftManufacturers.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;

public class AppDbContext : DbContext
{
    public DbSet<Continent> Continents { get; set; }
    public DbSet<AirlineEntity> Airlines { get; set; }
    public DbSet<AirportEntity> Airports { get; set; }
    public DbSet<AirportAirlineEntity> AirportAirlines { get; set; }
    public DbSet<StaffPositionEntity> StaffPositions { get; set; }
    public DbSet<StaffEntity> Staff { get; set; }
    public DbSet<AvailabilityStatusEntity> AvailabilityStatuses { get; set; }
    public DbSet<StaffAvailabilityEntity> StaffAvailabilities { get; set; }
    public DbSet<AircraftManufacturerEntity> AircraftManufacturers { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
