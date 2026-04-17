using sistema_gestor_de_tiquetes_aereos.Src.Modules.Directions.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Directions.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Directions.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Directions.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Directions.Application.UseCases;

public interface IUpdateDirectionUseCase
{
    Task ExecuteAsync(
        UpdateDirectionRequest request,
        CancellationToken cancellationToken = default
    );
}

public sealed class UpdateDirectionUseCase : IUpdateDirectionUseCase
{
    private readonly IDirectionRepository _repository;

    public UpdateDirectionUseCase(IDirectionRepository repository)
    {
        _repository = repository;
    }

    public Task ExecuteAsync(
        UpdateDirectionRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var x = Direction.Create(
            DirectionId.Create(request.Id),
            DirectionCityId.Create(request.CityId),
            DirectionStreetTypeId.Create(request.StreetTypeId),
            DirectionNameStreet.Create(request.StreetName),
            DirectionNumber.Create(request.Number),
            request.Complement,
            request.PostalCode
        );
        return _repository.UpdateAsync(x, cancellationToken);
    }
}
