using sistema_gestor_de_tiquetes_aereos.Src.Modules.RouteLayovers.Application.Interfaces;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.RouteLayovers.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.RouteLayovers.Domain.ValueObject;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.RouteLayovers.Application.UseCases;

public class UpdateRouteLayoverUseCase
{
    private readonly IRouteLayoverRepository _repository;

    public UpdateRouteLayoverUseCase(IRouteLayoverRepository repository)
    {
        _repository = repository;
    }

    public async Task ExecuteAsync(RouteLayoverId id, RouteId routeId, LayoverAirportId layoverAirportId, SequenceOrder sequenceOrder, LayoverDurationMin layoverDurationMin)
    {
        var routeLayover = RouteLayover.Reconstitute(id, routeId, layoverAirportId, sequenceOrder, layoverDurationMin);
        await _repository.UpdateAsync(routeLayover);
    }
}