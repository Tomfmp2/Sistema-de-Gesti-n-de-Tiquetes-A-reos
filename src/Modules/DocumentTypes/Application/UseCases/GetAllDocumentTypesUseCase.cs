using sistema_gestor_de_tiquetes_aereos.Src.Modules.DocumentTypes.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.DocumentTypes.Domain.Repositories;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.DocumentTypes.Application.UseCases;

public interface IGetAllDocumentTypesUseCase
{
    Task<IReadOnlyList<DocumentType>> ExecuteAsync(CancellationToken cancellationToken = default);
}

public sealed class GetAllDocumentTypesUseCase : IGetAllDocumentTypesUseCase
{
    private readonly IDocumentTypeRepository _repository;

    public GetAllDocumentTypesUseCase(IDocumentTypeRepository repository)
    {
        _repository = repository;
    }

    public Task<IReadOnlyList<DocumentType>> ExecuteAsync(
        CancellationToken cancellationToken = default
    ) => _repository.GetAllAsync(cancellationToken);
}
