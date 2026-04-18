namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.BaggageTypes.Application.UseCases;

using Domain.Aggregate;
using Domain.Repositories;

public class GetBaggageTypeByIdUseCase
{
    private readonly IBaggageTypeRepository _repository;

    public GetBaggageTypeByIdUseCase(IBaggageTypeRepository repository)
    {
        _repository = repository;
    }

    public async Task<BaggageType> ExecuteAsync(int id)
    {
        var baggageType = await _repository.GetByIdAsync(id);

        if (baggageType == null)
            throw new KeyNotFoundException($"BaggageType with ID {id} not found.");

        return baggageType;
    }
}
