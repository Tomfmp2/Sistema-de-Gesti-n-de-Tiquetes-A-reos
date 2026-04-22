using sistema_gestor_de_tiquetes_aereos.Src.Modules.Persons.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Persons.Infrastructure.Data;

public static class PersonDefaultData
{
    private static readonly DateTime SeedTimestamp = new(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc);

    public static readonly PersonEntity[] Persons =
    [
        new() { Id = 1, DocumentTypeId = 1, DocumentNumber = "90000001", FirstName = "Carlos", LastName = "Ramirez", BirthDate = new DateTime(1984, 3, 14), Gender = 'M', CreatedAt = SeedTimestamp, UpdatedAt = SeedTimestamp },
        new() { Id = 2, DocumentTypeId = 1, DocumentNumber = "90000002", FirstName = "Laura", LastName = "Gomez", BirthDate = new DateTime(1989, 7, 22), Gender = 'F', CreatedAt = SeedTimestamp, UpdatedAt = SeedTimestamp },
        new() { Id = 3, DocumentTypeId = 1, DocumentNumber = "90000003", FirstName = "Andrea", LastName = "Torres", BirthDate = new DateTime(1992, 11, 5), Gender = 'F', CreatedAt = SeedTimestamp, UpdatedAt = SeedTimestamp },
        new() { Id = 4, DocumentTypeId = 3, DocumentNumber = "AA100100", FirstName = "John", LastName = "Miller", BirthDate = new DateTime(1981, 1, 18), Gender = 'M', CreatedAt = SeedTimestamp, UpdatedAt = SeedTimestamp },
        new() { Id = 5, DocumentTypeId = 3, DocumentNumber = "AA100101", FirstName = "Emily", LastName = "Johnson", BirthDate = new DateTime(1987, 9, 12), Gender = 'F', CreatedAt = SeedTimestamp, UpdatedAt = SeedTimestamp },
        new() { Id = 6, DocumentTypeId = 3, DocumentNumber = "ES200200", FirstName = "Sofia", LastName = "Martin", BirthDate = new DateTime(1985, 6, 30), Gender = 'F', CreatedAt = SeedTimestamp, UpdatedAt = SeedTimestamp }
    ];
}
