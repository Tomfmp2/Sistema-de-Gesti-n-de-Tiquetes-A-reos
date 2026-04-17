using sistema_gestor_de_tiquetes_aereos.Src.Modules.RouteLayovers.Application.Interfaces;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.RouteLayovers.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.RouteLayovers.Domain.ValueObject;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.RouteLayovers.Application.UseCases;

public class CreateRouteLayoverUseCase
{
    private readonly IRouteLayoverRepository _repository;

    public CreateRouteLayoverUseCase(IRouteLayoverRepository repository)
    {
        _repository = repository;
    }

    public async Task ExecuteAsync(RouteId routeId, LayoverAirportId layoverAirportId, SequenceOrder sequenceOrder, LayoverDurationMin layoverDurationMin)
    {
        var routeLayover = RouteLayover.Create(routeId, layoverAirportId, sequenceOrder, layoverDurationMin);
        await _repository.AddAsync(routeLayover);
    }
}