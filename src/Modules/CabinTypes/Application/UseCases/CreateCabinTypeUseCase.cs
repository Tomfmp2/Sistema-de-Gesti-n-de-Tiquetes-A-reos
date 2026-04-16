using Aggregate = sistema_gestor_de_tiquetes_aereos.Src.Modules.CabinTypes.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.CabinTypes.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.CabinTypes.Domain.ValueObject;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.CabinTypes.Application.UseCases;

public class CreateCabinTypeUseCase
{
    private readonly ICabinTypeRepository _repository;

    public CreateCabinTypeUseCase(ICabinTypeRepository repository)
    {
        _repository = repository;
    }

    public async Task<Aggregate.CabinType> ExecuteAsync(string name)
    {
        var cabinTypeName = CabinTypeName.Create(name);
        var cabinType = Aggregate.CabinType.Create(cabinTypeName);
        await _repository.AddAsync(cabinType);
        return cabinType;
    }
}