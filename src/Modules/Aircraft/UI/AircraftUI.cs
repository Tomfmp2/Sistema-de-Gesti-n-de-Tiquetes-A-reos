using Aggregate = sistema_gestor_de_tiquetes_aereos.Src.Modules.Aircraft.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Aircraft.Application.UseCases;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Aircraft.Domain.ValueObject;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Aircraft.Infrastructure.Repository;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Helpers;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Ui;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Aircraft.UI;

public class AircraftUI : IModuleUI
{
    private readonly CreateAircraftUseCase _createUseCase;
    private readonly GetAllAircraftUseCase _getAllUseCase;
    private readonly GetAircraftByIdUseCase _getByIdUseCase;
    private readonly UpdateAircraftUseCase _updateUseCase;
    private readonly DeleteAircraftUseCase _deleteUseCase;

    public AircraftUI(AppDbContext context)
    {
        var repository = new AircraftRepository(context);
        _createUseCase = new CreateAircraftUseCase(repository);
        _getAllUseCase = new GetAllAircraftUseCase(repository);
        _getByIdUseCase = new GetAircraftByIdUseCase(repository);
        _updateUseCase = new UpdateAircraftUseCase(repository);
        _deleteUseCase = new DeleteAircraftUseCase(repository);
    }

    public async Task RunAsync()
    {
        bool exit = false;

        while (!exit)
        {
            SpectreUi.ModuleHeader("Aeronaves", "Gestión de flota");

            var items = new (string Label, Action Action)[]
            {
                ("Crear aeronave", () => CreateAircraftAsync().GetAwaiter().GetResult()),
                ("Listar todas", () => ViewAllAircraftAsync().GetAwaiter().GetResult()),
                ("Consultar por ID", () => ViewAircraftByIdAsync().GetAwaiter().GetResult()),
                ("Actualizar", () => UpdateAircraftAsync().GetAwaiter().GetResult()),
                ("Eliminar", () => DeleteAircraftAsync().GetAwaiter().GetResult()),
                ("Volver", () => exit = true),
            };

            MenuLogic.RunMenu(items);
        }
    }

    private async Task CreateAircraftAsync()
    {
        SpectreUi.ModuleHeader("Crear aeronave", null);
        try
        {
            Console.Write("ID del modelo: ");
            var modelId = int.Parse(Console.ReadLine() ?? "0");

            Console.Write("ID de la aerolínea: ");
            var airlineId = int.Parse(Console.ReadLine() ?? "0");

            Console.Write("Matrícula: ");
            var registration = Console.ReadLine() ?? "";

            Console.Write("Fecha de fabricación (yyyy-MM-dd, opcional): ");
            var dateInput = Console.ReadLine();
            DateOnly? manufacturingDate = null;
            if (!string.IsNullOrWhiteSpace(dateInput))
            {
                manufacturingDate = DateOnly.Parse(dateInput);
            }

            Console.Write("¿Activa? (true/false): ");
            var isActive = bool.Parse(Console.ReadLine() ?? "true");

            var aircraft = await _createUseCase.ExecuteAsync(
                ModelId.Create(modelId),
                AirlineId.Create(airlineId),
                Registration.Create(registration),
                manufacturingDate.HasValue ? ManufacturingDate.Create(manufacturingDate.Value) : null,
                IsActive.Create(isActive)
            );

            Console.WriteLine($"Aeronave creada con ID: {aircraft.Id.Value}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        SpectreUi.Pause();
    }

    private async Task ViewAllAircraftAsync()
    {
        SpectreUi.ModuleHeader("Listado de aeronaves", null);
        try
        {
            var aircraft = await _getAllUseCase.ExecuteAsync();
            foreach (var a in aircraft)
            {
                Console.WriteLine($"ID: {a.Id.Value}, Matrícula: {a.Registration.Value}, Activa: {a.IsActive.Value}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        SpectreUi.Pause();
    }

    private async Task ViewAircraftByIdAsync()
    {
        SpectreUi.ModuleHeader("Consultar aeronave", null);
        try
        {
            Console.Write("ID de la aeronave: ");
            var id = int.Parse(Console.ReadLine() ?? "0");

            var aircraft = await _getByIdUseCase.ExecuteAsync(AircraftId.Create(id));
            if (aircraft != null)
            {
                Console.WriteLine($"ID: {aircraft.Id.Value}");
                Console.WriteLine($"Modelo ID: {aircraft.ModelId.Value}");
                Console.WriteLine($"Aerolínea ID: {aircraft.AirlineId.Value}");
                Console.WriteLine($"Matrícula: {aircraft.Registration.Value}");
                Console.WriteLine($"Fabricación: {aircraft.ManufacturingDate?.Value.ToString("yyyy-MM-dd") ?? "N/D"}");
                Console.WriteLine($"Activa: {aircraft.IsActive.Value}");
            }
            else
            {
                Console.WriteLine("No se encontró la aeronave.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        SpectreUi.Pause();
    }

    private async Task UpdateAircraftAsync()
    {
        SpectreUi.ModuleHeader("Actualizar aeronave", null);
        try
        {
            Console.Write("ID de la aeronave: ");
            var id = int.Parse(Console.ReadLine() ?? "0");

            Console.Write("ID del modelo: ");
            var modelId = int.Parse(Console.ReadLine() ?? "0");

            Console.Write("ID de la aerolínea: ");
            var airlineId = int.Parse(Console.ReadLine() ?? "0");

            Console.Write("Matrícula: ");
            var registration = Console.ReadLine() ?? "";

            Console.Write("Fecha de fabricación (yyyy-MM-dd, opcional): ");
            var dateInput = Console.ReadLine();
            DateOnly? manufacturingDate = null;
            if (!string.IsNullOrWhiteSpace(dateInput))
            {
                manufacturingDate = DateOnly.Parse(dateInput);
            }

            Console.Write("¿Activa? (true/false): ");
            var isActive = bool.Parse(Console.ReadLine() ?? "true");

            await _updateUseCase.ExecuteAsync(
                AircraftId.Create(id),
                ModelId.Create(modelId),
                AirlineId.Create(airlineId),
                Registration.Create(registration),
                manufacturingDate.HasValue ? ManufacturingDate.Create(manufacturingDate.Value) : null,
                IsActive.Create(isActive)
            );

            Console.WriteLine("Aeronave actualizada correctamente.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        SpectreUi.Pause();
    }

    private async Task DeleteAircraftAsync()
    {
        SpectreUi.ModuleHeader("Eliminar aeronave", null);
        try
        {
            Console.Write("ID de la aeronave: ");
            var id = int.Parse(Console.ReadLine() ?? "0");

            await _deleteUseCase.ExecuteAsync(AircraftId.Create(id));
            Console.WriteLine("Aeronave eliminada.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        SpectreUi.Pause();
    }
}
