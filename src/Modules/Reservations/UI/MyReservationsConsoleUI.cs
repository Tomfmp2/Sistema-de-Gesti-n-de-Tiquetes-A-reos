using sistema_gestor_de_tiquetes_aereos.Src.Modules.Reservations.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Reservations.Application.UseCases;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationStatuses.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Helpers;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Ui;
using Microsoft.EntityFrameworkCore;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Reservations.UI;

public sealed class MyReservationsConsoleUI : IModuleUI
{
    private readonly AuthContext _auth;
    private readonly AppDbContext _ctx;
    private readonly GetReservationsByClientIdUseCase _getMineUseCase;
    private readonly CreateReservationUseCase _createUseCase;

    public MyReservationsConsoleUI(
        AuthContext auth,
        AppDbContext ctx,
        GetReservationsByClientIdUseCase getMineUseCase,
        CreateReservationUseCase createUseCase
    )
    {
        _auth = auth;
        _ctx = ctx;
        _getMineUseCase = getMineUseCase;
        _createUseCase = createUseCase;
    }

    public async Task RunAsync()
    {
        var exit = false;
        while (!exit)
        {
            SpectreUi.ModuleHeader("Mis reservaciones", "Solo las asociadas a tu cliente");

            if (_auth.ClientId is null)
            {
                SpectreUi.MarkupLineOrPlain(
                    "[yellow]Tu usuario no está asociado a un cliente.[/] [dim](no hay client_id; contacta administración.)[/]",
                    "Tu usuario no está asociado a un cliente (no hay client_id; contacta administración)."
                );
                SpectreUi.Pause();
                return;
            }

            var items = new List<(string Label, Action Action)>
            {
                ("Crear reservación", () => CreateReservation().GetAwaiter().GetResult()),
                ("Listar mis reservaciones", () => ListMine().GetAwaiter().GetResult()),
                ("Volver", () => exit = true),
            };

            MenuLogic.RunMenu(
                items,
                "[bold]¿Qué deseas hacer?[/]",
                "[grey]Crear o listar. «Volver» al menú principal.[/]"
            );
        }
    }

    private async Task ListMine()
    {
        try
        {
            var list = (await _getMineUseCase.ExecuteAsync(_auth.ClientId!.Value)).ToList();
            if (list.Count == 0)
            {
                SpectreUi.MarkupLineOrPlain(
                    "[grey]No tienes reservaciones.[/]",
                    "No tienes reservaciones."
                );
                return;
            }

            var statusById = await _ctx
                .Set<ReservationStatusEntity>()
                .AsNoTracking()
                .ToDictionaryAsync(s => s.Id, s => s.Name);

            SpectreUi.ShowTable(
                "Mis reservaciones",
                ["ID", "Estado", "Total", "Fecha", "Expira"],
                list
                    .OrderByDescending(x => x.ReservationDate.Value)
                    .Select(r =>
                    {
                        var sid = r.ReservationStatusId.Value;
                        var statusLabel = statusById.TryGetValue(sid, out var n) && !string.IsNullOrWhiteSpace(n)
                            ? n
                            : $"#{sid}";
                        return (IReadOnlyList<string>)
                        [
                            r.Id.Value.ToString(),
                            statusLabel,
                            r.TotalValue.Value.ToString("0.00"),
                            r.ReservationDate.Value.ToString("yyyy-MM-dd HH:mm"),
                            r.ExpiresAt.Value.HasValue
                                ? r.ExpiresAt.Value.Value.ToString("yyyy-MM-dd HH:mm")
                                : "-"
                        ];
                    })
                    .ToList()
            );
        }
        catch (OperationCanceledException)
        {
            SpectreUi.MarkupLineOrPlain("[grey]Operación cancelada.[/]", "Operación cancelada.");
        }
        catch (Exception ex)
        {
            SpectreUi.MarkupLineOrPlain(
                $"[red]Error:[/] {ExceptionFormatting.GetDiagnosticMessage(ex)}",
                $"Error: {ExceptionFormatting.GetDiagnosticMessage(ex)}"
            );
        }

        SpectreUi.Pause();
    }

    private async Task CreateReservation()
    {
        try
        {
            const int pendingStatusId = 1;
            var utcNow = DateTime.UtcNow;

            var totalRaw = SpectreUi.PromptRequiredCancelable(
                "Total (monto, p.ej. 150000)",
                "0/c/cancelar"
            )
                .Trim();
            if (!decimal.TryParse(totalRaw, out var total) || total < 0)
                throw new InvalidOperationException("Total inválido.");

            var minutesRaw = (SpectreUi.PromptOptionalCancelable(
                "Expira en (minutos)",
                "Enter = sin expiración (0/c/cancelar)"
            ) ?? string.Empty)
                .Trim();
            DateTime? expiresAt = null;
            if (!string.IsNullOrWhiteSpace(minutesRaw))
            {
                if (!int.TryParse(minutesRaw, out var minutes) || minutes < 1)
                    throw new InvalidOperationException("Minutos inválidos.");
                expiresAt = utcNow.AddMinutes(minutes);
            }

            var created = await _createUseCase.ExecuteAsync(
                new CreateReservationRequest(
                    ClientId: _auth.ClientId!.Value,
                    ReservationDate: utcNow,
                    ReservationStatusId: pendingStatusId,
                    TotalValue: total,
                    ExpiresAt: expiresAt,
                    CreatedAt: utcNow,
                    UpdatedAt: utcNow
                )
            );

            SpectreUi.MarkupLineOrPlain(
                $"[green]Reservación creada[/] id={created.Id.Value} total={created.TotalValue.Value:0.00}.",
                $"Reservación creada id={created.Id.Value} total={created.TotalValue.Value:0.00}."
            );
        }
        catch (OperationCanceledException)
        {
            SpectreUi.MarkupLineOrPlain("[grey]Operación cancelada.[/]", "Operación cancelada.");
        }
        catch (Exception ex)
        {
            SpectreUi.MarkupLineOrPlain(
                $"[red]Error:[/] {ExceptionFormatting.GetDiagnosticMessage(ex)}",
                $"Error: {ExceptionFormatting.GetDiagnosticMessage(ex)}"
            );
        }

        SpectreUi.Pause();
    }
}
