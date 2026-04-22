using sistema_gestor_de_tiquetes_aereos.Src.Modules.CabinConfiguration.Application.UseCases;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.CabinConfiguration.Domain.ValueObject;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.CabinConfiguration.Infrastructure.repository;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Helpers;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Ui;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.CabinConfiguration.UI;

public class CabinConfigurationConsoleUI : IModuleUI
{
    private readonly CreateCabinConfigurationUseCase _createUseCase;
    private readonly GetAllCabinConfigurationsUseCase _getAllUseCase;
    private readonly GetCabinConfigurationByIdUseCase _getByIdUseCase;
    private readonly UpdateCabinConfigurationUseCase _updateUseCase;
    private readonly DeleteCabinConfigurationUseCase _deleteUseCase;

    public CabinConfigurationConsoleUI(AppDbContext context)
    {
        var repository = new CabinConfigurationRepository(context);
        _createUseCase = new CreateCabinConfigurationUseCase(repository);
        _getAllUseCase = new GetAllCabinConfigurationsUseCase(repository);
        _getByIdUseCase = new GetCabinConfigurationByIdUseCase(repository);
        _updateUseCase = new UpdateCabinConfigurationUseCase(repository);
        _deleteUseCase = new DeleteCabinConfigurationUseCase(repository);
    }

    public async Task RunAsync()
    {
        bool exit = false;
        while (!exit)
        {
            SpectreUi.ModuleHeader("Configuración de cabina", "Distribución física por aeronave");

            var items = new (string Label, Action Action)[]
            {
                ("Crear configuración", () => CreateCabinConfigurationAsync().GetAwaiter().GetResult()),
                ("Listar todas", () => ViewAllCabinConfigurationsAsync().GetAwaiter().GetResult()),
                ("Consultar por ID", () => ViewCabinConfigurationByIdAsync().GetAwaiter().GetResult()),
                ("Actualizar", () => UpdateCabinConfigurationAsync().GetAwaiter().GetResult()),
                ("Eliminar", () => DeleteCabinConfigurationAsync().GetAwaiter().GetResult()),
                ("Volver", () => exit = true),
            };

            MenuLogic.RunMenu(items);
        }
    }

    private async Task CreateCabinConfigurationAsync()
    {
        SpectreUi.ModuleHeader("Crear configuración de cabina", null);
        try
        {
            Console.Write("ID de la aeronave: ");
            var aircraftId = int.Parse(Console.ReadLine() ?? "0");

            Console.Write("ID del tipo de cabina: ");
            var cabinTypeId = int.Parse(Console.ReadLine() ?? "0");

            Console.Write("Fila inicial: ");
            var startRow = int.Parse(Console.ReadLine() ?? "0");

            Console.Write("Fila final: ");
            var endRow = int.Parse(Console.ReadLine() ?? "0");

            Console.Write("Asientos por fila: ");
            var seatsPerRow = int.Parse(Console.ReadLine() ?? "0");

            Console.Write("Letras de asiento (p. ej. ABCDEF): ");
            var seatLetters = Console.ReadLine() ?? string.Empty;

            var cabinConfiguration = await _createUseCase.ExecuteAsync(
                aircraftId,
                cabinTypeId,
                startRow,
                endRow,
                seatsPerRow,
                seatLetters);

            Console.WriteLine($"Configuración creada con ID: {cabinConfiguration.Id.Value}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        SpectreUi.Pause();
    }

    private async Task ViewAllCabinConfigurationsAsync()
    {
        SpectreUi.ModuleHeader("Configuraciones registradas", null);
        try
        {
            var cabinConfigurations = await _getAllUseCase.ExecuteAsync();
            foreach (var configuration in cabinConfigurations)
            {
                Console.WriteLine($"ID: {configuration.Id.Value}, Aeronave: {configuration.AircraftId.Value}, Tipo cabina: {configuration.CabinTypeId.Value}, Filas: {configuration.StartRow.Value}-{configuration.EndRow.Value}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        SpectreUi.Pause();
    }

    private async Task ViewCabinConfigurationByIdAsync()
    {
        SpectreUi.ModuleHeader("Consultar configuración", null);
        try
        {
            Console.Write("ID: ");
            var id = int.Parse(Console.ReadLine() ?? "0");

            var configuration = await _getByIdUseCase.ExecuteAsync(CabinConfigurationId.Create(id));
            if (configuration != null)
            {
                Console.WriteLine($"ID: {configuration.Id.Value}");
                Console.WriteLine($"Aeronave: {configuration.AircraftId.Value}");
                Console.WriteLine($"Tipo cabina: {configuration.CabinTypeId.Value}");
                Console.WriteLine($"Fila inicio: {configuration.StartRow.Value}");
                Console.WriteLine($"Fila fin: {configuration.EndRow.Value}");
                Console.WriteLine($"Asientos/fila: {configuration.SeatsPerRow.Value}");
                Console.WriteLine($"Letras: {configuration.SeatLetters.Value}");
            }
            else
            {
                Console.WriteLine("No encontrada.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        SpectreUi.Pause();
    }

    private async Task UpdateCabinConfigurationAsync()
    {
        SpectreUi.ModuleHeader("Actualizar configuración", null);
        try
        {
            Console.Write("ID: ");
            var id = int.Parse(Console.ReadLine() ?? "0");

            Console.Write("ID de la aeronave: ");
            var aircraftId = int.Parse(Console.ReadLine() ?? "0");

            Console.Write("ID del tipo de cabina: ");
            var cabinTypeId = int.Parse(Console.ReadLine() ?? "0");

            Console.Write("Fila inicial: ");
            var startRow = int.Parse(Console.ReadLine() ?? "0");

            Console.Write("Fila final: ");
            var endRow = int.Parse(Console.ReadLine() ?? "0");

            Console.Write("Asientos por fila: ");
            var seatsPerRow = int.Parse(Console.ReadLine() ?? "0");

            Console.Write("Letras de asiento: ");
            var seatLetters = Console.ReadLine() ?? string.Empty;

            await _updateUseCase.ExecuteAsync(
                CabinConfigurationId.Create(id),
                aircraftId,
                cabinTypeId,
                startRow,
                endRow,
                seatsPerRow,
                seatLetters);

            Console.WriteLine("Actualizado correctamente.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        SpectreUi.Pause();
    }

    private async Task DeleteCabinConfigurationAsync()
    {
        SpectreUi.ModuleHeader("Eliminar configuración", null);
        try
        {
            Console.Write("ID: ");
            var id = int.Parse(Console.ReadLine() ?? "0");

            await _deleteUseCase.ExecuteAsync(CabinConfigurationId.Create(id));
            Console.WriteLine("Eliminada.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        SpectreUi.Pause();
    }
}
