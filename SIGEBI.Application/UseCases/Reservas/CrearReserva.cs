using SIGEBI.Application.Abstractions;
using SIGEBI.Contracts.Reservas;
using SIGEBI.Domain.Entities.Dbo;

namespace SIGEBI.Application.UseCases.Reservas;

public sealed class CrearReserva
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IRecursoRepository _recursoRepository;
    private readonly IReservaRepository _reservaRepository;

    public CrearReserva(
        IUsuarioRepository usuarioRepository,
        IRecursoRepository recursoRepository,
        IReservaRepository reservaRepository)
    {
        _usuarioRepository = usuarioRepository;
        _recursoRepository = recursoRepository;
        _reservaRepository = reservaRepository;
    }

    public async Task<Guid> HandleAsync(CreateReservaRequest request, CancellationToken ct)
    {
        // 1) Validación básica
        if (request.UsuarioId == Guid.Empty)
            throw new ArgumentException("UsuarioId inválido.");

        if (request.RecursoId == Guid.Empty)
            throw new ArgumentException("RecursoId inválido.");

        // 2) Usuario existe
        var usuario = await _usuarioRepository.ObtenerPorIdAsync(request.UsuarioId, ct);
        if (usuario is null)
            throw new InvalidOperationException("Usuario no existe.");

        // 3) Usuario activo
        if (!usuario.Activo)
            throw new InvalidOperationException("Usuario inactivo no puede reservar.");

        // 4) Recurso existe
        var recurso = await _recursoRepository.ObtenerPorIdAsync(request.RecursoId, ct);
        if (recurso is null)
            throw new InvalidOperationException("Recurso no existe.");

        // 5) No duplicar reserva activa
        var yaExiste = await _reservaRepository.ExisteReservaActivaAsync(request.UsuarioId, request.RecursoId, ct);
        if (yaExiste)
            throw new InvalidOperationException("Ya existe una reserva activa para este usuario y recurso.");

        // 6) Crear entidad dominio
        var reserva = new Reserva(request.UsuarioId, request.RecursoId, DateTime.UtcNow);

        // 7) Persistir
        await _reservaRepository.AgregarAsync(reserva, ct);
        await _reservaRepository.GuardarCambiosAsync(ct);

        return reserva.Id;
    }
}