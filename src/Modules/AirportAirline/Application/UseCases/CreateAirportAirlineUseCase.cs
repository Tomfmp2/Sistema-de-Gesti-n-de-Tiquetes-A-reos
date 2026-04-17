using sistema_gestor_de_tiquetes_aereos.Src.Modules.AirportAirline.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.AirportAirline.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.AirportAirline.Domain.ValueObject;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.AirportAirline.Application.UseCases;

public class CreateAirportAirlineUseCase
{
    private readonly IAirportAirlineRepository _repository;

    public CreateAirportAirlineUseCase(IAirportAirlineRepository repository)
    {
        _repository = repository;
    }

    public async Task<AirportAirlineRecord> ExecuteAsync(int airportId, int airlineId, string? terminal, DateOnly startDate, DateOnly? endDate)
    {
        var term = Terminal.Create(terminal);
        var airportAirline = AirportAirlineRecord.Create(airportId, airlineId, term, startDate, endDate);
        await _repository.AddAsync(airportAirline);
        return airportAirline;
    }
}