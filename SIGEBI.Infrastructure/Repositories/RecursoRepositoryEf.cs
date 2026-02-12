using Microsoft.EntityFrameworkCore;
using SIGEBI.Application.Abstractions;
using SIGEBI.Domain.Entities;
using SIGEBI.Infrastructure.Persistence;

namespace SIGEBI.Infrastructure.Repositories;

public class RecursoRepositoryEf : IRecursoRepository
{
    private readonly AppDbContext _db;

    public RecursoRepositoryEf(AppDbContext db)
    {
        _db = db;
    }

    public Task<List<Recurso>> ListarAsync(CancellationToken ct)
    {
        return _db.Recursos.AsNoTracking().ToListAsync(ct);
    }

    public Task<Recurso?> ObtenerPorIdAsync(Guid id, CancellationToken ct)
    {
        return _db.Recursos.FirstOrDefaultAsync(r => r.Id == id, ct);
    }

    public async Task AgregarAsync(Recurso recurso, CancellationToken ct)
    {
        await _db.Recursos.AddAsync(recurso, ct);
    }

    public Task EliminarAsync(Recurso recurso, CancellationToken ct)
    {
        _db.Recursos.Remove(recurso);
        return Task.CompletedTask;
    }

    public Task GuardarCambiosAsync(CancellationToken ct)
    {
        return _db.SaveChangesAsync(ct);
    }
}
