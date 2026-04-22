using sistema_gestor_de_tiquetes_aereos.Src.Modules.AvailabilityStatuses.Application.UseCases;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.AvailabilityStatuses.Domain.ValueObject;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Helpers;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Ui;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.AvailabilityStatuses.UI;

public class AvailabilityStatusConsoleUI : IModuleUI
{
    private readonly CreateAvailabilityStatusUseCase _createUseCase;
    private readonly GetAvailabilityStatusByIdUseCase _getByIdUseCase;
    private readonly GetAllAvailabilityStatusesUseCase _getAllUseCase;
    private readonly UpdateAvailabilityStatusUseCase _updateUseCase;
    private readonly DeleteAvailabilityStatusUseCase _deleteUseCase;

    public AvailabilityStatusConsoleUI(
        CreateAvailabilityStatusUseCase createUseCase,
        GetAvailabilityStatusByIdUseCase getByIdUseCase,
        GetAllAvailabilityStatusesUseCase getAllUseCase,
        UpdateAvailabilityStatusUseCase updateUseCase,
        DeleteAvailabilityStatusUseCase deleteUseCase)
    {
        _createUseCase = createUseCase;
        _getByIdUseCase = getByIdUseCase;
        _getAllUseCase = getAllUseCase;
        _updateUseCase = updateUseCase;
        _deleteUseCase = deleteUseCase;
    }

    public async Task RunAsync()
    {
        bool exit = false;
        while (!exit)
        {
            SpectreUi.ModuleHeader("Estados de disponibilidad", "Disponible, asignado, vacaciones…");

            var items = new (string Label, Action Action)[]
            {
                ("Crear estado", () => CreateAvailabilityStatus().GetAwaiter().GetResult()),
                ("Consultar por ID", () => GetAvailabilityStatusById().GetAwaiter().GetResult()),
                ("Listar todos", () => GetAllAvailabilityStatuses().GetAwaiter().GetResult()),
                ("Actualizar", () => UpdateAvailabilityStatus().GetAwaiter().GetResult()),
                ("Eliminar", () => DeleteAvailabilityStatus().GetAwaiter().GetResult()),
                ("Volver", () => exit = true),
            };

            MenuLogic.RunMenu(items);
        }
    }

    private async Task CreateAvailabilityStatus()
    {
        Console.Write("Enter name: ");
        var name = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(name))
        {
            try
            {
                var availabilityStatusName = AvailabilityStatusName.Create(name);
                await _createUseCase.ExecuteAsync(availabilityStatusName);
                Console.WriteLine("Availability Status created successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
        SpectreUi.Pause();
    }

    private async Task GetAvailabilityStatusById()
    {
        Console.Write("Enter ID: ");
        if (int.TryParse(Console.ReadLine(), out var id))
        {
            try
            {
                var availabilityStatusId = AvailabilityStatusId.Create(id);
                var availabilityStatus = await _getByIdUseCase.ExecuteAsync(availabilityStatusId);
                if (availabilityStatus != null)
                {
                    Console.WriteLine($"ID: {availabilityStatus.Id.Value}, Name: {availabilityStatus.Name.Value}");
                }
                else
                {
                    Console.WriteLine("Availability Status not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
        SpectreUi.Pause();
    }

    private async Task GetAllAvailabilityStatuses()
    {
        try
        {
            var availabilityStatuses = (await _getAllUseCase.ExecuteAsync()).ToList();
            if (availabilityStatuses.Count == 0)
            {
                Console.WriteLine("No hay estados para mostrar.");
                SpectreUi.Pause();
                return;
            }

            foreach (var as_ in availabilityStatuses)
            {
                Console.WriteLine($"ID: {as_.Id.Value}, Name: {as_.Name.Value}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        SpectreUi.Pause();
    }

    private async Task UpdateAvailabilityStatus()
    {
        Console.Write("Enter ID: ");
        if (int.TryParse(Console.ReadLine(), out var id))
        {
            Console.Write("Enter new name: ");
            var name = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(name))
            {
                try
                {
                    var availabilityStatusId = AvailabilityStatusId.Create(id);
                    var availabilityStatusName = AvailabilityStatusName.Create(name);
                    await _updateUseCase.ExecuteAsync(availabilityStatusId, availabilityStatusName);
                    Console.WriteLine("Availability Status updated successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }
        SpectreUi.Pause();
    }

    private async Task DeleteAvailabilityStatus()
    {
        Console.Write("Enter ID: ");
        if (int.TryParse(Console.ReadLine(), out var id))
        {
            try
            {
                var availabilityStatusId = AvailabilityStatusId.Create(id);
                await _deleteUseCase.ExecuteAsync(availabilityStatusId);
                Console.WriteLine("Availability Status deleted successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
        SpectreUi.Pause();
    }
}