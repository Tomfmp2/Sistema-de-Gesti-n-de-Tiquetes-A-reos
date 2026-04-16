using sistema_gestor_de_tiquetes_aereos.Src.Modules.StaffPositions.Application.Interfaces;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.StaffPositions.Domain.Aggregate;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.StaffPositions.Application.UseCases;

public class GetAllStaffPositionsUseCase
{
    private readonly IStaffPositionRepository _repository;

    public GetAllStaffPositionsUseCase(IStaffPositionRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<StaffPosition>> ExecuteAsync()
    {
        return await _repository.GetAllAsync();
    }
}