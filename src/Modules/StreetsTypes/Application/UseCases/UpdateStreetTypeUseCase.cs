using sistema_gestor_de_tiquetes_aereos.Src.Modules.StreetsTypes.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.StreetsTypes.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.StreetsTypes.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.StreetsTypes.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.StreetsTypes.Application.UseCases;

public interface IUpdateStreetTypeUseCase
{
    Task ExecuteAsync(
        UpdateStreetTypeRequest request,
        CancellationToken cancellationToken = default
    );
}

public sealed class UpdateStreetTypeUseCase : IUpdateStreetTypeUseCase
{
    private readonly IStreetTypeRepository _repository;

    public UpdateStreetTypeUseCase(IStreetTypeRepository repository)
    {
        _repository = repository;
    }

    public Task ExecuteAsync(
        UpdateStreetTypeRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var x = StreetType.Create(
            StreetTypeId.Create(request.Id),
            StreetTypeName.Create(request.Name)
        );
        return _repository.UpdateAsync(x, cancellationToken);
    }
}
