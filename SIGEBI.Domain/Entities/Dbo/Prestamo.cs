using System;

namespace SIGEBI.Domain.Entities;

public class Prestamo
{
    public Guid Id { get; private set; }
    public Guid UsuarioId { get; private set; }

    public Guid EjemplarId { get; private set; }
    public Ejemplar Ejemplar { get; private set; } = null!;

    public DateTime FechaPrestamo { get; private set; }
    public DateTime FechaVencimiento { get; private set; }
    public DateTime? FechaDevolucion { get; private set; }

    private Prestamo() { } // EF

    public Prestamo(Guid usuarioId, Guid ejemplarId, DateTime fechaPrestamoUtc, int diasPrestamo)
    {
        if (diasPrestamo <= 0)
            throw new ArgumentOutOfRangeException(nameof(diasPrestamo),
                "Los días de préstamo deben ser mayor que 0.");

        Id = Guid.NewGuid();
        UsuarioId = usuarioId;
        EjemplarId = ejemplarId;
        FechaPrestamo = fechaPrestamoUtc;
        FechaVencimiento = fechaPrestamoUtc.AddDays(diasPrestamo);
    }

    public void RegistrarDevolucion(DateTime fechaDevolucionUtc)
    {
        if (FechaDevolucion is not null)
            throw new InvalidOperationException("El préstamo ya fue devuelto.");

        if (fechaDevolucionUtc < FechaPrestamo)
            throw new InvalidOperationException("La fecha de devolución no puede ser menor que la fecha de préstamo.");

        FechaDevolucion = fechaDevolucionUtc;
    }

    public bool EstaVencido(DateTime ahoraUtc)
        => FechaDevolucion is null && ahoraUtc > FechaVencimiento;
}

