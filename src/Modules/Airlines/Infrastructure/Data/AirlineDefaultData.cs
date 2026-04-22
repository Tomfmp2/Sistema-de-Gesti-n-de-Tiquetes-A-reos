using sistema_gestor_de_tiquetes_aereos.Src.Modules.Airlines.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Airlines.Infrastructure.Data;

public static class AirlineDefaultData
{
    private static readonly DateTime SeedTimestamp = new(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc);

    public static readonly AirlineEntity[] Airlines =
    [
        new() { Id = 1, Name = "Avianca", IataCode = "AV", OriginCountryId = 1, IsActive = true, CreatedAt = SeedTimestamp, UpdatedAt = SeedTimestamp },
        new() { Id = 2, Name = "LATAM Airlines", IataCode = "LA", OriginCountryId = 6, IsActive = true, CreatedAt = SeedTimestamp, UpdatedAt = SeedTimestamp },
        new() { Id = 3, Name = "American Airlines", IataCode = "AA", OriginCountryId = 2, IsActive = true, CreatedAt = SeedTimestamp, UpdatedAt = SeedTimestamp },
        new() { Id = 4, Name = "Iberia", IataCode = "IB", OriginCountryId = 9, IsActive = true, CreatedAt = SeedTimestamp, UpdatedAt = SeedTimestamp },
        new() { Id = 5, Name = "Air France", IataCode = "AF", OriginCountryId = 10, IsActive = true, CreatedAt = SeedTimestamp, UpdatedAt = SeedTimestamp }
    ];
}
