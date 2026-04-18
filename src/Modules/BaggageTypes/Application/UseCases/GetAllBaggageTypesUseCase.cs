namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.BaggageTypes.Application.UseCases;

using Domain.Aggregate;
using Domain.Repositories;

public class GetAllBaggageTypesUseCase
{
    private readonly IBaggageTypeRepository _repository;

    public GetAllBaggageTypesUseCase(IBaggageTypeRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<BaggageType>> ExecuteAsync()
    {
        return await _repository.GetAllAsync();
    }
}
