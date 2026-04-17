using sistema_gestor_de_tiquetes_aereos.Src.Modules.Staff.Application.Interfaces;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Staff.Domain.Aggregate;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Staff.Application.UseCases;

public class GetAllStaffUseCase
{
    private readonly IStaffRepository _repository;

    public GetAllStaffUseCase(IStaffRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<StaffRecord>> ExecuteAsync()
    {
        return await _repository.GetAllAsync();
    }
}