using SIGEBI.Application.Abstractions;
using SIGEBI.Contracts.Prestamos;
using SIGEBI.Domain.Enums;
using SIGEBI.Domain.Entities;

namespace SIGEBI.Application.UseCases.Prestamos;

public class CrearPrestamo
{
    private readonly IPrestamoRepository _prestamos;
    private readonly IEjemplarRepository _ejemplares;

    public CrearPrestamo(IPrestamoRepository prestamos, IEjemplarRepository ejemplares)
    {
        _prestamos = prestamos;
        _ejemplares = ejemplares;
    }

    public async Task<Prestamo> Ejecutar(CreatePrestamoRequest req, CancellationToken ct)
    {
        if (req.DiasPrestamo <= 0)
            throw new InvalidOperationException("Los días de préstamo deben ser mayor que 0.");

        // 1) Buscar ejemplar
        var ejemplar = await _ejemplares.ObtenerPorIdAsync(req.EjemplarId, ct);
        if (ejemplar is null)
            throw new InvalidOperationException("El ejemplar no existe.");

        // 2) Validar estado
        if (!ejemplar.Activo)
            throw new InvalidOperationException("El ejemplar está inactivo.");

        if (ejemplar.Estado != EjemplarEstado.Disponible)
            throw new InvalidOperationException("El ejemplar no está disponible.");

        // 3) Validar que no tenga préstamo activo
        var yaPrestado = await _prestamos.ExistePrestamoActivoAsync(req.EjemplarId, ct);
        if (yaPrestado)
            throw new InvalidOperationException("Ya existe un préstamo activo para este ejemplar.");

        // 4) Marcar ejemplar como prestado (dominio)
        ejemplar.MarcarPrestado();

        // 5) Crear préstamo
        var prestamo = new Prestamo(
            req.UsuarioId,
            req.EjemplarId,
            DateTime.UtcNow,
            req.DiasPrestamo
        );

        await _prestamos.AgregarAsync(prestamo, ct);
        await _prestamos.GuardarCambiosAsync(ct);

        return prestamo;
    }
}
