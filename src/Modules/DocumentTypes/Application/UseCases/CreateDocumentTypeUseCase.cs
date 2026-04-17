using sistema_gestor_de_tiquetes_aereos.Src.Modules.DocumentTypes.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.DocumentTypes.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.DocumentTypes.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.DocumentTypes.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.DocumentTypes.Application.UseCases;

public interface ICreateDocumentTypeUseCase
{
    Task<DocumentType> ExecuteAsync(
        CreateDocumentTypeRequest request,
        CancellationToken cancellationToken = default
    );
}

public sealed class CreateDocumentTypeUseCase : ICreateDocumentTypeUseCase
{
    private readonly IDocumentTypeRepository _repository;

    public CreateDocumentTypeUseCase(IDocumentTypeRepository repository)
    {
        _repository = repository;
    }

    public Task<DocumentType> ExecuteAsync(
        CreateDocumentTypeRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var x = DocumentType.CreateNew(
            DocumentTypeName.Create(request.Name),
            DocumentTypeCode.Create(request.Code)
        );
        return _repository.AddAsync(x, cancellationToken);
    }
}
