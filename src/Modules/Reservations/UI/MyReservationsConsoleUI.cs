using sistema_gestor_de_tiquetes_aereos.Src.Modules.Reservations.Application.UseCases;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Helpers;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Ui;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Reservations.UI;

public sealed class MyReservationsConsoleUI : IModuleUI
{
    private readonly AuthContext _auth;
    private readonly GetReservationsByClientIdUseCase _getMineUseCase;

    public MyReservationsConsoleUI(AuthContext auth, GetReservationsByClientIdUseCase getMineUseCase)
    {
        _auth = auth;
        _getMineUseCase = getMineUseCase;
    }

    public async Task RunAsync()
    {
        SpectreUi.ModuleHeader("Mis reservaciones", "Solo las asociadas a tu cliente");

        if (_auth.ClientId is null)
        {
            Console.WriteLine("Tu usuario no está asociado a un cliente (client_id).");
            SpectreUi.Pause();
            return;
        }

        try
        {
            var list = (await _getMineUseCase.ExecuteAsync(_auth.ClientId.Value)).ToList();
            if (list.Count == 0)
            {
                Console.WriteLine("No tienes reservaciones.");
                SpectreUi.Pause();
                return;
            }

            foreach (var r in list)
            {
                Console.WriteLine(
                    $"ID: {r.Id.Value}, Client: {r.ClientId.Value}, Status: {r.ReservationStatusId.Value}, Total: {r.TotalValue.Value}, BookedAt: {r.ReservationDate.Value:yyyy-MM-dd HH:mm}"
                );
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        SpectreUi.Pause();
    }
}

