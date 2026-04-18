namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.BaggageTypes.Application.UseCases;

using Domain.Aggregate;
using Domain.Repositories;
using Domain.ValueObjects;

public class CreateBaggageTypeUseCase
{
    private readonly IBaggageTypeRepository _repository;

    public CreateBaggageTypeUseCase(IBaggageTypeRepository repository)
    {
        _repository = repository;
    }

    public async Task<BaggageType> ExecuteAsync(string name, decimal maxWeightKg, decimal basePrice)
    {
        var baggageTypeName = BaggageTypeName.Create(name);
        var maxWeight = MaxWeightKg.Create(maxWeightKg);
        var price = BasePrice.Create(basePrice);

        var baggageType = BaggageType.Create(baggageTypeName, maxWeight, price);

        return await _repository.CreateAsync(baggageType);
    }
}
