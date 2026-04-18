namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.BaggageTypes.Application.UseCases;

using Domain.Repositories;

public class DeleteBaggageTypeUseCase
{
    private readonly IBaggageTypeRepository _repository;

    public DeleteBaggageTypeUseCase(IBaggageTypeRepository repository)
    {
        _repository = repository;
    }

    public async Task ExecuteAsync(int id)
    {
        var baggageType = await _repository.GetByIdAsync(id);

        if (baggageType == null)
            throw new KeyNotFoundException($"BaggageType with ID {id} not found.");

        await _repository.DeleteAsync(id);
    }
}
