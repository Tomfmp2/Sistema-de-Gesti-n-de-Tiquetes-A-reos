using sistema_gestor_de_tiquetes_aereos.Src.Modules.StaffPositions.Application.Interfaces;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.StaffPositions.Domain.ValueObject;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.StaffPositions.Application.UseCases;

public class DeleteStaffPositionUseCase
{
    private readonly IStaffPositionRepository _repository;

    public DeleteStaffPositionUseCase(IStaffPositionRepository repository)
    {
        _repository = repository;
    }

    public async Task ExecuteAsync(StaffPositionId id)
    {
        await _repository.DeleteAsync(id);
    }
}