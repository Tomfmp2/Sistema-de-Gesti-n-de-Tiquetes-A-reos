using sistema_gestor_de_tiquetes_aereos.Src.Modules.Baggage.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Baggage.Domain.ValueObjects;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Baggage.Application.UseCases;

public sealed class UpdateBaggageUseCase
{
    private readonly IBaggageRepository _repository;

    public UpdateBaggageUseCase(IBaggageRepository repository)
    {
        _repository = repository;
    }

    public async Task ExecuteAsync(
        int id,
        int? baggageTypeId = null,
        decimal? weightKg = null,
        decimal? chargedPrice = null)
    {
        var baggage = await _repository.GetByIdAsync(id);
        if (baggage is null)
            throw new KeyNotFoundException($"Baggage with id {id} not found");

        if (baggageTypeId.HasValue)
            baggage.UpdateBaggageTypeId(baggageTypeId.Value);

        if (weightKg.HasValue)
            baggage.UpdateWeightKg(WeightKg.Create(weightKg.Value));

        if (chargedPrice.HasValue)
            baggage.UpdateChargedPrice(ChargedPrice.Create(chargedPrice.Value));

        await _repository.UpdateAsync(baggage);
    }
}
