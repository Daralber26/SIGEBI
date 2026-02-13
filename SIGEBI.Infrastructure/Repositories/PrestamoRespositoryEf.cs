using Microsoft.EntityFrameworkCore;
using SIGEBI.Application.Abstractions;
using SIGEBI.Domain.Entities;
using SIGEBI.Infrastructure.Persistence;

namespace SIGEBI.Infrastructure.Repositories;

public class PrestamoRepositoryEf : IPrestamoRepository
{
    private readonly AppDbContext _db;

    public PrestamoRepositoryEf(AppDbContext db)
    {
        _db = db;
    }

    public async Task AgregarAsync(Prestamo prestamo, CancellationToken ct)
    {
        await _db.Prestamos.AddAsync(prestamo, ct);
    }

    public Task GuardarCambiosAsync(CancellationToken ct)
    {
        return _db.SaveChangesAsync(ct);
    }

    public Task<bool> ExistePrestamoActivoAsync(Guid recursoId, CancellationToken ct)
    {
        return _db.Prestamos.AnyAsync(
            p => p.RecursoId == recursoId && p.FechaDevolucion == null,
            ct
        );
    }
}