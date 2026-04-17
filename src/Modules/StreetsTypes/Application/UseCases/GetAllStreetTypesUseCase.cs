using sistema_gestor_de_tiquetes_aereos.Src.Modules.StreetsTypes.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.StreetsTypes.Domain.Repositories;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.StreetsTypes.Application.UseCases;

public interface IGetAllStreetTypesUseCase
{
    Task<IReadOnlyList<StreetType>> ExecuteAsync(CancellationToken cancellationToken = default);
}

public sealed class GetAllStreetTypesUseCase : IGetAllStreetTypesUseCase
{
    private readonly IStreetTypeRepository _repository;

    public GetAllStreetTypesUseCase(IStreetTypeRepository repository)
    {
        _repository = repository;
    }

    public Task<IReadOnlyList<StreetType>> ExecuteAsync(
        CancellationToken cancellationToken = default
    ) => _repository.GetAllAsync(cancellationToken);
}
