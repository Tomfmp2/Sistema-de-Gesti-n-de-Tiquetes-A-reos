using sistema_gestor_de_tiquetes_aereos.Src.Modules.AvailabilityStatuses.Application.UseCases;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.AvailabilityStatuses.Domain.ValueObject;
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
        while (true)
        {
            Console.WriteLine("\nAvailability Statuses Management");
            Console.WriteLine("1. Create Availability Status");
            Console.WriteLine("2. Get Availability Status by ID");
            Console.WriteLine("3. Get All Availability Statuses");
            Console.WriteLine("4. Update Availability Status");
            Console.WriteLine("5. Delete Availability Status");
            Console.WriteLine("0. Back to Main Menu");
            Console.Write("Choose an option: ");

            var choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    await CreateAvailabilityStatus();
                    break;
                case "2":
                    await GetAvailabilityStatusById();
                    break;
                case "3":
                    await GetAllAvailabilityStatuses();
                    break;
                case "4":
                    await UpdateAvailabilityStatus();
                    break;
                case "5":
                    await DeleteAvailabilityStatus();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Invalid option. Try again.");
                    break;
            }
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
    }

    private async Task GetAllAvailabilityStatuses()
    {
        var availabilityStatuses = await _getAllUseCase.ExecuteAsync();
        foreach (var as_ in availabilityStatuses)
        {
            Console.WriteLine($"ID: {as_.Id.Value}, Name: {as_.Name.Value}");
        }
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
    }
}