using Microsoft.EntityFrameworkCore;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Users.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Users.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Users.Domain.ValueObjet;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Users.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Users.Infrastructure.repository;

public sealed class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetByIdAsync(UserId id, CancellationToken cancellationToken = default)
    {
        if (id.Value < 1)
        {
            return null;
        }

        var e = await _context.Set<UserEntity>().AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id.Value, cancellationToken);
        return e is null ? null : ToDomain(e);
    }

    public async Task<IReadOnlyList<User>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var list = await _context.Set<UserEntity>()
            .AsNoTracking()
            .Where(x => x.IsActive)
            .ToListAsync(cancellationToken);
        return list.Select(ToDomain).ToList();
    }

    public async Task<User> AddAsync(User entity, CancellationToken cancellationToken = default)
    {
        if (entity.Id.Value != 0)
        {
            throw new InvalidOperationException("Use User.CreateNew para insertar.");
        }

        if (entity.PersonId is { } pid)
        {
            var personTaken = await _context.Set<UserEntity>()
                .AsNoTracking()
                .AnyAsync(u => u.PersonId == pid, cancellationToken);
            if (personTaken)
            {
                throw new InvalidOperationException("Ya existe un usuario asociado a esta persona (person_id único).");
            }
        }

        var now = DateTime.UtcNow;
        var e = new UserEntity
        {
            Username = entity.Username.Value,
            PasswordHash = entity.PasswordHash.Value,
            PersonId = entity.PersonId,
            SystemRoleId = entity.SystemRoleId.Value,
            IsActive = entity.IsActive,
            LastAccessAt = entity.LastAccessAt,
            CreatedAt = now,
            UpdatedAt = now,
        };
        _context.Set<UserEntity>().Add(e);
        await _context.SaveChangesAsync(cancellationToken);
        return ToDomain(e);
    }

    public async Task UpdateAsync(User entity, CancellationToken cancellationToken = default)
    {
        if (entity.Id.Value < 1)
        {
            throw new InvalidOperationException("Id inválido.");
        }

        var e = await _context.Set<UserEntity>().FirstOrDefaultAsync(
            x => x.Id == entity.Id.Value,
            cancellationToken
        );

        if (e is null)
        {
            throw new InvalidOperationException($"No existe usuario {entity.Id.Value}.");
        }

        e.Username = entity.Username.Value;
        e.PasswordHash = entity.PasswordHash.Value;
        e.PersonId = entity.PersonId;
        e.SystemRoleId = entity.SystemRoleId.Value;
        e.IsActive = entity.IsActive;
        e.LastAccessAt = entity.LastAccessAt;
        e.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(UserId id, CancellationToken cancellationToken = default)
    {
        if (id.Value < 1)
        {
            return;
        }

        var e = await _context.Set<UserEntity>().FirstOrDefaultAsync(
            x => x.Id == id.Value,
            cancellationToken
        );

        if (e is null)
        {
            return;
        }

        e.IsActive = false;
        e.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync(cancellationToken);
    }

    private static User ToDomain(UserEntity e)
    {
        return User.Create(
            UserId.Create(e.Id),
            UserUsername.Create(e.Username ?? string.Empty),
            UserPasswordHash.Create(e.PasswordHash ?? string.Empty),
            e.PersonId,
            UserSystemRoleId.Create(e.SystemRoleId),
            e.IsActive,
            e.LastAccessAt,
            e.CreatedAt,
            e.UpdatedAt
        );
    }
}
