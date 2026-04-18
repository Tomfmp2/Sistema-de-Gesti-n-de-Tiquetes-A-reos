namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.BaggageTypes.Application.UseCases;

using Domain.Aggregate;
using Domain.Repositories;
using Domain.ValueObjects;

public class UpdateBaggageTypeUseCase
{
    private readonly IBaggageTypeRepository _repository;

    public UpdateBaggageTypeUseCase(IBaggageTypeRepository repository)
    {
        _repository = repository;
    }

    public async Task<BaggageType> ExecuteAsync(int id, string? name = null, decimal? maxWeightKg = null, decimal? basePrice = null)
    {
        var baggageType = await _repository.GetByIdAsync(id);

        if (baggageType == null)
            throw new KeyNotFoundException($"BaggageType with ID {id} not found.");

        if (!string.IsNullOrWhiteSpace(name))
        {
            var newName = BaggageTypeName.Create(name);
            baggageType.UpdateName(newName);
        }

        if (maxWeightKg.HasValue)
        {
            var newMaxWeight = MaxWeightKg.Create(maxWeightKg.Value);
            baggageType.UpdateMaxWeightKg(newMaxWeight);
        }

        if (basePrice.HasValue)
        {
            var newPrice = BasePrice.Create(basePrice.Value);
            baggageType.UpdateBasePrice(newPrice);
        }

        return await _repository.UpdateAsync(baggageType);
    }
}
