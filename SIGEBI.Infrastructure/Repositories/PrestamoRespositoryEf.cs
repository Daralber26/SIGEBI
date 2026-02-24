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

    public Task<bool> ExistePrestamoActivoAsync(Guid ejemplarId, CancellationToken ct)
    {
        return _db.Prestamos.AnyAsync(
            p => p.EjemplarId == ejemplarId && p.FechaDevolucion == null,
            ct
        );
    }

    public Task<Prestamo?> ObtenerPorIdAsync(Guid id, CancellationToken ct)
    {
        return _db.Prestamos.FirstOrDefaultAsync(p => p.Id == id, ct);
    }

    public async Task<int> ContarPrestamosActivosPorUsuarioAsync(Guid usuarioId, CancellationToken ct)
    {
        return await _db.Prestamos
            .Where(p => p.UsuarioId == usuarioId && p.FechaDevolucion == null)
            .CountAsync(ct);
    }
}
