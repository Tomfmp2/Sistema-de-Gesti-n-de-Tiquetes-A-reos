using sistema_gestor_de_tiquetes_aereos.Src.Modules.Routes.Application.Interfaces;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Routes.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Routes.Domain.ValueObject;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Routes.Application.UseCases;

public class CreateRouteUseCase
{
    private readonly IRouteRepository _repository;

    public CreateRouteUseCase(IRouteRepository repository)
    {
        _repository = repository;
    }

    public async Task ExecuteAsync(OriginAirportId originAirportId, DestinationAirportId destinationAirportId, DistanceKm distanceKm, EstimatedDurationMin estimatedDurationMin, RouteMiles miles)
    {
        var route = Route.Create(originAirportId, destinationAirportId, distanceKm, estimatedDurationMin, miles);
        await _repository.AddAsync(route);
    }
}