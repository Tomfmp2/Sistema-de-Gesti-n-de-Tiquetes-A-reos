using sistema_gestor_de_tiquetes_aereos.Src.Modules.Baggage.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Baggage.Domain.Repositories;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Baggage.Application.UseCases;

public sealed class GetAllBaggagesUseCase
{
    private readonly IBaggageRepository _repository;

    public GetAllBaggagesUseCase(IBaggageRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<BaggageItem>> ExecuteAsync()
    {
        return await _repository.GetAllAsync();
    }
}
