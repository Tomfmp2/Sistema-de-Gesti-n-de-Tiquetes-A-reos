using sistema_gestor_de_tiquetes_aereos.Src.Modules.StreetsTypes.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.StreetsTypes.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.StreetsTypes.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.StreetsTypes.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.StreetsTypes.Application.UseCases;

public interface ICreateStreetTypeUseCase
{
    Task<StreetType> ExecuteAsync(
        CreateStreetTypeRequest request,
        CancellationToken cancellationToken = default
    );
}

public sealed class CreateStreetTypeUseCase : ICreateStreetTypeUseCase
{
    private readonly IStreetTypeRepository _repository;

    public CreateStreetTypeUseCase(IStreetTypeRepository repository)
    {
        _repository = repository;
    }

    public Task<StreetType> ExecuteAsync(
        CreateStreetTypeRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var x = StreetType.CreateNew(StreetTypeName.Create(request.Name));
        return _repository.AddAsync(x, cancellationToken);
    }
}
