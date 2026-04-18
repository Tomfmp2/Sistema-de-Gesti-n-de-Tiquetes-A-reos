using sistema_gestor_de_tiquetes_aereos.Src.Modules.Baggage.Domain.Repositories;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Baggage.Application.UseCases;

public sealed class DeleteBaggageUseCase
{
    private readonly IBaggageRepository _repository;

    public DeleteBaggageUseCase(IBaggageRepository repository)
    {
        _repository = repository;
    }

    public async Task ExecuteAsync(int id)
    {
        var baggage = await _repository.GetByIdAsync(id);
        if (baggage is null)
            throw new KeyNotFoundException($"Baggage with id {id} not found");

        await _repository.DeleteAsync(id);
    }
}
