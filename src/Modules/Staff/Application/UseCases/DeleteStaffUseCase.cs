using sistema_gestor_de_tiquetes_aereos.Src.Modules.Staff.Application.Interfaces;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Staff.Domain.ValueObject;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Staff.Application.UseCases;

public class DeleteStaffUseCase
{
    private readonly IStaffRepository _repository;

    public DeleteStaffUseCase(IStaffRepository repository)
    {
        _repository = repository;
    }

    public async Task ExecuteAsync(StaffId id)
    {
        await _repository.DeleteAsync(id);
    }
}