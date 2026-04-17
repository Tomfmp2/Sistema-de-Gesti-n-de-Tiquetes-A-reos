using Microsoft.EntityFrameworkCore;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PhoneCodes.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PhoneCodes.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PhoneCodes.Domain.ValueObjet;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PhoneCodes.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.PhoneCodes.Infrastructure.repository;

public sealed class PhoneCodeRepository : IPhoneCodeRepository
{
    private readonly AppDbContext _context;

    public PhoneCodeRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<PhoneCode?> GetByIdAsync(
        PhoneCodeId id,
        CancellationToken cancellationToken = default
    )
    {
        if (id.Value < 1)
        {
            return null;
        }

        var e = await _context.Set<PhoneCodeEntity>().AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id.Value, cancellationToken);
        return e is null ? null : ToDomain(e);
    }

    public async Task<IReadOnlyList<PhoneCode>> GetAllAsync(
        CancellationToken cancellationToken = default
    )
    {
        var list = await _context.Set<PhoneCodeEntity>().AsNoTracking().ToListAsync(cancellationToken);
        return list.Select(ToDomain).ToList();
    }

    public async Task<PhoneCode> AddAsync(PhoneCode entity, CancellationToken cancellationToken = default)
    {
        if (entity.Id.Value != 0)
        {
            throw new InvalidOperationException("Use PhoneCode.CreateNew para insertar.");
        }

        var e = new PhoneCodeEntity
        {
            CountryDialCode = entity.CountryDialCode.Value,
            CountryName = entity.CountryName.Value,
        };
        _context.Set<PhoneCodeEntity>().Add(e);
        await _context.SaveChangesAsync(cancellationToken);
        return ToDomain(e);
    }

    public async Task UpdateAsync(PhoneCode entity, CancellationToken cancellationToken = default)
    {
        if (entity.Id.Value < 1)
        {
            throw new InvalidOperationException("Id inválido.");
        }

        var e = await _context.Set<PhoneCodeEntity>().FirstOrDefaultAsync(
            x => x.Id == entity.Id.Value,
            cancellationToken
        );

        if (e is null)
        {
            throw new InvalidOperationException($"No existe código telefónico {entity.Id.Value}.");
        }

        e.CountryDialCode = entity.CountryDialCode.Value;
        e.CountryName = entity.CountryName.Value;
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(PhoneCodeId id, CancellationToken cancellationToken = default)
    {
        if (id.Value < 1)
        {
            return;
        }

        var e = await _context.Set<PhoneCodeEntity>().FirstOrDefaultAsync(
            x => x.Id == id.Value,
            cancellationToken
        );

        if (e is null)
        {
            return;
        }

        _context.Set<PhoneCodeEntity>().Remove(e);
        await _context.SaveChangesAsync(cancellationToken);
    }

    private static PhoneCode ToDomain(PhoneCodeEntity e)
    {
        return PhoneCode.Create(
            PhoneCodeId.Create(e.Id),
            PhoneDialCode.Create(e.CountryDialCode ?? string.Empty),
            PhoneCodeCountryLabel.Create(e.CountryName ?? string.Empty)
        );
    }
}
