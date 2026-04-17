using sistema_gestor_de_tiquetes_aereos.Src.Modules.StaffPositions.Application.UseCases;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.StaffPositions.Domain.ValueObject;
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
            Console.WriteLine("\nStaff Positions Management");
            Console.WriteLine("1. Create Staff Position");
            Console.WriteLine("2. Get Staff Position by ID");
            Console.WriteLine("3. Get All Staff Positions");
            Console.WriteLine("4. Update Staff Position");
            Console.WriteLine("5. Delete Staff Position");
            Console.WriteLine("0. Back to Main Menu");
            Console.Write("Choose an option: ");

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
                    Console.WriteLine("Invalid option. Try again.");
                    break;
            }
        }
    }

    private async Task CreateStaffPosition()
    {
        Console.Write("Enter name: ");
        var name = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(name))
        {
            try
            {
                var staffPositionName = StaffPositionName.Create(name);
                await _createUseCase.ExecuteAsync(staffPositionName);
                Console.WriteLine("Staff Position created successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }

    private async Task GetStaffPositionById()
    {
        Console.Write("Enter ID: ");
        if (int.TryParse(Console.ReadLine(), out var id))
        {
            try
            {
                var staffPositionId = StaffPositionId.Create(id);
                var staffPosition = await _getByIdUseCase.ExecuteAsync(staffPositionId);
                if (staffPosition != null)
                {
                    Console.WriteLine($"ID: {staffPosition.Id.Value}, Name: {staffPosition.Name.Value}");
                }
                else
                {
                    Console.WriteLine("Staff Position not found.");
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
            Console.WriteLine($"ID: {sp.Id.Value}, Name: {sp.Name.Value}");
        }
    }

    private async Task UpdateStaffPosition()
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
                    var staffPositionId = StaffPositionId.Create(id);
                    var staffPositionName = StaffPositionName.Create(name);
                    await _updateUseCase.ExecuteAsync(staffPositionId, staffPositionName);
                    Console.WriteLine("Staff Position updated successfully.");
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
        Console.Write("Enter ID: ");
        if (int.TryParse(Console.ReadLine(), out var id))
        {
            try
            {
                var staffPositionId = StaffPositionId.Create(id);
                await _deleteUseCase.ExecuteAsync(staffPositionId);
                Console.WriteLine("Staff Position deleted successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}