using SIGEBI.Application.Abstractions;
using SIGEBI.Contracts.Prestamos;
using SIGEBI.Domain.Entities;
using SIGEBI.Domain.Enums;

namespace SIGEBI.Application.UseCases.Prestamos;

public class CrearPrestamo
{
    private readonly IPrestamoRepository _prestamos;
    private readonly IEjemplarRepository _ejemplares;
    private readonly IUsuarioRepository _usuarios;
    private readonly IPenalizacionRepository _penalizaciones;

    private const int MAX_PRESTAMOS_ACTIVOS = 3;

    public CrearPrestamo(
        IPrestamoRepository prestamos,
        IEjemplarRepository ejemplares,
        IUsuarioRepository usuarios,
        IPenalizacionRepository penalizaciones)
    {
        _prestamos = prestamos;
        _ejemplares = ejemplares;
        _usuarios = usuarios;
        _penalizaciones = penalizaciones;
    }

    public async Task<Prestamo> Ejecutar(CreatePrestamoRequest req, CancellationToken ct)
    {
        if (req.DiasPrestamo <= 0)
            throw new InvalidOperationException("Los días de préstamo deben ser mayor que 0.");

        // ✅ Validar usuario (antes de tocar ejemplar)
        var usuario = await _usuarios.ObtenerPorIdAsync(req.UsuarioId, ct);
        if (usuario is null)
            throw new InvalidOperationException("El usuario no existe.");

        if (!usuario.Activo)
            throw new InvalidOperationException("El usuario está inactivo.");

        //  Penalizaciones activas bloquean (según tu regla)
        var utcNow = DateTime.UtcNow;
        var penalizacionesActivas = await _penalizaciones.GetActivasPorUsuario(req.UsuarioId, ct);

        if (penalizacionesActivas.Any(p => p.Bloquea(utcNow)))
            throw new InvalidOperationException("El usuario tiene una penalización activa y no puede realizar préstamos.");

        //  Límite de préstamos activos
        var activos = await _prestamos.ContarPrestamosActivosPorUsuarioAsync(req.UsuarioId, ct);
        if (activos >= MAX_PRESTAMOS_ACTIVOS)
            throw new InvalidOperationException($"El usuario ya tiene el máximo de préstamos activos ({MAX_PRESTAMOS_ACTIVOS}).");

        // 1) Buscar ejemplar
        var ejemplar = await _ejemplares.ObtenerPorIdAsync(req.EjemplarId, ct);
        if (ejemplar is null)
            throw new InvalidOperationException("El ejemplar no existe.");

        // 2) Validar estado
        if (!ejemplar.Activo)
            throw new InvalidOperationException("El ejemplar está inactivo.");

        if (ejemplar.Estado != EjemplarEstado.Disponible)
            throw new InvalidOperationException("El ejemplar no está disponible.");

        // 3) Validar que no tenga préstamo activo (por ejemplar)
        var yaPrestado = await _prestamos.ExistePrestamoActivoAsync(req.EjemplarId, ct);
        if (yaPrestado)
            throw new InvalidOperationException("Ya existe un préstamo activo para este ejemplar.");

        // 4) Marcar ejemplar como prestado (dominio)
        ejemplar.MarcarPrestado();

        // 5) Crear préstamo
        var prestamo = new Prestamo(
            req.UsuarioId,
            req.EjemplarId,
            utcNow,
            req.DiasPrestamo
        );

        await _prestamos.AgregarAsync(prestamo, ct);
        await _prestamos.GuardarCambiosAsync(ct);

        return prestamo;
    }
}