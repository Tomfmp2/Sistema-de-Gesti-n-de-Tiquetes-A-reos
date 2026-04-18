using sistema_gestor_de_tiquetes_aereos.Src.Modules.Baggage.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Baggage.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Baggage.Domain.ValueObjects;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Baggage.Application.UseCases;

public sealed class CreateBaggageUseCase
{
    private readonly IBaggageRepository _repository;

    public CreateBaggageUseCase(IBaggageRepository repository)
    {
        _repository = repository;
    }

    public async Task ExecuteAsync(
        int checkinId,
        int baggageTypeId,
        decimal weightKg,
        decimal chargedPrice)
    {
        var weight = WeightKg.Create(weightKg);
        var price = ChargedPrice.Create(chargedPrice);

        var baggage = BaggageItem.Create(checkinId, baggageTypeId, weight, price);
        await _repository.CreateAsync(baggage);
    }
}
