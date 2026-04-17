using Aggregate = sistema_gestor_de_tiquetes_aereos.Src.Modules.CabinTypes.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.CabinTypes.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.CabinTypes.Domain.ValueObject;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.CabinTypes.Application.UseCases;

public class GetCabinTypeByIdUseCase
{
    private readonly ICabinTypeRepository _repository;

    public GetCabinTypeByIdUseCase(ICabinTypeRepository repository)
    {
        _repository = repository;
    }

    public async Task<Aggregate.CabinType?> ExecuteAsync(CabinTypeId id)
    {
        return await _repository.GetByIdAsync(id);
    }
}