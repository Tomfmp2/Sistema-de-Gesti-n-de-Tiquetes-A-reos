using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Spectre.Console;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Clients.Infrastructure.Entity;
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
                var user = await context.Set<UserEntity>()
                    .FirstOrDefaultAsync(u => u.Username == username && u.IsActive, cancellationToken);

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

                user.LastAccessAt = DateTime.UtcNow;

                var session = new SessionEntity
                {
                    UserId = user.Id,
                    StartedAt = DateTime.UtcNow,
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
                    user.Username ?? username,
                    session.Id,
                    user.SystemRoleId,
                    roleName,
                    clientId
                );
            }
            catch (Exception ex)
            {
                SpectreUi.ShowException(ex);
            }
        }

        throw new OperationCanceledException();
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
}
