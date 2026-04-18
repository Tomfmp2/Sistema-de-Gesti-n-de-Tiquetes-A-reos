using sistema_gestor_de_tiquetes_aereos.Src.Modules.Baggage.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Baggage.Domain.Repositories;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Baggage.Application.UseCases;

public sealed class GetBaggageByIdUseCase
{
    private readonly IBaggageRepository _repository;

    public GetBaggageByIdUseCase(IBaggageRepository repository)
    {
        _repository = repository;
    }

    public async Task<BaggageItem> ExecuteAsync(int id)
    {
        var baggage = await _repository.GetByIdAsync(id);
        if (baggage is null)
            throw new KeyNotFoundException($"Baggage with id {id} not found");

        return baggage;
    }
}
