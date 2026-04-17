using sistema_gestor_de_tiquetes_aereos.Src.Modules.AirportAirline.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.AirportAirline.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.AirportAirline.Domain.ValueObject;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.AirportAirline.Application.UseCases;

public class UpdateAirportAirlineUseCase
{
    private readonly IAirportAirlineRepository _repository;

    public UpdateAirportAirlineUseCase(IAirportAirlineRepository repository)
    {
        _repository = repository;
    }

    public async Task ExecuteAsync(int id, int airportId, int airlineId, string? terminal, DateOnly startDate, DateOnly? endDate, bool isActive)
    {
        var airportAirline = await _repository.GetByIdAsync(AirportAirlineId.Reconstitute(id));
        if (airportAirline == null) throw new Exception("AirportAirline not found");
        var term = Terminal.Create(terminal);
        airportAirline.Update(airportId, airlineId, term, startDate, endDate, isActive);
        await _repository.UpdateAsync(airportAirline);
    }
}