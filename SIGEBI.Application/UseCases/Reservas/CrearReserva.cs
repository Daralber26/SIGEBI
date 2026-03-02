using SIGEBI.Application.Abstractions;
using SIGEBI.Domain.Entities.Dbo;

namespace SIGEBI.Application.UseCases.Reservas;

public sealed class CrearReserva
{
    private readonly IReservaRepository _reservas;
    private readonly IEjemplarRepository _ejemplares;

    public CrearReserva(IReservaRepository reservas, IEjemplarRepository ejemplares)
    {
        _reservas = reservas;
        _ejemplares = ejemplares;
    }

    public async Task<Guid> ExecuteAsync(Guid usuarioId, Guid recursoId, CancellationToken ct = default)
    {
        if (usuarioId == Guid.Empty) throw new ArgumentException("UsuarioId inválido.");
        if (recursoId == Guid.Empty) throw new ArgumentException("RecursoId inválido.");

        // reserva activa por usuario por recurso
        if (await _reservas.ExistsActivaByUsuarioYRecursoAsync(usuarioId, recursoId, ct))
            throw new InvalidOperationException("Ya tienes una reserva activa para este recurso.");

        // Buscar un ejemplar disponible del recurso (no prestado y no reservado)
        var ejemplarId = await _ejemplares.GetEjemplarDisponibleIdAsync(recursoId, ct);
        if (ejemplarId is null || ejemplarId.Value == Guid.Empty)
            throw new InvalidOperationException("No hay ejemplares disponibles para reservar.");

        // Bloquear: 1 reserva activa por ejemplar (extra seguridad; la BD también lo asegura)
        if (await _reservas.ExistsActivaByEjemplarAsync(ejemplarId.Value, ct))
            throw new InvalidOperationException("Ese ejemplar ya está reservado.");

        var reserva = new Reserva(usuarioId, ejemplarId.Value);

        await _reservas.AddAsync(reserva, ct);
        await _reservas.SaveChangesAsync(ct);

        return reserva.Id;
    }
}