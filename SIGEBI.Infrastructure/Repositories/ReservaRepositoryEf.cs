using Microsoft.EntityFrameworkCore;
using SIGEBI.Application.Abstractions;
using SIGEBI.Domain.Entities.Dbo;
using SIGEBI.Infrastructure.Persistence;

namespace SIGEBI.Infrastructure.Repositories;

public sealed class ReservaRepositoryEf : IReservaRepository
{
    private readonly AppDbContext _db;

    public ReservaRepositoryEf(AppDbContext db)
    {
        _db = db;
    }

    public Task<bool> ExisteReservaActivaAsync(Guid usuarioId, Guid recursoId, CancellationToken ct)
    {
        return _db.Reservas.AnyAsync(r =>
            r.UsuarioId == usuarioId &&
            r.RecursoId == recursoId &&
            r.Estado == EstadoReserva.Activa,
            ct);
    }

    public async Task AgregarAsync(Reserva reserva, CancellationToken ct)
    {
        await _db.Reservas.AddAsync(reserva, ct);
    }

    public Task GuardarCambiosAsync(CancellationToken ct)
    {
        return _db.SaveChangesAsync(ct);
    }
}