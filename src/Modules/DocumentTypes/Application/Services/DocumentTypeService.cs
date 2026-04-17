using sistema_gestor_de_tiquetes_aereos.Src.Modules.DocumentTypes.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.DocumentTypes.Application.Interfaces;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.DocumentTypes.Application.UseCases;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.DocumentTypes.Domain.Aggregate;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.DocumentTypes.Application.Services;

public sealed class DocumentTypeService : IDocumentTypeService
{
    private readonly ICreateDocumentTypeUseCase _create;
    private readonly IGetDocumentTypeByIdUseCase _getById;
    private readonly IGetAllDocumentTypesUseCase _getAll;
    private readonly IUpdateDocumentTypeUseCase _update;
    private readonly IDeleteDocumentTypeUseCase _delete;

    public DocumentTypeService(
        ICreateDocumentTypeUseCase create,
        IGetDocumentTypeByIdUseCase getById,
        IGetAllDocumentTypesUseCase getAll,
        IUpdateDocumentTypeUseCase update,
        IDeleteDocumentTypeUseCase delete
    )
    {
        _create = create;
        _getById = getById;
        _getAll = getAll;
        _update = update;
        _delete = delete;
    }

    public Task<DocumentType> CreateAsync(
        CreateDocumentTypeRequest request,
        CancellationToken cancellationToken = default
    ) => _create.ExecuteAsync(request, cancellationToken);

    public Task<DocumentType?> GetByIdAsync(int id, CancellationToken cancellationToken = default) =>
        _getById.ExecuteAsync(id, cancellationToken);

    public Task<IReadOnlyList<DocumentType>> GetAllAsync(CancellationToken cancellationToken = default) =>
        _getAll.ExecuteAsync(cancellationToken);

    public Task UpdateAsync(
        UpdateDocumentTypeRequest request,
        CancellationToken cancellationToken = default
    ) => _update.ExecuteAsync(request, cancellationToken);

    public Task DeleteAsync(int id, CancellationToken cancellationToken = default) =>
        _delete.ExecuteAsync(id, cancellationToken);
}
