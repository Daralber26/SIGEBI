using Microsoft.EntityFrameworkCore;
using SIGEBI.Application.Abstractions;
using SIGEBI.Domain.Entities;
using SIGEBI.Infrastructure.Persistence;

namespace SIGEBI.Infrastructure.Repositories;

public class EjemplarRepositoryEf : IEjemplarRepository
{
    private readonly AppDbContext _db;

    public EjemplarRepositoryEf(AppDbContext db)
    {
        _db = db;
    }

    public Task AgregarAsync(Ejemplar ejemplar, CancellationToken ct)
        => _db.Ejemplares.AddAsync(ejemplar, ct).AsTask();

    public Task GuardarCambiosAsync(CancellationToken ct)
        => _db.SaveChangesAsync(ct);

    public Task<bool> ExisteCodigoAsync(string codigo, CancellationToken ct)
        => _db.Ejemplares.AnyAsync(e => e.Codigo == codigo, ct);

    public Task<Ejemplar?> ObtenerPorIdAsync(Guid id, CancellationToken ct)
        => _db.Ejemplares.FirstOrDefaultAsync(e => e.Id == id, ct);
}
