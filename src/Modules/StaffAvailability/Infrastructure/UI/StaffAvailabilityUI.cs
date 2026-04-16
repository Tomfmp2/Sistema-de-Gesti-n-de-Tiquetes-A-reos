using sistema_gestor_de_tiquetes_aereos.Src.Modules.StaffAvailability.Application.UseCases;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.StaffAvailability.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.StaffAvailability.Domain.ValueObject;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Ui;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.StaffAvailability.Infrastructure.UI;

public class StaffAvailabilityUI : IModuleUI
{
    private readonly CreateStaffAvailabilityUseCase _createUseCase;
    private readonly GetAllStaffAvailabilitiesUseCase _getAllUseCase;
    private readonly GetStaffAvailabilityByIdUseCase _getByIdUseCase;
    private readonly UpdateStaffAvailabilityUseCase _updateUseCase;
    private readonly DeleteStaffAvailabilityUseCase _deleteUseCase;

    public StaffAvailabilityUI(
        CreateStaffAvailabilityUseCase createUseCase,
        GetAllStaffAvailabilitiesUseCase getAllUseCase,
        GetStaffAvailabilityByIdUseCase getByIdUseCase,
        UpdateStaffAvailabilityUseCase updateUseCase,
        DeleteStaffAvailabilityUseCase deleteUseCase)
    {
        _createUseCase = createUseCase;
        _getAllUseCase = getAllUseCase;
        _getByIdUseCase = getByIdUseCase;
        _updateUseCase = updateUseCase;
        _deleteUseCase = deleteUseCase;
    }

    public async Task RunAsync()
    {
        while (true)
        {
            Console.WriteLine("\n=== Staff Availability Management ===");
            Console.WriteLine("1. Create Staff Availability");
            Console.WriteLine("2. View All Staff Availabilities");
            Console.WriteLine("3. View Staff Availability by ID");
            Console.WriteLine("4. Update Staff Availability");
            Console.WriteLine("5. Delete Staff Availability");
            Console.WriteLine("0. Back to Main Menu");
            Console.Write("Choose an option: ");

            var choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    await CreateStaffAvailabilityAsync();
                    break;
                case "2":
                    await ViewAllStaffAvailabilitiesAsync();
                    break;
                case "3":
                    await ViewStaffAvailabilityByIdAsync();
                    break;
                case "4":
                    await UpdateStaffAvailabilityAsync();
                    break;
                case "5":
                    await DeleteStaffAvailabilityAsync();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }

    private async Task CreateStaffAvailabilityAsync()
    {
        try
        {
            Console.Write("Enter Staff Availability ID (GUID): ");
            var id = Guid.Parse(Console.ReadLine()!);
            var staffAvailabilityId = StaffAvailabilityId.Create(id);

            Console.Write("Enter Staff ID (GUID): ");
            var staffIdValue = Guid.Parse(Console.ReadLine()!);
            var staffId = StaffId.Create(staffIdValue);

            Console.Write("Enter Availability Status ID (GUID): ");
            var availabilityStatusIdValue = Guid.Parse(Console.ReadLine()!);
            var availabilityStatusId = AvailabilityStatusId.Create(availabilityStatusIdValue);

            Console.Write("Enter Start Date (yyyy-MM-dd): ");
            var startDateValue = DateTime.Parse(Console.ReadLine()!);
            var startDate = StartDate.Create(startDateValue);

            Console.Write("Enter End Date (yyyy-MM-dd): ");
            var endDateValue = DateTime.Parse(Console.ReadLine()!);
            var endDate = EndDate.Create(endDateValue);

            Console.Write("Enter Observation (optional): ");
            var observationInput = Console.ReadLine();
            Observation? observation = null;
            if (!string.IsNullOrWhiteSpace(observationInput))
            {
                observation = Observation.Create(observationInput);
            }

            await _createUseCase.ExecuteAsync(staffAvailabilityId, staffId, availabilityStatusId, startDate, endDate, observation);
            Console.WriteLine("Staff availability created successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    private async Task ViewAllStaffAvailabilitiesAsync()
    {
        var staffAvailabilities = await _getAllUseCase.ExecuteAsync();
        foreach (var sa in staffAvailabilities)
        {
            Console.WriteLine($"ID: {sa.Id.Value}, Staff ID: {sa.StaffId.Value}, Status ID: {sa.AvailabilityStatusId.Value}, Start: {sa.StartDate.Value}, End: {sa.EndDate.Value}, Observation: {sa.Observation?.Value ?? "None"}");
        }
    }

    private async Task ViewStaffAvailabilityByIdAsync()
    {
        Console.Write("Enter Staff Availability ID (GUID): ");
        var id = Guid.Parse(Console.ReadLine()!);
        var staffAvailabilityId = StaffAvailabilityId.Create(id);

        var staffAvailability = await _getByIdUseCase.ExecuteAsync(staffAvailabilityId);
        if (staffAvailability != null)
        {
            Console.WriteLine($"ID: {staffAvailability.Id.Value}, Staff ID: {staffAvailability.StaffId.Value}, Status ID: {staffAvailability.AvailabilityStatusId.Value}, Start: {staffAvailability.StartDate.Value}, End: {staffAvailability.EndDate.Value}, Observation: {staffAvailability.Observation?.Value ?? "None"}");
        }
        else
        {
            Console.WriteLine("Staff availability not found.");
        }
    }

    private async Task UpdateStaffAvailabilityAsync()
    {
        try
        {
            Console.Write("Enter Staff Availability ID (GUID): ");
            var id = Guid.Parse(Console.ReadLine()!);
            var staffAvailabilityId = StaffAvailabilityId.Create(id);

            Console.Write("Enter new Staff ID (GUID): ");
            var staffIdValue = Guid.Parse(Console.ReadLine()!);
            var staffId = StaffId.Create(staffIdValue);

            Console.Write("Enter new Availability Status ID (GUID): ");
            var availabilityStatusIdValue = Guid.Parse(Console.ReadLine()!);
            var availabilityStatusId = AvailabilityStatusId.Create(availabilityStatusIdValue);

            Console.Write("Enter new Start Date (yyyy-MM-dd): ");
            var startDateValue = DateTime.Parse(Console.ReadLine()!);
            var startDate = StartDate.Create(startDateValue);

            Console.Write("Enter new End Date (yyyy-MM-dd): ");
            var endDateValue = DateTime.Parse(Console.ReadLine()!);
            var endDate = EndDate.Create(endDateValue);

            Console.Write("Enter new Observation (optional): ");
            var observationInput = Console.ReadLine();
            Observation? observation = null;
            if (!string.IsNullOrWhiteSpace(observationInput))
            {
                observation = Observation.Create(observationInput);
            }

            await _updateUseCase.ExecuteAsync(staffAvailabilityId, staffId, availabilityStatusId, startDate, endDate, observation);
            Console.WriteLine("Staff availability updated successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    private async Task DeleteStaffAvailabilityAsync()
    {
        Console.Write("Enter Staff Availability ID (GUID): ");
        var id = Guid.Parse(Console.ReadLine()!);
        var staffAvailabilityId = StaffAvailabilityId.Create(id);

        await _deleteUseCase.ExecuteAsync(staffAvailabilityId);
        Console.WriteLine("Staff availability deleted successfully.");
    }
}