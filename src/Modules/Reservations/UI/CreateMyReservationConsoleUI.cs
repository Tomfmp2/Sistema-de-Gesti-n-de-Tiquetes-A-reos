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
        SpectreUi.ModuleHeader("Crear reservación", "Alta rápida (cliente)");

        if (_auth.ClientId is null)
        {
            Console.WriteLine("Tu usuario no está asociado a un cliente (client_id).");
            SpectreUi.Pause();
            return;
        }

        try
        {
            // Para cliente: status inicial = Pendiente (1)
            const int pendingStatusId = 1;
            var utcNow = DateTime.UtcNow;

            Console.Write("Total (decimal, p.ej. 150000): ");
            var totalRaw = (Console.ReadLine() ?? string.Empty).Trim();
            if (!decimal.TryParse(totalRaw, out var total) || total < 0)
                throw new InvalidOperationException("Total inválido.");

            Console.Write("Expira en (minutos, opcional; Enter = sin expiración): ");
            var minutesRaw = (Console.ReadLine() ?? string.Empty).Trim();
            DateTime? expiresAt = null;
            if (!string.IsNullOrWhiteSpace(minutesRaw))
            {
                if (!int.TryParse(minutesRaw, out var minutes) || minutes < 1)
                    throw new InvalidOperationException("Minutos inválidos.");
                expiresAt = utcNow.AddMinutes(minutes);
            }

            var created = await _createUseCase.ExecuteAsync(
                new CreateReservationRequest(
                    ClientId: _auth.ClientId.Value,
                    ReservationDate: utcNow,
                    ReservationStatusId: pendingStatusId,
                    TotalValue: total,
                    ExpiresAt: expiresAt,
                    CreatedAt: utcNow,
                    UpdatedAt: utcNow
                )
            );

            SpectreUi.MarkupLineOrPlain(
                $"[green]Reservación creada[/] id={created.Id.Value} total={created.TotalValue.Value} status_id={created.ReservationStatusId.Value}.",
                $"Reservación creada id={created.Id.Value} total={created.TotalValue.Value} status_id={created.ReservationStatusId.Value}."
            );
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ExceptionFormatting.GetDiagnosticMessage(ex)}");
        }

        SpectreUi.Pause();
    }
}

