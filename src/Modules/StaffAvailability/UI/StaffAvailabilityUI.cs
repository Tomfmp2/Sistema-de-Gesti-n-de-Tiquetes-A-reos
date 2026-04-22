using sistema_gestor_de_tiquetes_aereos.Src.Modules.StaffAvailability.Application.UseCases;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.StaffAvailability.Domain.ValueObject;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Helpers;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Ui;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.StaffAvailability.UI;

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
        bool exit = false;
        while (!exit)
        {
            SpectreUi.ModuleHeader("Disponibilidad de personal", "Bloques de tiempo por empleado");

            var items = new (string Label, Action Action)[]
            {
                ("Registrar disponibilidad", () => CreateStaffAvailabilityAsync().GetAwaiter().GetResult()),
                ("Listar todas", () => ViewAllStaffAvailabilitiesAsync().GetAwaiter().GetResult()),
                ("Consultar por ID", () => ViewStaffAvailabilityByIdAsync().GetAwaiter().GetResult()),
                ("Actualizar", () => UpdateStaffAvailabilityAsync().GetAwaiter().GetResult()),
                ("Eliminar", () => DeleteStaffAvailabilityAsync().GetAwaiter().GetResult()),
                ("Volver", () => exit = true),
            };

            MenuLogic.RunMenu(items);
        }
    }

    private async Task CreateStaffAvailabilityAsync()
    {
        SpectreUi.ModuleHeader("Registrar disponibilidad", null);
        try
        {
            Console.Write("ID del empleado: ");
            var staffIdValue = int.Parse(Console.ReadLine()!);
            var staffId = StaffId.Create(staffIdValue);

            Console.Write("ID del estado de disponibilidad: ");
            var availabilityStatusIdValue = int.Parse(Console.ReadLine()!);
            var availabilityStatusId = AvailabilityStatusId.Create(availabilityStatusIdValue);

            Console.Write("Fecha/hora inicio: ");
            var startDateValue = DateTime.Parse(Console.ReadLine()!);
            var startDate = StartDate.Create(startDateValue);

            Console.Write("Fecha/hora fin: ");
            var endDateValue = DateTime.Parse(Console.ReadLine()!);
            var endDate = EndDate.Create(endDateValue);

            Console.Write("Observación (opcional): ");
            var observationInput = Console.ReadLine();
            Observation? observation = null;
            if (!string.IsNullOrWhiteSpace(observationInput))
            {
                observation = Observation.Create(observationInput);
            }

            await _createUseCase.ExecuteAsync(staffId, availabilityStatusId, startDate, endDate, observation);
            Console.WriteLine("Disponibilidad registrada.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        SpectreUi.Pause();
    }

    private async Task ViewAllStaffAvailabilitiesAsync()
    {
        SpectreUi.ModuleHeader("Disponibilidades", null);
        var staffAvailabilities = await _getAllUseCase.ExecuteAsync();
        foreach (var sa in staffAvailabilities)
        {
            Console.WriteLine($"ID: {sa.Id.Value}, Staff: {sa.StaffId.Value}, Estado: {sa.AvailabilityStatusId.Value}, Inicio: {sa.StartDate.Value}, Fin: {sa.EndDate.Value}");
        }

        SpectreUi.Pause();
    }

    private async Task ViewStaffAvailabilityByIdAsync()
    {
        SpectreUi.ModuleHeader("Consultar disponibilidad", null);
        Console.Write("ID: ");
        var id = int.Parse(Console.ReadLine()!);
        var staffAvailabilityId = StaffAvailabilityId.Create(id);

        var staffAvailability = await _getByIdUseCase.ExecuteAsync(staffAvailabilityId);
        if (staffAvailability != null)
        {
            Console.WriteLine($"ID: {staffAvailability.Id.Value}, Staff: {staffAvailability.StaffId.Value}, Estado: {staffAvailability.AvailabilityStatusId.Value}, Inicio: {staffAvailability.StartDate.Value}, Fin: {staffAvailability.EndDate.Value}");
        }
        else
        {
            Console.WriteLine("No encontrado.");
        }

        SpectreUi.Pause();
    }

    private async Task UpdateStaffAvailabilityAsync()
    {
        SpectreUi.ModuleHeader("Actualizar disponibilidad", null);
        try
        {
            Console.Write("ID: ");
            var id = int.Parse(Console.ReadLine()!);
            var staffAvailabilityId = StaffAvailabilityId.Create(id);

            Console.Write("Nuevo ID de empleado: ");
            var staffIdValue = int.Parse(Console.ReadLine()!);
            var staffId = StaffId.Create(staffIdValue);

            Console.Write("Nuevo ID de estado: ");
            var availabilityStatusIdValue = int.Parse(Console.ReadLine()!);
            var availabilityStatusId = AvailabilityStatusId.Create(availabilityStatusIdValue);

            Console.Write("Nueva fecha/hora inicio: ");
            var startDateValue = DateTime.Parse(Console.ReadLine()!);
            var startDate = StartDate.Create(startDateValue);

            Console.Write("Nueva fecha/hora fin: ");
            var endDateValue = DateTime.Parse(Console.ReadLine()!);
            var endDate = EndDate.Create(endDateValue);

            Console.Write("Observación (opcional): ");
            var observationInput = Console.ReadLine();
            Observation? observation = null;
            if (!string.IsNullOrWhiteSpace(observationInput))
            {
                observation = Observation.Create(observationInput);
            }

            await _updateUseCase.ExecuteAsync(staffAvailabilityId, staffId, availabilityStatusId, startDate, endDate, observation);
            Console.WriteLine("Actualizado.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        SpectreUi.Pause();
    }

    private async Task DeleteStaffAvailabilityAsync()
    {
        SpectreUi.ModuleHeader("Eliminar disponibilidad", null);
        Console.Write("ID: ");
        var id = int.Parse(Console.ReadLine()!);
        var staffAvailabilityId = StaffAvailabilityId.Create(id);

        await _deleteUseCase.ExecuteAsync(staffAvailabilityId);
        Console.WriteLine("Eliminado.");
        SpectreUi.Pause();
    }
}
