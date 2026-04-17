using sistema_gestor_de_tiquetes_aereos.Src.Modules.StaffPositions.Application.Interfaces;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.StaffPositions.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.StaffPositions.Domain.ValueObject;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.StaffPositions.Application.UseCases;

public class GetStaffPositionByIdUseCase
{
    private readonly IStaffPositionRepository _repository;

    public GetStaffPositionByIdUseCase(IStaffPositionRepository repository)
    {
        _repository = repository;
    }

    public async Task<StaffPosition?> ExecuteAsync(StaffPositionId id)
    {
        return await _repository.GetByIdAsync(id);
    }
}