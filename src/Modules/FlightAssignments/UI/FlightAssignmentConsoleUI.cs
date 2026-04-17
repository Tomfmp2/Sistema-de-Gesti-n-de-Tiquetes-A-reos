using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightAssignments.Application.UseCases;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightAssignments.UI;

public sealed class FlightAssignmentConsoleUI
{
    private readonly CreateFlightAssignmentUseCase _createUseCase;
    private readonly GetFlightAssignmentByIdUseCase _getByIdUseCase;
    private readonly GetAllFlightAssignmentsUseCase _getAllUseCase;
    private readonly UpdateFlightAssignmentUseCase _updateUseCase;
    private readonly DeleteFlightAssignmentUseCase _deleteUseCase;

    public FlightAssignmentConsoleUI(
        CreateFlightAssignmentUseCase createUseCase,
        GetFlightAssignmentByIdUseCase getByIdUseCase,
        GetAllFlightAssignmentsUseCase getAllUseCase,
        UpdateFlightAssignmentUseCase updateUseCase,
        DeleteFlightAssignmentUseCase deleteUseCase)
    {
        _createUseCase = createUseCase ?? throw new ArgumentNullException(nameof(createUseCase));
        _getByIdUseCase = getByIdUseCase ?? throw new ArgumentNullException(nameof(getByIdUseCase));
        _getAllUseCase = getAllUseCase ?? throw new ArgumentNullException(nameof(getAllUseCase));
        _updateUseCase = updateUseCase ?? throw new ArgumentNullException(nameof(updateUseCase));
        _deleteUseCase = deleteUseCase ?? throw new ArgumentNullException(nameof(deleteUseCase));
    }

    public async Task RunAsync()
    {
        while (true)
        {
            Console.WriteLine("\n=== Flight Assignment Management ===");
            Console.WriteLine("1. Create Flight Assignment");
            Console.WriteLine("2. Get Flight Assignment by ID");
            Console.WriteLine("3. Get All Flight Assignments");
            Console.WriteLine("4. Update Flight Assignment");
            Console.WriteLine("5. Delete Flight Assignment");
            Console.WriteLine("6. Exit");
            Console.Write("Choose an option: ");

            string? choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    await CreateFlightAssignmentAsync();
                    break;
                case "2":
                    await GetFlightAssignmentByIdAsync();
                    break;
                case "3":
                    await GetAllFlightAssignmentsAsync();
                    break;
                case "4":
                    await UpdateFlightAssignmentAsync();
                    break;
                case "5":
                    await DeleteFlightAssignmentAsync();
                    break;
                case "6":
                    return;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }

    private async Task CreateFlightAssignmentAsync()
    {
        Console.Write("Enter Flight ID: ");
        if (!int.TryParse(Console.ReadLine(), out int flightId) || flightId <= 0)
        {
            Console.WriteLine("Invalid Flight ID.");
            return;
        }

        Console.Write("Enter Staff ID: ");
        if (!int.TryParse(Console.ReadLine(), out int staffId) || staffId <= 0)
        {
            Console.WriteLine("Invalid Staff ID.");
            return;
        }

        Console.Write("Enter Flight Role ID: ");
        if (!int.TryParse(Console.ReadLine(), out int flightRoleId) || flightRoleId <= 0)
        {
            Console.WriteLine("Invalid Flight Role ID.");
            return;
        }

        try
        {
            var flightAssignment = await _createUseCase.ExecuteAsync(flightId, staffId, flightRoleId);
            Console.WriteLine($"Flight Assignment created successfully with ID: {flightAssignment.Id.Value}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    private async Task GetFlightAssignmentByIdAsync()
    {
        Console.Write("Enter Flight Assignment ID: ");
        if (!int.TryParse(Console.ReadLine(), out int id) || id <= 0)
        {
            Console.WriteLine("Invalid ID.");
            return;
        }

        try
        {
            var flightAssignment = await _getByIdUseCase.ExecuteAsync(id);
            
            if (flightAssignment == null)
            {
                Console.WriteLine("Flight Assignment not found.");
                return;
            }

            Console.WriteLine($"\nID: {flightAssignment.Id.Value}");
            Console.WriteLine($"Flight ID: {flightAssignment.FlightId.Value}");
            Console.WriteLine($"Staff ID: {flightAssignment.StaffId.Value}");
            Console.WriteLine($"Flight Role ID: {flightAssignment.FlightRoleId.Value}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    private async Task GetAllFlightAssignmentsAsync()
    {
        try
        {
            var flightAssignments = await _getAllUseCase.ExecuteAsync();
            
            var list = flightAssignments.ToList();
            if (!list.Any())
            {
                Console.WriteLine("No flight assignments found.");
                return;
            }

            Console.WriteLine("\n=== Flight Assignments ===");
            foreach (var fa in list)
            {
                Console.WriteLine($"ID: {fa.Id.Value} | Flight: {fa.FlightId.Value} | Staff: {fa.StaffId.Value} | Role: {fa.FlightRoleId.Value}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    private async Task UpdateFlightAssignmentAsync()
    {
        Console.Write("Enter Flight Assignment ID: ");
        if (!int.TryParse(Console.ReadLine(), out int id) || id <= 0)
        {
            Console.WriteLine("Invalid ID.");
            return;
        }

        Console.Write("Enter new Flight ID (or press Enter to skip): ");
        int? newFlightId = null;
        string? flightInput = Console.ReadLine();
        if (!string.IsNullOrEmpty(flightInput) && int.TryParse(flightInput, out int fid) && fid > 0)
            newFlightId = fid;

        Console.Write("Enter new Staff ID (or press Enter to skip): ");
        int? newStaffId = null;
        string? staffInput = Console.ReadLine();
        if (!string.IsNullOrEmpty(staffInput) && int.TryParse(staffInput, out int sid) && sid > 0)
            newStaffId = sid;

        Console.Write("Enter new Flight Role ID (or press Enter to skip): ");
        int? newFlightRoleId = null;
        string? roleInput = Console.ReadLine();
        if (!string.IsNullOrEmpty(roleInput) && int.TryParse(roleInput, out int rid) && rid > 0)
            newFlightRoleId = rid;

        try
        {
            var flightAssignment = await _updateUseCase.ExecuteAsync(id, newFlightId, newStaffId, newFlightRoleId);
            
            if (flightAssignment == null)
            {
                Console.WriteLine("Flight Assignment not found.");
                return;
            }

            Console.WriteLine("Flight Assignment updated successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    private async Task DeleteFlightAssignmentAsync()
    {
        Console.Write("Enter Flight Assignment ID to delete: ");
        if (!int.TryParse(Console.ReadLine(), out int id) || id <= 0)
        {
            Console.WriteLine("Invalid ID.");
            return;
        }

        try
        {
            bool deleted = await _deleteUseCase.ExecuteAsync(id);
            
            if (!deleted)
            {
                Console.WriteLine("Flight Assignment not found.");
                return;
            }

            Console.WriteLine("Flight Assignment deleted successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}
