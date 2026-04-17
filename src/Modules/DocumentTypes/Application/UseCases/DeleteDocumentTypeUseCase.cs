using sistema_gestor_de_tiquetes_aereos.Src.Modules.DocumentTypes.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.DocumentTypes.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.DocumentTypes.Application.UseCases;

public interface IDeleteDocumentTypeUseCase
{
    Task ExecuteAsync(int id, CancellationToken cancellationToken = default);
}

public sealed class DeleteDocumentTypeUseCase : IDeleteDocumentTypeUseCase
{
    private readonly IDocumentTypeRepository _repository;

    public DeleteDocumentTypeUseCase(IDocumentTypeRepository repository)
    {
        _repository = repository;
    }

    public Task ExecuteAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id < 1)
        {
            return Task.CompletedTask;
        }

        return _repository.DeleteAsync(DocumentTypeId.Create(id), cancellationToken);
    }
}
