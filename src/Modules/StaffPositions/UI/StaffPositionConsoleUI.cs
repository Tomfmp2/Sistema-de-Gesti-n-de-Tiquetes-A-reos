using sistema_gestor_de_tiquetes_aereos.Src.Modules.StaffPositions.Application.UseCases;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.StaffPositions.Domain.ValueObject;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Helpers;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Ui;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.StaffPositions.UI;

public class StaffPositionConsoleUI : IModuleUI
{
    private readonly CreateStaffPositionUseCase _createUseCase;
    private readonly GetStaffPositionByIdUseCase _getByIdUseCase;
    private readonly GetAllStaffPositionsUseCase _getAllUseCase;
    private readonly UpdateStaffPositionUseCase _updateUseCase;
    private readonly DeleteStaffPositionUseCase _deleteUseCase;

    public StaffPositionConsoleUI(
        CreateStaffPositionUseCase createUseCase,
        GetStaffPositionByIdUseCase getByIdUseCase,
        GetAllStaffPositionsUseCase getAllUseCase,
        UpdateStaffPositionUseCase updateUseCase,
        DeleteStaffPositionUseCase deleteUseCase)
    {
        _createUseCase = createUseCase;
        _getByIdUseCase = getByIdUseCase;
        _getAllUseCase = getAllUseCase;
        _updateUseCase = updateUseCase;
        _deleteUseCase = deleteUseCase;
    }

    public async Task RunAsync()
    {
        while (true)
        {
            Console.WriteLine("\nGestión de cargos (posiciones de personal)");
            Console.WriteLine("1. Crear cargo");
            Console.WriteLine("2. Consultar cargo por ID");
            Console.WriteLine("3. Listar todos los cargos");
            Console.WriteLine("4. Actualizar cargo");
            Console.WriteLine("5. Eliminar cargo");
            Console.WriteLine("0. Volver al menú principal");
            Console.Write("Elija una opción: ");

            var choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    await CreateStaffPosition();
                    break;
                case "2":
                    await GetStaffPositionById();
                    break;
                case "3":
                    await GetAllStaffPositions();
                    break;
                case "4":
                    await UpdateStaffPosition();
                    break;
                case "5":
                    await DeleteStaffPosition();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Opción no válida. Intente de nuevo.");
                    break;
            }
        }
    }

    private async Task CreateStaffPosition()
    {
        Console.Write("Nombre: ");
        var name = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(name))
        {
            try
            {
                var staffPositionName = StaffPositionName.Create(name);
                await _createUseCase.ExecuteAsync(staffPositionName);
                Console.WriteLine("Cargo creado correctamente.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }

    private async Task GetStaffPositionById()
    {
        Console.Write("ID: ");
        if (int.TryParse(Console.ReadLine(), out var id))
        {
            try
            {
                var staffPositionId = StaffPositionId.Create(id);
                var staffPosition = await _getByIdUseCase.ExecuteAsync(staffPositionId);
                if (staffPosition != null)
                {
                    Console.WriteLine($"ID: {staffPosition.Id.Value}, Nombre: {staffPosition.Name.Value}");
                }
                else
                {
                    Console.WriteLine("Cargo no encontrado.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }

    private async Task GetAllStaffPositions()
    {
        var staffPositions = await _getAllUseCase.ExecuteAsync();
        foreach (var sp in staffPositions)
        {
            Console.WriteLine($"ID: {sp.Id.Value}, Nombre: {sp.Name.Value}");
        }
    }

    private async Task UpdateStaffPosition()
    {
        Console.Write("ID: ");
        if (int.TryParse(Console.ReadLine(), out var id))
        {
            Console.Write("Nuevo nombre: ");
            var name = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(name))
            {
                try
                {
                    var staffPositionId = StaffPositionId.Create(id);
                    var staffPositionName = StaffPositionName.Create(name);
                    await _updateUseCase.ExecuteAsync(staffPositionId, staffPositionName);
                    Console.WriteLine("Cargo actualizado correctamente.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }
    }

    private async Task DeleteStaffPosition()
    {
        Console.Write("ID: ");
        if (int.TryParse(Console.ReadLine(), out var id))
        {
            try
            {
                var staffPositionId = StaffPositionId.Create(id);
                await _deleteUseCase.ExecuteAsync(staffPositionId);
                Console.WriteLine("Cargo eliminado correctamente.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}