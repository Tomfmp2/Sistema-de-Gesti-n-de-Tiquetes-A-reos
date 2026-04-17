using Aggregate = sistema_gestor_de_tiquetes_aereos.Src.Modules.CabinConfiguration.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.CabinConfiguration.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.CabinConfiguration.Domain.ValueObject;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.CabinConfiguration.Application.UseCases;

public class CreateCabinConfigurationUseCase
{
    private readonly ICabinConfigurationRepository _repository;

    public CreateCabinConfigurationUseCase(ICabinConfigurationRepository repository)
    {
        _repository = repository;
    }

    public async Task<Aggregate.CabinConfiguration> ExecuteAsync(
        int aircraftId,
        int cabinTypeId,
        int startRow,
        int endRow,
        int seatsPerRow,
        string seatLetters)
    {
        var cabinConfiguration = Aggregate.CabinConfiguration.Create(
            AircraftId.Create(aircraftId),
            CabinTypeId.Create(cabinTypeId),
            StartRow.Create(startRow),
            EndRow.Create(endRow),
            SeatsPerRow.Create(seatsPerRow),
            SeatLetters.Create(seatLetters));

        await _repository.AddAsync(cabinConfiguration);
        return cabinConfiguration;
    }
}