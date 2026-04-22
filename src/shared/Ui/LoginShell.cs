using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using Spectre.Console;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Clients.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.DocumentTypes.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.EmailDomains.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PersonEmails.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PersonPhones.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Persons.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PhoneCodes.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Sessions.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.SystemRoles.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Users.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Helpers;

namespace sistema_gestor_de_tiquetes_aereos.Src.Shared.Ui;

public static class LoginShell
{
    public static async Task<AuthContext> LoginAsync(AppDbContext context, CancellationToken cancellationToken = default)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            SpectreUi.Clear();

            var action = PromptLoginAction();
            if (action == LoginAction.Exit)
            {
                SpectreUi.MarkupLineOrPlain("[grey]Hasta luego.[/]", "Hasta luego.");
                Environment.Exit(0);
            }

            if (action == LoginAction.Register)
            {
                try
                {
                    await RegisterAsync(context, cancellationToken);
                    SpectreUi.Pause("Registro completado. Ahora puedes iniciar sesión…");
                }
                catch (OperationCanceledException)
                {
                    SpectreUi.MarkupLineOrPlain("[grey]Operación cancelada.[/]", "Operación cancelada.");
                    SpectreUi.Pause();
                }
                catch (Exception ex)
                {
                    SpectreUi.ShowException(ex);
                }

                continue;
            }

            string username;
            string password;

            if (SpectreUi.CanUseAnsiPrompts)
            {
                try
                {
                    AnsiConsole.Write(new Rule("[bold green]Inicio de sesión[/]").RuleStyle("grey"));
                    username = AnsiConsole.Ask<string>("[yellow]Usuario[/]:").Trim();
                    password = AnsiConsole.Prompt(
                        new TextPrompt<string>("[yellow]Contraseña[/]:")
                            .PromptStyle("red")
                            .Secret()
                    );
                }
                catch (IOException)
                {
                    SpectreUi.DisableRichConsole();
                    (username, password) = PromptLoginPlain();
                }
                catch (InvalidOperationException)
                {
                    SpectreUi.DisableRichConsole();
                    (username, password) = PromptLoginPlain();
                }
            }
            else
            {
                (username, password) = PromptLoginPlain();
            }

            try
            {
                username = (username ?? string.Empty).Trim();
                password = password ?? string.Empty;

                if (string.IsNullOrWhiteSpace(username) || string.IsNullOrEmpty(password))
                {
                    SpectreUi.MarkupLineOrPlain(
                        "[red]Usuario/contraseña inválidos.[/]",
                        "Usuario/contraseña inválidos."
                    );
                    SpectreUi.Pause();
                    continue;
                }

                var normalizedUsername = username.Trim().ToUpperInvariant();
                var user = await context.Set<UserEntity>()
                    .AsNoTracking()
                    .FirstOrDefaultAsync(
                        u => u.Username != null
                             && u.Username.ToUpper() == normalizedUsername
                             && u.IsActive,
                        cancellationToken
                    );

                if (user is null)
                {
                    SpectreUi.MarkupLineOrPlain(
                        "[red]Usuario/contraseña inválidos.[/]",
                        "Usuario/contraseña inválidos."
                    );
                    SpectreUi.Pause();
                    continue;
                }

                if (!VerifyPassword(password, user.PasswordHash ?? string.Empty))
                {
                    SpectreUi.MarkupLineOrPlain(
                        "[red]Usuario/contraseña inválidos.[/]",
                        "Usuario/contraseña inválidos."
                    );
                    SpectreUi.Pause();
                    continue;
                }

                // Actualizamos LastAccessAt sin usar Attach (evita conflicto de tracking).
                var utcNow = DateTime.UtcNow;
                await context.Set<UserEntity>()
                    .Where(u => u.Id == user.Id)
                    .ExecuteUpdateAsync(
                        s => s.SetProperty(u => u.LastAccessAt, utcNow),
                        cancellationToken
                    );

                var session = new SessionEntity
                {
                    UserId = user.Id,
                    StartedAt = utcNow,
                    ClosedAt = null,
                    OriginIp = null,
                    IsActive = true
                };

                context.Set<SessionEntity>().Add(session);
                await context.SaveChangesAsync(cancellationToken);

                var roleName = await context.Set<SystemRoleEntity>()
                    .Where(r => r.Id == user.SystemRoleId)
                    .Select(r => r.Name!)
                    .FirstOrDefaultAsync(cancellationToken) ?? "desconocido";

                int? clientId = null;
                if (user.PersonId.HasValue)
                {
                    clientId = await context.Set<ClientEntity>()
                        .Where(c => c.PersonId == user.PersonId.Value)
                        .Select(c => (int?)c.Id)
                        .FirstOrDefaultAsync(cancellationToken);
                }

                SpectreUi.MarkupLineOrPlain(
                    $"[green]Bienvenido[/] [bold]{user.Username}[/].",
                    $"Bienvenido {user.Username}."
                );
                SpectreUi.Pause("Pulse una tecla para entrar al sistema…");

                return new AuthContext(
                    user.Id,
                    user.Username ?? normalizedUsername,
                    session.Id,
                    user.SystemRoleId,
                    roleName,
                    clientId
                );
            }
            catch (Exception ex) when (IsTransientConnectionError(ex))
            {
                SpectreUi.MarkupLineOrPlain(
                    "[red]No se pudo conectar a la base de datos.[/] Verifica que MySQL esté encendido y la conexión/SSL.",
                    "No se pudo conectar a la base de datos. Verifica que MySQL esté encendido y la conexión/SSL."
                );
                SpectreUi.MarkupLineOrPlain(
                    $"[grey]{ExceptionFormatting.GetDiagnosticMessage(ex)}[/]",
                    ExceptionFormatting.GetDiagnosticMessage(ex)
                );
                SpectreUi.Pause("Pulse una tecla para reintentar…");
            }
            catch (Exception ex)
            {
                SpectreUi.ShowException(ex);
            }
        }

        throw new OperationCanceledException();
    }

    private enum LoginAction
    {
        Login,
        Register,
        Exit
    }

    private static LoginAction PromptLoginAction()
    {
        if (SpectreUi.CanUseAnsiPrompts)
        {
            try
            {
                AnsiConsole.Write(new Rule("[bold green]Inicio de sesión[/]").RuleStyle("grey"));
                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[grey]Elige una opción[/]")
                        .AddChoices("Iniciar sesión", "Registrarse", "Salir")
                );

                return choice switch
                {
                    "Registrarse" => LoginAction.Register,
                    "Salir" => LoginAction.Exit,
                    _ => LoginAction.Login
                };
            }
            catch
            {
                SpectreUi.DisableRichConsole();
            }
        }

        Console.WriteLine("=== Inicio de sesión ===");
        Console.WriteLine("1) Iniciar sesión");
        Console.WriteLine("2) Registrarse");
        Console.WriteLine("0) Salir");
        Console.Write("Opción: ");
        var raw = (Console.ReadLine() ?? string.Empty).Trim();
        return raw switch
        {
            "2" => LoginAction.Register,
            "0" => LoginAction.Exit,
            _ => LoginAction.Login
        };
    }

    private static (string Username, string Password) PromptLoginPlain()
    {
        Console.WriteLine("=== Inicio de sesión ===");
        Console.WriteLine("(Consola sin modo interactivo: la contraseña se verá al escribir.)");
        Console.Write("Usuario: ");
        var username = Console.ReadLine()?.Trim() ?? "";
        Console.Write("Contraseña: ");
        var password = Console.ReadLine() ?? "";
        return (username, password);
    }

    public static async Task LogoutAsync(AppDbContext context, AuthContext auth, CancellationToken cancellationToken = default)
    {
        var session = await context.Set<SessionEntity>().FirstOrDefaultAsync(
            s => s.Id == auth.SessionId,
            cancellationToken
        );

        if (session is null)
        {
            return;
        }

        session.IsActive = false;
        session.ClosedAt = DateTime.UtcNow;
        await context.SaveChangesAsync(cancellationToken);
    }

    private static bool VerifyPassword(string password, string storedHash)
    {
        if (string.Equals(password, storedHash, StringComparison.Ordinal))
        {
            return true;
        }

        var sha256Hex = Sha256Hex(password);
        return string.Equals(sha256Hex, storedHash, StringComparison.OrdinalIgnoreCase);
    }

    private static string Sha256Hex(string input)
    {
        var bytes = Encoding.UTF8.GetBytes(input);
        var hash = SHA256.HashData(bytes);
        var sb = new StringBuilder(hash.Length * 2);
        foreach (var b in hash)
        {
            sb.Append(b.ToString("x2"));
        }

        return sb.ToString();
    }

    private static bool IsTransientConnectionError(Exception ex)
    {
        // EF Core suele envolver como InvalidOperationException cuando falla la estrategia de ejecución.
        if (ex is InvalidOperationException ioe &&
            ioe.Message.Contains("transient failure", StringComparison.OrdinalIgnoreCase))
            return true;

        // MySqlConnector errores de conexión típicos.
        if (ex is MySqlException)
            return true;

        var msg = ex.ToString();
        return msg.Contains("Couldn't connect to server", StringComparison.OrdinalIgnoreCase)
               || msg.Contains("Unable to read data from the transport connection", StringComparison.OrdinalIgnoreCase)
               || msg.Contains("SocketException", StringComparison.OrdinalIgnoreCase);
    }

    private static async Task RegisterAsync(AppDbContext context, CancellationToken cancellationToken)
    {
        SpectreUi.Clear();
        SpectreUi.ModuleHeader("Registro", "Crear un usuario (Cliente) desde el login");

        var username = SpectreUi.PromptRequiredCancelable("Username", "0/c/cancelar para salir").Trim();
        var password = SpectreUi.PromptRequiredCancelable("Contraseña", "0/c/cancelar para salir");

        var firstName = SpectreUi.PromptRequiredCancelable("Nombre", "0/c/cancelar para salir").Trim();
        var lastName = SpectreUi.PromptRequiredCancelable("Apellido", "0/c/cancelar para salir").Trim();

        var (documentTypeId, documentTypeLabel) = await PromptDocumentTypeAsync(context, cancellationToken);
        var documentNumber = SpectreUi.PromptRequiredCancelable("Número de documento", "0/c/cancelar para salir").Trim();

        SpectreUi.MarkupLineOrPlain(
            $"[grey]Documento:[/] {documentTypeLabel} {documentNumber}",
            $"Documento: {documentTypeLabel} {documentNumber}"
        );

        var strategy = context.Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync(async () =>
        {
            await using var tx = await context.Database.BeginTransactionAsync(cancellationToken);

            var usernameExists = await context.Set<UserEntity>()
                .AsNoTracking()
                .AnyAsync(u => u.Username != null && u.Username.ToUpper() == username.ToUpper(), cancellationToken);
            if (usernameExists)
                throw new InvalidOperationException($"Ya existe un usuario con username '{username}'.");

            var personId = await EnsurePersonByDocumentAsync(
                context,
                documentTypeId,
                documentNumber,
                firstName,
                lastName,
                cancellationToken
            );

            await PromptEmailsAndPhonesAsync(context, personId, cancellationToken);

            var roleId = await EnsureClientRoleIdAsync(context, cancellationToken);

            var now = DateTime.UtcNow;
            var user = new UserEntity
            {
                Username = username,
                PasswordHash = password,
                PersonId = personId,
                SystemRoleId = roleId,
                IsActive = true,
                LastAccessAt = null,
                CreatedAt = now,
                UpdatedAt = now
            };

            context.Set<UserEntity>().Add(user);
            await context.SaveChangesAsync(cancellationToken);

            await EnsureClientForPersonAsync(context, personId, cancellationToken);

            await tx.CommitAsync(cancellationToken);
        });

        SpectreUi.MarkupLineOrPlain(
            $"[green]Usuario creado[/] username=[bold]{username}[/] persona={firstName} {lastName} ({documentTypeLabel} {documentNumber}).",
            $"Usuario creado username={username} persona={firstName} {lastName} ({documentTypeLabel} {documentNumber})."
        );
    }

    private static async Task<(int DocumentTypeId, string Label)> PromptDocumentTypeAsync(
        AppDbContext context,
        CancellationToken cancellationToken
    )
    {
        var types = await context.Set<DocumentTypeEntity>()
            .AsNoTracking()
            .OrderBy(t => t.Id)
            .Select(t => new { t.Id, t.Name, t.Code })
            .ToListAsync(cancellationToken);

        if (types.Count == 0)
            throw new InvalidOperationException("No hay tipos de documento (document_types).");

        SpectreUi.ShowTable(
            "Tipos de documento",
            ["Código", "Nombre"],
            types.Select(t => (IReadOnlyList<string>)[t.Code ?? "-", t.Name ?? "-"]).ToList()
        );

        var code = SpectreUi.PromptRequiredCancelable("Código (p.ej. CC/PAS)", "0/c/cancelar para salir").Trim();
        var match = types.FirstOrDefault(t => string.Equals(t.Code, code, StringComparison.OrdinalIgnoreCase))
                    ?? types.FirstOrDefault(t => string.Equals(t.Name, code, StringComparison.OrdinalIgnoreCase));
        if (match is null)
            throw new InvalidOperationException("Tipo de documento inválido.");

        return (match.Id, $"{match.Code}");
    }

    private static async Task<int> EnsurePersonByDocumentAsync(
        AppDbContext context,
        int documentTypeId,
        string documentNumber,
        string firstName,
        string lastName,
        CancellationToken cancellationToken
    )
    {
        var existing = await context.Set<PersonEntity>()
            .AsNoTracking()
            .Where(p => p.DocumentTypeId == documentTypeId && p.DocumentNumber == documentNumber)
            .Select(p => p.Id)
            .FirstOrDefaultAsync(cancellationToken);

        if (existing > 0)
            return existing;

        var now = DateTime.UtcNow;
        var inserted = await context.Database.ExecuteSqlRawAsync(
            """
            INSERT INTO persons
              (document_type_id, document_number, first_name, last_name, birth_date, gender, address_id, created_at, updated_at)
            VALUES
              ({0}, {1}, {2}, {3}, NULL, NULL, NULL, {4}, {5})
            """,
            [documentTypeId, documentNumber, firstName, lastName, now, now],
            cancellationToken
        );

        if (inserted != 1)
            throw new InvalidOperationException("No se pudo crear la persona.");

        var ids = await context.Database.SqlQueryRaw<int>("SELECT LAST_INSERT_ID() AS Value")
            .ToListAsync(cancellationToken);
        var id = ids.FirstOrDefault();
        if (id < 1)
            throw new InvalidOperationException("No se pudo obtener el id de la persona creada.");

        return id;
    }

    private static async Task PromptEmailsAndPhonesAsync(
        AppDbContext context,
        int personId,
        CancellationToken cancellationToken
    )
    {
        var wantsEmail = SpectreUi.PromptBool("¿Deseas registrar email(s)?", defaultValue: true);
        if (wantsEmail)
        {
            var first = true;
            while (true)
            {
                var email = SpectreUi.PromptRequiredCancelable(
                    first ? "Email principal" : "Email adicional",
                    "ej: nombre@dominio.com (0/c/cancelar para salir)"
                ).Trim();

                var parts = email.Split('@', 2, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                if (parts.Length != 2 || string.IsNullOrWhiteSpace(parts[0]) || string.IsNullOrWhiteSpace(parts[1]))
                    throw new InvalidOperationException("Email inválido (formato esperado: local@dominio).");

                var local = parts[0];
                var domain = parts[1].ToLowerInvariant();

                var domainId = await EnsureEmailDomainIdAsync(context, domain, cancellationToken);

                var exists = await context.Set<PersonEmailEntity>()
                    .AsNoTracking()
                    .AnyAsync(e => e.PersonId == personId && e.EmailLocalPart == local && e.EmailDomainId == domainId, cancellationToken);

                if (!exists)
                {
                    context.Set<PersonEmailEntity>().Add(new PersonEmailEntity
                    {
                        PersonId = personId,
                        EmailLocalPart = local,
                        EmailDomainId = domainId,
                        IsPrimary = first
                    });
                    await context.SaveChangesAsync(cancellationToken);
                }

                first = false;
                var more = SpectreUi.PromptBool("¿Registrar otro email?", defaultValue: false);
                if (!more)
                    break;
            }
        }

        var wantsPhone = SpectreUi.PromptBool("¿Deseas registrar teléfono(s)?", defaultValue: true);
        if (wantsPhone)
        {
            var codes = await context.Set<PhoneCodeEntity>()
                .AsNoTracking()
                .OrderBy(c => c.Id)
                .Select(c => new { c.Id, c.CountryDialCode, c.CountryName })
                .ToListAsync(cancellationToken);

            if (codes.Count == 0)
                throw new InvalidOperationException("No hay códigos telefónicos (phone_codes).");

            SpectreUi.ShowTable(
                "Códigos telefónicos",
                ["Id", "Código", "País"],
                codes.Take(40).Select(c => (IReadOnlyList<string>)[c.Id.ToString(), c.CountryDialCode ?? "-", c.CountryName ?? "-"]).ToList()
            );

            var first = true;
            while (true)
            {
                var codeRaw = SpectreUi.PromptRequiredCancelable(
                    "Código país",
                    "escribe Id o el código (ej: +57) (0/c/cancelar para salir)"
                ).Trim();

                int codeId;
                if (int.TryParse(codeRaw, out var parsedId))
                {
                    codeId = parsedId;
                }
                else
                {
                    var match = codes.FirstOrDefault(c =>
                        string.Equals(c.CountryDialCode, codeRaw, StringComparison.OrdinalIgnoreCase));
                    codeId = match?.Id ?? 0;
                }

                if (codes.All(c => c.Id != codeId))
                    throw new InvalidOperationException("Código país inválido.");

                var number = SpectreUi.PromptRequiredCancelable(
                    first ? "Teléfono principal" : "Teléfono adicional",
                    "solo número (0/c/cancelar para salir)"
                ).Trim();

                var exists = await context.Set<PersonPhoneEntity>()
                    .AsNoTracking()
                    .AnyAsync(p => p.PersonId == personId && p.PhoneCodeId == codeId && p.Number == number, cancellationToken);

                if (!exists)
                {
                    context.Set<PersonPhoneEntity>().Add(new PersonPhoneEntity
                    {
                        PersonId = personId,
                        PhoneCodeId = codeId,
                        Number = number,
                        IsPrimary = first
                    });
                    await context.SaveChangesAsync(cancellationToken);
                }

                first = false;
                var more = SpectreUi.PromptBool("¿Registrar otro teléfono?", defaultValue: false);
                if (!more)
                    break;
            }
        }
    }

    private static async Task<int> EnsureEmailDomainIdAsync(
        AppDbContext context,
        string domain,
        CancellationToken cancellationToken
    )
    {
        var existing = await context.Set<EmailDomainEntity>()
            .AsNoTracking()
            .Where(d => d.Domain != null && d.Domain.ToLower() == domain.ToLower())
            .Select(d => d.Id)
            .FirstOrDefaultAsync(cancellationToken);

        if (existing > 0)
            return existing;

        var entity = new EmailDomainEntity { Domain = domain };
        context.Set<EmailDomainEntity>().Add(entity);
        await context.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }

    private static async Task<int> EnsureClientRoleIdAsync(AppDbContext context, CancellationToken cancellationToken)
    {
        // Preferimos role name "Cliente" pero toleramos variaciones.
        var roleId = await context.Set<SystemRoleEntity>()
            .AsNoTracking()
            .Where(r => r.Name != null && (
                r.Name == "Cliente"
                || r.Name == "CLIENTE"
                || r.Name == "client"
                || r.Name == "CLIENT"
            ))
            .Select(r => r.Id)
            .FirstOrDefaultAsync(cancellationToken);

        if (roleId > 0)
            return roleId;

        // Si no existe, lo creamos.
        var role = new SystemRoleEntity { Name = "Cliente", Description = "Cliente" };
        context.Set<SystemRoleEntity>().Add(role);
        await context.SaveChangesAsync(cancellationToken);
        return role.Id;
    }

    private static async Task<int> EnsureClientForPersonAsync(
        AppDbContext context,
        int personId,
        CancellationToken cancellationToken
    )
    {
        var existing = await context.Set<ClientEntity>()
            .AsNoTracking()
            .Where(c => c.PersonId == personId)
            .Select(c => (int?)c.Id)
            .FirstOrDefaultAsync(cancellationToken);

        if (existing.HasValue && existing.Value > 0)
            return existing.Value;

        var utcNow = DateTime.UtcNow;
        var inserted = await context.Database.ExecuteSqlRawAsync(
            """
            INSERT INTO clients (person_id, created_at)
            VALUES ({0}, {1})
            """,
            [personId, utcNow],
            cancellationToken
        );

        if (inserted != 1)
            throw new InvalidOperationException("No se pudo crear el cliente asociado a la persona.");

        var ids = await context.Database.SqlQueryRaw<int>("SELECT LAST_INSERT_ID() AS Value")
            .ToListAsync(cancellationToken);
        var id = ids.FirstOrDefault();
        if (id < 1)
            throw new InvalidOperationException("No se pudo obtener el id del cliente creado.");

        return id;
    }
}
