using Microsoft.EntityFrameworkCore;
using SIGEBI.Application.Abstractions;
using SIGEBI.Domain.Entities;
using SIGEBI.Domain.Enums;
using SIGEBI.Infrastructure.Persistence;

namespace SIGEBI.Infrastructure.Repositories;

public class PenalizacionRepositoryEf : IPenalizacionRepository
{
    private readonly AppDbContext _db;

    public PenalizacionRepositoryEf(AppDbContext db)
    {
        _db = db;
    }

    public async Task<List<Penalizacion>> GetActivasPorUsuario(Guid usuarioId, CancellationToken ct)
    {
        return await _db.Set<Penalizacion>()
            .Where(p => p.UsuarioId == usuarioId && p.Estado == PenalizacionEstado.Activa)
            .ToListAsync(ct);
    }
}