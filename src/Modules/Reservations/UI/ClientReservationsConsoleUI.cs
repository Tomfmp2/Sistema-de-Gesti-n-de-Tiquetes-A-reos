using sistema_gestor_de_tiquetes_aereos.Src.Modules.Reservations.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Reservations.Application.UseCases;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Helpers;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Ui;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Reservations.UI;

/// <summary>
/// UI para rol Cliente (solo opera sobre su propio client_id).
/// </summary>
public sealed class ClientReservationsConsoleUI : IModuleUI
{
    private readonly AuthContext _auth;
    private readonly CreateReservationUseCase _create;
    private readonly GetReservationsByClientIdUseCase _getMine;
    private readonly GetReservationByIdUseCase _getById;
    private readonly UpdateReservationUseCase _update;
    private readonly DeleteReservationUseCase _delete;

    public ClientReservationsConsoleUI(
        AuthContext auth,
        CreateReservationUseCase create,
        GetReservationsByClientIdUseCase getMine,
        GetReservationByIdUseCase getById,
        UpdateReservationUseCase update,
        DeleteReservationUseCase delete
    )
    {
        _auth = auth;
        _create = create;
        _getMine = getMine;
        _getById = getById;
        _update = update;
        _delete = delete;
    }

    public async Task RunAsync()
    {
        var exit = false;
        while (!exit)
        {
            SpectreUi.ModuleHeader("Reservaciones", "Crear / ver / modificar / cancelar");

            if (_auth.ClientId is null)
            {
                Console.WriteLine("Tu usuario no está asociado a un cliente (client_id).");
                SpectreUi.Pause();
                return;
            }

            var items = new List<(string Label, Action Action)>
            {
                ("Crear reservación", () => Create().GetAwaiter().GetResult()),
                ("Listar mis reservaciones", () => ListMine().GetAwaiter().GetResult()),
                ("Modificar (por ID)", () => Update().GetAwaiter().GetResult()),
                ("Cancelar (por ID)", () => Cancel().GetAwaiter().GetResult()),
                ("Volver", () => exit = true),
            };

            MenuLogic.RunMenu(items);
        }
    }

    private async Task ListMine()
    {
        try
        {
            var list = (await _getMine.ExecuteAsync(_auth.ClientId!.Value)).ToList();
            if (list.Count == 0)
            {
                Console.WriteLine("No tienes reservaciones.");
                return;
            }

            foreach (var r in list.OrderByDescending(x => x.ReservationDate.Value))
            {
                Console.WriteLine(
                    $"ID: {r.Id.Value}, Status: {r.ReservationStatusId.Value}, Total: {r.TotalValue.Value}, BookedAt: {r.ReservationDate.Value:yyyy-MM-dd HH:mm}, ExpiresAt: {(r.ExpiresAt.Value.HasValue ? r.ExpiresAt.Value.Value.ToString("yyyy-MM-dd HH:mm") : "null")}"
                );
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ExceptionFormatting.GetDiagnosticMessage(ex)}");
        }

        SpectreUi.Pause();
    }

    private async Task Create()
    {
        try
        {
            // Para cliente: status inicial = Pendiente (1)
            const int pendingStatusId = 1;
            var utcNow = DateTime.UtcNow;

            Console.WriteLine("Se creará con estado inicial: Pendiente.");
            Console.Write("Total (decimal, p.ej. 150000). Enter = 0: ");
            var totalRaw = (Console.ReadLine() ?? string.Empty).Trim();
            var total = 0m;
            if (!string.IsNullOrWhiteSpace(totalRaw))
            {
                if (!decimal.TryParse(totalRaw, out total) || total < 0)
                    throw new InvalidOperationException("Total inválido.");
            }

            Console.Write("Expira en (minutos, opcional; Enter = sin expiración): ");
            var minutesRaw = (Console.ReadLine() ?? string.Empty).Trim();
            DateTime? expiresAt = null;
            if (!string.IsNullOrWhiteSpace(minutesRaw))
            {
                if (!int.TryParse(minutesRaw, out var minutes) || minutes < 1)
                    throw new InvalidOperationException("Minutos inválidos.");
                expiresAt = utcNow.AddMinutes(minutes);
            }

            var created = await _create.ExecuteAsync(
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

    private async Task Update()
    {
        try
        {
            Console.Write("ID reservación: ");
            var id = int.Parse(Console.ReadLine()!);

            var current = await _getById.ExecuteAsync(id);
            if (current is null)
                throw new InvalidOperationException("No encontrada.");

            if (current.ClientId.Value != _auth.ClientId!.Value)
                throw new InvalidOperationException("No puedes modificar reservaciones de otro cliente.");

            // Edición mínima: total + expiración. (Status lo maneja el flujo del sistema.)
            Console.Write($"Total (actual={current.TotalValue.Value}): ");
            var totalRaw = (Console.ReadLine() ?? string.Empty).Trim();
            var newTotal = string.IsNullOrWhiteSpace(totalRaw)
                ? current.TotalValue.Value
                : decimal.Parse(totalRaw);
            if (newTotal < 0)
                throw new InvalidOperationException("Total inválido.");

            Console.Write(
                $"Expira en (minutos; Enter=mantener, 0=quitar) (actual={(current.ExpiresAt.Value.HasValue ? current.ExpiresAt.Value.Value.ToString("yyyy-MM-dd HH:mm") : "null")}): "
            );
            var minutesRaw = (Console.ReadLine() ?? string.Empty).Trim();
            var utcNow = DateTime.UtcNow;
            DateTime? expiresAt = current.ExpiresAt.Value;
            if (!string.IsNullOrWhiteSpace(minutesRaw))
            {
                if (!int.TryParse(minutesRaw, out var minutes) || minutes < 0)
                    throw new InvalidOperationException("Minutos inválidos.");
                expiresAt = minutes == 0 ? null : utcNow.AddMinutes(minutes);
            }

            await _update.ExecuteAsync(
                new UpdateReservationRequest(
                    Id: current.Id.Value,
                    ClientId: current.ClientId.Value,
                    ReservationDate: current.ReservationDate.Value,
                    ReservationStatusId: current.ReservationStatusId.Value,
                    TotalValue: newTotal,
                    ExpiresAt: expiresAt,
                    CreatedAt: current.CreatedAt.Value,
                    UpdatedAt: utcNow
                )
            );

            Console.WriteLine("Actualizada.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ExceptionFormatting.GetDiagnosticMessage(ex)}");
        }

        SpectreUi.Pause();
    }

    private async Task Cancel()
    {
        try
        {
            Console.Write("ID reservación: ");
            var id = int.Parse(Console.ReadLine()!);

            var current = await _getById.ExecuteAsync(id);
            if (current is null)
                throw new InvalidOperationException("No encontrada.");

            if (current.ClientId.Value != _auth.ClientId!.Value)
                throw new InvalidOperationException("No puedes cancelar reservaciones de otro cliente.");

            // Implementación simple: delete (si quieres, luego lo cambiamos a status=Cancelada (3))
            await _delete.ExecuteAsync(id);
            Console.WriteLine("Cancelada.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ExceptionFormatting.GetDiagnosticMessage(ex)}");
        }

        SpectreUi.Pause();
    }
}

