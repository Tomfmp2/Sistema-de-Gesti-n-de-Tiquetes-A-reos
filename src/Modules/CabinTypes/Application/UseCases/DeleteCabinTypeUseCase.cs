using sistema_gestor_de_tiquetes_aereos.Src.Modules.CabinTypes.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.CabinTypes.Domain.ValueObject;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.CabinTypes.Application.UseCases;

public class DeleteCabinTypeUseCase
{
    private readonly ICabinTypeRepository _repository;

    public DeleteCabinTypeUseCase(ICabinTypeRepository repository)
    {
        _repository = repository;
    }

    public async Task ExecuteAsync(CabinTypeId id)
    {
        var existing = await _repository.GetByIdAsync(id);
        if (existing == null)
        {
            throw new InvalidOperationException("CabinType not found.");
        }

        await _repository.DeleteAsync(id);
    }
}