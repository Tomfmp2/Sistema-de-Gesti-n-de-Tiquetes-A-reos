using sistema_gestor_de_tiquetes_aereos.Src.Modules.StaffPositions.Application.Interfaces;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.StaffPositions.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.StaffPositions.Domain.ValueObject;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.StaffPositions.Application.UseCases;

public class CreateStaffPositionUseCase
{
    private readonly IStaffPositionRepository _repository;

    public CreateStaffPositionUseCase(IStaffPositionRepository repository)
    {
        _repository = repository;
    }

    public async Task ExecuteAsync(StaffPositionName name)
    {
        // Generate new ID (in real app, use a proper ID generator)
        var id = StaffPositionId.Create(new Random().Next(1, int.MaxValue));
        var staffPosition = StaffPosition.Create(id, name);
        await _repository.AddAsync(staffPosition);
    }
}