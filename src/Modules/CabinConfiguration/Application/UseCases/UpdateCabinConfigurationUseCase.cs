using Aggregate = sistema_gestor_de_tiquetes_aereos.Src.Modules.CabinConfiguration.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.CabinConfiguration.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.CabinConfiguration.Domain.ValueObject;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.CabinConfiguration.Application.UseCases;

public class UpdateCabinConfigurationUseCase
{
    private readonly ICabinConfigurationRepository _repository;

    public UpdateCabinConfigurationUseCase(ICabinConfigurationRepository repository)
    {
        _repository = repository;
    }

    public async Task ExecuteAsync(
        CabinConfigurationId id,
        int aircraftId,
        int cabinTypeId,
        int startRow,
        int endRow,
        int seatsPerRow,
        string seatLetters)
    {
        var existing = await _repository.GetByIdAsync(id);
        if (existing == null)
        {
            throw new InvalidOperationException("Cabin configuration not found.");
        }

        var updated = Aggregate.CabinConfiguration.Reconstitute(
            id,
            AircraftId.Create(aircraftId),
            CabinTypeId.Create(cabinTypeId),
            StartRow.Create(startRow),
            EndRow.Create(endRow),
            SeatsPerRow.Create(seatsPerRow),
            SeatLetters.Create(seatLetters));

        await _repository.UpdateAsync(updated);
    }
}