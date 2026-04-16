using sistema_gestor_de_tiquetes_aereos.Src.Modules.DocumentTypes.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.DocumentTypes.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.DocumentTypes.Domain.Repositories;

public interface IDocumentTypeRepository
{
    Task<DocumentType?> GetByIdAsync(
        DocumentTypeId id,
        CancellationToken cancellationToken = default
    );

    Task<IReadOnlyList<DocumentType>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<DocumentType> AddAsync(DocumentType entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(DocumentType entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(DocumentTypeId id, CancellationToken cancellationToken = default);
}
