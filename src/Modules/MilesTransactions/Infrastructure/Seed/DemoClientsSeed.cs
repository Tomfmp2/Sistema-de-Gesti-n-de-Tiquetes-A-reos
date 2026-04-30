using Microsoft.EntityFrameworkCore;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Clients.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.MilesTransactions.Domain.Enum;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.MilesTransactions.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Persons.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Reservations.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Users.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.MilesTransactions.Infrastructure.Seed;

/// <summary>
/// Seed idempotente de 5 clientes demo con reservas confirmadas y millas acumuladas.
/// Se ejecuta al inicio junto con el resto del bootstrap. Cada cliente se identifica
/// por su número de documento único; si ya existe, se omite.
/// </summary>
public static class DemoClientsSeed
{
    // ── Datos fijos de los 5 clientes ─────────────────────────────────────────
    private record DemoClient(
        string DocNumber,
        string FirstName,
        string LastName,
        char Gender,
        string Username,
        decimal AccumulatedMiles,   // millas ya "ganadas" históricamente
        decimal CurrentMiles        // saldo real en cuenta (puede ser menor si redimió)
    );

    private static readonly DemoClient[] Clients =
    [
        new("CC-DEMO-001", "Valentina",  "Ríos",     'F', "valentina.rios",   95_000m, 95_000m),
        new("CC-DEMO-002", "Carlos",     "Mendoza",  'M', "carlos.mendoza",  230_000m, 80_000m),  // redimió 150k
        new("CC-DEMO-003", "Luisa",      "Herrera",  'F', "luisa.herrera",    42_500m, 42_500m),
        new("CC-DEMO-004", "Sebastián",  "Castro",   'M', "sebastian.castro", 510_000m,10_000m),  // redimió 500k
        new("CC-DEMO-005", "Mariana",    "Torres",   'F', "mariana.torres",   18_750m, 18_750m),
    ];

    public static async Task SeedAsync(AppDbContext ctx)
    {
        // Necesitamos DocumentTypeId=1 (Cédula) y al menos 1 vuelo con status=2 (Programado/activo)
        var docTypeId = await ctx.DocumentTypes
            .OrderBy(d => d.Id)
            .Select(d => (int?)d.Id)
            .FirstOrDefaultAsync() ?? 1;

        // Obtenemos el primer vuelo disponible para asociar las reservas demo
        var flightId = await ctx.Flights
            .OrderBy(f => f.Id)
            .Select(f => (int?)f.Id)
            .FirstOrDefaultAsync();

        if (flightId is null)
            return; // Si no hay vuelos aún, no podemos crear reservas con sentido

        // Rol cliente (id más bajo que no sea administrador)
        var clientRoleId = await ctx.SystemRoles
            .OrderBy(r => r.Id)
            .Where(r => r.Name != null &&
                        r.Name.ToLower() != "administrador" &&
                        r.Name.ToLower() != "admin" &&
                        r.Name.ToLower() != "root" &&
                        r.Name.ToLower() != "operaciones")
            .Select(r => (int?)r.Id)
            .FirstOrDefaultAsync();

        // Si no existe rol cliente, usamos el primero disponible
        clientRoleId ??= await ctx.SystemRoles
            .OrderBy(r => r.Id)
            .Select(r => (int?)r.Id)
            .FirstOrDefaultAsync() ?? 1;

        var now = DateTime.UtcNow;

        foreach (var demo in Clients)
        {
            // ── 1. Person ─────────────────────────────────────────────────────
            var person = await ctx.Persons
                .FirstOrDefaultAsync(p => p.DocumentNumber == demo.DocNumber);

            if (person is null)
            {
                person = new PersonEntity
                {
                    DocumentTypeId = docTypeId,
                    DocumentNumber = demo.DocNumber,
                    FirstName      = demo.FirstName,
                    LastName       = demo.LastName,
                    Gender         = demo.Gender,
                    BirthDate      = new DateTime(1990, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    CreatedAt      = now,
                    UpdatedAt      = now
                };
                ctx.Persons.Add(person);
                await ctx.SaveChangesAsync();
            }

            // ── 2. User ───────────────────────────────────────────────────────
            var userExists = await ctx.Users.AnyAsync(u => u.PersonId == person.Id);
            if (!userExists)
            {
                ctx.Users.Add(new UserEntity
                {
                    Username     = demo.Username,
                    PasswordHash = "12345",
                    PersonId     = person.Id,
                    SystemRoleId = clientRoleId.Value,
                    IsActive     = true,
                    CreatedAt    = now,
                    UpdatedAt    = now
                });
                await ctx.SaveChangesAsync();
            }

            // ── 3. Client ─────────────────────────────────────────────────────
            var client = await ctx.Clients.FirstOrDefaultAsync(c => c.PersonId == person.Id);
            if (client is null)
            {
                client = new ClientEntity { PersonId = person.Id, CreatedAt = now };
                ctx.Clients.Add(client);
                await ctx.SaveChangesAsync();
            }

            // ── 4. Reserva confirmada (estado 2) ──────────────────────────────
            var reservationExists = await ctx.Reservations
                .AnyAsync(r => r.ClientId == client.Id && r.ReservationStatusId == 2);

            if (!reservationExists)
            {
                var reservation = new ReservationEntity
                {
                    ClientId            = client.Id,
                    ReservationDate     = now.AddDays(-30),
                    ReservationStatusId = 2,          // Confirmada
                    TotalValue          = 480_000m,
                    OriginalTotalValue  = 480_000m,
                    DiscountPercentage  = 0m,
                    CreatedAt           = now.AddDays(-30),
                    UpdatedAt           = now.AddDays(-30)
                };
                ctx.Reservations.Add(reservation);
                await ctx.SaveChangesAsync();

                // ReservationFlight vincula la reserva al vuelo
                ctx.Set<sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationFlights.Infrastructure.Entity.ReservationFlightEntity>()
                    .Add(new sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationFlights.Infrastructure.Entity.ReservationFlightEntity
                    {
                        ReservationId = reservation.Id,
                        FlightId      = flightId.Value
                    });
                await ctx.SaveChangesAsync();

                // ── 5. Millas acumuladas históricas ───────────────────────────
                // Entrada de acumulación principal (simula múltiples vuelos pasados)
                ctx.MilesTransactions.Add(new MilesTransactionEntity
                {
                    ClientId        = client.Id,
                    ReservationId   = reservation.Id,
                    Amount          = demo.AccumulatedMiles,
                    TransactionType = TransactionType.Accumulation,
                    TransactionDate = now.AddDays(-30)
                });

                // Si el cliente redimió (saldo actual < acumulado), agregamos la redención
                if (demo.CurrentMiles < demo.AccumulatedMiles)
                {
                    var redeemed = demo.AccumulatedMiles - demo.CurrentMiles;
                    ctx.MilesTransactions.Add(new MilesTransactionEntity
                    {
                        ClientId        = client.Id,
                        ReservationId   = reservation.Id,
                        Amount          = -redeemed,
                        TransactionType = TransactionType.Redemption,
                        TransactionDate = now.AddDays(-15)
                    });
                }

                await ctx.SaveChangesAsync();
            }
        }
    }
}
