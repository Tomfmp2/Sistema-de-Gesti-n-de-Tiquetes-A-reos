using Microsoft.EntityFrameworkCore;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Persons.Infrastructure.Data;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Persons.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Persons.Infrastructure.Seed;

public static class CatalogSeed
{
    public static async Task SeedAsync(AppDbContext context)
    {
        foreach (var row in PersonDefaultData.Persons)
        {
            var exists = await context.Persons.AnyAsync(p =>
                p.DocumentTypeId == row.DocumentTypeId && p.DocumentNumber == row.DocumentNumber
            );
            if (exists)
                continue;

            context.Persons.Add(
                new PersonEntity
                {
                    DocumentTypeId = row.DocumentTypeId,
                    DocumentNumber = row.DocumentNumber,
                    FirstName = row.FirstName,
                    LastName = row.LastName,
                    BirthDate = row.BirthDate,
                    Gender = row.Gender,
                    AddressId = row.AddressId,
                    CreatedAt = row.CreatedAt,
                    UpdatedAt = row.UpdatedAt
                }
            );
        }

        await context.SaveChangesAsync();
    }
}
