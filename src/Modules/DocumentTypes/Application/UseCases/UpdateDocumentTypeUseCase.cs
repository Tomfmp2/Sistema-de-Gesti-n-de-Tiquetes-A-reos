using sistema_gestor_de_tiquetes_aereos.Src.Modules.DocumentTypes.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.DocumentTypes.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.DocumentTypes.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.DocumentTypes.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.DocumentTypes.Application.UseCases;

public interface IUpdateDocumentTypeUseCase
{
    Task ExecuteAsync(
        UpdateDocumentTypeRequest request,
        CancellationToken cancellationToken = default
    );
}

public sealed class UpdateDocumentTypeUseCase : IUpdateDocumentTypeUseCase
{
    private readonly IDocumentTypeRepository _repository;

    public UpdateDocumentTypeUseCase(IDocumentTypeRepository repository)
    {
        _repository = repository;
    }

    public Task ExecuteAsync(
        UpdateDocumentTypeRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var x = DocumentType.Create(
            DocumentTypeId.Create(request.Id),
            DocumentTypeName.Create(request.Name),
            DocumentTypeCode.Create(request.Code)
        );
        return _repository.UpdateAsync(x, cancellationToken);
    }
}
