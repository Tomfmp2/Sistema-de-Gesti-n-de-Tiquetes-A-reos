using Aggregate = sistema_gestor_de_tiquetes_aereos.Src.Modules.CabinTypes.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.CabinTypes.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.CabinTypes.Domain.ValueObject;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.CabinTypes.Application.UseCases;

public class UpdateCabinTypeUseCase
{
    private readonly ICabinTypeRepository _repository;

    public UpdateCabinTypeUseCase(ICabinTypeRepository repository)
    {
        _repository = repository;
    }

    public async Task ExecuteAsync(CabinTypeId id, string name)
    {
        var existing = await _repository.GetByIdAsync(id);
        if (existing == null)
        {
            throw new InvalidOperationException("CabinType not found.");
        }

        var cabinTypeName = CabinTypeName.Create(name);
        var updated = Aggregate.CabinType.Reconstitute(id, cabinTypeName);
        await _repository.UpdateAsync(updated);
    }
}