using SIGEBI.Application.Abstractions;

namespace SIGEBI.Application.UseCases.Prestamos;

public class DevolverPrestamo
{
    private readonly IPrestamoRepository _prestamos;
    private readonly IEjemplarRepository _ejemplares;

    public DevolverPrestamo(IPrestamoRepository prestamos, IEjemplarRepository ejemplares)
    {
        _prestamos = prestamos;
        _ejemplares = ejemplares;
    }

    public async Task EjecutarAsync(Guid prestamoId, CancellationToken ct)
    {
        // 1) Buscar préstamo
        var prestamo = await _prestamos.ObtenerPorIdAsync(prestamoId, ct);
        if (prestamo is null)
            throw new InvalidOperationException("El préstamo no existe.");

        // 2) Validar que no esté devuelto
        if (prestamo.FechaDevolucion is not null)
            throw new InvalidOperationException("El préstamo ya fue devuelto.");

        // 3) Buscar ejemplar
        var ejemplar = await _ejemplares.ObtenerPorIdAsync(prestamo.EjemplarId, ct);
        if (ejemplar is null)
            throw new InvalidOperationException("El ejemplar no existe.");

        // 4) Ejecutar reglas de dominio
        prestamo.RegistrarDevolucion(DateTime.UtcNow);
        ejemplar.MarcarDisponible();

        // 5) Guardar (un solo SaveChanges)
        await _prestamos.GuardarCambiosAsync(ct);
    }
}
