using sistema_gestor_de_tiquetes_aereos.Src.Modules.Staff.Application.Interfaces;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Staff.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Staff.Domain.ValueObject;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Staff.Application.UseCases;

public class GetStaffByIdUseCase
{
    private readonly IStaffRepository _repository;

    public GetStaffByIdUseCase(IStaffRepository repository)
    {
        _repository = repository;
    }

    public async Task<StaffRecord?> ExecuteAsync(StaffId id)
    {
        return await _repository.GetByIdAsync(id);
    }
}