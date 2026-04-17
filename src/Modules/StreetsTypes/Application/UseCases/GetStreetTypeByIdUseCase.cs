using sistema_gestor_de_tiquetes_aereos.Src.Modules.StreetsTypes.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.StreetsTypes.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.StreetsTypes.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.StreetsTypes.Application.UseCases;

public interface IGetStreetTypeByIdUseCase
{
    Task<StreetType?> ExecuteAsync(int id, CancellationToken cancellationToken = default);
}

public sealed class GetStreetTypeByIdUseCase : IGetStreetTypeByIdUseCase
{
    private readonly IStreetTypeRepository _repository;

    public GetStreetTypeByIdUseCase(IStreetTypeRepository repository)
    {
        _repository = repository;
    }

    public Task<StreetType?> ExecuteAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id < 1)
        {
            return Task.FromResult<StreetType?>(null);
        }

        return _repository.GetByIdAsync(StreetTypeId.Create(id), cancellationToken);
    }
}
