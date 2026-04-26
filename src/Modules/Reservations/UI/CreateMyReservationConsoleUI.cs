using sistema_gestor_de_tiquetes_aereos.Src.Modules.Reservations.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Reservations.Application.UseCases;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Helpers;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Ui;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Reservations.UI;

public sealed class CreateMyReservationConsoleUI : IModuleUI
{
    private readonly AuthContext _auth;
    private readonly CreateReservationUseCase _createUseCase;

    public CreateMyReservationConsoleUI(AuthContext auth, CreateReservationUseCase createUseCase)
    {
        _auth = auth;
        _createUseCase = createUseCase;
    }

    public async Task RunAsync()
    {
        var exit = false;
        while (!exit)
        {
            SpectreUi.ModuleHeader("Crear reservación", "Alta rápida (cliente)");

            if (_auth.ClientId is null)
            {
                SpectreUi.MarkupLineOrPlain(
                    "[red]Tu usuario no está asociado a un cliente (client_id).[/]",
                    "Tu usuario no está asociado a un cliente (client_id)."
                );
                SpectreUi.Pause();
                return;
            }

            var items = new List<(string Label, Action Action)>
            {
                ("Crear", () => CreateAsync().GetAwaiter().GetResult()),
                ("Volver", () => exit = true),
            };

            MenuLogic.RunMenu(items);
        }
    }

    private async Task CreateAsync()
    {
        try
        {
            // Para cliente: status inicial = Pendiente (1)
            const int pendingStatusId = 1;
            var utcNow = DateTime.UtcNow;

            SpectreUi.ModuleHeader("Crear reservación", "Datos de la reservación");

            decimal total;
            while (true)
            {
                var totalRaw = SpectreUi.PromptRequiredCancelable("Total", "decimal (0/c/cancelar para salir)").Trim();
                if (decimal.TryParse(totalRaw, out total) && total >= 0)
                    break;
                SpectreUi.MarkupLineOrPlain("[red]Total inválido.[/]", "Total inválido.");
            }

            DateTime? expiresAt = null;
            while (true)
            {
                var minutesRaw = (SpectreUi.PromptOptionalCancelable(
                    "Expira en (minutos)",
                    "Enter = sin expiración (0/c/cancelar para salir)"
                ) ?? string.Empty).Trim();

                if (string.IsNullOrWhiteSpace(minutesRaw))
                    break;

                if (int.TryParse(minutesRaw, out var minutes) && minutes >= 1)
                {
                    expiresAt = utcNow.AddMinutes(minutes);
                    break;
                }
                SpectreUi.MarkupLineOrPlain("[red]Minutos inválidos.[/]", "Minutos inválidos.");
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

            SpectreUi.ShowTable(
                "Reservación creada",
                ["Campo", "Valor"],
                [
                    ["ID", created.Id.Value.ToString()],
                    ["EstadoId", created.ReservationStatusId.Value.ToString()],
                    ["Total", created.TotalValue.Value.ToString("0.00")],
                    ["Fecha", created.ReservationDate.Value.ToString("yyyy-MM-dd HH:mm")],
                    ["Expira", created.ExpiresAt.Value.HasValue ? created.ExpiresAt.Value.Value.ToString("yyyy-MM-dd HH:mm") : "-"]
                ]
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

