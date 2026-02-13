using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGEBI.Domain.Entities;

public class Prestamo
{
    public Guid Id { get; private set; }
    public Guid UsuarioId { get; private set; }
    public Guid RecursoId { get; private set; }

    public DateTime FechaPrestamo { get; private set; }
    public DateTime FechaVencimiento { get; private set; }

    public DateTime? FechaDevolucion { get; private set; }

    private Prestamo() { } // EF

    public Prestamo(Guid usuarioId, Guid recursoId, DateTime fechaPrestamo, int diasPrestamo)
    {
        Id = Guid.NewGuid();
        UsuarioId = usuarioId;
        RecursoId = recursoId;
        FechaPrestamo = fechaPrestamo;
        FechaVencimiento = fechaPrestamo.AddDays(diasPrestamo);
    }

    public void RegistrarDevolucion(DateTime fechaDevolucion)
    {
        if (FechaDevolucion is not null)
            throw new InvalidOperationException("El préstamo ya fue devuelto.");

        FechaDevolucion = fechaDevolucion;
    }

    public bool EstaVencido(DateTime ahoraUtc)
    {
        return FechaDevolucion is null && ahoraUtc > FechaVencimiento;
    }
}
