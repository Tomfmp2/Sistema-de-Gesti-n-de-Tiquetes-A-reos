using sistema_gestor_de_tiquetes_aereos.Src.Modules.Fares.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Fares.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Fares.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Fares.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Fares.Application.UseCases;

public interface ICreateFareUseCase
{
    Task<Fare> ExecuteAsync(
        CreateFareRequest request,
        CancellationToken cancellationToken = default
    );
}

public sealed class CreateFareUseCase : ICreateFareUseCase
{
    private readonly IFareRepository _repository;

    public CreateFareUseCase(IFareRepository repository)
    {
        _repository = repository;
    }

    public Task<Fare> ExecuteAsync(
        CreateFareRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var x = Fare.Create(new FareId(0), FareRouteId.Create(request.RouteId), FareCabinTypeId.Create(request.CabinTypeId), FarePassengerTypeId.Create(request.PassengerTypeId), FareSeasonId.Create(request.SeasonId), FareBasePrice.Create(request.BasePrice), FareValidFrom.Create(request.ValidFrom), FareValidTo.Create(request.ValidTo));
        return _repository.AddAsync(x, cancellationToken);
    }
}
