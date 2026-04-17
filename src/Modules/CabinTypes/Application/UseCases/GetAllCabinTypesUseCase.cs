using Aggregate = sistema_gestor_de_tiquetes_aereos.Src.Modules.CabinTypes.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.CabinTypes.Domain.Repositories;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.CabinTypes.Application.UseCases;

public class GetAllCabinTypesUseCase
{
    private readonly ICabinTypeRepository _repository;

    public GetAllCabinTypesUseCase(ICabinTypeRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Aggregate.CabinType>> ExecuteAsync()
    {
        return await _repository.GetAllAsync();
    }
}