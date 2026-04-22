using Microsoft.EntityFrameworkCore;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.DocumentTypes.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.SystemRoles.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Users.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Users.Application.Services;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Clients.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Users.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Helpers;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Ui;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Users.UI;

public sealed class UserConsoleUI : IModuleUI
{
    private readonly AppDbContext _ctx;
    private readonly UserService _service;

    public UserConsoleUI(AppDbContext ctx, UserService service)
    {
        _ctx = ctx;
        _service = service;
    }

    public async Task RunAsync()
    {
        var exit = false;
        while (!exit)
        {
            SpectreUi.ModuleHeader("Usuarios", "Registro y administración de usuarios");

            var items = new List<(string Label, Action Action)>
            {
                ("Registrar usuario nuevo", () => CreateUser().GetAwaiter().GetResult()),
                ("Listar usuarios", () => ListAll().GetAwaiter().GetResult()),
                ("Consultar por ID", () => GetById().GetAwaiter().GetResult()),
                ("Actualizar usuario", () => UpdateUser().GetAwaiter().GetResult()),
                ("Eliminar usuario", () => DeleteUser().GetAwaiter().GetResult()),
                ("Volver", () => exit = true),
            };

            MenuLogic.RunMenu(items);
        }
    }

    private async Task CreateUser()
    {
        try
        {
            Console.Write("Username: ");
            var username = (Console.ReadLine() ?? string.Empty).Trim();
            if (string.IsNullOrWhiteSpace(username))
                throw new InvalidOperationException("Username es obligatorio.");

            Console.Write("Contraseña (se guardará tal cual o como SHA-256 hex): ");
            var password = Console.ReadLine() ?? string.Empty;

            var (roleId, roleName) = await PromptRoleByNameAsync();

            Console.Write("Nombre: ");
            var firstName = (Console.ReadLine() ?? string.Empty).Trim();
            Console.Write("Apellido: ");
            var lastName = (Console.ReadLine() ?? string.Empty).Trim();

            if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName))
            {
                throw new InvalidOperationException("Nombre y apellido son obligatorios.");
            }

            var (documentTypeId, documentTypeLabel) = await PromptDocumentTypeAsync();

            Console.Write("Número de documento: ");
            var documentNumber = (Console.ReadLine() ?? string.Empty).Trim();
            if (string.IsNullOrWhiteSpace(documentNumber))
                throw new InvalidOperationException("Número de documento es obligatorio.");

            Console.Write("Fecha de nacimiento (yyyy-MM-dd, opcional): ");
            var birthRaw = (Console.ReadLine() ?? string.Empty).Trim();
            DateTime? birthDate = null;
            if (!string.IsNullOrWhiteSpace(birthRaw))
            {
                if (!DateTime.TryParse(birthRaw, out var bd))
                    throw new InvalidOperationException("Fecha de nacimiento inválida.");
                birthDate = bd.Date;
            }

            Console.Write("Género (M/F/N, opcional): ");
            var genderRaw = (Console.ReadLine() ?? string.Empty).Trim();
            char? gender = null;
            if (!string.IsNullOrWhiteSpace(genderRaw))
            {
                var g = char.ToUpperInvariant(genderRaw[0]);
                if (g is not ('M' or 'F' or 'N'))
                    throw new InvalidOperationException("Género inválido. Use M/F/N.");
                gender = g;
            }

            Console.Write("¿Activo? (true/false, default=true): ");
            var isActiveRaw = Console.ReadLine();
            var isActive = string.IsNullOrWhiteSpace(isActiveRaw) || bool.Parse(isActiveRaw);

            await using var tx = await _ctx.Database.BeginTransactionAsync();
            var usernameExists = await _ctx.Set<UserEntity>()
                .AsNoTracking()
                .AnyAsync(u => u.Username != null && u.Username.ToUpper() == username.ToUpper());
            if (usernameExists)
                throw new InvalidOperationException($"Ya existe un usuario con username '{username}'.");

            var personId = await CreatePersonForUserAsync(
                documentTypeId,
                documentNumber,
                firstName,
                lastName,
                birthDate,
                gender
            );

            var created = await _service.CreateAsync(
                new CreateUserRequest(
                    Username: username,
                    PasswordHash: password,
                    PersonId: personId,
                    SystemRoleId: roleId,
                    IsActive: isActive
                )
            );

            int? clientId = null;
            if (string.Equals(roleName, "Cliente", StringComparison.OrdinalIgnoreCase))
            {
                clientId = await EnsureClientForPersonAsync(personId);
            }

            await tx.CommitAsync();

            SpectreUi.MarkupLineOrPlain(
                $"[green]Usuario creado[/] id={created.Id.Value} username=[bold]{created.Username.Value}[/] role={roleName} persona={firstName} {lastName} ({documentTypeLabel} {documentNumber}){(clientId.HasValue ? $" client_id={clientId.Value}" : "")}.",
                $"Usuario creado id={created.Id.Value} username={created.Username.Value} role={roleName} persona={firstName} {lastName} ({documentTypeLabel} {documentNumber}){(clientId.HasValue ? $" client_id={clientId.Value}" : "")}."
            );
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ExceptionFormatting.GetDiagnosticMessage(ex)}");
        }

        SpectreUi.Pause();
    }

    private async Task<int> CreatePersonForUserAsync(
        int documentTypeId,
        string documentNumber,
        string firstName,
        string lastName,
        DateTime? birthDate,
        char? gender
    )
    {
        // Insertamos con SQL directo para no depender de mapeos EF en runtime.
        var utcNow = DateTime.UtcNow;

        var inserted = await _ctx.Database.ExecuteSqlRawAsync(
            """
            INSERT INTO persons
              (document_type_id, document_number, first_name, last_name, birth_date, gender, address_id, created_at, updated_at)
            VALUES
              ({0}, {1}, {2}, {3}, {4}, {5}, NULL, {6}, {7})
            """,
            documentTypeId,
            documentNumber,
            firstName,
            lastName,
            birthDate,
            gender.HasValue ? gender.Value.ToString() : DBNull.Value,
            utcNow,
            utcNow
        );

        if (inserted != 1)
        {
            throw new InvalidOperationException("No se pudo crear la persona.");
        }

        var ids = await _ctx.Database.SqlQueryRaw<int>("SELECT LAST_INSERT_ID() AS Value").ToListAsync();
        var id = ids.FirstOrDefault();
        if (id < 1)
        {
            throw new InvalidOperationException("No se pudo obtener el id de la persona creada.");
        }

        return id;
    }

    private async Task<int> EnsureClientForPersonAsync(int personId)
    {
        // Si ya existe un cliente para esa persona, lo reutilizamos.
        var existing = await _ctx.Set<ClientEntity>()
            .AsNoTracking()
            .Where(c => c.PersonId == personId)
            .Select(c => (int?)c.Id)
            .FirstOrDefaultAsync();

        if (existing.HasValue && existing.Value > 0)
            return existing.Value;

        var utcNow = DateTime.UtcNow;
        var inserted = await _ctx.Database.ExecuteSqlRawAsync(
            """
            INSERT INTO clients (person_id, created_at)
            VALUES ({0}, {1})
            """,
            personId,
            utcNow
        );

        if (inserted != 1)
            throw new InvalidOperationException("No se pudo crear el cliente asociado a la persona.");

        var ids = await _ctx.Database.SqlQueryRaw<int>("SELECT LAST_INSERT_ID() AS Value").ToListAsync();
        var id = ids.FirstOrDefault();
        if (id < 1)
            throw new InvalidOperationException("No se pudo obtener el id del cliente creado.");

        return id;
    }

    private async Task GetById()
    {
        try
        {
            Console.Write("ID: ");
            var id = int.Parse(Console.ReadLine()!);
            var u = await _service.GetByIdAsync(id);
            if (u is null)
            {
                Console.WriteLine("No encontrado.");
            }
            else
            {
                Console.WriteLine(
                    $"id={u.Id.Value} username={u.Username.Value} role_id={u.SystemRoleId.Value} active={u.IsActive} person_id={(u.PersonId?.ToString() ?? "null")}"
                );
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ExceptionFormatting.GetDiagnosticMessage(ex)}");
        }
        SpectreUi.Pause();
    }

    private async Task ListAll()
    {
        try
        {
            var list = await _service.GetAllAsync();
            if (list.Count == 0)
            {
                Console.WriteLine("No hay usuarios.");
                SpectreUi.Pause();
                return;
            }

            foreach (var u in list.OrderBy(x => x.Id.Value))
            {
                Console.WriteLine(
                    $"id={u.Id.Value} username={u.Username.Value} role_id={u.SystemRoleId.Value} active={u.IsActive} person_id={(u.PersonId?.ToString() ?? "null")}"
                );
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ExceptionFormatting.GetDiagnosticMessage(ex)}");
        }
        SpectreUi.Pause();
    }

    private async Task UpdateUser()
    {
        try
        {
            Console.Write("ID: ");
            var id = int.Parse(Console.ReadLine()!);

            Console.Write("Nuevo username: ");
            var username = (Console.ReadLine() ?? string.Empty).Trim();

            Console.Write("Nueva contraseña (se guardará tal cual o como SHA-256 hex): ");
            var password = Console.ReadLine() ?? string.Empty;

            var (roleId, _) = await PromptRoleByNameAsync();

            Console.Write("PersonId (opcional, Enter para null): ");
            var personIdRaw = Console.ReadLine();
            int? personId = null;
            if (!string.IsNullOrWhiteSpace(personIdRaw))
            {
                if (int.TryParse(personIdRaw, out var pid))
                    personId = pid;
                else
                    throw new InvalidOperationException("PersonId inválido.");
            }

            Console.Write("¿Activo? (true/false): ");
            var isActive = bool.Parse(Console.ReadLine()!);

            await _service.UpdateAsync(
                new UpdateUserRequest(
                    Id: id,
                    Username: username,
                    PasswordHash: password,
                    PersonId: personId,
                    SystemRoleId: roleId,
                    IsActive: isActive,
                    LastAccessAt: null
                )
            );

            Console.WriteLine("Actualizado.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ExceptionFormatting.GetDiagnosticMessage(ex)}");
        }
        SpectreUi.Pause();
    }

    private async Task DeleteUser()
    {
        try
        {
            Console.Write("ID: ");
            var id = int.Parse(Console.ReadLine()!);
            await _service.DeleteAsync(id);
            Console.WriteLine("Eliminado.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ExceptionFormatting.GetDiagnosticMessage(ex)}");
        }
        SpectreUi.Pause();
    }

    private async Task<(int RoleId, string RoleName)> PromptRoleByNameAsync()
    {
        var roles = await _ctx.Set<SystemRoleEntity>()
            .AsNoTracking()
            .OrderBy(r => r.Id)
            .Select(r => new { r.Id, r.Name })
            .ToListAsync();

        if (roles.Count == 0)
        {
            throw new InvalidOperationException("No hay roles en system_roles. Ejecuta seed/migraciones.");
        }

        Console.WriteLine("Roles disponibles:");
        foreach (var r in roles)
        {
            Console.WriteLine($"- {r.Id}: {r.Name}");
        }

        Console.Write("Rol (escriba el nombre, p.ej. admin): ");
        var roleInput = (Console.ReadLine() ?? string.Empty).Trim();
        if (string.IsNullOrWhiteSpace(roleInput))
            throw new InvalidOperationException("Rol es obligatorio.");

        var match = roles.FirstOrDefault(r => string.Equals(r.Name, roleInput, StringComparison.OrdinalIgnoreCase));
        if (match is null)
        {
            throw new InvalidOperationException($"Rol inválido: '{roleInput}'.");
        }

        return (match.Id, match.Name ?? "desconocido");
    }

    private async Task<(int DocumentTypeId, string Label)> PromptDocumentTypeAsync()
    {
        // En la DB: document_types(id, name, code)
        var types = await _ctx.Set<DocumentTypeEntity>()
            .AsNoTracking()
            .OrderBy(t => t.Id)
            .Select(t => new { t.Id, t.Name, t.Code })
            .ToListAsync();

        if (types.Count == 0)
        {
            throw new InvalidOperationException("No hay tipos de documento. Ejecuta seed/migraciones.");
        }

        Console.WriteLine("Tipos de documento disponibles:");
        foreach (var t in types)
        {
            Console.WriteLine($"- {t.Code}: {t.Name}");
        }

        Console.Write("Tipo de documento (código, p.ej. CC/PAS): ");
        var code = (Console.ReadLine() ?? string.Empty).Trim();
        if (string.IsNullOrWhiteSpace(code))
            throw new InvalidOperationException("Tipo de documento es obligatorio.");

        var match = types.FirstOrDefault(t => string.Equals(t.Code, code, StringComparison.OrdinalIgnoreCase))
                    ?? types.FirstOrDefault(t => string.Equals(t.Name, code, StringComparison.OrdinalIgnoreCase));

        if (match is null)
            throw new InvalidOperationException($"Tipo de documento inválido: '{code}'.");

        var label = $"{match.Code}";
        return (match.Id, label);
    }
}

