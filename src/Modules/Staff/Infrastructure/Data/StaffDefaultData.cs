using sistema_gestor_de_tiquetes_aereos.Src.Modules.Staff.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Staff.Infrastructure.Data;

public static class StaffDefaultData
{
    private static readonly DateTime SeedTimestamp = new(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc);

    public static readonly StaffEntity[] Staff =
    [
        new() { Id = 1, PersonId = 1, PositionId = 1, AirlineId = 1, AirportId = 1, HireDate = new DateOnly(2016, 4, 1), IsActive = true, CreatedAt = SeedTimestamp, UpdatedAt = SeedTimestamp },
        new() { Id = 2, PersonId = 2, PositionId = 2, AirlineId = 1, AirportId = 1, HireDate = new DateOnly(2018, 6, 15), IsActive = true, CreatedAt = SeedTimestamp, UpdatedAt = SeedTimestamp },
        new() { Id = 3, PersonId = 3, PositionId = 3, AirlineId = 1, AirportId = 1, HireDate = new DateOnly(2019, 2, 20), IsActive = true, CreatedAt = SeedTimestamp, UpdatedAt = SeedTimestamp },
        new() { Id = 4, PersonId = 4, PositionId = 1, AirlineId = 3, AirportId = 5, HireDate = new DateOnly(2014, 9, 10), IsActive = true, CreatedAt = SeedTimestamp, UpdatedAt = SeedTimestamp },
        new() { Id = 5, PersonId = 5, PositionId = 2, AirlineId = 3, AirportId = 6, HireDate = new DateOnly(2017, 8, 8), IsActive = true, CreatedAt = SeedTimestamp, UpdatedAt = SeedTimestamp },
        new() { Id = 6, PersonId = 6, PositionId = 1, AirlineId = 4, AirportId = 9, HireDate = new DateOnly(2015, 5, 12), IsActive = true, CreatedAt = SeedTimestamp, UpdatedAt = SeedTimestamp }
    ];
}
