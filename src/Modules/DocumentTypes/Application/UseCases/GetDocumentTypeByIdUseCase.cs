using sistema_gestor_de_tiquetes_aereos.Src.Modules.DocumentTypes.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.DocumentTypes.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.DocumentTypes.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.DocumentTypes.Application.UseCases;

public interface IGetDocumentTypeByIdUseCase
{
    Task<DocumentType?> ExecuteAsync(int id, CancellationToken cancellationToken = default);
}

public sealed class GetDocumentTypeByIdUseCase : IGetDocumentTypeByIdUseCase
{
    private readonly IDocumentTypeRepository _repository;

    public GetDocumentTypeByIdUseCase(IDocumentTypeRepository repository)
    {
        _repository = repository;
    }

    public Task<DocumentType?> ExecuteAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id < 1)
        {
            return Task.FromResult<DocumentType?>(null);
        }

        return _repository.GetByIdAsync(DocumentTypeId.Create(id), cancellationToken);
    }
}
