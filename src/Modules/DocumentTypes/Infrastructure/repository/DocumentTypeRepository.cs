using Microsoft.EntityFrameworkCore;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.DocumentTypes.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.DocumentTypes.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.DocumentTypes.Domain.ValueObjet;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.DocumentTypes.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.DocumentTypes.Infrastructure.repository;

public sealed class DocumentTypeRepository : IDocumentTypeRepository
{
    private readonly AppDbContext _context;

    public DocumentTypeRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<DocumentType?> GetByIdAsync(
        DocumentTypeId id,
        CancellationToken cancellationToken = default
    )
    {
        if (id.Value < 1)
        {
            return null;
        }

        var e = await _context.Set<DocumentTypeEntity>().AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id.Value, cancellationToken);
        return e is null ? null : ToDomain(e);
    }

    public async Task<IReadOnlyList<DocumentType>> GetAllAsync(
        CancellationToken cancellationToken = default
    )
    {
        var list = await _context.Set<DocumentTypeEntity>().AsNoTracking().ToListAsync(cancellationToken);
        return list.Select(ToDomain).ToList();
    }

    public async Task<DocumentType> AddAsync(
        DocumentType entity,
        CancellationToken cancellationToken = default
    )
    {
        if (entity.Id.Value != 0)
        {
            throw new InvalidOperationException("Use DocumentType.CreateNew para insertar.");
        }

        var e = new DocumentTypeEntity
        {
            Name = entity.Name.Value,
            Code = entity.Code.Value,
        };
        _context.Set<DocumentTypeEntity>().Add(e);
        await _context.SaveChangesAsync(cancellationToken);
        return ToDomain(e);
    }

    public async Task UpdateAsync(DocumentType entity, CancellationToken cancellationToken = default)
    {
        if (entity.Id.Value < 1)
        {
            throw new InvalidOperationException("Id inválido.");
        }

        var e = await _context.Set<DocumentTypeEntity>().FirstOrDefaultAsync(
            x => x.Id == entity.Id.Value,
            cancellationToken
        );

        if (e is null)
        {
            throw new InvalidOperationException($"No existe tipo de documento {entity.Id.Value}.");
        }

        e.Name = entity.Name.Value;
        e.Code = entity.Code.Value;
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(DocumentTypeId id, CancellationToken cancellationToken = default)
    {
        if (id.Value < 1)
        {
            return;
        }

        var e = await _context.Set<DocumentTypeEntity>().FirstOrDefaultAsync(
            x => x.Id == id.Value,
            cancellationToken
        );

        if (e is null)
        {
            return;
        }

        _context.Set<DocumentTypeEntity>().Remove(e);
        await _context.SaveChangesAsync(cancellationToken);
    }

    private static DocumentType ToDomain(DocumentTypeEntity e)
    {
        return DocumentType.Create(
            DocumentTypeId.Create(e.Id),
            DocumentTypeName.Create(e.Name ?? string.Empty),
            DocumentTypeCode.Create(e.Code ?? string.Empty)
        );
    }
}
