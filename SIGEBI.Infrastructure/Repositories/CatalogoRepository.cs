using Microsoft.EntityFrameworkCore;
using SIGEBI.Application.Abstractions;
using SIGEBI.Contracts.Resources;
using SIGEBI.Infrastructure.Persistence;

namespace SIGEBI.Infrastructure.Repositories;

public class CatalogoRepository : ICatalogoRepository
{
    private readonly AppDbContext _db;

    public CatalogoRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<List<ResourceDto>> ListarCatalogoAsync(CancellationToken ct)
    {
        var query =
            from r in _db.Recursos.AsNoTracking()
            select new ResourceDto
            {
                Id = r.Id,
                Titulo = r.Titulo,
                Autor = r.Autor,
                Isbn = r.Isbn,

                // Regla: ejemplar activo y sin préstamo activo
                CopiasDisponibles = _db.Ejemplares.Count(e =>
                   e.RecursoId == r.Id &&
                   e.Activo == true &&
                    !_db.Prestamos.Any(p =>
                        p.EjemplarId == e.Id &&
                        p.FechaDevolucion == null
                     ) &&
                     !_db.Reservas.Any(res =>
                         res.EjemplarId == e.Id &&
                         res.FechaCancelacionUtc == null
                     )
                )
            };

        return await query
            .OrderBy(x => x.Titulo)
            .ToListAsync(ct);
    }
}