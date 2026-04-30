using sistema_gestor_de_tiquetes_aereos.Src.Modules.Persons.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Persons.Infrastructure.Data;

public static class PersonDefaultData
{
    private static readonly DateTime SeedTimestamp = new(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc);

    /// <summary>Los ids los asigna la base (incremental). Staff y cliente se enlazan por documento.</summary>
    public static readonly PersonEntity[] Persons =
    [
        new()
        {
            DocumentTypeId = 1,
            DocumentNumber = "8000123456",
            FirstName = "Carlos",
            LastName = "Mendoza",
            BirthDate = new DateTime(1985, 3, 12, 0, 0, 0, DateTimeKind.Unspecified),
            Gender = 'M',
            AddressId = null,
            CreatedAt = SeedTimestamp,
            UpdatedAt = SeedTimestamp
        },
        new()
        {
            DocumentTypeId = 1,
            DocumentNumber = "8000123457",
            FirstName = "Laura",
            LastName = "Villalobos",
            BirthDate = new DateTime(1990, 7, 22, 0, 0, 0, DateTimeKind.Unspecified),
            Gender = 'F',
            AddressId = null,
            CreatedAt = SeedTimestamp,
            UpdatedAt = SeedTimestamp
        },
        new()
        {
            DocumentTypeId = 1,
            DocumentNumber = "8000123458",
            FirstName = "Santiago",
            LastName = "Ortiz",
            BirthDate = new DateTime(1992, 1, 5, 0, 0, 0, DateTimeKind.Unspecified),
            Gender = 'M',
            AddressId = null,
            CreatedAt = SeedTimestamp,
            UpdatedAt = SeedTimestamp
        },
        new()
        {
            DocumentTypeId = 1,
            DocumentNumber = "2000456123",
            FirstName = "James",
            LastName = "Carter",
            BirthDate = new DateTime(1980, 11, 2, 0, 0, 0, DateTimeKind.Unspecified),
            Gender = 'M',
            AddressId = null,
            CreatedAt = SeedTimestamp,
            UpdatedAt = SeedTimestamp
        },
        new()
        {
            DocumentTypeId = 1,
            DocumentNumber = "2000456124",
            FirstName = "Emily",
            LastName = "Stone",
            BirthDate = new DateTime(1993, 4, 18, 0, 0, 0, DateTimeKind.Unspecified),
            Gender = 'F',
            AddressId = null,
            CreatedAt = SeedTimestamp,
            UpdatedAt = SeedTimestamp
        },
        new()
        {
            DocumentTypeId = 1,
            DocumentNumber = "1000789456",
            FirstName = "Miguel",
            LastName = "Fernández",
            BirthDate = new DateTime(1982, 9, 9, 0, 0, 0, DateTimeKind.Unspecified),
            Gender = 'M',
            AddressId = null,
            CreatedAt = SeedTimestamp,
            UpdatedAt = SeedTimestamp
        },
        new()
        {
            DocumentTypeId = 1,
            DocumentNumber = "1098765432",
            FirstName = "María",
            LastName = "Gómez Demo",
            BirthDate = new DateTime(1995, 6, 1, 0, 0, 0, DateTimeKind.Unspecified),
            Gender = 'F',
            AddressId = null,
            CreatedAt = SeedTimestamp,
            UpdatedAt = SeedTimestamp
        }
    ];
}
