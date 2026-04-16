using sistema_gestor_de_tiquetes_aereos.Src.Modules.StaffPositions.Application.Interfaces;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.StaffPositions.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.StaffPositions.Domain.ValueObject;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.StaffPositions.Application.UseCases;

public class UpdateStaffPositionUseCase
{
    private readonly IStaffPositionRepository _repository;

    public UpdateStaffPositionUseCase(IStaffPositionRepository repository)
    {
        _repository = repository;
    }

    public async Task ExecuteAsync(StaffPositionId id, StaffPositionName name)
    {
        var staffPosition = await _repository.GetByIdAsync(id);
        if (staffPosition == null) throw new KeyNotFoundException("StaffPosition not found");
        staffPosition.UpdateName(name);
        await _repository.UpdateAsync(staffPosition);
    }
}