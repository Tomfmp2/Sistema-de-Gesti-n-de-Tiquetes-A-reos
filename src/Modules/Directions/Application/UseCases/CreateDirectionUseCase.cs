using sistema_gestor_de_tiquetes_aereos.Src.Modules.Directions.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Directions.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Directions.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Directions.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Directions.Application.UseCases;

public interface ICreateDirectionUseCase
{
    Task<Direction> ExecuteAsync(
        CreateDirectionRequest request,
        CancellationToken cancellationToken = default
    );
}

public sealed class CreateDirectionUseCase : ICreateDirectionUseCase
{
    private readonly IDirectionRepository _repository;

    public CreateDirectionUseCase(IDirectionRepository repository)
    {
        _repository = repository;
    }

    public Task<Direction> ExecuteAsync(
        CreateDirectionRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var x = Direction.CreateNew(
            DirectionCityId.Create(request.CityId),
            DirectionStreetTypeId.Create(request.StreetTypeId),
            DirectionNameStreet.Create(request.StreetName),
            DirectionNumber.Create(request.Number),
            request.Complement,
            request.PostalCode
        );
        return _repository.AddAsync(x, cancellationToken);
    }
}
