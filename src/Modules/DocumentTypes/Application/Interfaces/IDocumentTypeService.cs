using sistema_gestor_de_tiquetes_aereos.Src.Modules.DocumentTypes.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.DocumentTypes.Domain.Aggregate;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.DocumentTypes.Application.Interfaces;

public interface IDocumentTypeService
{
    Task<DocumentType> CreateAsync(
        CreateDocumentTypeRequest request,
        CancellationToken cancellationToken = default
    );

    Task<DocumentType?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<DocumentType>> GetAllAsync(CancellationToken cancellationToken = default);

    Task UpdateAsync(
        UpdateDocumentTypeRequest request,
        CancellationToken cancellationToken = default
    );

    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
}
