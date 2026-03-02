using Microsoft.EntityFrameworkCore;
using SIGEBI.Application.Abstractions;
using SIGEBI.Domain.Entities.Dbo;
using SIGEBI.Infrastructure.Persistence;

namespace SIGEBI.Infrastructure.Repositories;

public sealed class ReservaRepositoryEf : IReservaRepository
{
    private readonly AppDbContext _db;

    public ReservaRepositoryEf(AppDbContext db) => _db = db;

    public Task<bool> ExistsActivaAsync(Guid usuarioId, Guid ejemplarId, CancellationToken ct = default)
        => _db.Reservas.AnyAsync(r =>
            r.UsuarioId == usuarioId &&
            r.EjemplarId == ejemplarId &&
            r.FechaCancelacionUtc == null, ct);

    public Task<bool> ExistsActivaByEjemplarAsync(Guid ejemplarId, CancellationToken ct = default)
        => _db.Reservas.AnyAsync(r =>
            r.EjemplarId == ejemplarId &&
            r.FechaCancelacionUtc == null, ct);

    public Task AddAsync(Reserva reserva, CancellationToken ct = default)
        => _db.Reservas.AddAsync(reserva, ct).AsTask();

    public Task SaveChangesAsync(CancellationToken ct = default)
        => _db.SaveChangesAsync(ct);

    public Task<bool> ExistsActivaByUsuarioYRecursoAsync(Guid usuarioId, Guid recursoId, CancellationToken ct = default)
    {
        return _db.Reservas
            .Join(_db.Ejemplares,
                  r => r.EjemplarId,
                  e => e.Id,
                  (r, e) => new { r, e })
            .AnyAsync(x =>
                x.r.UsuarioId == usuarioId &&
                x.e.RecursoId == recursoId &&
                x.r.FechaCancelacionUtc == null, ct);
    }
}