namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Staff.Infrastructure.Data;

public static class StaffDefaultData
{
    private static readonly DateTime SeedTimestamp = new(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc);

    /// <summary>Fila de seed: <see cref="PersonDocumentNumber"/> debe existir en personas (mismo tipo de documento).</summary>
    public sealed record Row(
        int Id,
        int PersonDocumentTypeId,
        string PersonDocumentNumber,
        int PositionId,
        int? AirlineId,
        int? AirportId,
        DateOnly HireDate,
        bool IsActive,
        DateTime CreatedAt,
        DateTime UpdatedAt
    );

    public static readonly Row[] Rows =
    [
        new(1, 1, "8000123456", 1, 1, 1, new DateOnly(2016, 4, 1), true, SeedTimestamp, SeedTimestamp),
        new(2, 1, "8000123457", 2, 1, 1, new DateOnly(2018, 6, 15), true, SeedTimestamp, SeedTimestamp),
        new(3, 1, "8000123458", 3, 1, 1, new DateOnly(2019, 2, 20), true, SeedTimestamp, SeedTimestamp),
        new(4, 1, "2000456123", 1, 3, 5, new DateOnly(2014, 9, 10), true, SeedTimestamp, SeedTimestamp),
        new(5, 1, "2000456124", 2, 3, 6, new DateOnly(2017, 8, 8), true, SeedTimestamp, SeedTimestamp),
        new(6, 1, "1000789456", 1, 4, 9, new DateOnly(2015, 5, 12), true, SeedTimestamp, SeedTimestamp)
    ];
}
